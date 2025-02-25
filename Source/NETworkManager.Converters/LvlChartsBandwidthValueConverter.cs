﻿using NETworkManager.Utilities;
using System;
using System.Globalization;
using System.Windows.Data;

namespace NETworkManager.Converters;

public sealed class LvlChartsBandwidthValueConverter : IValueConverter
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value">ChartPoint.Instance (object)</param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is LvlChartsDefaultInfo info)
            return $"{FileSizeConverter.GetBytesReadable((long)info.Value * 8)}it/s";

        return "-/-";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
