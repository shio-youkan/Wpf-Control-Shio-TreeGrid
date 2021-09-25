using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;

namespace Shio
{
    public sealed class ShioTreeGridItemDepthMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double margin = 0;

            try
            {
                if (value != null &&
                    targetType == typeof(double) &&
                    typeof(DependencyObject).IsAssignableFrom(value.GetType()))
                {
                    DependencyObject elm = value as DependencyObject;

                    int level = -1;
                    for (; elm != null; elm = VisualTreeHelper.GetParent(elm))
                    {
                        if (typeof(ShioTreeGridItem).IsAssignableFrom(elm.GetType()))
                        {
                            level++;
                        }
                    }

                    margin = ShioTreeGridItemExpander.Indentation * (double)level;
                }
            }
            catch
            {
            }
            return margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

    public sealed class ShioTreeGridItemDepthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int level = -1;

            try
            {
                if (value != null &&
                    typeof(DependencyObject).IsAssignableFrom(value.GetType()))
                {
                    DependencyObject elm = value as DependencyObject;

                    for (; elm != null; elm = VisualTreeHelper.GetParent(elm))
                    {
                        if (typeof(ShioTreeGridItem).IsAssignableFrom(elm.GetType()))
                        {
                            level++;
                        }
                    }
                }
            }
            catch
            {
            }

            return level;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal sealed class ShioTreeGridColumnHeaderMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var margin = new Thickness();
            try
            {
                if (value != null &&
                    targetType == typeof(Thickness) &&
                    typeof(ShioTreeGridView).IsAssignableFrom(value.GetType()) == true)
                {
                    var t = (ShioTreeGridView)value;

                    margin.Left = t.SectionThickness.Left;
                    margin.Right = t.SectionThickness.Right;
                }
            }
            catch
            {
            }

            return margin;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}