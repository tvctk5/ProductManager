using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Product_ filter values",
        Description = "Quản lý - Product_ filter values",
        Code = "ModProduct_FilterValues",
        Access = 31,
        Order = 200,
        ShowInMenu = false,
        CssClass = "icon-16-component")]
    public class ModProduct_FilterValuesController : CPController
    {
        public ModProduct_FilterValuesController()
        {
            //khoi tao Service
            DataService = ModProduct_FilterValuesService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_FilterValuesModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_FilterValuesService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_FilterValuesModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModProduct_FilterValuesService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModProduct_FilterValuesEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(ModProduct_FilterValuesModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_FilterValuesModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_FilterValuesModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void getString(ref string Url)
        {
            //CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add", "Index")) + "";
            Url = "tb_show('', '" + CPViewPage.Request.RawUrl.Replace("ModProduct_FilterValues", "FormProduct_FilterValues").Replace("Add", "Index") + "?TB_iframe=true;height=500;width=700;', '');";
        }

        #region private func

        private ModProduct_FilterValuesEntity item = null;

        private bool ValidSave(ModProduct_FilterValuesModel model)
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

                try
                {
                    //save
                    ModProduct_FilterValuesService.Instance.Save(item);
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

    public class ModProduct_FilterValuesModel : DefaultModel
    {
        public string Url { get; set; }
    }
}

