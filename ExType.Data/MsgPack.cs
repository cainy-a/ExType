using System.Diagnostics.CodeAnalysis;
using ExType.Shared;
using MessagePack;
using MessagePack.Resolvers;

namespace ExType.Data
{
    /// <summary>
    /// Extension methods to pack and unpack MessagePack
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class MsgPack
    {
        /// <summary>
        /// Packs an object with MessagePack
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="privateMembers">Pack private members</param>
        /// <returns></returns>
        /// <exception cref="DataConversionException"></exception>
        public static byte[] Pack(this object obj, bool privateMembers = false)
        {
            try
            {
                return MessagePackSerializer.Serialize(obj, privateMembers
                    ? ContractlessStandardResolverAllowPrivate.Options
                    : ContractlessStandardResolver.Options);
            }
            catch (MessagePackSerializationException)
            {
                throw new DataConversionException("Could not pack object");
            }
        }

        /// <summary>
        /// Unpacks an object from MessagePack
        /// </summary>
        /// <param name="msgpack"></param>
        /// <param name="privateMembers">Unpack private members</param>
        /// <returns></returns>
        /// <exception cref="DataConversionException"></exception>
        public static T UnPack<T>(this byte[] msgpack, bool privateMembers = false)
        {
            try
            {
                return MessagePackSerializer.Deserialize<T>(msgpack, privateMembers
                    ? ContractlessStandardResolverAllowPrivate.Options
                    : ContractlessStandardResolver.Options);
            }
            catch (MessagePackSerializationException)
            {
                throw new DataConversionException("Could not unpack object. Was it valid MessagePack?");
            }
        }

        /// <summary>
        /// Converts MessagePack to JSON
        /// </summary>
        /// <param name="msgpack"></param>
        /// <param name="privateMembers">Convert private members</param>
        /// <returns></returns>
        /// <exception cref="DataConversionException"></exception>
        public static string MsgPackToJson(this byte[] msgpack, bool privateMembers = false)
        {
            try
            {
                return MessagePackSerializer.ConvertToJson(msgpack, privateMembers
                    ? ContractlessStandardResolverAllowPrivate.Options
                    : ContractlessStandardResolver.Options);
            }
            catch (MessagePackSerializationException)
            {
                throw new DataConversionException("Could not convert msgpack to JSON. Was it valid MessagePack?");
            }
        }
        
        /// <summary>
        /// Converts JSON to MessagePack
        /// </summary>
        /// <param name="json"></param>
        /// <param name="privateMembers">Convert private members</param>
        /// <returns></returns>
        /// <exception cref="DataConversionException"></exception>
        public static byte[] JsonToMsgPack(this string json, bool privateMembers = false)
        {
            try
            {
                return MessagePackSerializer.ConvertFromJson(json, privateMembers
                    ? ContractlessStandardResolverAllowPrivate.Options
                    : ContractlessStandardResolver.Options);
            }
            catch (MessagePackSerializationException)
            {
                throw new DataConversionException("Could not convert JSON to msgpack. Was it valid JSON?");
            }
        }
    }
}