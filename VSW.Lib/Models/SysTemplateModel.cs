using System;

using VSW.Core.Interface;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class SysTemplateEntity : EntityBase, ITemplateInterface
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        public string Custom { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public string CssFile { get; set; }
        
        [DataInfo]
        public string JsFile { get; set; }

        [DataInfo]
        public string CssContent { get; set; }

        [DataInfo]
        public string JsContent { get; set; }

        #endregion
    }

    public class SysTemplateService : ServiceBase<SysTemplateEntity>, ITemplateServiceInterface
    {
        #region Autogen by VSW

        public SysTemplateService()
            : base("[Sys_Template]")
        {

        }

        private static SysTemplateService _Instance = null;
        public static SysTemplateService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SysTemplateService();

                return _Instance;
            }
        }

        #endregion

        public SysTemplateEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        #region ITemplateServiceInterface Members

        public ITemplateInterface VSW_Core_GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle_Cache();
        }

        public void VSW_Core_CPSave(ITemplateInterface item)
        {
            base.Save(item as SysTemplateEntity);
        }

        #endregion
    }
}
