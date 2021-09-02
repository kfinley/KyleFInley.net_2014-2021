using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using _928.Core.Cryptography;

namespace _928.Web {
    public static class CacheBuster {
        static IDictionary<string, string> hashes = new Dictionary<string, string>();
        static object synchLock = new object();

        public static void ProcessDirectory(string assetDirectory) {
            var root = HttpContext.Current.Server.MapPath("/");

            foreach (var file in Directory.GetFiles(root + assetDirectory)) {
                hashes.Add(Path.GetFileName(file).ToLower(), CreateSignature(file));
            }
        }

        public static string RetrieveFileNameWithHash(string assetName) {
            lock (synchLock) {
                return string.Concat(assetName, '?', hashes[assetName.ToLower()]);
            }
        }

        private static string CreateSignature(string file) {
            byte[] bytes;
            using (var hash = new Crc32()) {
                bytes = hash.ComputeHash(Encoding.ASCII.GetBytes(File.ReadAllText(file)));
            }

            var data = new StringBuilder();
            Array.ForEach(bytes, b => data.Append(b.ToString("x2")));
            return data.ToString();
        }
    }
}
