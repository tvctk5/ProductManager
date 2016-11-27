using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysPageController : CPController
    {
        public SysPageController()
        {
            //khoi tao Service
            DataService = SysPageService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysPageModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Order]");

            // tao danh sach
            var dbQuery = SysPageService.Instance.CreateQuery()
                                .Where(o => o.ParentID == model.ParentID && o.LangID == model.LangID)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(SysPageModel model)
        {
            if (model.RecordID > 0)
            {
                item = SysPageService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new SysPageEntity();

                // khoi tao gia tri mac dinh khi insert
                item.ParentID = model.ParentID;
                item.Activity = true;
                item.LangID = model.LangID;
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(SysPageModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(SysPageModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(SysPageModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public override void ActionDelete(int[] arrID)
        {
            List<int> list = new List<int>();
            GetPageIDChildForDelete(ref list, arrID);

            if (list != null && list.Count > 0)
            {
                string sWhere = "[ID] IN (" + VSW.Core.Global.Array.ToString(list.ToArray()) + ")";

                //xoa page
                SysPageService.Instance.Delete(sWhere);
            }

            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private SysPageEntity item = null;

        private bool ValidSave(SysPageModel model)
        {
            TryUpdateModel(item);

            if (item.ModuleCode == "0")
                item.ModuleCode = string.Empty;

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên trang.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                // neu code khong duoc nhap -> tu dong tao ra khi them moi
                if (item.Code == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                //neu di chuyen thi cap nhat lai Order
                if (model.RecordID > 0 && item.ParentID != model.ParentID)
                    item.Order = GetMaxOrder(model);

                try
                {
                    //save
                    SysPageService.Instance.Save(item);

                    // save file
                    BuildFileCssJs(item);
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

        private int GetMaxOrder(SysPageModel model)
        {
            return SysPageService.Instance.CreateQuery()
                    .Where(o => o.LangID == model.LangID && o.ParentID == model.ParentID)
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        private void GetPageIDChildForDelete(ref List<int> list, int[] ArrID)
        {
            for (int i = 0; ArrID != null && i < ArrID.Length; i++)
            {
                GetPageIDChild(ref list, ArrID[i]);
            }
        }

        private void GetPageIDChild(ref List<int> list, int page_id)
        {
            list.Add(page_id);

            List<SysPageEntity> _ListPage = SysPageService.Instance.CreateQuery()
                                                .Where(o => o.ParentID == page_id)
                                                .ToList();

            for (int i = 0; _ListPage != null && i < _ListPage.Count; i++)
            {
                GetPageIDChild(ref list, _ListPage[i].ID);
            }
        }

        private void BuildFileCssJs(SysPageEntity item)
        {
            if (item == null || item.ID <= 0)
                return;

            if (string.IsNullOrEmpty(item.CssContent) && string.IsNullOrEmpty(item.JsContent))
                return;

            if (!string.IsNullOrEmpty(item.CssContent))
            {
                string CssPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/modules/css/p" + item.ID + ".css");

                VSW.Lib.Global.File.WriteTextUnicode(CssPath, "/*" + item.Code + "*/ \r\n" + item.CssContent, true);
            }

            if (!string.IsNullOrEmpty(item.JsContent))
            {
                string JsPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/modules/js/p" + item.ID + ".js");

                VSW.Lib.Global.File.WriteTextUnicode(JsPath, "// " + item.Code + "\r\n" + item.JsContent, true);
            }
        }
        #endregion
    }

    public class SysPageModel : DefaultModel
    {
        public int ParentID { get; set; }

        private int _LangID = 1;
        public int LangID 
        {
            get { return _LangID; }
            set { _LangID = value; } 
        }
    }
}
