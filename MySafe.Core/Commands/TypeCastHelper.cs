using System;
using System.Globalization;

namespace MySafe.Core.Commands {
    public static class TypeCastHelper {
        public static object TryCast(object value, Type targetType) {
            Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;
            if(underlyingType.IsEnum && value is string) {
                value = Enum.Parse(underlyingType, (string)value, false);
            } else if( value is IConvertible && !targetType.IsAssignableFrom(value.GetType())) {
                value = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
            }
            if(value == null && targetType.IsValueType)
                value = Activator.CreateInstance(targetType);
            return value;
        }
    }
}