using System;
using System.Web.UI;

namespace VSW.Lib.MVC
{
    public class CPViewTemplate : MasterPage
    {
        public VSW.Lib.MVC.CPViewPage CPViewPage
        {
            get { return ((VSW.Lib.MVC.CPViewPage)this.Page); }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (CPViewPage.ViewControl != null)
                FindControl("cphMain").Controls.Add(CPViewPage.ViewControl);
        }
    }
}
