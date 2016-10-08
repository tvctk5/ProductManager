using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Khách hàng",
        Description = "Quản lý - Khách hàng",
        Code = "ModProduct_Customers",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component")]

    public class ModProduct_CustomersController : CPController
    {

        public ModProduct_CustomersController()
        {
            //khoi tao Service
            DataService = ModProduct_CustomersService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_CustomersModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_CustomersService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_CustomersModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_CustomersService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_CustomersEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.PointTotal = 1;
                item.CreateDate = DateTime.Now;
            }

            if (item.ID > 0)
                model.NewPassword = "PASSWORD";

            ViewBag.Data = item;
            ViewBag.Model = model;

            // Khởi tạo danh sách nhóm khách hàng
            item.CustomGroupInId = ModProduct_CustomersGroupsService.Instance.GetListIdCustomersGroups_By_CustomerId(item.ID, true);
            item.CustomGroupOutId = ModProduct_CustomersGroupsService.Instance.GetListIdCustomersGroups_By_CustomerId(item.ID, false);

            ViewBag.GetListCustomersGroupsOut = GetListCustomersGroupsByCustomerId(item, false);
            ViewBag.GetListCustomersGroupsIn = GetListCustomersGroupsByCustomerId(item, true);

        }

        /// <summary>
        ///  Lấy danh sách nhóm khách hàng theo Khách hàng Id
        ///  CanTV      2012/09/24      Tạo mới
        /// </summary>
        /// <param name="CustomerId">CustomerId: Id khách hàng</param>
        /// <param name="WhereIn">WhereIn = True nếu lấy danh sách nhóm Khách hàng chứa khách hàng | False: Nếu lấy DS Nhóm khách hàng không chứa khách hàng này</param>
        /// <returns></returns>
        public List<ModProduct_CustomersGroupsEntity> GetListCustomersGroupsByCustomerId(ModProduct_CustomersEntity objCustomerEntity, bool WhereIn)
        {
            List<ModProduct_CustomersGroupsEntity> lstCustomersGroups = null;

            if (objCustomerEntity.ID > 0)
            {
                if (WhereIn)
                {
                    if (!string.IsNullOrEmpty(objCustomerEntity.CustomGroupInId))
                        lstCustomersGroups = ModProduct_CustomersGroupsService.Instance.CreateQuery()
                                        .WhereIn(o => o.ID, objCustomerEntity.CustomGroupInId).ToList();
                }
                else
                {
                    if (!string.IsNullOrEmpty(objCustomerEntity.CustomGroupOutId))
                        lstCustomersGroups = ModProduct_CustomersGroupsService.Instance.CreateQuery()
                                       .WhereIn(o => o.ID, objCustomerEntity.CustomGroupOutId).ToList();
                }
            }

            // Lấy tất cả danh sách nếu Id = 0
            else
                if (!WhereIn)
                    lstCustomersGroups = ModProduct_CustomersGroupsService.Instance.CreateQuery().ToList();

            // Nếu trong trường tạo mới, danh sách các Nhóm KH chứa khách hàng sẽ = Null
            return lstCustomersGroups;
        }

        public void ActionSave(ModProduct_CustomersModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_CustomersModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_CustomersModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionSexGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@Sex", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private ModProduct_CustomersEntity item = null;

        private bool ValidSave(ModProduct_CustomersModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            ViewBag.Data = item;
            ViewBag.Model = model;

            string sCustomGroupInId = item.CustomGroupInId.Trim(',');
            string sCustomGroupOutId = item.CustomGroupOutId.Trim(',');

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            if (string.IsNullOrEmpty(item.UserName.Trim()))
                CPViewPage.Message.ListMessage.Add("Yêu cầu nhập tên đăng nhập");

            if (string.IsNullOrEmpty(model.NewPassword.Trim()))
            {
                if (item.ID <= 0)
                    CPViewPage.Message.ListMessage.Add("Yêu cầu nhập mật khẩu");
            }
            else
            {
                if (item.ID > 0)
                    item.Pass = VSW.Lib.Global.Security.MD5(model.NewPassword);
                else
                    if (model.NewPassword.Trim().ToUpper() != "PASSWORD")
                        item.Pass = VSW.Lib.Global.Security.MD5(model.NewPassword);
            }

            if (string.IsNullOrEmpty(item.FullName.Trim()))
                CPViewPage.Message.ListMessage.Add("Yêu cầu họ và tên khách hàng");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {

                try
                {
                    //save
                    ModProduct_CustomersService.Instance.Save(item);

                    // Cập nhật lại danh sách: Khách hàng - Nhóm khách hàng
                    Customer_Groups_Save(sCustomGroupInId, item);
                }
                catch (Exception ex)
                {
                    Global.Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        ///  Cập nhật lại danh sách: Khách hàng - Nhóm khách hàng
        /// </summary>
        /// <param name="sArrGroupsId">Danh sách nhóm khách hàng</param>
        /// <param name="itemCustomer">Khách hàng Id</param>
        private void Customer_Groups_Save(string sArrGroupsId, ModProduct_CustomersEntity itemCustomer)
        {
            string sQueryDelete = "[CustomersId]=" + itemCustomer.ID;
            string[] ArrGroupsId = null;

            // Xóa dữ liệu cũ đi            
            ModProduct_Customers_GroupsService.Instance.Delete(sQueryDelete);

            // Thêm dữ liệu cập nhật
            if (string.IsNullOrEmpty(sArrGroupsId))
                return;

            ArrGroupsId = sArrGroupsId.Split(',');
            if (ArrGroupsId == null || ArrGroupsId.Length <= 0)
                return;

            ModProduct_Customers_GroupsEntity objCustomers_Groups = null;
            foreach (string itemId in ArrGroupsId)
            {
                objCustomers_Groups = new ModProduct_Customers_GroupsEntity();
                objCustomers_Groups.CustomersGroupsId = Convert.ToInt32(itemId);
                objCustomers_Groups.CustomersId = Convert.ToInt32(itemCustomer.ID);
                objCustomers_Groups.CreateDate = DateTime.Now;
                objCustomers_Groups.Activity = true;

                // Cập nhật, lưu lại thông tin
                ModProduct_Customers_GroupsService.Instance.Save(objCustomers_Groups);
            }
        }

        #endregion
    }

    public class ModProduct_CustomersModel : DefaultModel
    {
        public string NewPassword { get; set; }
    }
}

