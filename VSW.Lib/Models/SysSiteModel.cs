﻿using System;

using VSW.Core.Interface;
using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class SysSiteEntity : EntityBase, ISiteInterface
    {
        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int PageID { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public string Custom { get; set; }

        [DataInfo]
        public bool Default { get; set; }

        [DataInfo]
        public int Order { get; set; }

        #endregion
    }

    public class SysSiteService : ServiceBase<SysSiteEntity>, ISiteServiceInterface
    {
        #region Autogen by VSW

        public SysSiteService()
            : base("[Sys_Site]")
        {

        }

        private static SysSiteService _Instance = null;
        public static SysSiteService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SysSiteService();

                return _Instance;
            }
        }

        #endregion

        public SysSiteEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        #region ISiteServiceInterface Members

        public ISiteInterface VSW_Core_GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle_Cache();
        }

        public ISiteInterface VSW_Core_GetByCode(string code)
        {
            return base.CreateQuery()
               .Where(o => o.Code == code)
               .ToSingle_Cache();
        }

        public ISiteInterface VSW_Core_GetDefault()
        {
            return base.CreateQuery()
               .Where(o => o.Default == true)
               .ToSingle_Cache();
        }

        #endregion
    }
}