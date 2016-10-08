using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_PropertiesListEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int? PropertiesGroupsId { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public string Note { get; set; }

        [DataInfo]
        // 0: Text Inline | 1: Text MutiLine
        public int Type { get; set; }

        [DataInfo]
        // Có hiển thị dữ liệu trước đó lên để cho chọn hay không. 0: Không hiển thị | 1: Hiển thị
        public bool ViewOldData { get; set; }

        [DataInfo]
        // Đơn vị tính giá trị thuộc tính
        public string Unit { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public int Order { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_PropertiesListService : ServiceBase<ModProduct_PropertiesListEntity>
    {

        #region Autogen by VSW

        private ModProduct_PropertiesListService()
            : base("[Mod_Product_PropertiesList]")
        {

        }

        private static ModProduct_PropertiesListService _Instance = null;
        public static ModProduct_PropertiesListService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_PropertiesListService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_PropertiesListEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }


        /// <summary>
        /// Kiểm tra mã trùng
        /// CanTV       2012/09/21      Tạo mới
        /// </summary>
        /// <param name="sCode">Mã tỷ giá</param>
        /// <param name="IdUpdate">Id bản ghi: Nếu thêm mới thì Id=0</param>
        /// <returns>True: Nếu Duplicate | False: nếu không Duplicate</returns>
        public bool DuplicateCode(string sCode, int IdUpdate, ref string sMess)
        {
            try
            {
                // Có mã trùng
                List<ModProduct_PropertiesListEntity> lstEntity =
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