using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysTemplateController : CPController
    {
        public SysTemplateController()
        {
            //khoi tao Service
            DataService = SysTemplateService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysTemplateModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Order]");

            // tao danh sach
            var dbQuery = SysTemplateService.Instance.CreateQuery()
                                .Where(o => o.LangID == model.LangID)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(SysTemplateModel model)
        {
            if (model.RecordID > 0)
            {
                item = SysTemplateService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new SysTemplateEntity();

                // khoi tao gia tri mac dinh khi insert
                item.LangID = model.LangID;
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(SysTemplateModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(SysTemplateModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(SysTemplateModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        #region private func

        private SysTemplateEntity item = null;

        private bool ValidSave(SysTemplateModel model)
        {
            TryUpdateModel(item);

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên mẫu giao diện.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                try
                {
                    //save
                    SysTemplateService.Instance.Save(item);

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

        private int GetMaxOrder(SysTemplateModel model)
        {
            return SysTemplateService.Instance.CreateQuery()
                    .Where(o => o.LangID == model.LangID)
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        private void BuildFileCssJs(SysTemplateEntity item)
        {
            if (item == null || item.ID <= 0)
                return;

            if (string.IsNullOrEmpty(item.CssContent) && string.IsNullOrEmpty(item.JsContent))
                return;

            if (!string.IsNullOrEmpty(item.CssContent))
            {
                string CssPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/modules/css/t" + item.ID + ".css");

                VSW.Lib.Global.File.WriteTextUnicode(CssPath, "/*" + item.Name + "*/ \r\n" + item.CssContent, true);
            }

            if (!string.IsNullOrEmpty(item.JsContent))
            {
                string JsPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/modules/js/t" + item.ID + ".js");

                VSW.Lib.Global.File.WriteTextUnicode(JsPath, "// " + item.Name + "\r\n" + item.JsContent, true);
            }
        }
        #endregion
    }

    public class SysTemplateModel : DefaultModel
    {
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }
    }
}
