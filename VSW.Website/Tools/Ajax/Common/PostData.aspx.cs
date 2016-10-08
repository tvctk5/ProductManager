using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSW.Website.Tools.Ajax.Common
{
    public partial class PostDataCommon : System.Web.UI.Page
    {
        VSW.Website.CP.Tools.Common objCommon = new VSW.Website.CP.Tools.Common();
        DataOutput objDataOutput = new DataOutput();

        #region Tập các giá trị control được post lên để sử dụng
        int RecordID = 0;
        #endregion

        /// <summary>
        /// Lấy các giá trị từ các control post
        /// </summary>
        private void GetValueControl()
        {
            RecordID = objCommon.ConvertToInt32(Request["RecordID"]);
        }
        
        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string sType = Request.QueryString["Type"];

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

        private void CheckDuplicate()
        { 

        }
    }

    /// <summary>
    /// Class, Data Output
    /// </summary>
    public class DataOutput
    {
        public bool Error { get; set; }
        public string MessError { get; set; }
        public string MessSuccess { get; set; }
        public bool NotDuplicate { get; set; }

        public DataOutput()
        {
            Error = false;
            NotDuplicate = true;
        }
    }
}