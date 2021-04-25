using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExType.TypeConv
{
    /// <summary>
    /// Adds QoL features for streams
    /// </summary>
    public static class StreamTools
    {
        /// <summary>
        /// Generates a stream from a string
        /// </summary>
        /// <param name="str">String to write into a stream</param>
        /// <returns>A stream containing the string, with position at the beginning</returns>
        public static MemoryStream ToStream(this string str)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(str);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        /// Reads a whole stream and converts to a string
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="enc"></param>
        /// <returns></returns>
        public static string ReadStringToEnd(this Stream stream, Encoding enc = null)
        {
            enc ??= Encoding.Default;
            return enc.GetString(stream.ReadToEnd());
        }

        /// <summary>
        /// Reads a whole stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ReadToEnd(this Stream stream)
        {
            var buffer = new List<byte>();
            int? result = null;
            while (result != -1)
            {
                if (result != null)
                    buffer.Add(result.ToByte());
                result = stream.ReadByte();
            }

            return buffer.ToArray();
        }
    }
}