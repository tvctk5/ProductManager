using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Website.CP.Tools.Ajax.Common.ModMenu_Dynamic
{
    partial class PostData : System.Web.UI.Page
    {
        Tools.Common objCommon = new Tools.Common();
        DataOutput objDataOutput = new DataOutput();
        string sMessError = string.Empty;

        #region Tập các giá trị control được post lên để sử dụng
        int RecordID = 0;
        string Code = string.Empty;
        #endregion

        #region Lấy các giá trị từ các control post
        /// <summary>
        /// Lấy các giá trị từ các control post
        /// </summary>
        private void GetValueControl()
        {
            RecordID = objCommon.ConvertToInt32(Request["RecordID"]);
            Code = objCommon.ConvertToString(Request["Code"]);
        }
        #endregion

        #region Methord Return Object
        /// <summary>
        /// Return Object
        /// </summary>
        /// <param name="objDataReturn"></param>
        private void RenderMessage(DataOutput objDataReturn)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonMessage = oSerializer.Serialize(objDataReturn);
            this.Page.Response.Clear();
            this.Page.Response.ContentType = "application/json";
            this.Page.Response.Write(strJsonMessage);
            this.Page.Response.End();
        }
        #endregion

        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string sType = Request.QueryString["Type"];

            // Lấy giá trị các control post lên
            GetValueControl();

            // Tùy từng trường hợp thao tác dữ liệu khác khau
            switch (sType)
            {
                case "1": GetMenuCha(); break;
                case "3": break;
                default:
                    break;
            }

            // Trả lại dữ liệu
            RenderMessage(objDataOutput);
        }

        /// <summary>
        /// Lấy danh sách tỉnh thành gửi về cho Client
        /// </summary>
        private void GetMenuCha()
        {
            int iModMenuTypeID = 0;
            int iCurrentID = 0;
            try
            {
                iModMenuTypeID = Convert.ToInt32(Request.Form["ModMenuTypeID"]);
                iCurrentID = Convert.ToInt32(Request.Form["ParentID"]);

                if (iModMenuTypeID <= 0)
                    return;
            }
            catch (Exception ex)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = ex.ToString();
            }

            objDataOutput.MessSuccess = ShowMenuCha(iModMenuTypeID, iCurrentID);
        }

        /// <summary>
        /// Hiển thị menu cha
        /// </summary>
        /// <param name="ModMenuTypeID"></param>
        /// <param name="CurrentId"></param>
        /// <returns></returns>
        public string ShowMenuCha(int ModMenuTypeID, int CurrentId)
        {
            List<ModMenu_DynamicEntity> lstModMenu_DynamicEntity = ModMenu_DynamicService.Instance.CreateQuery()
                .Where(p => p.ModMenuTypeID == ModMenuTypeID && p.Activity == true && p.ID!=RecordID).ToList();
            if (lstModMenu_DynamicEntity == null || lstModMenu_DynamicEntity.Count <= 0)
                return string.Empty;

            string sReturn = string.Empty;

            foreach (var item in lstModMenu_DynamicEntity)
            {
                if (item.ID == CurrentId)
                    sReturn += "<option value=\"" + item.ID + "\" selected=\"selected\">" + item.Name + "</option>";
                else
                    sReturn += "<option value=\"" + item.ID + "\">" + item.Name + "</option>";
            }

            return sReturn;
        }
    }

    /// <summary>
    /// Class, Data Output
    /// </summary>
    public class DataOutput : VSW.Website.CP.Tools.Ajax.Common.DataOutput
    {
        public DataOutput()
        {

        }
    }
}