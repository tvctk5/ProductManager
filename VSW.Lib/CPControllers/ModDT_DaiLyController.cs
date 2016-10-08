using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Doanh thu - đại lý",
        Description = "Quản lý - Doanh thu - đại lý", 
        Code = "ModDT_DaiLy", 
        Access = 31, 
        Order = 200, 
        ShowInMenu = true,
        CssClass = "icon-16-component",
        MenuGroupId = 5)]
    public class ModDT_DaiLyController : CPController
    {
        public ModDT_DaiLyController()
        {
            //khoi tao Service
            DataService = ModDT_DaiLyService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModDT_DaiLyModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModDT_DaiLyService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModDT_DaiLyModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModDT_DaiLyService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModDT_DaiLyEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModDT_DaiLyModel model)
        {
            if (ValidSave(model))
               SaveRedirect();
        }

        public void ActionApply(ModDT_DaiLyModel model)
        {
            if (ValidSave(model))
               ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModDT_DaiLyModel model)
        {
            if (ValidSave(model))
               SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModDT_DaiLyEntity item = null;

        private bool ValidSave(ModDT_DaiLyModel model)
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

            //if(item.ModProductAgentId<=0)
            //{
            //    CPViewPage.Message.ListMessage.Add("Yêu cầu chọn Đại lý được giới thiệu");
            //    return false;
            //}

            // Lấy Thông tin đại lý được giới thiệu.
            ModProduct_AgentEntity objModProduct_AgentEntity = ModProduct_AgentService.Instance.CreateQuery().Where(o => o.ID == item.ModProductAgentId).ToSingle();
            if (objModProduct_AgentEntity != null)
            {
                item.Code = objModProduct_AgentEntity.Code;
                item.Name = objModProduct_AgentEntity.Name;
            }

            // Lấy thông tin loại đại lý
            if (item.ModDTLoaiDaiLyId>0)
            {
                ModLoai_DaiLyEntity objLoaiDaiLy = ModLoai_DaiLyService.Instance.CreateQuery().Where(o => o.ID == item.ModDTLoaiDaiLyId).ToSingle();
                if (objLoaiDaiLy != null)
                {
                    item.ModLoaiDaiLyCode = objLoaiDaiLy.Code;
                    item.ModLoaiDaiLyName = objLoaiDaiLy.Name;
                    item.ModLoaiDaiLyType = objLoaiDaiLy.Type;
                    item.ModLoaiDaiLyValue = objLoaiDaiLy.Value;
                }
            }

            //kiem tra ten 
            if (item.Name == null || item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Yêu cầu nhập tên Đại lý");


            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                 //neu khong nhap code -> tu sinh
                if (item.Code == null && item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                try
                {
                    //save
                    ModDT_DaiLyService.Instance.Save(item);
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

    public class ModDT_DaiLyModel : DefaultModel
    {
        public string SearchText { get; set; }
    }
}

