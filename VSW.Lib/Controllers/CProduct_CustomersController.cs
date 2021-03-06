﻿using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "ĐK : Khách hàng", Code = "CProduct_Customers", IsControl = true, Order = 50)]
    public class CProduct_CustomersController : Controller
    {

        //[VSW.Core.MVC.PropertyInfo("Số lượng")]
        //public int PageSize = 10;

        [VSW.Core.MVC.PropertyInfo("Tiêu đề")]
        public string Title;

        public override void OnLoad()
        {
            // Không hiển thị module
            if (ShowModule.Equals((int)VSW.Lib.Global.EnumValue.Activity.FALSE))
            {
                ViewBag.ShowModule = false;
                return;
            }

            //ViewBag.Data = ModProduct_CustomersService.Instance.CreateQuery()
            //                .Where(o => o.Activity == true)
            //                .OrderByDesc(o => o.ID)
            //                .Take(PageSize)
            //                .ToList_Cache();

            ViewBag.Title = Title;
        }

    }
}
