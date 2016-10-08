using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;
using System.Linq;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Doanh thu - kỳ - đại lý",
        Description = "Quản lý - Doanh thu - kỳ - đại lý",
        Code = "ModDT_Ky_DaiLy",
        Access = 31,
        Order = 202,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 5)]
    public class ModDT_Ky_DaiLyController : CPController
    {
        public ModDT_Ky_DaiLyController()
        {
            //khoi tao Service
            DataService = ModDT_Ky_DaiLyService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModDT_Ky_DaiLyModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            if (string.IsNullOrEmpty(orderBy))
                orderBy = "ID DESC";

            if (model.KyId <= 0)
            {
                ModDT_KyEntity objModDT_KyEntity = ModDT_KyService.Instance.CreateQuery().OrderByDesc(o => o.ID).Take(1).ToSingle();
                if (objModDT_KyEntity != null)
                {
                    model.KyId = objModDT_KyEntity.ID;
                    model.DaChotKy = objModDT_KyEntity.Activity ? (int)EnumValue.Activity.FALSE : (int)EnumValue.Activity.TRUE;
                }
            }

            // tao danh sach
            var dbQuery = ModDT_Ky_DaiLyService.Instance.CreateQuery()
                .Where(o => o.ModDtKyId == (model.KyId))
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModDT_Ky_DaiLyModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModDT_Ky_DaiLyService.Instance.GetByID(model.RecordID);

                List<ModDT_Ky_DaiLyEntity> lstListData = new List<ModDT_Ky_DaiLyEntity>();

                // Lấy danh sách các đại lý con bên trong đại lý này
                var dbQuery = ModDT_Ky_DaiLyService.Instance.CreateQuery()
                                .Where(o => o.ModDtKyId == item.ModDtKyId &&
                                 (o.ModProductAgentId == item.ModProductAgentId || o.ModProductAgentParentId == item.ModProductAgentId));

                List<ModDT_Ky_DaiLyEntity> lstData01 = dbQuery.ToList();
                if (lstData01 == null || lstData01.Count <= 0)
                    ViewBag.ListData = lstListData;
                else
                {
                    var dbQueryAllKy = ModDT_Ky_DaiLyService.Instance.CreateQuery()
                                .Where(o => o.ModDtKyId == item.ModDtKyId);

                    List<ModDT_Ky_DaiLyEntity> lstData02_all = dbQueryAllKy.ToList();

                    foreach (var itemCheck in lstData01)
                    {
                        // Thêm vào danh sách
                        lstListData.Add(itemCheck);

                        // Là Item hiện tại
                        if (itemCheck.ID == item.ID)
                            continue;

                        // Duyệt tìm những phần tử con nếu có
                        FindDataChild(lstData02_all, itemCheck, ref lstListData);
                    }
                }

                // khoi tao gia tri mac dinh khi update
                ViewBag.ListData = lstListData;

                // Lấy thông tin kỳ
                ModDT_KyEntity objModDT_KyEntity = ModDT_KyService.Instance.GetByID(item.ModDtKyId);
                model.DaChotKy = objModDT_KyEntity.Activity ? (int)EnumValue.Activity.FALSE : (int)EnumValue.Activity.TRUE;
            }
            else
            {
                item = new ModDT_Ky_DaiLyEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                ViewBag.ListData = new List<ModDT_Ky_DaiLyEntity>();
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        /// <summary>
        /// Lấy những cấp con nếu có
        /// </summary>
        /// <param name="lstDataAll"></param>
        /// <param name="itemCheck"></param>
        /// <param name="lstDataResult"></param>
        private void FindDataChild(List<ModDT_Ky_DaiLyEntity> lstDataAll, ModDT_Ky_DaiLyEntity itemCheck, ref List<ModDT_Ky_DaiLyEntity> lstDataResult)
        {
            if (itemCheck == null || (lstDataAll == null || lstDataAll.Count <= 0) || lstDataResult == null)
                return;

            List<ModDT_Ky_DaiLyEntity> lstFindChild = lstDataAll.Where(o => o.ModProductAgentParentId == itemCheck.ModProductAgentId).ToList();
            if (lstFindChild == null || lstFindChild.Count <= 0)
                return;

            foreach (var itemChild in lstFindChild)
            {
                lstDataResult.Add(itemChild);

                // Lấy tiếp những con khác nếu có
                FindDataChild(lstDataAll, itemChild, ref lstDataResult);
            }
        }

        public void ActionSave(ModDT_Ky_DaiLyModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModDT_Ky_DaiLyModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModDT_Ky_DaiLyModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModDT_Ky_DaiLyEntity item = null;

        private bool ValidSave(ModDT_Ky_DaiLyModel model)
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
                    ModDT_Ky_DaiLyService.Instance.Save(item);
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

    public class ModDT_Ky_DaiLyModel : DefaultModel
    {
        public string SearchText { get; set; }

        public int KyId { get; set; }
        public int DaChotKy { get; set; }
    }
}

