using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using DotNetCommonLibrary.Diagnostics;

namespace DotNetCommonLibrary.IO.File.Serializer
{
    public class EncryptionSerializer
    {
        public static string DefaultFilePath
        {
            get
            {
                return Path.Combine(Paths.GetApplicationFolderPath(), FileVersionInfo.GetProductName(), "masterdata");
            }
        }

        static void Save(byte[] data, string path)
        {
            var directory = Directory.CreateDirectory(Path.GetDirectoryName(path));
            if (directory != null)
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        static byte[] Load(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var buffer = new byte[fs.Length];
                var readLength = fs.Read(buffer, 0, buffer.Length);
                if (readLength < fs.Length)
                {
                    throw new FileLoadException("failed to read: " + path);
                }
                return buffer;
            }
        }

        // The data converte to protected data
        static byte[] Protect<T>(T data, DataProtectionScope scope)
        {
            byte[] buffer = null;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, data);
                buffer = ms.ToArray();
            }
            return ProtectedData.Protect(buffer, null, scope);
        }

        // The protected data to unprotected data
        static T Unprotect<T>(byte[] data, DataProtectionScope scope)
        {
            var unprotectedData = ProtectedData.Unprotect(data, null, scope);
            using (var ms = new MemoryStream(unprotectedData))
            {
                var bf = new BinaryFormatter();
                return (T)bf.Deserialize(ms);
            }
        }

        /// <summary>
        /// Saves a data to a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="path"></param>
        /// <param name="scope"></param>
        public static void Encrypt<T>(T data, string path, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            var encryptedData = Protect(data, scope);
            Save(encryptedData, path);
        }

        /// <summary>
        /// Loads a data from a file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public static T Decrypt<T>(string path, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            var buffer = Load(path);
            return Unprotect<T>(buffer, scope);
        }
    }
}
