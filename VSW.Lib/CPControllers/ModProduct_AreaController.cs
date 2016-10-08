using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Ngành nghề - lĩnh vực", 
        Description = "Quản lý - Ngành nghề, lĩnh vực", 
        Code = "ModProduct_Area", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 1)]
    public class ModProduct_AreaController : CPController
    {
        public ModProduct_AreaController()
        {
            //khoi tao Service
            DataService = ModProduct_AreaService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_AreaModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_AreaService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_AreaModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_AreaService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_AreaEntity();

                // khoi tao gia tri mac dinh khi insert
                item.CreateDate = DateTime.Now;
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

        /// <summary>
        ///  Lấy danh sách NHóm thuộc tính theo  Id Product Info: Not in hoặc In
        ///  CanTV      2012/09/24      Tạo mới
        /// </summary>
        /// <param name="objProductGroupEntity">objPropertiesGroups: Thuộc tính Id</param>
        /// <param name="WhereIn">WhereIn = True nếu lấy IN | False: Nếu lấy  Not in</param>
        /// <returns></returns>
        public List<ModProduct_PropertiesGroupsEntity> GetListPropertiesGroupByProductId(
            ModProduct_AreaEntity objProductGroupEntity,
            bool WhereIn,
            ref string sPropertiesGroupsId)
        {
            List<ModProduct_Area_PropretyGroupEntity> lstGroupsList = null;
            List<ModProduct_PropertiesGroupsEntity> lstReturnList = null;
            if (objProductGroupEntity.ID > 0)
            {
                if (WhereIn)
                {
                    lstGroupsList = ModProduct_Area_PropretyGroupService.Instance.CreateQuery()
                                    .Where(o => o.ProductAreaId == objProductGroupEntity.ID).ToList();
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
                foreach (ModProduct_Area_PropretyGroupEntity item in lstGroupsList)
                {
                    if (string.IsNullOrEmpty(sPropertiesGroupsId))
                        sPropertiesGroupsId = item.PropertiesGroupId.ToString();
                    else
                        sPropertiesGroupsId = sPropertiesGroupsId + "," + item.PropertiesGroupId.ToString();
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

        /// <summary>
        /// Cập nhật dữ liệu mới
        /// </summary>
        /// <param name="iProductAreaId"></param>
        /// <param name="ArrPropertiesGroupsIn"></param>
        private void UpdateProductGroups_PropertiesGroups(int iProductAreaId, string ArrPropertiesGroupsIn)
        {
            ArrPropertiesGroupsIn = ArrPropertiesGroupsIn.Trim(',');
            string sQueryDelete = "[ProductAreaId]=" + iProductAreaId;

            // Xóa dữ liệu cũ
            ModProduct_Area_PropretyGroupService.Instance.Delete(sQueryDelete);

            if (string.IsNullOrEmpty(ArrPropertiesGroupsIn))
                return;

            string[] lstPropertiesGroupsIn = ArrPropertiesGroupsIn.Split(',');
            if (lstPropertiesGroupsIn == null || lstPropertiesGroupsIn.Length <= 0)
                return;

            ModProduct_Area_PropretyGroupEntity objProductGroupsEntity = null;
            List<ModProduct_Area_PropretyGroupEntity> lstProductGroupsEntity = new List<ModProduct_Area_PropretyGroupEntity>();
            foreach (string item in lstPropertiesGroupsIn)
            {
                objProductGroupsEntity = new ModProduct_Area_PropretyGroupEntity();
                objProductGroupsEntity.ProductAreaId = iProductAreaId;
                objProductGroupsEntity.PropertiesGroupId = Convert.ToInt32(item);
                objProductGroupsEntity.CreateDate = DateTime.Now;
                lstProductGroupsEntity.Add(objProductGroupsEntity);
            }

            ModProduct_Area_PropretyGroupService.Instance.Save(lstProductGroupsEntity);
        }

        public void ActionSave(ModProduct_AreaModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModProduct_AreaModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_AreaModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_AreaEntity item = null;

        private bool ValidSave(ModProduct_AreaModel model)
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

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                 //neu khong nhap code -> tu sinh
                 if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModProduct_AreaService.Instance.Save(item);

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
    }

    public class ModProduct_AreaModel : DefaultModel
    {
        public string SearchText { get; set; }
        public string PropertiesGroupsInId { get; set; }
        public string PropertiesGroupsOutId { get; set; }
    }
}

