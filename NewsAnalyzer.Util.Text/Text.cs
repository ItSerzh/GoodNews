using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsAnalyzer.Util.Text
{
    public static class Text
    {
        public static Dictionary<string, int?> ReadAfinn(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Dictionary<string, int?>>(json);
                return items;
            }

        }

        public static string RemoveHiddenSymbols(string target)
        {
            return Regex.Replace(target, @"\t|\n|\r", " ");
        }
        public static string PrepareForIspras(string target)
        {
            target = RemoveHiddenSymbols(target);

            return target.Replace("\"", "");
        }

        public static string EncryptSHA256(string str)
        {
            var sha256 = new SHA256CryptoServiceProvider();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));

            return Encoding.UTF8.GetString(hashedBytes);
        }
    }
}
