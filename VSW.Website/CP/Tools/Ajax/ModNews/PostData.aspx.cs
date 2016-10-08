using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using VSW.Lib.Global;

namespace VSW.Website.CP.Tools.Ajax.ModNews
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
                case "sentmailnewsletter": SentMailNewsLetter(); break;
            }

            // Trả lại dữ liệu
            RenderMessage(objDataOutput);
        }

        /// <summary>
        /// SentMailNewsLetter
        /// </summary>
        private void SentMailNewsLetter()
        {
            try
            {
                // Lấy thông tin bài viết
                var objNewsLetter = ModNewsService.Instance.GetByID(RecordID);
                if (objNewsLetter == null)
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = "Không tìm thấy thông tin bài viết";
                    return;
                }

                // Gửi email
                var ListMail = ModListMailNewsLetterService.Instance.CreateQuery().Where(o => o.Activity == true).ToList();
                if (ListMail == null || ListMail.Count <= 0)
                {
                    objDataOutput.MessSuccess = "Gửi Email thành công";
                    return;
                }

                string sHostApp = ConvertTool.GetKeyApp("HostApp");
                int iPort = ConvertTool.ConvertToInt32(ConvertTool.GetKeyApp("EmailPort"));
                string sHost = ConvertTool.GetKeyApp("EmailServer");
                string sTaiKhoanEmail = ConvertTool.GetKeyApp("EmailSent");
                string sMatKhau = ConvertTool.GetKeyApp("EmailPass");

                foreach (var itemMail in ListMail)
                {
                    // Gửi email
                    SentMail(objNewsLetter.Name, objNewsLetter.Content, itemMail.Email, sHostApp, iPort, sHost, sTaiKhoanEmail, sMatKhau);
                }
            }
            catch (Exception ex)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = ex.ToString();
            }

            objDataOutput.MessSuccess = "Gửi Email thành công";
        }

        /// <summary>
        /// Gửi email
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sBody"></param>
        /// <param name="sMailTo"></param>
        /// <param name="sHostApp"></param>
        /// <param name="iPort"></param>
        /// <param name="sHost"></param>
        /// <param name="sTaiKhoanEmail"></param>
        /// <param name="sMatKhau"></param>
        /// <returns></returns>
        private bool SentMail(string title, string sBody, string sMailTo, string sHostApp, int iPort, string sHost, string sTaiKhoanEmail, string sMatKhau)
        {
            try
            {
                bool bolSendmail = ConvertTool.DoSendMail(sHostApp + " - Quản trị website", sMailTo, title, sBody,
                      iPort, sHost, sTaiKhoanEmail, sMatKhau);

                if (!bolSendmail)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
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