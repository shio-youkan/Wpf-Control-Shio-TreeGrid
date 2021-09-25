using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Shio
{
    public partial class ShioTreeGridView : GridView, IShioTreeGridParts
    {
        public static readonly DependencyProperty EdgeLineBrushProperty =
                    DependencyProperty.Register(
                        nameof(ShioTreeGridView.EdgeLineBrush),
                        typeof(Brush),
                        typeof(ShioTreeGridView),
                        new FrameworkPropertyMetadata(
                            new SolidColorBrush(Color.FromArgb(0xFF, 0x02, 0x1C, 0x33)),
                            FrameworkPropertyMetadataOptions.AffectsArrange |
                            FrameworkPropertyMetadataOptions.AffectsMeasure |
                            FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush EdgeLineBrush
        {
            get { return (Brush)this.GetValue(ShioTreeGridView.EdgeLineBrushProperty); }
            set { this.SetValue(ShioTreeGridView.EdgeLineBrushProperty, value); }
        }

        public static readonly DependencyProperty EdgeLineVisibiiltyProperty =
            DependencyProperty.Register(
                nameof(ShioTreeGridView.EdgeLineVisibility),
                typeof(bool),
                typeof(ShioTreeGridView),
                new FrameworkPropertyMetadata(
                    true,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public bool EdgeLineVisibility
        {
            get { return (bool)this.GetValue(ShioTreeGridView.EdgeLineVisibiiltyProperty); }
            set { this.SetValue(ShioTreeGridView.EdgeLineVisibiiltyProperty, value); }
        }

        public static readonly DependencyProperty SectionVisibiiltyProperty =
            DependencyProperty.Register(
                nameof(ShioTreeGridView.SectionVisibility),
                typeof(bool),
                typeof(ShioTreeGridView),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public bool SectionVisibility
        {
            get { return (bool)this.GetValue(ShioTreeGridView.SectionVisibiiltyProperty); }
            set { this.SetValue(ShioTreeGridView.SectionVisibiiltyProperty, value); }
        }

        public static readonly DependencyProperty SectionBrushProperty =
                    DependencyProperty.Register(
                        nameof(ShioTreeGridView.SectionBrush),
                        typeof(Brush),
                        typeof(ShioTreeGridView),
                        new FrameworkPropertyMetadata(
                            new SolidColorBrush(Color.FromArgb(0xFF, 0x59, 0x59, 0x59)),
                            FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush SectionBrush
        {
            get { return (Brush)this.GetValue(SectionBrushProperty); }
            set { this.SetValue(SectionBrushProperty, value); }
        }

        public static readonly DependencyProperty SectionThicknessProperty =
                    DependencyProperty.Register(
                        nameof(ShioTreeGridView.SectionThickness),
                        typeof(Thickness),
                        typeof(ShioTreeGridView),
                        new FrameworkPropertyMetadata(
                            new Thickness(1d),
                            FrameworkPropertyMetadataOptions.AffectsArrange |
                            FrameworkPropertyMetadataOptions.AffectsMeasure |
                            FrameworkPropertyMetadataOptions.AffectsRender));

        public Thickness SectionThickness
        {
            get { return (Thickness)this.GetValue(ShioTreeGridView.SectionThicknessProperty); }
            set { this.SetValue(ShioTreeGridView.SectionThicknessProperty, value); }
        }

        private Visual m_owner;

        private Pen m_edgeLinePen;

        internal Visual Parent
        {
            get { return this.m_owner; }
            set { this.m_owner = value; }
        }

        internal System.Windows.Media.Pen EdgeLinePen
        {
            get
            {
                if (this.m_edgeLinePen == null)
                    this.m_edgeLinePen = this.CreateEdgeLinePen();

                return this.m_edgeLinePen;
            }
        }

        private Pen CreateEdgeLinePen()
        {
            var pen = new Pen(this.EdgeLineBrush, 2d);
            pen.DashStyle = DashStyles.Dot;
            pen.Freeze();

            return pen;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (object.ReferenceEquals(e.Property, ShioTreeGridView.EdgeLineBrushProperty) == true)
            {
                var acx = new Action(() =>
                {
                    this.m_edgeLinePen = this.CreateEdgeLinePen();
                });

                if (this.Dispatcher.CheckAccess() == true)
                    acx();
                else
                    this.Dispatcher.BeginInvoke(acx, DispatcherPriority.Loaded);
            }

            if (object.ReferenceEquals(e.Property, ShioTreeGridView.EdgeLineVisibiiltyProperty) == true)
            {
                foreach (var n in this.Parent.GetVisualDescendants<ShioTreeGridItemEdgeRender>())
                {
                    var acx = new Action(() =>
                    {
                        n.InvalidateVisual();
                    });

                    if (n.IsLoaded == true)
                    {
                        if (n.CheckAccess() == true)
                            acx();
                        else
                            n.Dispatcher.BeginInvoke(acx, System.Windows.Threading.DispatcherPriority.Loaded);
                    }
                }
            }
        }

        public ShioTreeGridView()
        {
            this.Initialize();
        }

        private void Initialize(bool cleaning = true)
        {
            if (cleaning == true)
            {
                this.m_owner = null;
                this.m_edgeLinePen = this.CreateEdgeLinePen();
            }
        }
    }
}
