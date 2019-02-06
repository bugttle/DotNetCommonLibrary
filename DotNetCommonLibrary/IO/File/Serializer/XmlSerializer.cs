using System.IO;
using System.Text;
using DotNetCommonLibrary.Diagnostics;

namespace DotNetCommonLibrary.IO.File.Serializer
{
    public static class XmlSerializer
    {
        public static string DefaultFilePath
        {
            get
            {
                return Path.Combine(Paths.GetApplicationFolderPath(), "user_settings.xml");
            }
        }

        /// <summary>
        /// Saves a data to a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public static void Save<T>(T data, string path)
        {
            // create the folder
            var baseFolderPath = Path.GetDirectoryName(path);
            Directory.CreateDirectory(baseFolderPath);

            // save the file
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var sw = new StreamWriter(path, false, new UTF8Encoding(false)))
            {
                serializer.Serialize(sw, data);
            }
        }

        /// <summary>
        /// Loads a data from a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            using (var sr = new StreamReader(path, new UTF8Encoding(false)))
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(sr);
            }
        }

    }
}
