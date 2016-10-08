using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Khảo sát - chi tiết",
        Description = "Quản lý - Khảo sát - chi tiết",
        Code = "ModProduct_SurveyGroup_Detail",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 4)]
    public class ModProduct_SurveyGroup_DetailController : CPController
    {
        public ModProduct_SurveyGroup_DetailController()
        {
            //khoi tao Service
            DataService = ModProduct_SurveyGroup_DetailService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_SurveyGroup_DetailModel model)
        {
            // sap xep tu dong
            string orderBy = string.Empty;
            if (model.Sort != null)
                orderBy = AutoSort(model.Sort);
            else
                orderBy = "[SurveyGroupId],[Order] ASC";

            // tao danh sach
            var dbQuery = ModProduct_SurveyGroup_DetailService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_SurveyGroup_DetailModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_SurveyGroup_DetailService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update 
            }
            else
            {
                item = new ModProduct_SurveyGroup_DetailEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Order = GetMaxOrder(model);
                item.Activity = CPViewPage.UserPermissions.Approve;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_SurveyGroup_DetailModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_SurveyGroup_DetailModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_SurveyGroup_DetailModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModProduct_SurveyGroup_DetailEntity item = null;

        private bool ValidSave(ModProduct_SurveyGroup_DetailModel model)
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

                if (item.Vote < 0)
                    item.Vote = 0;

                try
                {
                    //save
                    ModProduct_SurveyGroup_DetailService.Instance.Save(item);
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

        private int GetMaxOrder(ModProduct_SurveyGroup_DetailModel model)
        {
            return ModProduct_SurveyGroup_DetailService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }
        #endregion
    }

    public class ModProduct_SurveyGroup_DetailModel : DefaultModel
    {
        public string SearchText { get; set; }

        /// <summary>
        /// Lấy tên nhóm
        /// </summary>
        /// <param name="iGroupId"></param>
        /// <param name="lstSurveyGroup"></param>
        /// <returns></returns>
        public static string GetGroupName(int iGroupId, List<ModProduct_SurveyGroupEntity> lstSurveyGroup)
        {
            if (lstSurveyGroup == null || lstSurveyGroup.Count <= 0)
                return string.Empty;

            var Check = lstSurveyGroup.Where(o => o.ID == iGroupId).FirstOrDefault();
            if (Check == null)
                return string.Empty;

            return (Check.Name + "<span class='SurveyGroup-infoDateTime'> ( Ngày bắt đầu: " + (ConvertTool.CheckDateIsNull(Check.StartDate) ? "Không xác định" : Check.StartDate.ToString("dd/MM/yyyy HH:mm"))
                + " - Ngày kết thúc: " + (ConvertTool.CheckDateIsNull(Check.FinishDate) ? "Không xác định" : Check.FinishDate.ToString("dd/MM/yyyy HH:mm")) + " )</span>"
                );
        }
    }
}

