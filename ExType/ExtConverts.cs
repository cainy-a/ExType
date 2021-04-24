using System;
using System.Diagnostics.CodeAnalysis;

namespace ExType
{
    /// <summary>
    /// Extension methods to convert between builtin types - object.To[type]()
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static partial class ExtConverts
    {
        /// <summary>
        /// Converts a type to bool. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "TailRecursiveCall")]
        public static bool ToBool(this object obj)
        {
            switch (obj)
            {
                // WHYYYYYY
                case bool b:
                    return b;
                // int numbers
                case int:
                case short:
                case long:
                case uint:
                case ushort:
                case ulong:
                    return (ulong) obj != 0;
                // decimal numbers
                case Half:
                case float:
                case double:
                case decimal:
                    return (decimal) obj != 0;
                // strings
                case string s:
                    if (double.TryParse(s, out var sd))
                        return sd.ToBool();
                    return s.Length > 0;
                // chars
                case char c:
                    if (int.TryParse(c.ToString(), out var ci))
                        return ci.ToBool();
                    return c != default; // '\0'
                default:
                    throw new TypeConversionException($"{obj.GetType().Name} could not be converted to bool");
            }
        }
    }
}