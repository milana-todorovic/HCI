﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SekretarView
{
    public class DayBorderColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string notes = (string)value;

            if (string.IsNullOrEmpty(notes)) return null;

            if (notes.Length > 0) return new LinearGradientBrush(Color.FromRgb(220, 74, 56), Color.FromRgb(198, 56, 40), new Point(0.5, 0), new Point(0.5, 1));

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
