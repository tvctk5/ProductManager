using System;
using VSW.Lib.Global;

namespace VSW.Lib.MVC
{
    public class CPController : VSW.Core.MVC.Controller
    {
        public CPViewPage CPViewPage { get { return base.ViewPageBase as CPViewPage; } }
        public CPViewControl CPViewControl { get { return base.ViewControl as CPViewControl; } }

        protected dynamic DataService { get; set; }
        protected bool CheckPermissions { get; set; }

        protected string AutoSort(string sort)
        {
            return AutoSort(sort, "[ID] DESC");
        }

        protected string AutoSort(string sort, string orderDefault)
        {
            if (string.IsNullOrEmpty(sort))
                return orderDefault;

            string sortType = sort.Split('-')[0]
                                  .Replace("'", string.Empty)
                                  .Replace("-", string.Empty)
                                  .Replace(";", string.Empty);

            bool sortDesc = "desc" == sort.Split('-')[1].ToLower();

            return "[" + sortType + "] " + (sortDesc ? "DESC" : "ASC");
        }

        protected int GetState(int[] arrState)
        {
            int state = 0;

            for (int i = 0; arrState != null && i < arrState.Length; i++)
                if (arrState[i] >= 0) state ^= arrState[i];

            return state;
        }

        protected string GetNewsType(int[] arrType)
        {
            string type = string.Empty;

            for (int i = 0; arrType != null && i < arrType.Length; i++)
                type += arrType[i] + ",";

            if (!string.IsNullOrEmpty(type))
                type = type.Trim().Trim(',');

            if (!string.IsNullOrEmpty(type))
                type = "," + type + ",";

            if (string.IsNullOrEmpty(type))
                type = ",0,";

            return type;
        }

        protected int GetValueRadioButton(int[] arrValue)
        {
            int value = 0;

            for (int i = 0; arrValue != null && i < arrValue.Length; i++)
                if (arrValue[i] >= 0) value ^= arrValue[i];

            return value;
        }

        protected void SaveRedirect()
        {
            CPViewPage.SetMessage("Thông tin đã cập nhật.");
            CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx"));
        }

        protected void ApplyRedirect(int RecordID, int EntityID)
        {
            CPViewPage.SetMessage("Thông tin đã cập nhật.");

            if (RecordID > 0)
                CPViewPage.RefreshPage();
            else
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl + "/RecordID/" + EntityID);
        }

        protected void SaveNewRedirect(int RecordID, int EntityID)
        {
            CPViewPage.SetMessage("Thông tin đã cập nhật.");

            if (RecordID > 0)
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("/RecordID/" + EntityID, string.Empty));
            else
                CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl);
        }

        public virtual void ActionCancel()
        {
            CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx"));
        }

        public virtual void ActionCancel(int RecordID)
        {

        }

        public virtual void ActionConfig()
        {
            Global.Utils.Clear_Cache();

            //thong bao
            CPViewPage.SetMessage("Xóa cache thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionCopy(int id)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            dynamic item = DataService.GetByID(id);

            item.ID = 0;
            item.Name = item.Name + " - (Bản sao)";

            DataService.Save(item);

            //thong bao
            CPViewPage.SetMessage("Sao chép thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionPublish(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")",
                    "@Activity", 1);

            //thong bao
            CPViewPage.SetMessage("Đã duyệt thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionUnPublish(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")",
                    "@Activity", 0);

            //thong bao
            CPViewPage.SetMessage("Đã bỏ duyệt thành công.");
            CPViewPage.RefreshPage();
        }

        /// <summary>
        /// Delete - Modified by CanTV
        /// </summary>
        /// <param name="arrID"></param>
        public virtual void ActionDelete(int[] arrID)
        {
            string sMess = string.Empty;
            try
            {
                if (CheckPermissions && !CPViewPage.UserPermissions.Delete)
                {
                    //thong bao
                    CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                    CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                    return;
                }

                DataService.Delete("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")");

                // thành công
                sMess = "Đã xóa thành công.";
            }
            catch (Exception ex)
            {
                // Lỗi phát sinh
                sMess = "Xóa thất bại. Lỗi phát sinh trong quá trình xóa hoặc tồn tại ràng buộc dữ liệu:" + ex.Message;
            }

            //thong bao
            CPViewPage.SetMessage(sMess);
            CPViewPage.RefreshPage();
        }

        public virtual void ActionSaveOrder(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            for (int i = 0; i < arrID.Length - 1; i = i + 2)
            {
                DataService.Update("[ID]=" + arrID[i],
                        "@Order", arrID[i + 1]);
            }

            //thong bao
            CPViewPage.SetMessage("Đã sắp xếp thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionPublishGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@Activity", arrID[1]);

            //thong bao
            CPViewPage.SetMessage(arrID[1] == 0 ? "Đã bỏ duyệt thành công." : "Đã duyệt thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionViewInMenu(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@ViewInMenu", arrID[1]);

            //thong bao
            CPViewPage.SetMessage(arrID[1] == 0 ? "Đã cho hiển thị trên Menu thành công." : "Đã ẩn trên Menu thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionPublishActivateBasic(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@Activity", arrID[1]);

            //thong bao
            CPViewPage.SetMessage(arrID[1] == 0 ? "Đã chuyển trạng thái 'Không sử dụng' thành công." : "Đã chuyển trạng thái 'Sử dụng' thành công.");
            CPViewPage.RefreshPage();
        }

        public virtual void ActionViewInSiteMap(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@ViewInSiteMap", arrID[1]);

            //thong bao
            CPViewPage.SetMessage(arrID[1] == 0 ? "Đã cho hiển thị trên Sitemap thành công." : "Đã ẩn trên Sitemap thành công.");
            CPViewPage.RefreshPage();
        }

        /// <summary>
        /// Chuyển đổi trạng thái và hiển thị Mess tùy biến
        /// arrID[0] : Id
        /// arrID[1] : Status
        /// arrID[2] : Mess  if set to False
        /// arrID[3] : Mess if set to True
        /// </summary>
        /// <param name="arrID">Arr list Parameter</param>
        public virtual void ActionPublishModified(string[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + Convert.ToInt32(arrID[0]),
                        "@Activity", Convert.ToInt32(arrID[1]));

            //thong bao
            CPViewPage.SetMessage(arrID[1] == "0" ? arrID[2] : arrID[3]);
            CPViewPage.RefreshPage();
        }

    }

    public class DefaultModel
    {
        private int _PageIndex = 0;
        public int PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value - 1; }
        }

        private int _PageSize = 20;
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }

        public int TotalRecord { get; set; }

        public string Sort { get; set; }

        public int RecordID { get; set; }
    }
}
