using System;
using System.IO;
using System.Text;

//using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace VSW.Lib.Global
{
    public class ObjectCookies<T>
    {
        public static bool Exists(string Key)
        {
            Key = "OBJ_" + Key;

            return Cookies.Exists(Key);
        }

        public static void SetValue(string Key, T Value)
        {
            Key = "OBJ_" + Key;

            try
            {
                MemoryStream stream = new MemoryStream();
                //BinaryFormatter format = new BinaryFormatter();
                XmlSerializer format = new XmlSerializer(typeof(T));
                format.Serialize(stream, Value);
                string s = Convert.ToBase64String(stream.ToArray());
                stream.Close();

                Cookies.SetValue(Key, s, true);
            }
            catch { }
        }

        public static T GetValue(string Key)
        {
            Key = "OBJ_" + Key;

            T t = default(T);

            if (!Cookies.Exists(Key))
                return t;

            try
            {
                string s = Cookies.GetValue(Key, true);
                byte[] arrBytes = Convert.FromBase64String(s);
                MemoryStream stream = new MemoryStream();
                stream.Write(arrBytes, 0, arrBytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                //BinaryFormatter format = new BinaryFormatter();
                XmlSerializer format = new XmlSerializer(typeof(T));
                t = (T)format.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                Cookies.Remove(Key); 
            }

            return t;
        }

        public static void Remove(string Key)
        {
            Key = "OBJ_" + Key;

            Cookies.Remove(Key);
        }
    }
}
