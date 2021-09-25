using System;
using System.Collections.Generic;
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

namespace Shio
{
    public class ShioTreeGridItemEdgeRender : Control, IShioTreeGridParts
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            var grid = this.GetVisualParent<ShioTreeGrid>();
            if (grid == null)
                return;

            if (grid.View.EdgeLineVisibility == false)
                return;

            var item = this.GetVisualParent<ShioTreeGridItem>();
            if (item == null)
                return;

            var itemRowPresenter = item.GetVisualDescendant<GridViewRowPresenter>();
            if (itemRowPresenter == null)
                return;

            var expander = this.GetVisualParent<ShioTreeGridItemExpander>();
            if (expander == null)
                return;

            var expanderPath = expander.ExpandPath;
            if (expanderPath == null)
                return;

            if (item.IsTop == true)
                return;

            var parent = item.GetVisualParent<ShioTreeGridItem>();
            if (parent == null)
                return;

            var parentRowPresenter = parent.GetVisualDescendant<GridViewRowPresenter>();
            if (parentRowPresenter == null)
                return;

            Size d_sz = this.RenderSize;

            var cw = (ShioTreeGridItemExpander.Indentation / 2d);
            var s_wz = d_sz.Width - cw;
            var e_wz = d_sz.Width;

            var s_hz = d_sz.Height / 2d;
            var e_hz = d_sz.Height;

            if (item.IsFirst == true)
            {
                drawingContext.DrawLine(
                    grid.View.EdgeLinePen,
                    new Point(s_wz, 0d - (parentRowPresenter.RenderSize.Height - 16d)),
                    new Point(s_wz, 0d));
            }

            if (item.IsLast == true)
            {
                drawingContext.DrawLine(
                    grid.View.EdgeLinePen,
                    new Point(s_wz, 0d),
                    new Point(s_wz, 8d));

                drawingContext.DrawLine(
                    grid.View.EdgeLinePen,
                    new Point(s_wz, 8d),
                    new Point(e_wz, 8d));
            }
            else
            {
                drawingContext.DrawLine(
                    grid.View.EdgeLinePen,
                    new Point(s_wz, 0d),
                    new Point(s_wz, e_hz));

                drawingContext.DrawLine(
                    grid.View.EdgeLinePen,
                    new Point(s_wz, 8d),
                    new Point(e_wz, 8d));
            }

            while (parent != null &&
                   parent.IsExpanded == true &&
                   parent.IsTop == false &&
                   parent.IsLast == false)
            {
                s_wz = s_wz - ShioTreeGridItemExpander.Indentation;

                drawingContext.DrawLine(
                    grid.View.EdgeLinePen,
                    new Point(s_wz, 0d),
                    new Point(s_wz, e_hz));

                parent = parent.GetVisualParent<ShioTreeGridItem>();
            }
        }

        static ShioTreeGridItemEdgeRender()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                                typeof(ShioTreeGridItemEdgeRender),
                                new FrameworkPropertyMetadata(typeof(ShioTreeGridItemEdgeRender)));
        }

        public ShioTreeGridItemEdgeRender()
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
