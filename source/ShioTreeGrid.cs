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
    public partial class ShioTreeGrid : TreeView, IShioTreeGridParts
    {
        public static readonly DependencyProperty ViewProperty =
                    DependencyProperty.Register(
                        nameof(ShioTreeGrid.View),
                        typeof(ShioTreeGridView),
                        typeof(ShioTreeGrid),
                        new FrameworkPropertyMetadata(
                            null,
                            FrameworkPropertyMetadataOptions.AffectsArrange |
                            FrameworkPropertyMetadataOptions.AffectsMeasure |
                            FrameworkPropertyMetadataOptions.AffectsRender));

        public ShioTreeGridView View
        {
            get { return (ShioTreeGridView)this.GetValue(ShioTreeGrid.ViewProperty); }
            set { this.SetValue(ShioTreeGrid.ViewProperty, value); }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ShioTreeGridItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ShioTreeGridItem;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            Action acx = null;

            if (object.ReferenceEquals(e.Property, ShioTreeGrid.ViewProperty) == true)
            {
                ShioTreeGridView newView = e.NewValue as ShioTreeGridView;
                ShioTreeGridView oldView = e.OldValue as ShioTreeGridView;
                if (newView != null)
                {
                    if (newView.Parent != null)
                    {
                        throw new InvalidOperationException(
                                        string.Format(
                                                "{0} is used in {1} visual.",
                                                newView.GetType().Name,
                                                newView.Parent.GetType().Name));
                    }

                    newView.Parent = this;
                }

                if (oldView != null)
                {
                    oldView.Parent = null;
                }
            }

            if (acx != null)
            {
                if (this.Dispatcher.CheckAccess() == true)
                {
                    acx();
                }
                else
                {
                    this.Dispatcher.BeginInvoke(acx, DispatcherPriority.Loaded);
                }
            }
        }

        static ShioTreeGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShioTreeGrid), new FrameworkPropertyMetadata(typeof(ShioTreeGrid)));
        }

        public ShioTreeGrid()
        {
            this.Initialize();
        }

        private void Initialize(bool cleaning = true)
        {
            if (cleaning == true)
            {
            }
        }

    }
}
