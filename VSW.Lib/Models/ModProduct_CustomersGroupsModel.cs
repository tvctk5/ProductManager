using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_CustomersGroupsEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public int PointStart { get; set; }

        [DataInfo]
        public int PointEnd { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        #endregion

    }

    public class ModProduct_CustomersGroupsService : ServiceBase<ModProduct_CustomersGroupsEntity>
    {

        #region Autogen by VSW

        private ModProduct_CustomersGroupsService()
            : base("[Mod_Product_CustomersGroups]")
        {

        }

        private static ModProduct_CustomersGroupsService _Instance = null;
        public static ModProduct_CustomersGroupsService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_CustomersGroupsService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_CustomersGroupsEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        /// <summary>
        ///  Lấy danh sách ID nhóm khách hàng theo Khách hàng Id, Cách nhau bằng dấu ","
        ///  CanTV      2012/09/24      Tạo mới
        /// </summary>
        /// <param name="CustomerId">CustomerId: Id khách hàng</param>
        /// <param name="WhereIn">WhereIn = True nếu lấy danh sách nhóm Khách hàng chứa khách hàng | False: Nếu lấy DS Nhóm khách hàng không chứa khách hàng này</param>
        /// <returns></returns>
        public string GetListIdCustomersGroups_By_CustomerId(int CustomerId, bool WhereIn)
        {
            string sListId = string.Empty;
            int[] ArrCustomers_Groups = null;
            int i = 0;

            List<ModProduct_Customers_GroupsEntity> lstModProduct_Customers_Groups = null;
            // Nếu lấy DS nhóm khách hàng chứa khách hàng
            if (WhereIn)
                if (CustomerId > 0) // Cập nhật người dùng
                    lstModProduct_Customers_Groups = ModProduct_Customers_GroupsService.Instance.CreateQuery()
                        .Where(o => o.CustomersId == CustomerId)
                        .Where(o => o.Activity == true)
                        .ToList();
                else
                    return sListId;  // Thêm mới người dùng

            // Nếu lấy DS nhóm khách hàng KHÔNG chứa khách hàng
            else
                if (CustomerId > 0) // Cập nhật người dùng
                {
                    // Lấy danh sách các ID nhóm chứ khách hàng
                    sListId = GetListIdCustomersGroups_By_CustomerId(CustomerId, true);
                    List<ModProduct_CustomersGroupsEntity> lstModProduct_CustomersGroups = null;
                    // Nếu không có nhóm nào thì lấy tất cả
                    if (string.IsNullOrEmpty(sListId))
                        lstModProduct_CustomersGroups = ModProduct_CustomersGroupsService.Instance.CreateQuery()
                            .Where(o => o.Activity == true)
                            .ToList();
                    else
                        // Nếu có nhóm thì lấy Not In: Để lấy ra những nhóm không chứa khách hàng đó
                        lstModProduct_CustomersGroups = ModProduct_CustomersGroupsService.Instance.CreateQuery()
                           .Where(o => o.Activity == true)
                           .WhereNotIn(o => o.ID, sListId).Distinct()
                           .ToList();

                    // Tạo mới mảng
                    if (lstModProduct_CustomersGroups == null || lstModProduct_CustomersGroups.Count <= 0)
                        return string.Empty;

                    ArrCustomers_Groups = new int[lstModProduct_CustomersGroups.Count];
                    foreach (ModProduct_CustomersGroupsEntity item in lstModProduct_CustomersGroups)
                    {
                        ArrCustomers_Groups[i] = item.ID;
                        i++;
                    }

                    // Lấy danh sách chuỗi
                    sListId = VSW.Core.Global.Array.ToString(ArrCustomers_Groups);
                    return sListId;

                }
                else  // Thêm mới người dùng
                    lstModProduct_Customers_Groups = ModProduct_Customers_GroupsService.Instance.CreateQuery()
                            .Where(o => o.Activity == true)
                            .ToList();

            if (lstModProduct_Customers_Groups == null || lstModProduct_Customers_Groups.Count <= 0)
                return sListId;

            // Tạo mới mảng
            if (lstModProduct_Customers_Groups == null || lstModProduct_Customers_Groups.Count <= 0)
                return sListId;

            ArrCustomers_Groups = new int[lstModProduct_Customers_Groups.Count];
            foreach (ModProduct_Customers_GroupsEntity item in lstModProduct_Customers_Groups)
            {
                ArrCustomers_Groups[i] = item.CustomersGroupsId;
                i++;
            }

            // Lấy danh sách chuỗi
            sListId = VSW.Core.Global.Array.ToString(ArrCustomers_Groups);
            return sListId;
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
                List<ModProduct_CustomersGroupsEntity> lstEntity =
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