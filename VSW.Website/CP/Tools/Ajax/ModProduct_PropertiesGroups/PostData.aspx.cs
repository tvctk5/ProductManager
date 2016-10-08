using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Website.CP.Tools.Ajax.Common.ModProduct_PropertiesGroups
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
                case "1": CheckDuplicate(); break;
                case "2": break;
                case "3": break;
                default:
                    break;
            }

            // Trả lại dữ liệu
            RenderMessage(objDataOutput);
        }

        /// <summary>
        /// Check duplicate
        /// </summary>
        private void CheckDuplicate()
        {
            // Kiểm tra mã xem có trùng với mã nào khác đã có không
            if (ModProduct_PropertiesGroupsService.Instance.DuplicateCode(Code, RecordID, ref sMessError))
            {
                if (string.IsNullOrEmpty(sMessError))
                {
                    objDataOutput.NotDuplicate = false;
                    objDataOutput.MessSuccess = CPViewControl.ShowMessDuplicate("Mã nhóm thuộc tính", Code);
                }
                else
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = sMessError;
                }
            }
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