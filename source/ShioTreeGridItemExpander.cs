using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class ShioTreeGridItemExpander : ToggleButton, IShioTreeGridParts
    {
        public const double Indentation = 10;

        internal ShioTreeGridItemEdgeRender EdgeRender
        {
            get { return this.GetTemplateChild("PART_EdgeRender") as ShioTreeGridItemEdgeRender; }
        }

        internal System.Windows.Shapes.Path ExpandPath
        {
            get { return this.GetTemplateChild("PART_ExpandPath") as System.Windows.Shapes.Path; }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            Action acx = null;

            if (acx != null)
            {
                if (this.IsLoaded == true &&
                    this.Dispatcher.CheckAccess() == true)
                {
                    acx();
                }
                else
                {
                    this.Dispatcher.BeginInvoke(acx, DispatcherPriority.Loaded);
                }
            }
        }

        static ShioTreeGridItemExpander()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                            typeof(ShioTreeGridItemExpander),
                            new FrameworkPropertyMetadata(typeof(ShioTreeGridItemExpander)));
        }

        public ShioTreeGridItemExpander()
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
