using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_PropertiesGroupsEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_PropertiesGroupsService : ServiceBase<ModProduct_PropertiesGroupsEntity>
    {

        #region Autogen by VSW

        private ModProduct_PropertiesGroupsService()
            : base("[Mod_Product_PropertiesGroups]")
        {

        }

        private static ModProduct_PropertiesGroupsService _Instance = null;
        public static ModProduct_PropertiesGroupsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_PropertiesGroupsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_PropertiesGroupsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// CanTV       2012/09/21      Tạo mới
        /// </summary>
        /// <param name="sCode">Mã kiểm tra</param>
        /// <param name="IdUpdate">Id bản ghi: Nếu thêm mới thì Id=0</param>
        /// <returns>True: Nếu Duplicate | False: nếu không Duplicate</returns>
        public bool DuplicateCode(string sCode, int IdUpdate, ref string sMess)
        {
            try
            {
                // Có mã trùng
                List<ModProduct_PropertiesGroupsEntity> lstEntity =
                base.CreateQuery()
                        .Where(o => o.ID != IdUpdate && o.Code == sCode)
                        .ToList();

                if (lstEntity == null)
                    return false;

                if (lstEntity.Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                sMess = ex.ToString();
                return true;
            }
        }

    }
}