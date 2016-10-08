using System;
using System.Collections.Generic;

namespace VSW.Lib.Global
{
    public class IniResourceService : VSW.Core.Interface.IResourceServiceInterface
    {
        Dictionary<string, string> listResource;
        public IniResourceService(string file_ini)
        {
            string Key_Cache = "App.IniResourceService." + VSW.Lib.Global.Security.MD5(file_ini);
            object obj = VSW.Core.Web.Cache.GetValue(Key_Cache);
            if (obj != null)
                listResource = (Dictionary<string, string>)obj;
            else
            {
                listResource = new Dictionary<string,string>();
                if (System.IO.File.Exists(file_ini))
                {
                    System.IO.StreamReader _StreamReader = new System.IO.StreamReader(file_ini);
                    while (_StreamReader.Peek() != -1)
                    {
                        string s = _StreamReader.ReadLine();

                        if (s == null)
                            continue;

                        s = s.Trim();
                        if (s == string.Empty || s.StartsWith("//"))
                            continue;

                        int index = s.IndexOf('=');
                        if (index == -1)
                            continue;

                        string key = s.Substring(0, index).Trim();
                        string value = s.Substring(index + 1).Trim();

                        listResource[key] = value;
                    }
                    _StreamReader.Close();
                }
                VSW.Core.Web.Cache.SetValue(Key_Cache, listResource);
            }
        }

        public string VSW_Core_GetByCode(string code, string defalt)
        {
            if (listResource.ContainsKey(code))
                return listResource[code];
            else
                return defalt;
        }
    }
}
