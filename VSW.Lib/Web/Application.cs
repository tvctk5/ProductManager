using System;
using System.Web;
using System.Collections.Generic;

using VSW.Lib.MVC;

namespace VSW.Lib.Web
{
    public class Application : System.Web.HttpApplication
    {
        public static List<CPModuleInfo> CPModules { get; set; }
        public static List<ModuleInfo> Modules { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            if (CPModules == null)
            {
                CPModules = new List<CPModuleInfo>();
                Modules = new List<ModuleInfo>();

                var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
                foreach (var type in types)
                {
                    object[] attributes = type.GetCustomAttributes(typeof(CPModuleInfo), true);
                    if (attributes == null || attributes.GetLength(0) == 0)
                    {
                        attributes = type.GetCustomAttributes(typeof(ModuleInfo), true);
                        if (attributes == null || attributes.GetLength(0) == 0)
                            continue;

                        var moduleInfo = attributes[0] as ModuleInfo;

                        if (Modules.Find(o => o.Code == moduleInfo.Code) == null)
                        {
                            moduleInfo.ModuleType = type;

                            Modules.Add(moduleInfo);
                        }

                        continue;
                    }

                    {
                        var moduleInfo = attributes[0] as CPModuleInfo;

                        if (CPModules.Find(o => o.Code == moduleInfo.Code) == null)
                            CPModules.Add(moduleInfo);
                    }
                }
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            VSW.Core.Web.Application.BeginRequest();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            VSW.Lib.Global.Application.OnError();
        }
    }
}