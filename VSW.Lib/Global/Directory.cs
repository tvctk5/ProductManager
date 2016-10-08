using System;

namespace VSW.Lib.Global
{
    public class Directory
    {
        public static string GetFolder(int GID)
        {
            string strID = GID.ToString();
            string folder = string.Empty;
            for (int i = 0; (strID.Length > 2); i++)
            {
                folder += strID.Substring(0, 1) + "/";
                strID = strID.Remove(0, 1);
            }
            return folder;
        }

        public static void Create(string path)
        {
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
        }

        public static void Delete(string path)
        {
            if (System.IO.Directory.Exists(path))
                System.IO.Directory.Delete(path);
        }

        public static void DeleteAll(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                string[] dir_list = System.IO.Directory.GetDirectories(path);
                foreach (string s in dir_list)
                    DeleteAll(s);

                string[] file_list = System.IO.Directory.GetFiles(path);
                foreach (string s in file_list)
                    System.IO.File.Delete(s);

                Delete(path);
            }
        }

        public static string[] GetListDir(string path)
        {
            if (!System.IO.Directory.Exists(path))
                return new string[] { };

            return System.IO.Directory.GetDirectories(path);
        }
    }
}
