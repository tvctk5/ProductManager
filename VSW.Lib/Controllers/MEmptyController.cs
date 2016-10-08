using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Empty", Code = "MEmpty", Order = 50)]
    public class MEmptyController : Controller
    {
        public void ActionIndex(MEmptyModel model)
        {
        }
    }

    public class MEmptyModel
    {
        private int _Page = 0;
        public int Page
        {
            get { return _Page; }
            set { _Page = value - 1; }
        }

        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
    }
}
