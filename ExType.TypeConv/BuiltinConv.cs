using System;
using System.Diagnostics.CodeAnalysis;
using ExType.Shared;

namespace ExType.TypeConv
{
    /// <summary>
    /// Extension methods to convert between builtin types - object.To[type]()
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "TailRecursiveCall")]
    public static partial class BuiltinConv
    {
        /// <summary>
        /// Converts a type to bool. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(this object obj)
        {
            switch (obj)
            {
                // WHYYYYYY
                case bool b:
                    return b;
                // int numbers
                case int _:
                case short _:
                case long _:
                case uint _:
                case ushort _:
                case ulong _:
                    return (ulong) obj != 0;
                // decimal numbers
                case float _:
                case double _:
                case decimal _:
                    return (decimal) obj != 0;
                // strings
                case string s:
                    if (decimal.TryParse(s, out var sd))
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

        /// <summary>
        /// Converts a type to char. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static char ToChar(this object obj)
        {
            switch (obj)
            {
                case int _:
                case uint _:
                case short _:
                case ushort _:
                case long _:
                case ulong _:
                    return (char) obj.ToUInt16();
                case float _:
                case double _:
                case decimal _:
                    return Math.Round((decimal) obj).ToChar();
                case string s:
                    if (s.Length == 1)
                        return s[0];
                    throw new TypeConversionException("string was not 1 character and could not be converted to char");
                case bool b:
                    return b ? '1' : '0';
                default:
                    throw new TypeConversionException($"{obj.GetType().Name} could not be converted to char");
            }
        }
    }
}