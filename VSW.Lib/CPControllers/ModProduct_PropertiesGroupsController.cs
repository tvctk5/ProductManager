using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Nhóm thuộc tính",
        Description = "Quản lý - Nhóm thuộc tính",
        Code = "ModProduct_PropertiesGroups",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_PropertiesGroupsController : CPController
    {
        public ModProduct_PropertiesGroupsController()
        {
            //khoi tao Service
            DataService = ModProduct_PropertiesGroupsService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_PropertiesGroupsModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_PropertiesGroupsModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_PropertiesGroupsService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_PropertiesGroupsEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                var objMax = ModProduct_PropertiesGroupsService.Instance.CreateQuery().OrderByDesc(o => o.ID).ToSingle();
                if (objMax != null)
                {
                    item.Order = objMax.Order + 1;
                    item.Code = objMax.Code;
                }
            }

            string sPropertiesInId = string.Empty;
            string sPropertiesOutId = string.Empty;
            ViewBag.GetListPropertiesIn = GetListPropertiesByGroupsId(item, true, ref sPropertiesInId);
            ViewBag.GetListPropertiesOut = GetListPropertiesByGroupsId(item, false, ref sPropertiesOutId);

            // Khởi tạo danh sách thuộc tính
            model.PropertiesInId = sPropertiesInId;
            model.PropertiesOutId = sPropertiesOutId;

            ViewBag.Data = item;
            ViewBag.Model = model;

        }

        public void ActionSave(ModProduct_PropertiesGroupsModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_PropertiesGroupsModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_PropertiesGroupsModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_PropertiesGroupsEntity item = null;

        private bool ValidSave(ModProduct_PropertiesGroupsModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            ViewBag.Data = item;
            ViewBag.Model = model;

            string sPropertiesInId = model.PropertiesInId.Trim(',');
            string sPropertiesOutId = model.PropertiesOutId.Trim(',');

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {

                // Kiểm tra mã xem có trùng với mã nào khác đã có không
                string sMessError = string.Empty;
                if (ModProduct_PropertiesGroupsService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                {
                    if (string.IsNullOrEmpty(sMessError))
                        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã nhóm thuộc tính", item.Code));
                    else
                        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                    return false;
                }

                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProduct_PropertiesGroupsService.Instance.Save(item);

                    // Cập nhật lại danh sách: Thuộc tính - Nhóm thuộc tính
                    Properties_Groups_Save(sPropertiesInId, item.ID, model);
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
        ///  Cập nhật lại danh sách: Thuộc tính - Nhóm thuộc tính
        /// </summary>
        /// <param name="sArrPropertiesId">Danh sách thuộc tính</param>
        /// <param name="itemPropertiesGroups">Đối tượng nhóm thuộc tính</param>
        private void Properties_Groups_Save(string sArrPropertiesId, int PropertiesGroupsId, ModProduct_PropertiesGroupsModel model)
        {
            string[] ArrPropertiesId = null;
            int? iPropertiesGroups = PropertiesGroupsId;

            // Đưa tất cả thuộc tính của nhóm về NULL
            ModProduct_PropertiesListService.Instance.Update(o => o.PropertiesGroupsId == iPropertiesGroups,
                "@PropertiesGroupsId", null
                );

            // Thêm dữ liệu cập nhật
            if (string.IsNullOrEmpty(sArrPropertiesId))
                return;

            ArrPropertiesId = sArrPropertiesId.Split(',');
            if (ArrPropertiesId == null || ArrPropertiesId.Length <= 0)
                return;

            ModProduct_PropertiesListService.Instance.Update("[ID] IN (" + sArrPropertiesId + ")",
                "@PropertiesGroupsId", PropertiesGroupsId
                );

        }

        #endregion

        /// <summary>
        ///  Lấy danh sách thuộc tính theo  Id Group: Not in hoặc In
        ///  CanTV      2012/09/24      Tạo mới
        /// </summary>
        /// <param name="objPropertiesGroups">objPropertiesGroups: Thuộc tính Id</param>
        /// <param name="WhereIn">WhereIn = True nếu lấy danh sách thuộc tính trong nhóm thuộc tính | False: Nếu lấy  danh sách thuộc tính KHÔNG THUỘC trong nhóm thuộc tính</param>
        /// <returns></returns>
        public List<ModProduct_PropertiesListEntity> GetListPropertiesByGroupsId(ModProduct_PropertiesGroupsEntity objPropertiesGroups, bool WhereIn, ref string sPropertiesGroupsId)
        {
            List<ModProduct_PropertiesListEntity> lstPropertiesList = null;

            if (objPropertiesGroups.ID > 0)
            {
                if (WhereIn)
                {
                    lstPropertiesList = ModProduct_PropertiesListService.Instance.CreateQuery()
                                    .Where(o => o.PropertiesGroupsId == objPropertiesGroups.ID).ToList();
                }
                else
                {
                    lstPropertiesList = ModProduct_PropertiesListService.Instance.CreateQuery()
                                   .Where(o => o.PropertiesGroupsId == null).ToList();
                }
            }

            // Lấy tất cả danh sách nếu Id = 0
            else
                if (!WhereIn)
                    lstPropertiesList = ModProduct_PropertiesListService.Instance
                        .CreateQuery()
                        .Where(o => o.PropertiesGroupsId == null)
                        .ToList();

            if (lstPropertiesList == null)
                return null;

            // Lấy danh sách chuỗi
            foreach (ModProduct_PropertiesListEntity item in lstPropertiesList)
            {
                if (string.IsNullOrEmpty(sPropertiesGroupsId))
                    sPropertiesGroupsId = item.ID.ToString();
                else
                    sPropertiesGroupsId = sPropertiesGroupsId + "," + item.ID.ToString();
            }

            // Nếu trong trường tạo mới, danh sách các Nhóm KH chứa khách hàng sẽ = Null
            return lstPropertiesList;
        }
    }

    public class ModProduct_PropertiesGroupsModel : DefaultModel
    {
        public string SearchText { get; set; }
        public string PropertiesInId { get; set; }
        public string PropertiesOutId { get; set; }
    }
}

