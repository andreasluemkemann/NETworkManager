﻿using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace NETworkManager.Converters;

public sealed class IPAddressToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is IPAddress address ? address.ToString() : "-/-";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
