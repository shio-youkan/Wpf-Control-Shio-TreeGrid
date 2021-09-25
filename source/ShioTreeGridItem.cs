using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
    public partial class ShioTreeGridItem : TreeViewItem, IShioTreeGridParts
    {
        public static readonly DependencyProperty RowStyleProperty =
                    DependencyProperty.Register(
                                nameof(RowStyle),
                                typeof(Style),
                                typeof(ShioTreeGridItem),
                                new FrameworkPropertyMetadata(
                                    null,
                                    FrameworkPropertyMetadataOptions.AffectsArrange |
                                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                                    FrameworkPropertyMetadataOptions.AffectsRender));

        public Style RowStyle
        {
            get { return (Style)this.GetValue(ShioTreeGridItem.RowStyleProperty); }
            set { this.SetValue(ShioTreeGridItem.RowStyleProperty, value); }
        }

        [Bindable(true)]
        public bool IsTop
        {
            get
            {
                var parent = this.GetVisualParent<ShioTreeGridItem>();
                if (parent == null)
                    return true;

                return false;
            }
        }

        [Bindable(true)]
        public bool HasChild
        {
            get { return this.HasItems; }
        }

        [Bindable(true)]
        public bool IsFirst
        {
            get
            {
                var parent = this.GetVisualParent<ShioTreeGridItem>();
                if (parent == null)
                    return false;

                var first = parent.GetVisualDescendants<ShioTreeGridItem>().
                                    Where(x =>
                                    {
                                        var e = x.GetVisualParent<ShioTreeGridItem>();
                                        if (e == null)
                                            return false;

                                        if (object.ReferenceEquals(parent, e) == false)
                                            return false;

                                        return true;
                                    }).
                                    FirstOrDefault();
                if (first == null)
                {
                    return true;
                }

                if (object.ReferenceEquals(first, this) == false)
                {
                    return false;
                }

                return true;
            }
        }

        [Bindable(true)]
        public bool IsLast
        {
            get
            {
                var parent = this.GetVisualParent<ShioTreeGridItem>();
                if (parent == null)
                    return false;

                var last = parent.GetVisualDescendants<ShioTreeGridItem>().
                                    Where(x =>
                                    {
                                        var e = x.GetVisualParent<ShioTreeGridItem>();
                                        if (e == null)
                                            return false;

                                        if (object.ReferenceEquals(parent, e) == false)
                                            return false;

                                        return true;
                                    }).
                                    LastOrDefault();
                if (last == null)
                {
                    return true;
                }

                if (object.ReferenceEquals(last, this) == false)
                {
                    return false;
                }

                return true;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ShioTreeGridItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ShioTreeGridItem;
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (this.DataContext != BindingOperations.DisconnectedSource)
            {
                foreach (var n in this.GetVisualDescendants<ShioTreeGridItemEdgeRender>())
                {
                    var acx = new Action(() =>
                    {
                        n.InvalidateVisual();
                    });

                    if (n.IsLoaded == true)
                    {
                        if (n.CheckAccess() == true)
                        {
                            acx();
                        }
                        else
                        {
                            n.Dispatcher.BeginInvoke(acx, System.Windows.Threading.DispatcherPriority.Loaded);
                        }
                    }
                }
            }
        }

        static ShioTreeGridItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                                typeof(ShioTreeGridItem),
                                new FrameworkPropertyMetadata(typeof(ShioTreeGridItem)));
        }

        public ShioTreeGridItem()
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
