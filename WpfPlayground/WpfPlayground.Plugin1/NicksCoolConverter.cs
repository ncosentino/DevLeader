using Microsoft.Extensions.DependencyInjection;

using System.Globalization;
using System.Windows.Data;

namespace WpfPlayground.Converters;

public sealed class StringFormattingHelper
{
    public string FormatDouble(
        double value,
        int decimalPlaces) =>
        value.ToString($"F{decimalPlaces}", CultureInfo.InvariantCulture);
}

public class NicksCoolConverter :
    IValueConverter
{
    private readonly StringFormattingHelper _stringFormattingHelper;

    public NicksCoolConverter(StringFormattingHelper stringFormattingHelper)
    {
        _stringFormattingHelper = stringFormattingHelper;
    }

    public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
    {
        var numericValue = (double)value;
        var formatted = _stringFormattingHelper.FormatDouble(
            numericValue,
            3);
        return formatted;
    }

    public object ConvertBack(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture) => throw new NotImplementedException();
}