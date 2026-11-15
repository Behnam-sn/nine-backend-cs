using System.ComponentModel;
using System.Globalization;
using Nine.Identity.Domain.Contracts.Users.ValueObjects;

namespace Nine.Identity.Domain.Contracts.Users.Converters;

public sealed class UserIdTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || sourceType == typeof(Guid) || base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return value switch
        {
            string s => UserId.Parse(s),
            Guid g => UserId.From(g),
            _ => base.ConvertFrom(context, culture, value)
        };
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return destinationType == typeof(string) && value is UserId id
            ? id.ToString()
            : base.ConvertTo(context, culture, value, destinationType);
    }
}
