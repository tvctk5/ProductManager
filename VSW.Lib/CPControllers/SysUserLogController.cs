﻿using System;
using System.Collections.Generic;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class SysUserLogController : CPController
    {
        public SysUserLogController()
        {
            //khoi tao Service
            DataService = CPUserLogService.Instance;
            //CheckPermissions = false;
        }

        public void ActionIndex(SysUserLogModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = CPUserLogService.Instance.CreateQuery()
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }
    }

    public class SysUserLogModel : DefaultModel
    {
       
    }
}
