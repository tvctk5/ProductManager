using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace VSW.Website.CP.Views.ModProduct_Manufacturer
{
    public partial class Ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod()]
        public static string GetData()
        {
            int userid = 1;
            /*You can do database operations here if required*/
            return "my userid is" + userid.ToString();
        }
    }
}