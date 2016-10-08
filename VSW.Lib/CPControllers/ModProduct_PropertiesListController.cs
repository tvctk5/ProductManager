using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Thuộc tính sản phẩm",
        Description = "Quản lý - Thuộc tính sản phẩm",
        Code = "ModProduct_PropertiesList",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_PropertiesListController : CPController
    {
        public ModProduct_PropertiesListController()
        {
            //khoi tao Service
            DataService = ModProduct_PropertiesListService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_PropertiesListModel model)
        {
            // sap xep tu dong
            string orderBy = "[ID] ASC";
            int iPropertiesGroupsId = 0;
            string PropertiesGroupsId = CPViewPage.Request["PropertiesGroupsId"];
            if(!string.IsNullOrEmpty(PropertiesGroupsId))
                iPropertiesGroupsId = Convert.ToInt32(PropertiesGroupsId);
             
            // tao danh sach
            var dbQuery = ModProduct_PropertiesListService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Where(iPropertiesGroupsId > 0, o => o.PropertiesGroupsId == iPropertiesGroupsId)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                //.GroupBy("[PropertiesGroupsId]")
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.ModelPropertiesGroupsId = iPropertiesGroupsId;
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
            ViewBag.GetListPropertiesGroup = ModProduct_PropertiesGroupsService.Instance.CreateQuery().ToList();
        }

        public void ActionAdd(ModProduct_PropertiesListModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_PropertiesListService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
                var objMax = ModProduct_PropertiesListService.Instance.CreateQuery().Where(p => p.Code != "").OrderByDesc(o => o.ID).ToSingle();
                if (objMax != null)
                    item.Order = objMax.Order + 1;
            }
            else
            {
                item = new ModProduct_PropertiesListEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;

                // khoi tao gia tri mac dinh khi update
                var objMax = ModProduct_PropertiesListService.Instance.CreateQuery().Where(p => p.Code != "").OrderByDesc(o => o.ID).ToSingle();
                if (objMax != null)
                    item.Order = objMax.Order + 1;
            }

            // LẤy tất cả danh sách của nhóm thuộc tính
            //model.lstPropertiesGroups = GetPropertiesGroups();

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_PropertiesListModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        protected void SaveNewRedirect_Next(int RecordID, int EntityID)
        {
            CPViewPage.SetMessage("Thông tin đã cập nhật.");

            if (RecordID > 0)
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("/" + RecordID, "/" + EntityID));
            else
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl);
        }

        public void ActionApply(ModProduct_PropertiesListModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_PropertiesListModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_PropertiesListEntity item = null;

        private bool ValidSave(ModProduct_PropertiesListModel model)
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

            // Kiểm tra xem có chọn nhóm thuộc tính không
            if (model.ModelPropertiesGroupsId == 0)
                item.PropertiesGroupsId = null;
            else
                item.PropertiesGroupsId = model.ModelPropertiesGroupsId;

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                //// Kiểm tra mã xem có trùng với mã nào khác đã có không
                //string sMessError = string.Empty;
                //if (ModProduct_PropertiesListService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                //{
                //    if (string.IsNullOrEmpty(sMessError))
                //        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã thuộc tính", item.Code));
                //    else
                //        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                //    return false;
                //}

                item.Name = item.Name.Trim();

                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProduct_PropertiesListService.Instance.Save(item);
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

        private List<ModProduct_PropertiesGroupsEntity> GetPropertiesGroups()
        {
            return ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                .Where(o => o.Activity == true)
                .ToList();
        }

        #endregion
    }

    public class ModProduct_PropertiesListModel : DefaultModel
    {
        public string SearchText { get; set; }
        public int ModelPropertiesGroupsId { get; set; }
        public List<ModProduct_PropertiesGroupsEntity> lstPropertiesGroups { get; set; }
    }
}

