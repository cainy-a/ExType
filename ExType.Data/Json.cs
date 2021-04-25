using ExType.Shared;
using Utf8Json;

namespace ExType.Data
{
    public static class Json
    {
        /// <summary>
        /// Converts an object to JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="DataConversionException"></exception>
        public static string ToJson(this object obj)
        {
            try
            {
                return JsonSerializer.ToJsonString(obj);
            }
            catch (JsonParsingException)
            {
                throw new DataConversionException("Could not serialize object");
            }
        }

        /// <summary>
        /// Creates an object from JSON
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        /// <exception cref="DataConversionException"></exception>
        public static T UnPack<T>(this string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (JsonParsingException)
            {
                throw new DataConversionException("Could not deserialize object. Was it valid JSON?");
            }
        }
    }
}