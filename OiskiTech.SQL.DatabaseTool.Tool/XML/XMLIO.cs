using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Oiski.SQL.DatabaseTool
{
    /// <summary>
    /// Represents a simple I/O system for handling XML files
    /// </summary>
    internal static class XMLIO
    {
        /// <summary>
        /// Serialize an object of type <typeparamref name="T"/>, where <typeparamref name="T"/> is any serializable <see cref="object"/>.
        /// </summary>
        /// <typeparam name="T">The type to serialize</typeparam>
        /// <param name="_obj">The object to serialize</param>
        /// <param name="_path">The folder structure path to the file, including the file name but without the file extension</param>
        public static void SerializeXML<T>(T _obj, string _path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            Stream stream = new FileStream($"{_path}.xml", FileMode.Create);
            XmlWriter writer = new XmlTextWriter(stream, Encoding.Unicode);
            serializer.Serialize(writer, _obj);
            writer.Close();
        }

        /// <summary>
        /// Deserialize an object of type <typeparamref name="T"/>, where <typeparamref name="T"/> is any deserializable <see cref="object"/>.
        /// </summary>
        /// <param name="_path">The folder structure path to the file, including the file name but without the file extension</param>
        /// <returns>A new <see langword="instance"/> of type <typeparamref name="T"/> where all <see langword="values"/> are set according to deserialized XML batch</returns>
        public static T DeserializeXML<T>(string _path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result;

            using ( TextReader reader = File.OpenText($"{_path}.xml") )
            {
                result = ( T ) serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
