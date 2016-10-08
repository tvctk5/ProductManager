using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Chủng loại sản phẩm",
        Description = "Quản lý - Chủng loại sản phẩm",
        Code = "ModProduct_Groups",
        Access = 31,
        Order = 200,
        ShowInMenu = false,
        CssClass = "icon-16-component")]
    public class ModProduct_GroupsController : CPController
    {
        public ModProduct_GroupsController()
        {
            //khoi tao Service
            DataService = ModProduct_GroupsService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_GroupsModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_GroupsService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Where(o => o.ShowHide == true) // Chỉ load những nhóm được phép hiển thị
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_GroupsModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_GroupsService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_GroupsEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                item.ParentId = 0;
            }

            string sPropertiesGroupsInId = string.Empty;
            string sPropertiesGroupsOutId = string.Empty;

            ViewBag.GetListPropertiesGroupsIn = GetListPropertiesGroupByProductId(item, true, ref sPropertiesGroupsInId);
            ViewBag.GetListPropertiesGroupsOut = GetListPropertiesGroupByProductId(item, false, ref sPropertiesGroupsOutId);

            // Khởi tạo danh sách thuộc tính

            model.PropertiesGroupsInId = sPropertiesGroupsInId;
            model.PropertiesGroupsOutId = sPropertiesGroupsOutId;


            ViewBag.Data = item;
            ViewBag.Model = model;
        }


        public void ActionSave(ModProduct_GroupsModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_GroupsModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_GroupsModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_GroupsEntity item = null;

        private bool ValidSave(ModProduct_GroupsModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                // Kiểm tra mã xem có trùng với mã nào khác đã có không
                string sMessError = string.Empty;
                if (ModProduct_GroupsService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                {
                    if (string.IsNullOrEmpty(sMessError))
                        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã nhóm sản phẩm", item.Code));
                    else
                        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                    return false;
                }

                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                #region Tạo mới chuyên mục
                // Tạo mới Chuyên mục
                WebMenuEntity objWebMenuEntity = null;

                // Lấy thông tin của chủng loại Cha
                var dbQuery = ModProduct_GroupsService.Instance.CreateQuery()
                                         .Where(o => o.ID == item.ParentId).ToList();

                if (dbQuery != null && dbQuery.Count > 0)
                {
                    // Parent ID mới của chuyên mục
                    item.Web_MenuParentId = dbQuery[0].Web_MenuId;

                    // Nếu tạo mới
                    if (item.ID <= 0)
                    {
                        objWebMenuEntity = new WebMenuEntity();
                        // Thêm mới nhận vào là MEnu cha
                        objWebMenuEntity.ParentID = dbQuery[0].Web_MenuId;
                    }
                    else
                    {
                        var dbQueryWebMenu = WebMenuService.Instance.CreateQuery()
                                         .Where(o => o.ID == item.Web_MenuId).ToList();

                        if (dbQueryWebMenu != null && dbQueryWebMenu.Count > 0)
                            objWebMenuEntity = dbQueryWebMenu[0];
                        else
                            objWebMenuEntity = new WebMenuEntity();

                        // Cập nhật lại ID cha
                        objWebMenuEntity.ParentID = item.Web_MenuParentId;
                    }

                    objWebMenuEntity.Name = item.Name;
                    objWebMenuEntity.Code = Data.GetCode(item.Name);
                    objWebMenuEntity.Type = "Product_Info";
                    objWebMenuEntity.Activity = true;
                    objWebMenuEntity.LangID = model.LangID;
                    objWebMenuEntity.Order = Convert.ToInt32(DateTime.Now.ToString("yyyyMMmmss"));

                    try
                    {
                        // Tạo mới chuyên mục thành công
                        WebMenuService.Instance.Save(objWebMenuEntity);
                    }
                    catch (Exception ex)
                    {
                        // Tạo mới chuyên mục thất bại
                        CPViewPage.Message.ListMessage.Add(ex.Message);
                    }
                }
                #endregion

                // Lấy thông tin chuyên mục cho chủng loại
                item.Web_MenuId = objWebMenuEntity.ID;
                item.ShowHide =true;

                try
                {
                    //save
                    ModProduct_GroupsService.Instance.Save(item);

                    string sArrPropertiesGroupsIn = model.PropertiesGroupsInId.Trim();

                    // update

                    UpdateProductGroups_PropertiesGroups(item.ID, sArrPropertiesGroupsIn);
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

        #endregion

        /// <summary>
        ///  Lấy danh sách NHóm thuộc tính theo  Id Product Info: Not in hoặc In
        ///  CanTV      2012/09/24      Tạo mới
        /// </summary>
        /// <param name="objProductGroupEntity">objPropertiesGroups: Thuộc tính Id</param>
        /// <param name="WhereIn">WhereIn = True nếu lấy IN | False: Nếu lấy  Not in</param>
        /// <returns></returns>
        public List<ModProduct_PropertiesGroupsEntity> GetListPropertiesGroupByProductId(
            ModProduct_GroupsEntity objProductGroupEntity,
            bool WhereIn,
            ref string sPropertiesGroupsId)
        {
            List<ModProduct_Groups_PropertiesGroupsEntity> lstGroupsList = null;
            List<ModProduct_PropertiesGroupsEntity> lstReturnList = null;
            if (objProductGroupEntity.ID > 0)
            {
                if (WhereIn)
                {
                    lstGroupsList = ModProduct_Groups_PropertiesGroupsService.Instance.CreateQuery()
                                    .Where(o => o.ProductGroupsId == objProductGroupEntity.ID).ToList();
                }
                else
                {
                    // Lấy những nhóm chứa
                    GetListPropertiesGroupByProductId(objProductGroupEntity, true, ref sPropertiesGroupsId);

                    //Lấy những nhóm không chứa (Not in) 
                    lstReturnList = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                                   .WhereNotIn(o => o.ID, string.IsNullOrEmpty(sPropertiesGroupsId) ? "0" : sPropertiesGroupsId)
                                   .ToList();
                }
            }

            // Lấy tất cả danh sách nếu Id = 0
            else
                if (!WhereIn)
                    // Lấy toàn bộ các Nhóm
                    lstReturnList = ModProduct_PropertiesGroupsService.Instance
                        .CreateQuery()
                        .ToList();

            if (lstGroupsList == null)
            {
                if (lstReturnList == null)
                    return null;

                else
                {
                    // Lấy danh sách chuỗi
                    foreach (ModProduct_PropertiesGroupsEntity item in lstReturnList)
                    {
                        if (string.IsNullOrEmpty(sPropertiesGroupsId))
                            sPropertiesGroupsId = item.ID.ToString();
                        else
                            sPropertiesGroupsId = sPropertiesGroupsId + "," + item.ID.ToString();
                    }
                }
            }
            else
            {
                // Lấy danh sách chuỗi
                foreach (ModProduct_Groups_PropertiesGroupsEntity item in lstGroupsList)
                {
                    if (string.IsNullOrEmpty(sPropertiesGroupsId))
                        sPropertiesGroupsId = item.PropertiesGroupsId.ToString();
                    else
                        sPropertiesGroupsId = sPropertiesGroupsId + "," + item.PropertiesGroupsId.ToString();
                }

                // Lấy nhóm theo danh sách
                if (WhereIn)
                    lstReturnList = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                        .WhereIn(o => o.ID, sPropertiesGroupsId)
                        .ToList();
                else
                    lstReturnList = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                    .WhereNotIn(o => o.ID, sPropertiesGroupsId)
                    .ToList();
            }
            // Nếu trong trường tạo mới, danh sách các chứa sẽ = Null
            return lstReturnList;
        }
        private void UpdateProductGroups_PropertiesGroups(int iProductGroupsId, string ArrPropertiesGroupsIn)
        {
            ArrPropertiesGroupsIn = ArrPropertiesGroupsIn.Trim(',');
            string sQueryDelete = "[ProductGroupsId]=" + iProductGroupsId;

            // Xóa dữ liệu cũ
            ModProduct_Groups_PropertiesGroupsService.Instance.Delete(sQueryDelete);

            if (string.IsNullOrEmpty(ArrPropertiesGroupsIn))
                return;

            string[] lstPropertiesGroupsIn = ArrPropertiesGroupsIn.Split(',');
            if (lstPropertiesGroupsIn == null || lstPropertiesGroupsIn.Length <= 0)
                return;

            ModProduct_Groups_PropertiesGroupsEntity objProductGroupsEntity = null;
            foreach (string item in lstPropertiesGroupsIn)
            {
                objProductGroupsEntity = new ModProduct_Groups_PropertiesGroupsEntity();
                objProductGroupsEntity.ProductGroupsId = iProductGroupsId;
                objProductGroupsEntity.PropertiesGroupsId = Convert.ToInt32(item);
                ModProduct_Groups_PropertiesGroupsService.Instance.Save(objProductGroupsEntity);
            }
        }
    }

    public class ModProduct_GroupsModel : DefaultModel
    {
        public string SearchText { get; set; }
        public string PropertiesGroupsInId { get; set; }
        public string PropertiesGroupsOutId { get; set; }

        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }
    }
}

