using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VSW.Website.Tools
{
    public partial class PostForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var Data = Request.Form.AllKeys;
           RenderMessage(CheckData());
        }

        private DataReturn CheckData()
        {
            DataReturn objDataReturn = new DataReturn();

            var Type = Convert.ToInt32(Request.QueryString["Type"]);
            if (Type == 0)
            {
                objDataReturn.Erros = true;
                objDataReturn.ThongTin = "Post lên FALSE";
            }
            else
            {
                objDataReturn.Erros = false;
                objDataReturn.ThongTin = "Post lên TRUE";
            }

            return objDataReturn;
        }

        /// <summary>
        /// write message về client
        /// </summary>
        /// <param name="objDataReturn"></param>
        /// <modified>
        /// Author				Date					comments
        /// QuangNĐ				30/06/2011				Tạo mới
        ///</modified>
        private void RenderMessage(DataReturn objDataReturn)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonMessage = oSerializer.Serialize(objDataReturn);
            this.Page.Response.Clear();
            this.Page.Response.ContentType = "application/json";
            this.Page.Response.Write(strJsonMessage);
            this.Page.Response.End();
        }
    }

    public class DataReturn
    {
        public bool Erros { get; set; }
        public string ThongTin { get; set; }
    }
}