using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Liên hệ", 
        Description = "Quản lý - Liên hệ", 
        Code = "ModFeedback", 
        Access = 9,
        Order = 9900, 
        ShowInMenu = true,
        CssClass = "icon-16-massmail",
        MenuGroupId = 4)]
    public class ModFeedbackController : CPController
    {
        public ModFeedbackController()
        {
            //khoi tao Service
            DataService = ModFeedbackService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModFeedbackModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModFeedbackService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModFeedbackModel model)
        {
            if (model.RecordID > 0)
            {
                item = ModFeedbackService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new ModFeedbackEntity();

                // khoi tao gia tri mac dinh khi insert
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        #region private func

        private ModFeedbackEntity item = null;

        #endregion
    }

    public class ModFeedbackModel : DefaultModel
    {
       
    }
}
