using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysRoleController : CPController
    {
        public SysRoleController()
        {
            //khoi tao Service
            DataService = CPRoleService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysRoleModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Order]");

            // tao danh sach
            var dbQuery = CPRoleService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(SysRoleModel model)
        {
            if (model.RecordID > 0)
            {
                item = CPRoleService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new CPRoleEntity();

                // khoi tao gia tri mac dinh khi insert
                item.Order = GetMaxOrder(model);
            }

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(SysRoleModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(SysRoleModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(SysRoleModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public override void ActionDelete(int[] arrID)
        {
            for (int i = 0; i < arrID.Length; i++)
            {
                int id = arrID[i];

                CPRoleEntity _Item = CPRoleService.Instance.GetByID(id);

                if (_Item.Lock)
                    continue;

                //thuc thi
                CPUserRoleService.Instance.Delete(o => o.RoleID == id);
                CPAccessService.Instance.Delete(o => o.RoleID == id);
                CPRoleService.Instance.Delete(id);
            }

            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private CPRoleEntity item = null;

        private bool ValidSave(SysRoleModel model)
        {
            TryUpdateModel(item);

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên nhóm người sử dụng.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                try
                {
                    //save
                    CPRoleService.Instance.Save(item);

                    UpdateRoleModule(model);

                }
                catch (Exception ex)
                {
                    Global.Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }

                Global.Utils.Clear_Cache();

                return true;
            }

            return false;
        }

        private void UpdateRoleModule(SysRoleModel model)
        {
            CPAccessService.Instance.Delete(o => o.Type == "CP.MODULE" && o.RoleID == item.ID);

            for (int i = -1; i < VSW.Lib.Web.Application.CPModules.Count; i++)
            {
                string moduleCode = "SysAdministrator";

                if (i > -1) 
                    moduleCode = VSW.Lib.Web.Application.CPModules[i].Code;

                int _Access = 0;

                if (model.ArrApprove != null && Array.IndexOf(model.ArrApprove, moduleCode) > -1)
                    _Access |= 16;
                if (model.ArrDelete != null && Array.IndexOf(model.ArrDelete, moduleCode) > -1)
                    _Access |= 8;
                if (model.ArrEdit != null && Array.IndexOf(model.ArrEdit, moduleCode) > -1)
                    _Access |= 4;
                if (model.ArrAdd != null && Array.IndexOf(model.ArrAdd, moduleCode) > -1)
                    _Access |= 2;
                if (model.ArrView != null && Array.IndexOf(model.ArrView, moduleCode) > -1)
                    _Access |= 1;

                if (_Access > 0)
                {
                    if ((_Access & 1) != 1)
                        _Access |= 1;

                    CPAccessEntity _AccessEntity = new CPAccessEntity();
                    _AccessEntity.RefCode = moduleCode;
                    _AccessEntity.RoleID = item.ID;
                    _AccessEntity.Value = _Access;
                    _AccessEntity.Type = "CP.MODULE";
                    CPAccessService.Instance.Save(_AccessEntity);
                }
            }
        }

        private int GetMaxOrder(SysRoleModel model)
        {
            return CPRoleService.Instance.CreateQuery()
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        #endregion
    }

    public class SysRoleModel : DefaultModel
    {
        public string[] ArrApprove { get; set; }
        public string[] ArrDelete { get; set; }
        public string[] ArrEdit { get; set; }
        public string[] ArrAdd { get; set; }
        public string[] ArrView { get; set; }
    }
}
