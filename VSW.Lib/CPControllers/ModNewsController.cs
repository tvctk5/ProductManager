using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;


namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Bài viết", 
        Description = "Quản lý  - Bài viết", 
        Code = "ModNews", 
        Access = 31, 
        Order = 10, 
        ShowInMenu = true, 
        CssClass = "icon-16-article",
        MenuGroupId = 2)]
    public class ModNewsController : CPController
    {
        public ModNewsController()
        {
            //khoi tao Service
            DataService = ModNewsService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModNewsModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModNewsService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Where(model.State > 0, o => (o.State & model.State) == model.State)
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("News", model.MenuID, model.LangID))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModNewsModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModNewsService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModNewsEntity();

                // khoi tao gia tri mac dinh khi insert
                item.MenuID = model.MenuID;
                item.Published = DateTime.Now;
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.Order = GetMaxOrder(model);
                item.CountViewed = 0;
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModNewsModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModNewsModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModNewsModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private ModNewsEntity item = null;

        private bool ValidSave(ModNewsModel model)
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
                CPViewPage.Message.ListMessage.Add("Nhập tiêu đề.");

            //kiem tra chuyen muc
            if (item.MenuID < 1)
                CPViewPage.Message.ListMessage.Add("Chọn chuyên mục.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                //cap nhat state
                item.State = GetState(model.ArrState);

                // Cập nhật loại tin
                item.Type = GetNewsType(model.ArrType);

                // Cập nhật loại slide
                item.SlideType = GetValueRadioButton(model.ArrSlideType);

                try
                {
                    //save
                    ModNewsService.Instance.Save(item);

                    //update tag
                    ModTagService.Instance.UpdateTag(item.ID, item.Tags);
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

        private int GetMaxOrder(ModNewsModel model)
        {
            return ModNewsService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class ModNewsModel : DefaultModel
    {
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

        public int MenuID { get; set; }
        public int State { get; set; }
        public string SearchText { get; set; }

        public int[] ArrState { get; set; }
        public int[] ArrType { get; set; }
        public int[] ArrSlideType { get; set; }
    }
}
