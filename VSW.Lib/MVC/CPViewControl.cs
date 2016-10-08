using System;
using VSW.Core.Models;
using System.Collections.Generic;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.MVC
{
    public class CPViewControl : VSW.Core.MVC.ViewControl
    {
        public CPViewPage CPViewPage
        {
            get { return this.Page as CPViewPage; }
        }

        protected string GetName(EntityBase entityBase)
        {
            return entityBase == null ? string.Empty : entityBase.Name;
        }

        protected string GetOrder(int id, int order)
        {
            return "<input type=\"text\" id=\"order[" + id + "]\" size=\"10\" value=\"" + order + "\" class=\"text-area-order\" style=\"text-align: center\" />";
        }

        protected string GetCheckbox(int id, int index)
        {
            return "<input type=\"checkbox\" id=\"cb" + index + "\" name=\"cid\" value=\"" + id + "\" onclick=\"isChecked(this.checked);\" />";
        }

        protected string GetPublish(int id, bool activity)
        {
            return "<a class=\"jgrid hasTip\" href=\"javascript:void(0);\" onclick=\"vsw_exec_cmd('[publishgx][" + id + "," + !activity + "]');return false;\" title=\"Click để duyệt và hủy duyệt\"><span class=\"state " + (activity ? "publish" : "unpublish") + "\"></span></a>";
        }

        // <summary>
        /// Hiển thị Tooltip với nội dung: Có hiển thị trên menu hay không
        /// CanTV       2012/09/20      Tạo mới
        /// </summary>
        protected string GetViewInMenu(int id, bool viewInMenu)
        {
            return "<a class=\"jgrid hasTip\" href=\"javascript:void(0);\" onclick=\"vsw_exec_cmd('[ViewInMenu][" + id + "," + !viewInMenu + "]');return false;\" title=\"Bấm để ẩn/ hiện trên menu\"><span class=\"state " + (viewInMenu ? "publish" : "unpublish") + "\"></span></a>";
        }

        // <summary>
        /// Hiển thị Tooltip với nội dung: Có hiển thị trên sitemap hay không
        /// CanTV       2012/09/20      Tạo mới
        /// </summary>
        protected string GetViewInSiteMap(int id, bool ViewInSiteMap)
        {
            return "<a class=\"jgrid hasTip\" href=\"javascript:void(0);\" onclick=\"vsw_exec_cmd('[ViewInSiteMap][" + id + "," + !ViewInSiteMap + "]');return false;\" title=\"Bấm để ẩn/ hiện trên sitemap\"><span class=\"state " + (ViewInSiteMap ? "publish" : "unpublish") + "\"></span></a>";
        }
         
        /// <summary>
        /// Hiển thị Tooltip với nội dung: sử dụng hoặc ko sử dụng
        /// CanTV       2012/09/20      Tạo mới
        /// </summary>
        protected string GetPublish_ActivateBasic(int id, bool activity)
        {
            return "<a class=\"jgrid hasTip\" href=\"javascript:void(0);\" onclick=\"vsw_exec_cmd('[publishActivateBasic][" + id + "," + !activity + "]');return false;\" title=\"Bấm để sử dụng hoặc không sử dụng.\"><span class=\"state " + (activity ? "publish" : "unpublish") + "\"></span></a>";
        }

        /// <summary>
        /// Hiển thị Tooltip với nội dung, và mess sau khi cập nhật tùy biến
        /// CanTV       2012/09/26      Tạo mới
        /// </summary>
        protected string GetPublish_ActivateModified(int id, bool activity, string sTooltip, string sMessSetToFalse, string sMessSetToTrue)
        {
            return "<a class=\"jgrid hasTip\" href=\"javascript:void(0);\" onclick=\"vsw_exec_cmd('[publishModified][" + id + "," + !activity + "," + sMessSetToFalse + "," + sMessSetToTrue + "]');return false;\" title=\"" + sTooltip + "\"><span class=\"state " + (activity ? "publish" : "unpublish") + "\"></span></a>";
        }

        protected string GetSortLink(string name, string key)
        {
            return "<a href=\"javascript:VSWRedirect('Index', '" + key + "-" + GetSortTypeDesc(key) + "', 'Sort');\">" + name + " " + GetImgSortTypeDesc(key) + "</a>";
        }

        protected string GetDefaultAddCommand()
        {
            return GetListCommand("apply|Lưu,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,space,cancel|Đóng");
        }

        protected string GetDefaultListCommand()
        {
            return GetListCommand("new|Thêm,edit|Sửa,space,publish|Duyệt,unpublish|Bỏ duyệt,space,delete|Xóa,copy|Sao chép,space,config|Xóa cache");
        }

        protected string GetListCommand(string commands)
        {
            string[] ArrCommand = commands.Split(',');

            string s = "<ul>";
            for (int i = 0; i < ArrCommand.Length; i++)
            {
                if (ArrCommand[i] == "space")
                {
                    s += "<li class=\"divider\"></li>";
                    continue;
                }

                string key = ArrCommand[i].Split('|')[0];
                string name = ArrCommand[i].Split('|')[1];

                s += "<li class=\"button\" id=\"toolbar-" + key + "\">";

                if (key == "delete")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){if(confirm('Bạn chắc là mình muốn xóa chứ !')){vsw_exec_cmd('delete')}}\" class=\"toolbar\"><span class=\"icon-32-delete\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "new")
                    s += "<a href=\"#\" onclick=\"javascript:VSWRedirect('Add')\" class=\"toolbar\"><span class=\"icon-32-new\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "publish" || key == "unpublish" || key == "edit" || key == "copy")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";

                s += "</li>";
            }
            s += "</ul>";

            return s;
        }

        protected string GetDefaultAddCommandValidation()
        {
            return GetListCommandValidation("apply|Lưu,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,space,cancel|Đóng");
        }

        protected string GetDefaultAddCommandThickboxValidation()
        {
            return GetListCommandThickboxValidation("apply|Lưu,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,space,list|Danh sách,space,closepopup|Đóng");
        }

        protected string GetDefaultListCommand_Not_Create_Copy_Delete()
        {
            return GetListCommand("edit|Sửa,space,publish|Duyệt,unpublish|Bỏ duyệt,space,config|Xóa cache");
        }

        protected string GetDefaultAddCommandValidation_Not_SaveAndCreate()
        {
            return GetListCommandValidation("apply|Lưu,save|Lưu  &amp; đóng,space,cancel|Đóng");
        }

        protected string GetListCommandValidation(string commands)
        {
            string[] ArrCommand = commands.Split(',');

            string s = "<ul>";
            for (int i = 0; i < ArrCommand.Length; i++)
            {
                if (ArrCommand[i] == "space")
                {
                    s += "<li class=\"divider\"></li>";
                    continue;
                }

                string key = ArrCommand[i].Split('|')[0];
                string name = ArrCommand[i].Split('|')[1];

                s += "<li class=\"button\" id=\"toolbar-" + key + "\">";

                if (key == "delete")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){if(confirm('Bạn chắc là mình muốn xóa?')){vsw_exec_cmd('delete')}}\" class=\"toolbar\"><span class=\"icon-32-delete\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "new")
                    s += "<a href=\"#\" onclick=\"javascript:VSWRedirect('Add')\" class=\"toolbar\"><span class=\"icon-32-new\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "publish" || key == "unpublish" || key == "edit" || key == "copy")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "cancel")
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else
                    s += "<a href=\"#\" onclick=\"if(CheckValidationForm()){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";

                s += "</li>";
            }
            s += "</ul>";

            return s;
        }

        protected string GetListCommandThickboxValidation(string commands)
        {
            string[] ArrCommand = commands.Split(',');

            string s = "<ul>";
            for (int i = 0; i < ArrCommand.Length; i++)
            {
                if (ArrCommand[i] == "space")
                {
                    s += "<li class=\"divider\"></li>";
                    continue;
                }

                string key = ArrCommand[i].Split('|')[0];
                string name = ArrCommand[i].Split('|')[1];

                s += "<li class=\"button\" id=\"toolbar-" + key + "\">";

                if (key == "delete")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){if(confirm('Bạn chắc là mình muốn xóa?')){vsw_exec_cmd('delete')}}\" class=\"toolbar\"><span class=\"icon-32-delete\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "new")
                    s += "<a href=\"#\" onclick=\"javascript:VSWRedirect('Add')\" class=\"toolbar\"><span class=\"icon-32-new\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "publish" || key == "unpublish" || key == "edit" || key == "copy")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "closepopup")
                    s += "<a href=\"#\" onclick=\"ReturnParent('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-cancel\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "list")
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('cancel')\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else
                    s += "<a href=\"#\" onclick=\"if(CheckValidationForm()){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";

                s += "</li>";
            }
            s += "</ul>";

            return s;
        }

        protected string GetDefaultAddCommandValidationProductInfo()
        {
            return GetListCommandValidationProductInfo("apply|Lưu,space,Properties|Thuộc tính,space,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,space,cancel|Đóng");
        }

        protected string GetListCommandValidationProductInfo(string commands)
        {
            string[] ArrCommand = commands.Split(',');

            string s = "<ul>";
            for (int i = 0; i < ArrCommand.Length; i++)
            {
                if (ArrCommand[i] == "space")
                {
                    s += "<li class=\"divider\"></li>";
                    continue;
                }

                string key = ArrCommand[i].Split('|')[0];
                string name = ArrCommand[i].Split('|')[1];

                s += "<li class=\"button\" id=\"toolbar-" + key + "\">";

                if (key == "delete")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){if(confirm('Bạn chắc là mình muốn xóa chứ !')){vsw_exec_cmd('delete')}}\" class=\"toolbar\"><span class=\"icon-32-delete\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "new")
                    s += "<a href=\"#\" onclick=\"javascript:VSWRedirect('Add')\" class=\"toolbar\"><span class=\"icon-32-new\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "publish" || key == "unpublish" || key == "edit" || key == "copy")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "cancel")
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "Properties")
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-default\" title=\"" + name + "\"></span>" + name + " </a>";
                else
                    s += "<a href=\"#\" onclick=\"if(CheckValidationForm()){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";

                s += "</li>";
            }
            s += "</ul>";

            return s;
        }

        protected string GetPagination(int pageIndex, int pageSize, int totalRecord)
        {
            Global.Pager _Pager = new Global.Pager();

            _Pager.IsCPLayout = true;
            _Pager.ActionName = "Index";
            _Pager.ParamName = "PageIndex";
            _Pager.PageIndex = pageIndex;
            _Pager.PageSize = pageSize;
            _Pager.TotalRecord = totalRecord;

            _Pager.Update();

            string s = "<div class=\"pagination\">";
            s += "<div class=\"limit\">Hiển thị #" + ShowDDLLimit(_Pager.PageSize) + "</div>";
            s += _Pager.HtmlPage;
            s += "<div class=\"limit\">Trang " + (_Pager.PageIndex + 1) + " của " + _Pager.TotalPage + "</div>";
            s += "</div>";
            return s;
        }

        /// <summary>
        /// Comment by CanTV
        /// ex <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord, "AddRelativeProduct", "OnChangePageSize_LienQuan(this,'AddRelativeProduct');", "javascript:VSWRedirect")%>
        /// Tuy nhiên, nó redrect tới trang đó chứ ko pải postback chỉnh trang đó
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="controllerName"></param>
        /// <param name="FunctionOnchange">Function thay đổi trên dropdownlist</param>
        /// <param name="FunctionOnchangePage"></param>
        /// <returns></returns>
        protected string GetPagination(int pageIndex, int pageSize, int totalRecord, string controllerName, string FunctionOnchange, string FunctionOnchangePage)
        {
            Global.Pager _Pager = new Global.Pager();

            _Pager.IsCPLayout = true;
            _Pager.ActionName = controllerName;
            _Pager.ParamName = "PageIndex";
            _Pager.PageIndex = pageIndex;
            _Pager.PageSize = pageSize;
            _Pager.TotalRecord = totalRecord;

            _Pager.Update(FunctionOnchangePage, controllerName);

            string s = "<div class=\"pagination\">";
            if (!string.IsNullOrEmpty(FunctionOnchange))
                s += "<div class=\"limit\">Hiển thị #" + ShowDDLLimit(_Pager.PageSize, controllerName, FunctionOnchange, FunctionOnchangePage) + "</div>";
            else
                s += "<div class=\"limit\">Hiển thị #" + ShowDDLLimit(_Pager.PageSize, controllerName) + "</div>";
            s += _Pager.HtmlPage;
            s += "<div class=\"limit\">Trang " + (_Pager.PageIndex + 1) + " của " + _Pager.TotalPage + "</div>";
            s += "</div>";
            return s;
        }

        /// <summary>
        /// Hàm chuyển thay đổi PageIndex nhưng postback trên chính trang đó
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="controllerName"></param>
        /// <param name="FunctionOnchange"></param>
        /// <returns></returns>
        protected string GetPagination(int pageIndex, int pageSize, int totalRecord, string controllerName, string FunctionOnchange, string FunctionOnchangePage, bool PostbackOnPage)
        {
            Global.Pager _Pager = new Global.Pager();

            _Pager.IsCPLayout = true;
            _Pager.ActionName = controllerName;
            _Pager.ParamName = "PageIndex";
            _Pager.PageIndex = pageIndex;
            _Pager.PageSize = pageSize;
            _Pager.TotalRecord = totalRecord;

            _Pager.Update(controllerName, FunctionOnchangePage, true);

            string s = "<div class=\"pagination\">";
            if (!string.IsNullOrEmpty(FunctionOnchange))
                s += "<div class=\"limit\">Hiển thị #" + ShowDDLLimit(_Pager.PageSize, controllerName, FunctionOnchange) + "</div>";
            else
                s += "<div class=\"limit\">Hiển thị #" + ShowDDLLimit(_Pager.PageSize, controllerName) + "</div>";
            s += _Pager.HtmlPage;
            s += "<div class=\"limit\">Trang " + (_Pager.PageIndex + 1) + " của " + _Pager.TotalPage + "</div>";
            s += "</div>";
            return s;
        }

        protected string ShowDDLLimit(int pageSize)
        {
            return ShowDDLLimit(pageSize, "Index");
        }

        protected string ShowDDLLimit(int pageSize, string key)
        {
            int[] Arr = new int[] { 5, 10, 15, 20, 30, 50, 100 };

            string s = "<select name=\"limit\" id=\"limit\" onchange=\"VSWRedirect('" + key + "')\" class=\"inputbox\" size=\"1\">";
            for (int i = 0; i < Arr.Length; i++)
            {
                s += "<option " + (Arr[i] == pageSize ? "selected" : string.Empty) + " value=\"" + Arr[i] + "\">" + Arr[i] + "</option>";
            }
            s += "</select>";

            return s;
        }

        protected string ShowDDLLimit(int pageSize, string key, string FunctionOnchange, string FunctionOnchangePage)
        {
            int[] Arr = new int[] { 5, 10, 15, 20, 30, 50, 100 };
            string s = string.Empty;

            if (!string.IsNullOrEmpty(FunctionOnchange))
                s = "<select name=\"limit\" id=\"limit\" onchange=\"" + FunctionOnchange + "\" class=\"inputbox\" size=\"1\">";
            else
                s = "<select name=\"limit\" id=\"limit\" onchange=\"VSWRedirect('" + key + "')\" class=\"inputbox\" size=\"1\">";

            for (int i = 0; i < Arr.Length; i++)
            {
                s += "<option " + (Arr[i] == pageSize ? "selected" : string.Empty) + " value=\"" + Arr[i] + "\">" + Arr[i] + "</option>";
            }
            s += "</select>";

            return s;
        }

        /// <summary>
        /// Add by CanTV: Not using function FunctionOnchangePage
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <param name="FunctionOnchange"></param>
        /// <returns></returns>
        protected string ShowDDLLimit(int pageSize, string key, string FunctionOnchange)
        {
            int[] Arr = new int[] { 5, 10, 15, 20, 30, 50, 100 };
            string s = string.Empty;

            if (!string.IsNullOrEmpty(FunctionOnchange))
                s = "<select name=\"limit\" id=\"limit\" onchange=\"" + FunctionOnchange + "\" class=\"inputbox\" size=\"1\">";
            else
                s = "<select name=\"limit\" id=\"limit\" onchange=\"VSWRedirect('" + key + "')\" class=\"inputbox\" size=\"1\">";

            for (int i = 0; i < Arr.Length; i++)
            {
                s += "<option " + (Arr[i] == pageSize ? "selected" : string.Empty) + " value=\"" + Arr[i] + "\">" + Arr[i] + "</option>";
            }
            s += "</select>";

            return s;
        }

        protected string ShowDDLLang(int langID)
        {
            return ShowDDLLang(langID, "Index");
        }

        protected string ShowDDLLang(int langID, string key)
        {
            List<SysLangEntity> list = SysLangService.Instance.CreateQuery().ToList_Cache();

            string s = "<select name=\"filter_lang\" id=\"filter_lang\" onchange=\"VSWRedirect('" + key + "','0','parent_id')\" class=\"inputbox\" size=\"1\">";
            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].ID == langID ? "selected" : string.Empty) + " value=\"" + list[i].ID + "\">" + list[i].Name + "</option>";
            }
            s += "</select>";

            return s;
        }

        protected string ShowMap(List<EntityBase> listMap)
        {
            string s = "<a href=\"javascript:VSWRedirect('Index', '0', 'ParentID');\">Root</a>";
            for (int i = 0; listMap != null && i < listMap.Count; i++)
            {
                s += ">> <a href=\"javascript:VSWRedirect('Index', '" + listMap[i].ID + "', 'ParentID');\">" + listMap[i].Name + "</a>";
            }

            return s;
        }

        protected string ShowMessage()
        {
            string s = string.Empty;

            if (Cookies.GetValue("message") != string.Empty)
            {
                s += "<dl id=\"system-message\">";
                s += "<dd class=\"message message\"><ul>";
                s += "<li style=\"color:Blue !important;\">" + Data.Base64Decode(Cookies.GetValue("message")) + "</li>";
                s += "</ul></dd>";
                s += "</dl>";

                Cookies.Remove("message");
            }
            else
            {
                Message _Message = CPViewPage.Message;

                if (_Message != null && _Message.ListMessage.Count > 0)
                {
                    s += "<dl id=\"system-message\">";
                    s += "<dd class=\"message " + _Message.MessageTypeName + "\"><ul>";
                    for (int i = 0; i < _Message.ListMessage.Count; i++)
                    {
                        s += "<li style=\"color:Blue !important;\"> - " + _Message.ListMessage[i] + "</li>";
                    }
                    s += "</ul></dd>";
                    s += "</dl>";
                }
            }

            return s;
        }

        protected void CreatePathUpload(string pathChild)
        {
            try
            {
                Directory.Create(Server.MapPath("~/Data/upload/" + pathChild));
            }
            catch { }
        }

        /// <summary>
        /// Chuyển đổi kiểu Double sang dạng tiền
        /// 
        /// CanTV       2012/09/21      Tạo mới
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="DecimalNumber"></param>
        /// <returns></returns>
        protected string ConvertToMoney(object objValue, int DecimalNumber = 0)
        {
            string sValue = objValue.ToString();
            sValue = sValue.Replace(" ", string.Empty);
            try
            {
                return String.Format("{0:C" + DecimalNumber.ToString() + "}", Convert.ToDouble(sValue)).Replace("$", "").Replace("£", "");
            }
            catch (Exception)
            { return sValue; }
        }

        protected string ShowRequireValidation()
        {
            return string.Empty; //"<span>&nbsp(<span class='RequireValidate'>*</span>)</span>";
        }

        public static string ShowMessDuplicate(string sObjectName, string sValues)
        {
            return " - " + sObjectName + " '" + sValues + "' đã được sử dụng. Yêu cầu nhập " + sObjectName + " khác.";
        }

        protected string GetAddCommandValidationOrderAdmin()
        {
            return GetListCommandValidationOrderAdmin("apply|Lưu,space,AddProduct|Thêm sản phẩm,space,save|Lưu  &amp; đóng,save-new|Lưu &amp; thêm,space,cancel|Đóng");
        }

        protected string GetListCommandValidationOrderAdmin(string commands)
        {
            string[] ArrCommand = commands.Split(',');

            string s = "<ul>";
            for (int i = 0; i < ArrCommand.Length; i++)
            {
                if (ArrCommand[i] == "space")
                {
                    s += "<li class=\"divider\"></li>";
                    continue;
                }

                string key = ArrCommand[i].Split('|')[0];
                string name = ArrCommand[i].Split('|')[1];

                s += "<li class=\"button\" id=\"toolbar-" + key + "\">";

                if (key == "delete")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){if(confirm('Bạn chắc là mình muốn xóa chứ !')){vsw_exec_cmd('delete')}}\" class=\"toolbar\"><span class=\"icon-32-delete\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "new")
                    s += "<a href=\"#\" onclick=\"javascript:VSWRedirect('Add')\" class=\"toolbar\"><span class=\"icon-32-new\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "publish" || key == "unpublish" || key == "edit" || key == "copy")
                    s += "<a href=\"#\" onclick=\"javascript:if(document.vswForm.boxchecked.value>0){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "cancel")
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";
                else if (key == "AddProduct")
                    s += "<a href=\"#\" onclick=\"vsw_exec_cmd('" + key + "')\" class=\"toolbar\"><span class=\"icon-32-new\" title=\"" + name + "\"></span>" + name + " </a>";
                else
                    s += "<a href=\"#\" onclick=\"if(CheckValidationForm()){vsw_exec_cmd('" + key + "')}\" class=\"toolbar\"><span class=\"icon-32-" + key + "\" title=\"" + name + "\"></span>" + name + " </a>";

                s += "</li>";
            }
            s += "</ul>";

            return s;
        }

        public static string GetUrl(string RawUrl, string sParameter)
        {
            string sUrl = RawUrl;

            if (!RawUrl.Contains(sParameter))
                return sUrl;

            sParameter = sParameter.ToUpper();
            string[] ArrParameter = RawUrl.Split('/');

            sUrl = string.Empty;
            foreach (string itemPara in ArrParameter)
            {
                if (itemPara.ToUpper() == sParameter)
                    break;

                if (!string.IsNullOrEmpty(sUrl))
                    sUrl += "/";

                sUrl += itemPara;
            }

            return "/" + sUrl;
        }
        #region private

        private string SortType
        {
            get
            {
                return CPViewPage.PageViewState.GetValue("Sort").ToString().Trim().Split('-')[0]
                    .Replace("'", string.Empty)
                    .Replace("-", string.Empty)
                    .Replace(";", string.Empty);
            }
        }

        private bool SortDesc
        {
            get
            {
                try
                {
                    return "desc" == CPViewPage.PageViewState.GetValue("Sort").ToString().Trim().Split('-')[1].ToLower();
                }
                catch { }

                return false;
            }
        }

        private string GetSortTypeDesc(string type)
        {
            if (type != SortType)
                return "desc";

            return SortDesc ? "asc" : "desc";
        }

        private string GetImgSortTypeDesc(string type)
        {
            if (type != SortType)
                return string.Empty;

            return "<img src=\"/{CPPath}/Content/media/system/images/sort_" + (SortDesc ? "desc" : "asc") + ".png\" alt=\"\">";
        }

        #endregion
    }
}
