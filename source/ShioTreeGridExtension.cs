using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Shio
{
    internal static class ShioTreeGridExtension
    {
        public static T GetVisualParent<T>(this DependencyObject obj) where T : DependencyObject
        {
            DependencyObject parent = obj;

            while (true)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent == null || ((parent as T) != null))
                    break;
            }

            return parent as T;
        }

        public static IEnumerable<T> GetVisualChildren<T>(this DependencyObject obj) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(obj);
            if (count == 0)
                yield break;

            for (int i = 0; i < count; i++)
            {
                var t_child = VisualTreeHelper.GetChild(obj, i) as T;
                if (t_child != null)
                    yield return t_child;
            }
        }

        public static T GetVisualDescendant<T>(this DependencyObject obj) where T : DependencyObject
        {
            foreach (var child in obj.GetVisualChildren<DependencyObject>())
            {
                var t_child = child as T;
                if (t_child != null)
                {
                    return child as T;
                }

                return child.GetVisualDescendant<T>();
            }

            return null;
        }

        public static IEnumerable<T> GetVisualDescendants<T>(this DependencyObject obj) where T : DependencyObject
        {
            foreach (var child in obj.GetVisualChildren<DependencyObject>())
            {
                var t_child = child as T;
                if (t_child != null)
                    yield return t_child;

                foreach (var visual in child.GetVisualDescendants<T>())
                {
                    var t_visual = visual as T;
                    if (t_visual != null)
                        yield return t_visual;
                }
            }
        }
    }
}