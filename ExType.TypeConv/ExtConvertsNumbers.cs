using System;
using System.Diagnostics.CodeAnalysis;
using ExType.Shared;

namespace ExType.TypeConv
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    partial class ExtConv
    {
        // Signed
        /// <summary>
        /// Converts a type to signed byte. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="TypeConversionException"></exception>
        public static sbyte ToSByte(this object obj)
        {
            try
            {
                return (sbyte) obj.ToInt64();
            }
            catch (InvalidCastException)
            {
                throw new TypeConversionException("Value too big for a signed byte");
            }
        }

        /// <summary>
        /// Converts a type to short. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="TypeConversionException"></exception>
        public static short ToInt16(this object obj)
        {
            try
            {
                return (short) obj.ToInt64();
            }
            catch (InvalidCastException)
            {
                throw new TypeConversionException("Value too big for a short");
            }
        }

        /// <summary>
        /// Converts a type to int. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="TypeConversionException"></exception>
        public static int ToInt32(this object obj)
        {
            try
            {
                return (int) obj.ToInt64();
            }
            catch (InvalidCastException)
            {
                throw new TypeConversionException("Value too big for an int");
            }
        }

        /// <summary>
        /// Converts a type to long. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToInt64(this object obj)
        {
            switch (obj)
            {
                case int _:
                case short _:
                case long _:
                    return (long) obj;
                case uint _:
                case ushort _:
                case ulong _:
                    try
                    {
                        // ReSharper disable once PossibleInvalidCastException
                        return (long) obj;
                    }
                    catch (InvalidCastException)
                    {
                        throw new TypeConversionException("Value too big for a long");
                    }
                case bool b:
                    return b ? 1 : 0;
                case string s:
                    if (long.TryParse(s, out var si))
                        return si;
                    throw new TypeConversionException("String could not be converted to long");
                case char c:
                    if (long.TryParse(c.ToString(), out var ci))
                        return ci;
                    throw new TypeConversionException("Char could not be converted to long");
                default:
                    throw new TypeConversionException($"{obj.GetType().Name} could not be converted to long");
            }
        }
        
        // Unsigned
        /// <summary>
        /// Converts a type to byte. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="TypeConversionException"></exception>
        public static byte ToByte(this object obj)
        {
            try
            {
                return (byte) obj.ToUInt64();
            }
            catch (InvalidCastException)
            {
                throw new TypeConversionException("Value too big for a byte");
            }
        }
        
        /// <summary>
        /// Converts a type to unsigned short. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="TypeConversionException"></exception>
        public static ushort ToUInt16(this object obj)
        {
            try
            {
                return (ushort) obj.ToUInt64();
            }
            catch (InvalidCastException)
            {
                throw new TypeConversionException("Value too big for a short");
            }
        }

        /// <summary>
        /// Converts a type to unsigned int. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="TypeConversionException"></exception>
        public static uint ToUInt32(this object obj)
        {
            try
            {
                return (uint) obj.ToUInt64();
            }
            catch (InvalidCastException)
            {
                throw new TypeConversionException("Value too big for an int");
            }
        }

        /// <summary>
        /// Converts a type to unsigned long. See documentation for specifics.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this object obj)
        {
            switch (obj)
            {
                case int _:
                case short _:
                case long _:
                    try
                    {
                        // ReSharper disable once PossibleInvalidCastException
                        return (ulong) obj;
                    }
                    catch (InvalidCastException)
                    {
                        throw new TypeConversionException("Value was negative");
                    }
                case uint _:
                case ushort _:
                case ulong _:
                    return (ulong) obj;
                case bool b:
                    return (ulong) (b ? 1 : 0);
                case string s:
                    if (ulong.TryParse(s, out var si))
                        return si;
                    throw new TypeConversionException("String could not be converted to ulong");
                case char c:
                    if (ulong.TryParse(c.ToString(), out var ci))
                        return ci;
                    throw new TypeConversionException("Char could not be converted to ulong");
                default:
                    throw new TypeConversionException($"{obj.GetType().Name} could not be converted to ulong");
            }
        }
    }
}