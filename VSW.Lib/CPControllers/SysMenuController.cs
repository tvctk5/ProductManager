using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysMenuController : CPController
    {
        public SysMenuController()
        {
            //khoi tao Service
            DataService = WebMenuService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysMenuModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort, "[Order]");

            // tao danh sach
            var dbQuery = WebMenuService.Instance.CreateQuery()
                                .Where(o => o.ParentID == model.ParentID && o.LangID == model.LangID)
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(SysMenuModel model)
        {
            if (model.RecordID > 0)
            {
                item = WebMenuService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update
            }
            else
            {
                item = new WebMenuEntity();

                // khoi tao gia tri mac dinh khi insert
                item.ParentID = model.ParentID;
                item.Activity = true;
                item.LangID = model.LangID;
                item.Order = GetMaxOrder(model);

                if (model.ParentID > 0)
                    item.Type = WebMenuService.Instance.GetByID(model.ParentID).Type;
                else
                    item.Type = "News";
            }

            model.ParentID_Save = item.ParentID;

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSave(SysMenuModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(SysMenuModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(SysMenuModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public override void ActionDelete(int[] arrID)
        {
            List<int> list = new List<int>();
            GetMenuIDChildForDelete(ref list, arrID);

            if (list != null && list.Count > 0)
            {
                string sWhere = "[ID] IN (" + VSW.Core.Global.Array.ToString(list.ToArray()) + ")";

                //xoa menu
                WebMenuService.Instance.Delete(sWhere);

                sWhere = "[MenuID] IN (" + VSW.Core.Global.Array.ToString(list.ToArray()) + ")";

                //xoa news
                ModNewsService.Instance.Delete(sWhere);
                //xoa adv
                ModAdvService.Instance.Delete(sWhere);
            }

            //thong bao
            CPViewPage.SetMessage("Đã xóa thành công.");
            CPViewPage.RefreshPage();
        }

        #region private func

        private WebMenuEntity item = null;

        private bool ValidSave(SysMenuModel model)
        {
            TryUpdateModel(item);

            ViewBag.Data = item;
            ViewBag.Model = model;
            WebMenuEntity objWebMenuEntity_Parent = null;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra ten 
            if (item.Name.Trim() == string.Empty)
                CPViewPage.Message.ListMessage.Add("Nhập tên chuyên mục.");

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                // neu code khong duoc nhap -> tu dong tao ra khi them moi
                if (item.Code == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                if (model.ModProductAreaId <= 0)
                    item.ProductAreaId = null;
                else
                    item.ProductAreaId = model.ModProductAreaId;

                try
                {
                    //neu di chuyen thi cap nhat lai Type va Order
                    if (model.RecordID > 0 && item.ParentID != model.ParentID_Save) // !=model.ParentID
                    {
                        // cap nhat Type
                        if (item.ParentID != 0)
                        {
                            objWebMenuEntity_Parent = WebMenuService.Instance.GetByID(item.ParentID);

                            // Nếu thay đổi Cha ==> Cập nhật lại mã phân cấp
                            if (item.ParentCode != objWebMenuEntity_Parent.CurrentCode)
                            {
                                string sMaPhanCapCu = item.CurrentCode;
                                string sMaPhanCapMoi = LayMaPhanCap(objWebMenuEntity_Parent.CurrentCode);

                                // Lấy cha mới
                                item.ParentCode = objWebMenuEntity_Parent.CurrentCode;

                                // Lấy mã phân cấp mới
                                item.CurrentCode = sMaPhanCapMoi;

                                // Cập nhật các mã phân cấp con (Nếu có)
                                CapNhatMaPhanCap(sMaPhanCapCu, sMaPhanCapMoi);
                            }

                            item.Type = objWebMenuEntity_Parent.Type;
                        }

                        // Bắt buộc cha !=0 nên sẽ ko có else

                        //cap nhat Order
                        item.Order = GetMaxOrder(model);
                    }
                    else
                        // Tạo mới
                        if (model.RecordID <= 0)
                        {
                            // Nếu Thêm mới ==> Sinh mã phân cấp
                            if (item.ParentID <= 0)
                            {
                                item.CurrentCode = LayMaPhanCap("0");
                                item.ParentCode = "0";
                            }
                            else
                            {
                                objWebMenuEntity_Parent = WebMenuService.Instance.GetByID(item.ParentID);
                                item.CurrentCode = LayMaPhanCap(objWebMenuEntity_Parent.CurrentCode);
                                item.ParentCode = objWebMenuEntity_Parent.CurrentCode;
                            }
                        }

                    //save
                    WebMenuService.Instance.Save(item);


                    //neu di chuyen thi cap nhat lai Type cua chuyen muc con
                    // Comment by CanTV
                    //if (model.RecordID > 0 && item.ParentID != model.ParentID && item.ParentID != 0)
                    if (model.RecordID > 0 && item.ParentID != model.ParentID_Save && model.ParentID_Save != 0)
                    {
                        // lay danh sach chuyen muc con
                        List<int> list = new List<int>();
                        GetMenuIDChild(ref list, model.RecordID);

                        //cap nhat Type cho danh sach chuyen muc con
                        if (list.Count > 1)
                            WebMenuService.Instance.Update("[ID] IN (" + VSW.Core.Global.Array.ToString(list.ToArray()) + ")",
                                "@Type", WebMenuService.Instance.GetByID(item.ParentID).Type);
                    }
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

        private int GetMaxOrder(SysMenuModel model)
        {
            return WebMenuService.Instance.CreateQuery()
                    .Where(o => o.LangID == model.LangID && o.ParentID == model.ParentID)
                    .Max(o => o.Order)
                    .ToValue().ToInt(0) + 1;
        }

        private void GetMenuIDChildForDelete(ref List<int> list, int[] ArrID)
        {
            for (int i = 0; ArrID != null && i < ArrID.Length; i++)
            {
                GetMenuIDChild(ref list, ArrID[i]);
            }
        }

        private void GetMenuIDChild(ref List<int> list, int menu_id)
        {
            list.Add(menu_id);

            List<WebMenuEntity> _ListMenu = WebMenuService.Instance.CreateQuery()
                                                .Where(o => o.ParentID == menu_id)
                                                .ToList();

            for (int i = 0; _ListMenu != null && i < _ListMenu.Count; i++)
            {
                GetMenuIDChild(ref list, _ListMenu[i].ID);
            }
        }

        #endregion

        #region Sinh hoặc cập nhật mã phân cấp

        /// <summary>
        /// Sinh mã phân cấp dựa vào mã phân cấp cha
        /// CanTV           20/02/2014          Tạo mới
        /// </summary>
        /// <param name="MaPhanCapCha"></param>
        /// <returns></returns>
        private string LayMaPhanCap(string MaPhanCapCha)
        {
            WebMenuEntity objWebMenu = null;
            int iMaPhanCap = 0;
            string MaPhanCap_Start = string.Empty;
            string MaPhanCap = string.Empty;

            // Là chứng khoán cha
            if (string.IsNullOrEmpty(MaPhanCapCha) || MaPhanCapCha.Equals("0"))
            {
                MaPhanCapCha = "0";

                objWebMenu = WebMenuService.Instance.CreateQuery().Where(o => o.ParentCode == "0" || o.ParentCode == null).OrderByDesc(p => p.CurrentCode).ToSingle();
                if (objWebMenu == null)
                    return "001";

                if (string.IsNullOrEmpty(objWebMenu.CurrentCode) || objWebMenu.CurrentCode.Length < 3)
                    return "001";

                MaPhanCap_Start = objWebMenu.CurrentCode.Substring(0, objWebMenu.CurrentCode.Length - 3);
                iMaPhanCap = Convert.ToInt32(objWebMenu.CurrentCode.Substring(objWebMenu.CurrentCode.Length - 3));
                iMaPhanCap = iMaPhanCap + 1;

                MaPhanCap = MaPhanCap_Start + TangSoMaPhanCap(iMaPhanCap);
                return MaPhanCap;
            }
            // Nếu tạo mới mã phân cấp cho con
            else
            {
                // Lấy con gần nhất
                objWebMenu = WebMenuService.Instance.CreateQuery().Where(p => p.ParentCode == (MaPhanCapCha))
                                                                    .OrderByDesc(p => p.CurrentCode).ToSingle();
                if (objWebMenu == null)
                    return MaPhanCapCha + "001";

                if (string.IsNullOrEmpty(objWebMenu.CurrentCode) || objWebMenu.CurrentCode.Length < 3)
                    return MaPhanCapCha + "001";

                MaPhanCap_Start = objWebMenu.CurrentCode.Substring(0, objWebMenu.CurrentCode.Length - 3);
                iMaPhanCap = Convert.ToInt32(objWebMenu.CurrentCode.Substring(objWebMenu.CurrentCode.Length - 3));
                iMaPhanCap = iMaPhanCap + 1;

                MaPhanCap = MaPhanCap_Start + TangSoMaPhanCap(iMaPhanCap);
                return MaPhanCap;
            }
        }

        /// <summary>
        /// Chèn số 0 phía trước thành một chuỗi có 3 ký tự: ví dụ: 001, 002, ...
        /// </summary>
        /// <param name="MaPhanCap"></param>
        /// <returns></returns>
        private string TangSoMaPhanCap(int MaPhanCap)
        {
            string sMaPhanCap = MaPhanCap.ToString();
            int iLength = 3 - sMaPhanCap.Length;

            // Nếu là 3 ký tự rồi thì thôi, không làm j nữa
            if (iLength == 0)
                return sMaPhanCap;

            for (int i = 0; i < iLength; i++)
            {
                sMaPhanCap = "0" + sMaPhanCap;
            }

            // Trả ra giá trị
            return sMaPhanCap;
        }

        /// <summary>
        /// Cập nhật các mã phân cấp con, nếu có
        /// </summary>
        /// <param name="MaPhanCapChaCu"></param>
        /// <param name="MaPhanCapChaMoi"></param>
        /// <returns></returns>
        private bool CapNhatMaPhanCap(string MaPhanCapChaCu, string MaPhanCapChaMoi)
        {
            try
            {
                string sMaChaCu = string.Empty;
                int iSTTMaPhanCap = 1;

                // Lấy tất cả các con nếu có
                List<WebMenuEntity> lstWebMenuEntity_Sub = WebMenuService.Instance.CreateQuery()
                                                            .Where(p => p.ParentCode == MaPhanCapChaCu).OrderByAsc(o => o.CurrentCode)
                                                            .ToList();
                if (lstWebMenuEntity_Sub == null || lstWebMenuEntity_Sub.Count <= 0)
                    return true;

                foreach (var itemWebMenuEntity in lstWebMenuEntity_Sub)
                {
                    // Lưu lại mã cũ
                    sMaChaCu = itemWebMenuEntity.CurrentCode;

                    itemWebMenuEntity.ParentCode = MaPhanCapChaMoi;
                    itemWebMenuEntity.CurrentCode = MaPhanCapChaMoi + TangSoMaPhanCap(iSTTMaPhanCap);

                    // Cập nhật lại các Con nếu có
                    CapNhatMaPhanCap(sMaChaCu, itemWebMenuEntity.CurrentCode);

                    iSTTMaPhanCap++;
                }

                // Lưu lại thay đổi
                WebMenuService.Instance.Save(lstWebMenuEntity_Sub);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }

    public class SysMenuModel : DefaultModel
    {
        public int ParentID { get; set; }

        public int ParentID_Save { get; set; }

        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

        public int ModProductAreaId { get; set; }
    }
}
