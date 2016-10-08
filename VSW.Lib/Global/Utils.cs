using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

using VSW.Lib.Models;
using VSW.Core.Models;
using System.Data;
using VSW.Lib.Global.ListItem;


namespace VSW.Lib.Global
{
    public class Utils : VSW.Core.Web.Utils
    {

        public static string GetHTMLForSeo(string html)
        {
            string lower_html = html.ToLower();

            var listAutoLinks = ModAutoLinksService.Instance.CreateQuery()
                                    .Where(o => o.Activity == true)
                                    .OrderByDesc(o => o.Name)
                                    .ToList_Cache();

            for (int i = 0; listAutoLinks != null && i < listAutoLinks.Count; i++)
            {
                listAutoLinks[i].Name = listAutoLinks[i].Name.Trim();

                if (lower_html.IndexOf(listAutoLinks[i].Name.ToLower()) > -1)
                    html = Regex.Replace(html, "(?<!<a+[^<]*>)" + listAutoLinks[i].Name + "(?![^<]+>)", "<a href=\"" + listAutoLinks[i].Link + "\" target=\"_blank\" style=\"color:#0148BA\" title=\"" + listAutoLinks[i].Title + "\">$&</a>", RegexOptions.IgnoreCase);
            }

            return html;
        }


        public static string GetURL(int MenuID, string Code)
        {
            string Key_Cache = "Lib.App.ViewPage.GetURL." + MenuID;

            string _MapCode = null;
            object obj = VSW.Core.Web.Cache.GetValue(Key_Cache);
            if (obj != null)
            {
                _MapCode = obj.ToString();
            }
            else
            {
                SysPageEntity _NewPage = null;

                while (MenuID > 0)
                {
                    _NewPage = SysPageService.Instance.CreateQuery()
                                    .Where(o => o.MenuID == MenuID && o.Activity == true)
                                    .ToSingle_Cache();

                    if (_NewPage != null)
                        break;

                    WebMenuEntity _Menu = WebMenuService.Instance.GetByID_Cache(MenuID);

                    if (_Menu == null || (_Menu != null && _Menu.ParentID == 0))
                        break;

                    MenuID = _Menu.ParentID;
                }

                if (_NewPage != null)
                    _MapCode = "/" + SysPageService.Instance.GetMapCode_Cache(_NewPage) + "/" + Code + ".aspx";
                else
                    _MapCode = string.Empty;

                VSW.Core.Web.Cache.SetValue(Key_Cache, _MapCode);
            }

            return _MapCode;
        }

        public static List<WebMenuEntity> GetMenuByType(string type, int langID)
        {
            List<WebMenuEntity> list = WebMenuService.Instance.CreateQuery()
                                        .Where(o => o.ParentID == 0 && o.LangID == langID && o.Type == type)
                                        .OrderByAsc(o => o.Order)
                                        .ToList_Cache();

            if (list != null)
            {
                int _parent_id = list[0].ID;

                return WebMenuService.Instance.CreateQuery()
                        .Where(o => o.ParentID == _parent_id)
                        .OrderByAsc(o => o.Order)
                        .ToList_Cache();
            }

            return null;
        }

        public static string GetNameOfConfig(string configKey, int value)
        {
            List<ListItem.Item> list = ListItem.List.GetListByConfigkey(configKey);

            ListItem.Item item = list.Find(o => o.Value == value.ToString());

            return item == null ? string.Empty : item.Name;
        }

        public static string GetEmailAddress(string Eml)
        {
            Eml = Eml.Trim().ToLower();
            Match match = Regex.Match(Eml, @"^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,4})$");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }

        public bool IsGoodLoginName(string s)
        {
            if (s.Length < 4 || s.Length > 22) return false;
            if (!Char.IsLetter(s[0])) return false;
            return (!Regex.IsMatch(s, "[^a-z0-9_]", RegexOptions.IgnoreCase));
        }

        public static string GetRandString()
        {
            Random rand = new Random();
            int iRan = rand.Next();
            string sKey = VSW.Lib.Global.Security.MD5(iRan.ToString());
            return sKey.Substring(0, 2) + iRan.ToString()[0] + sKey.Substring(3, 2);
        }

        public static int GetCountOnline()
        {
            int _Online = ModOnlineService.Instance.CreateQuery()
                            .Where(o => o.TimeValue > DateTime.Now.AddMinutes(-5).Ticks)
                            .Count()
                            .ToValue().ToInt();

            if (_Online == 0)
                _Online = 1;

            return _Online;
        }

        public static int GetCountVisit()
        {
            return WebSettingService.Instance.CreateQuery()
                     .Where(o => o.Code == "VISIT")
                     .Select(o => o.Value)
                     .ToValue().ToInt();
        }

        public static void UpdateOnline()
        {
            if (VSW.Core.Global.Config.GetValue("Mod.Visit").ToBool())
            {
                if (HttpContext.Current.Request.Cookies["Mod.Online"] == null)
                {
                    WebSettingService.Instance.Update("[Code]='VISIT'",
                        "@Value", GetCountVisit() + 1);

                    if (VSW.Core.Global.Config.GetValue("Mod.Online").ToBool())
                    {
                        ModOnlineService.Instance.Delete(o => o.TimeValue < DateTime.Now.AddMinutes(-5).Ticks);
                        ModOnlineService.Instance.Save(new ModOnlineEntity()
                        {
                            SessionID = HttpContext.Current.Session.SessionID,
                            TimeValue = DateTime.Now.Ticks
                        });
                    }

                    HttpContext.Current.Response.Cookies["Mod.Online"].Value = "1";
                    HttpContext.Current.Response.Cookies["Mod.Online"].Expires = System.DateTime.Now.AddMinutes(5);
                }
            }
        }

        public static string ShowDDLMenuByParent(int parentID, int selectID)
        {
            if (parentID < 1)
                return string.Empty;

            List<WebMenuEntity> list = WebMenuService.Instance.CreateQuery()
                                          .Where(o => o.ParentID == parentID)
                                          .OrderByAsc(o => o.Order)
                                          .ToList_Cache();

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].ID == selectID ? "selected" : string.Empty) + " value=\"" + list[i].ID + "\">" + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowDDLMenuByType2(string type, int langID, int selectID)
        {
            string s = string.Empty;

            List<WebMenuEntity> list = WebMenuService.Instance.CreateQuery()
                                        .Where(o => o.ParentID == 0 && o.LangID == langID && o.Type == type)
                                        .OrderByAsc(o => o.Order)
                                        .ToList_Cache();

            if (list != null)
            {
                int _parent_id = list[0].ID;

                list = WebMenuService.Instance.CreateQuery()
                        .Where(o => o.ParentID == _parent_id)
                        .OrderByAsc(o => o.Order)
                        .ToList_Cache();

                for (int i = 0; list != null && i < list.Count; i++)
                {
                    s += "<option " + (list[i].ID == selectID ? "selected" : string.Empty) + " value=\"" + list[i].ID + "\">" + list[i].Name + "</option>";
                }
            }

            return s;
        }

        public static string ShowDDLMenuByType(string type, int langID, int selectID)
        {
            List<ListItem.Item> list = ListItem.List.GetList(WebMenuService.Instance, langID, type);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == selectID.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowDDLMenuByType(string type, int langID, int selectID, bool bolOrderBy_MA_PHAN_CAP)
        {
            List<ListItem.Item> list = ListItem.List.GetList(WebMenuService.Instance, langID, type, bolOrderBy_MA_PHAN_CAP);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == selectID.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowDDLProductGroups(string sGroupsParentId, string sGroupId)
        {
            string sWhere = string.Empty;
            string s = string.Empty;

            if (string.IsNullOrEmpty(sGroupId))
                sWhere = "[ID] NOT IN (SELECT [ID] FROM Mod_Product_Groups WHERE ParentId= " + sGroupId + ") AND [ID] <> " + sGroupId;
            else
                sWhere = "[ID] <> " + sGroupId;

            List<ListItem.Item> list = ListItem.List.GetList_Modified(ModProduct_GroupsService.Instance, sWhere, "[ID]");

            if (list == null || list.Count <= 0)
                return s;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == sGroupsParentId ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }
            return s;
        }

        public static string ShowDDLProductGroups(string sGroupId)
        {
            string sWhere = string.Empty;
            string s = string.Empty;

            List<ListItem.Item> list = ListItem.List.GetList_Modified(ModProduct_GroupsService.Instance, sWhere, "[ID]");

            if (list == null || list.Count <= 0)
                return s;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == sGroupId ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }
            return s;
        }

        public static string ShowDDLProductFilterGroups(int iGroupFilterSeletedId)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                        SELECT * FROM Mod_Product_FilterGroups
                        ORDER BY [Order]
            ";

            string sGroupFilterId = iGroupFilterSeletedId.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ModProduct_FilterGroupsService.Instance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sGroupFilterId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;
        }

        public static string ShowDDLProductSurveyGroups(int iGroupSurveySeletedId)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                        SELECT * FROM Mod_Product_SurveyGroup
                        ORDER BY [CreateDate] DESC
            ";

            string sGroupSurveyId = iGroupSurveySeletedId.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ModProduct_SurveyGroupService.Instance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sGroupSurveyId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;
        }

        public static string ShowDDLMenuType(int iMenuTypeId)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                        SELECT * FROM Mod_Menu_Type
                        ORDER BY [Name]
            ";

            string sMenuTypeId = iMenuTypeId.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ModMenu_TypeService.Instance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sMenuTypeId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;
        }

        public static string ShowDDLMenuDynamicByMenuType(int iMenuTypeId, int iCurrentValue, int iRecordID)
        {
            /*string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                        SELECT * FROM Mod_Menu_Dynamic
                        WHERE ModMenuTypeID=" + iMenuTypeId + " AND ID<> " + iRecordID + @"
                        ORDER BY [Name]
            ";

            string sCurrentValue = iCurrentValue.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ModMenu_DynamicService.Instance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            //List<Item> lstItem = List.BuildItem();

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sCurrentValue ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;*/

            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                        SELECT * FROM Mod_Menu_Dynamic
                        WHERE ModMenuTypeID=" + iMenuTypeId + " AND ID<> " + iRecordID + @"
                        ORDER BY [Name]
            ";

            string sCurrentValue = iCurrentValue.ToString();
            List<Item> lstItem = List.GetList(ModMenu_DynamicService.Instance, sWhere);

            if (lstItem == null || lstItem.Count <= 0)
                return s;

            foreach (var item in lstItem)
	        {
                s += "<option " + (item.Value == sCurrentValue ? "selected" : string.Empty) + " value=\"" + item.Value + "\">&nbsp; " + item.Name + "</option>";
            }
            return s;
        }

        /// <summary>
        /// MẶc định lấy ra là ID và NAME
        /// </summary>
        /// <param name="Service"></param>
        /// <param name="iItemSeletedId"></param>
        /// <returns></returns>
        public static string ShowDDLList(dynamic ServiceInstance, int iItemSeletedId = 0)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                          SELECT * FROM " + ServiceInstance.TableName +
                        " ORDER BY [Name]"
            ;

            string sItemSelectedId = iItemSeletedId.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ServiceInstance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sItemSelectedId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;

        }

        /// <summary>
        /// MẶc định lấy ra là ID và NAME và WHERE
        /// </summary>
        /// <param name="Service"></param>
        /// <param name="iItemSeletedId"></param>
        /// <returns></returns>
        public static string ShowDDLList(dynamic ServiceInstance, string strWhere, int iItemSeletedId = 0)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                          SELECT * FROM " + ServiceInstance.TableName + (string.IsNullOrEmpty(strWhere)? "" : "WHERE " + strWhere) +
                        " ORDER BY [Name]"
            ;

            string sItemSelectedId = iItemSeletedId.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ServiceInstance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sItemSelectedId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;

        }

        /// <summary>
        /// MẶc định lấy ra là ID và NAME và WHERE, Order by
        /// </summary>
        /// <param name="Service"></param>
        /// <param name="iItemSeletedId"></param>
        /// <returns></returns>
        public static string ShowDDLList(dynamic ServiceInstance, string strWhere, string strOrderBy, int iItemSeletedId = 0)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                          SELECT * FROM " + ServiceInstance.TableName + (string.IsNullOrEmpty(strWhere) ? "" : "WHERE " + strWhere) + " ORDER BY " +
                                          (string.IsNullOrEmpty(strOrderBy)? "[Name]" : strOrderBy);

            string sItemSelectedId = iItemSeletedId.ToString();
            DataTable tblData = Utils.GetDataByQueryString(ServiceInstance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sItemSelectedId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;

        }

        /// <summary>
        /// MẶc định lấy ra là ID và NAME và WHERE, Order by
        /// </summary>
        /// <param name="Service"></param>
        /// <param name="iItemSeletedId"></param>
        /// <returns></returns>
        public static string ShowDDLList_SelectTop(dynamic ServiceInstance, string strWhere, string strOrderBy)
        {
            string sWhere = string.Empty;
            string s = string.Empty;
            sWhere = @"
                          SELECT * FROM " + ServiceInstance.TableName + (string.IsNullOrEmpty(strWhere) ? "" : "WHERE " + strWhere) + " ORDER BY " +
                                          (string.IsNullOrEmpty(strOrderBy) ? "[Name]" : strOrderBy);

            DataTable tblData = Utils.GetDataByQueryString(ServiceInstance, sWhere);

            if (tblData == null || tblData.Rows.Count <= 0)
                return s;

            string sItemSelectedId = tblData.Rows[0]["ID"].ToString();

            for (int i = 0; tblData != null && i < tblData.Rows.Count; i++)
            {
                s += "<option " + (tblData.Rows[i]["ID"].ToString() == sItemSelectedId ? "selected" : string.Empty) + " value=\"" + tblData.Rows[i]["ID"].ToString() + "\">&nbsp; " + tblData.Rows[i]["Name"].ToString() + "</option>";
            }
            return s;

        }

        private static void BuildItem(DataTable dtData, int parentID, string space, ref List<Item> lstList)
        {
            DataRow[] dr = dtData.Select("ParentID=" + parentID, "CodeParent");

            for (int i = 0; i < dr.Length; i++)
            {
                string _text = space + " " + dr[i]["Name"].ToString().Trim();

                parentID = VSW.Core.Global.Convert.ToInt(dr[i]["ID"]);

                lstList.Add(new Item(_text, parentID.ToString()));

                BuildItem(dtData, parentID, space + "----", ref lstList);
            }
        }

        public static string ShowDDLPropertiesGroups(int iPropertiesGroupsId)
        {
            List<ModProduct_PropertiesGroupsEntity> lstPropertiesGroups = ModProduct_PropertiesGroupsService.Instance
                .CreateQuery()
                .ToList();

            if (lstPropertiesGroups == null || lstPropertiesGroups.Count <= 0)
                return string.Empty;

            string s = string.Empty;
            string Activate = string.Empty;
            string Style = string.Empty;

            foreach (ModProduct_PropertiesGroupsEntity item in lstPropertiesGroups)
            {
                Style = string.Empty;
                Activate = string.Empty;

                if (Convert.ToString(item.Activity).ToUpper() == "FALSE")
                {
                    Activate = " - [ Đang ngừng sử dụng ]";
                    Style = " style=\"color: Red !important;\" ";
                }
                s += "<option " + Style + (item.ID == iPropertiesGroupsId ? "selected=\"selected\"" : string.Empty) + " value=\"" + item.ID + "\">&nbsp; " + item.Code + " ( " + item.Name + " ) " + Activate + "</option>";
            }

            return s;
        }

        public static string ShowDDLProductTypes(string sProductTypesId)
        {
            List<ModProduct_TypesEntity> lstPropertiesGroups = ModProduct_TypesService.Instance
                .CreateQuery()
                .ToList();

            if (lstPropertiesGroups == null || lstPropertiesGroups.Count <= 0)
                return string.Empty;

            string s = string.Empty;
            string Activate = string.Empty;
            string Style = string.Empty;

            foreach (ModProduct_TypesEntity item in lstPropertiesGroups)
            {
                Style = string.Empty;
                Activate = string.Empty;

                if (Convert.ToString(item.Activity).ToUpper() == "FALSE")
                {
                    Activate = " - [ Đang ngừng sử dụng ]";
                    Style = " style=\"color: Red !important;\" ";
                }
                s += "<option " + Style + (item.ID.ToString() == sProductTypesId ? "selected=\"selected\"" : string.Empty) + " value=\"" + item.ID + "\">&nbsp; " + item.Name + " ( " + item.Code + " ) " + Activate + "</option>";
            }

            return s;
        }

        public static string ShowDDLManufacturer(string sManufacturerId)
        {
            List<ModProduct_ManufacturerEntity> lstManufacturers = ModProduct_ManufacturerService.Instance
                .CreateQuery()
                .ToList();

            if (lstManufacturers == null || lstManufacturers.Count <= 0)
                return string.Empty;

            string s = string.Empty;
            string Activate = string.Empty;
            string Style = string.Empty;

            foreach (ModProduct_ManufacturerEntity item in lstManufacturers)
            {
                Style = string.Empty;
                Activate = string.Empty;

                if (Convert.ToString(item.Activity).ToUpper() == "FALSE")
                {
                    Activate = " - [ Đang ngừng sử dụng ]";
                    Style = " style=\"color: Red !important;\" ";
                }
                s += "<option " + Style + (item.ID.ToString() == sManufacturerId ? "selected=\"selected\"" : string.Empty) + " value=\"" + item.ID + "\">&nbsp; " + item.Name + " ( " + item.Code + " ) " + Activate + "</option>";
            }

            return s;
        }

        public static string ShowDDLProductSupplier(string sSupplierId)
        {
            List<ModProduct_SupplierEntity> lstSupplier = ModProduct_SupplierService.Instance
                .CreateQuery()
                .ToList();

            if (lstSupplier == null || lstSupplier.Count <= 0)
                return string.Empty;

            string s = string.Empty;
            string Activate = string.Empty;
            string Style = string.Empty;

            foreach (ModProduct_SupplierEntity item in lstSupplier)
            {
                Style = string.Empty;
                Activate = string.Empty;

                if (Convert.ToString(item.Activity).ToUpper() == "FALSE")
                {
                    Activate = " - [ Đang ngừng sử dụng ]";
                    Style = " style=\"color: Red !important;\" ";
                }
                s += "<option " + Style + (item.ID.ToString() == sSupplierId ? "selected=\"selected\"" : string.Empty) + " value=\"" + item.ID + "\">&nbsp; " + item.Name + " ( " + item.Code + " ) " + Activate + "</option>";
            }

            return s;
        }

        public static string ShowDDLTrangLienKet(int iPageId, int iLangID = 0)
        {
            List<ListItem.Item> list = ListItem.List.GetList(SysPageService.Instance, iLangID, string.Empty);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == iPageId.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        /// <summary>
        /// Doanh thu dai ly
        /// </summary>
        /// <param name="serviceBase"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="selectID"></param>
        /// <returns></returns>
        public static string ShowDDLDTDaiLy(dynamic serviceBase, string where, string order, int selectID)
        {
            List<ListItem.Item> list = ListItem.List.GetList_Dynamic(serviceBase, where, order);

            //Lay cac dai ly da co trong bang Doanh Thu Dai Ly
            List<ModDT_DaiLyEntity> lstModDT_DaiLyEntity = ModDT_DaiLyService.Instance.CreateQuery().ToList();
            if (lstModDT_DaiLyEntity == null)
                lstModDT_DaiLyEntity = new List<ModDT_DaiLyEntity>();

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                bool bolDisable = false;

                if (list[i].Value != selectID.ToString() && lstModDT_DaiLyEntity.Where(o => o.ModProductAgentId.Equals(ConvertTool.ConvertToInt32(list[i].Value))).SingleOrDefault()!=null)
                    bolDisable = true;

                s += "<option " + (list[i].Value == selectID.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\"" + (bolDisable ? "disabled='disabled'" : "") + ">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowDDLDynamic(dynamic serviceBase, string where, string order, int selectID)
        {
            List<ListItem.Item> list = ListItem.List.GetList_Dynamic(serviceBase, where, order);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == selectID.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowDDLDynamic(dynamic serviceBase, string where, string order, int selectValue,
            string strID, string strParentID)
        {
            List<ListItem.Item> list = ListItem.List.GetList_Dynamic(serviceBase, where, order, strID, strParentID);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == selectValue.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowDDLDynamic(dynamic serviceBase, string where, string order, int selectValue,
            string strID, string strParentID, string strTextName, string strValueName)
        {
            List<ListItem.Item> list = ListItem.List.GetList_Dynamic(serviceBase, where, order, strID, strParentID, strTextName, strValueName);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == selectValue.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">&nbsp; " + list[i].Name + "</option>";
            }

            return s;
        }

        public static string ShowRadioByConfigkey(string configKey, string name, int flag)
        {
            List<ListItem.Item> list = ListItem.List.GetListByConfigkey(configKey);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<input name=\"" + name + "\"" + ((flag & VSW.Core.Global.Convert.ToInt(list[i].Value)) == VSW.Core.Global.Convert.ToInt(list[i].Value) ? "checked=\"checked\"" : string.Empty) + " value=\"" + list[i].Value + "\" type=\"radio\" />" + list[i].Name + " &nbsp;";
            }

            return s;
        }

        public static string ShowCheckBoxByConfigkey(string configKey, string name, int flag)
        {
            List<ListItem.Item> list = ListItem.List.GetListByConfigkey(configKey);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<input name=\"" + name + "\"" + ((flag & VSW.Core.Global.Convert.ToInt(list[i].Value)) == VSW.Core.Global.Convert.ToInt(list[i].Value) ? "checked=\"checked\"" : string.Empty) + " value=\"" + list[i].Value + "\" type=\"checkbox\" />" + list[i].Name + " &nbsp;";
            }

            return s;
        }

        /// <summary>
        ///  Hiển thị checkbox: cho phép chọn nhiều
        /// </summary>
        /// <param name="configKey"></param>
        /// <param name="name"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static string ShowCheckBoxByConfigkey(string configKey, string name, string flag)
        {
            List<ListItem.Item> list = ListItem.List.GetListByConfigkey(configKey);
            // Kiểm tra xem, có cần check giá trị cần select hay không
            bool bolCheckValueSeleted = false;
            List<string> lstValues = null;
            if (!string.IsNullOrEmpty(flag))
            {
                bolCheckValueSeleted = true;
                lstValues = flag.Trim(',').Split(',').ToList();
            }
            
            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<input name=\"" + name + "\"" + ((bolCheckValueSeleted && CheckValueExist(lstValues, list[i].Value)) ? "checked=\"checked\"" : string.Empty);
                s += " value=\"" + list[i].Value + "\" type=\"checkbox\" />" + list[i].Name + " &nbsp;";
            }

            return s;
        }

        private static bool CheckValueExist(List<string> lstValues, string sValue)
        {
            if (lstValues == null || lstValues.Count <= 0)
                return false;

            var exist = lstValues.Where(o => o == sValue);
            if (exist == null)
                return false;

            return true;
        }

        public static string ShowDDLByConfigkey(string configKey, int selectID)
        {
            List<ListItem.Item> list = ListItem.List.GetListByConfigkey(configKey);

            string s = string.Empty;

            for (int i = 0; list != null && i < list.Count; i++)
            {
                s += "<option " + (list[i].Value == selectID.ToString() ? "selected" : string.Empty) + " value=\"" + list[i].Value + "\">" + list[i].Name + "</option>";
            }

            return s;
        }

        public static string DayOfWeekVN(DateTime dt)
        {
            string[] ArrDayOfWeek = "Chủ nhật,Thứ hai,Thứ ba,Thứ tư,Thứ năm,Thứ sáu,Thứ bảy".Split(',');

            return ArrDayOfWeek[(int)dt.DayOfWeek];
        }

        public static string GetMapPage(SysPageEntity Page, string sKey)
        {
            return GetMapPage(Page, sKey, string.Empty);
        }

        public static string GetMapPage(SysPageEntity Page, string Space, string CssClass)
        {
            if (Page == null || Page.Root)
                return string.Empty;

            VSW.Lib.MVC.ViewPage ViewPage = VSW.Core.Web.Application.CurrentViewPage as VSW.Lib.MVC.ViewPage;

            string sMap = string.Empty;

            if (CssClass != string.Empty)
                sMap = string.Format("<a href='{0}' class='{2}'>{1}</a>", ViewPage.GetPageURL(Page), Page.Name, CssClass);
            else
                sMap = string.Format("<a href='{0}'>{1}</a>", ViewPage.GetPageURL(Page), Page.Name);

            SysPageEntity _Parent = SysPageService.Instance.GetByID_Cache(Page.ParentID);

            if (_Parent == null || _Parent.Root)
                return sMap;
            else
                return GetMapPage(_Parent, Space, CssClass) + Space + sMap;
        }

        public static string GetCodeAdv(ModAdvEntity _Adv)
        {
            if (!string.IsNullOrEmpty(_Adv.File))
            {
                if (!string.IsNullOrEmpty(_Adv.URL))
                {
                    if (_Adv.File.ToLower().EndsWith(".swf"))
                    {
                        return GetMedia(0, _Adv.File, _Adv.Name, _Adv.Width, _Adv.Height, null, false, _Adv.AddInTag);
                    }
                    else
                    {
                        return "<a href=\"" + _Adv.URL + "\" target=\"" + _Adv.Target + "\">" + GetMedia(0, _Adv.File, _Adv.Name, _Adv.Width, _Adv.Height, null, false, _Adv.AddInTag) + "</a>";
                    }
                }
                else
                {
                    return GetMedia(0, _Adv.File, _Adv.Name, _Adv.Width, _Adv.Height, null, false, _Adv.AddInTag);
                }
            }

            if (!string.IsNullOrEmpty(_Adv.AdvCode))
                return _Adv.AdvCode;

            return string.Empty;
        }

        public static string GetResizeFile(string File, int TypeResize, int Width, int Height)
        {
            if (string.IsNullOrEmpty(File))
                return string.Empty;

            if (File.ToLower().StartsWith("http"))
                return File;

            string applicationPath = "/" + VSW.Core.Web.HttpRequest.ApplicationPath;

            try
            {
                File = System.Web.HttpUtility.UrlDecode(File);

                string pathFile = HttpContext.Current.Server.MapPath(File);
                string pathTempFile = System.IO.Path.GetDirectoryName(File.ToLower().Replace("~/data/upload/", string.Empty));

                Global.Directory.Create(HttpContext.Current.Server.MapPath("~/Data/ResizeImage/" + pathTempFile));

                string _temp_file = "~/Data/ResizeImage/" + pathTempFile + "/" + Global.File.FormatFileName(System.IO.Path.GetFileNameWithoutExtension(File)) + "x" + Width + "x" + Height + "x" + TypeResize + System.IO.Path.GetExtension(File);
                string _temp_file_full = HttpContext.Current.Server.MapPath(_temp_file);

                if (!System.IO.File.Exists(_temp_file_full))
                {
                    if (System.IO.File.Exists(pathFile))
                    {
                        VSW.Lib.Global.Image.ResizeImageFile(Width, Height, TypeResize, pathFile, _temp_file_full, System.Drawing.Imaging.ImageFormat.Jpeg);

                        if (System.IO.File.Exists(_temp_file_full))
                            return applicationPath + _temp_file.Replace("~/", string.Empty).Replace("\\", "/").Replace("//", "/").Replace("//", "/");
                    }
                }
                else
                    return applicationPath + _temp_file.Replace("~/", string.Empty).Replace("\\", "/").Replace("//", "/").Replace("//", "/");

            }
            catch { }

            return applicationPath + File.Replace("~/", string.Empty);
        }

        public static string GetMedia(int TypeResize, string File, string Alt, int Width, int Height, string CssClass, bool Compression, string AddInTag)
        {
            if (string.IsNullOrEmpty(File))
                return string.Empty;

            string attAlt = null;
            string attCssClass = null;
            string attWidth = null;
            string attHeight = null;

            if (!string.IsNullOrEmpty(Alt))
                attAlt = " alt=\"" + Alt + "\" ";

            if (!string.IsNullOrEmpty(CssClass))
                attCssClass = " class=\"" + CssClass + "\" ";

            if (Width > 0)
                attWidth = " width=\"" + Width + "\" ";

            if (Height > 0)
                attHeight = " height=\"" + Height + "\" ";

            string extFile = System.IO.Path.GetExtension(File).ToUpper();
            if (extFile == ".SWF")
            {
                File = File.Replace("~/", string.Empty);

                if (!File.ToLower().StartsWith("http"))
                    File = "/" + VSW.Core.Web.HttpRequest.ApplicationPath + HttpContext.Current.Server.UrlPathEncode(File);

                string htmlFlash = "<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" border=\"0\" " + attWidth + attHeight + AddInTag + ">";
                htmlFlash += "<param name=\"movie\" value=\"" + File + "\">";
                htmlFlash += "<param name=\"AllowScriptAccess\" value=\"always\">";
                htmlFlash += "<param name=\"quality\" value=\"high\">";
                htmlFlash += "<param name=\"wmode\" value=\"transparent\">";
                htmlFlash += "<embed src=\"" + File + "\" quality=\"high\" wmode=\"transparent\" allowscriptaccess=\"always\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" " + attWidth + attHeight + AddInTag + "></embed>";
                htmlFlash += "</object>";

                return htmlFlash;
            }
            else
            {
                if (!Compression)
                {
                    File = File.Replace("~/", string.Empty);

                    if (!File.ToLower().StartsWith("http"))
                        File = "/" + VSW.Core.Web.HttpRequest.ApplicationPath + HttpContext.Current.Server.UrlPathEncode(File);

                    return "<img " + attAlt + attCssClass + " src=\"" + File + "\" " + attWidth + attHeight + AddInTag + "/>";
                }
                else
                {
                    bool isFix = true;
                    if (!File.ToLower().StartsWith("http"))
                    {
                        if (TypeResize == 4)
                            isFix = false;

                        File = GetResizeFile(File, TypeResize, Width, Height);
                    }

                    return "<img " + attAlt + attCssClass + " src=\"" + File + "\" " + (isFix ? attWidth + attHeight : string.Empty) + AddInTag + "/>";
                }
            }
        }

        public static string GetMedia(string Url, int Width, int Height, string CssClass, bool Compression, string AddInTag)
        {
            return GetMedia(2, Url, null, Width, Height, CssClass, Compression, AddInTag);
        }

        public static string GetMedia(string Url, int Width, int Height, string CssClass, bool Compression)
        {
            return GetMedia(2, Url, null, Width, Height, CssClass, Compression, null);
        }

        public static string GetMedia(string Url, int Width, string CssClass, bool Compression)
        {
            return GetMedia(2, Url, null, Width, 0, CssClass, Compression, null);
        }

        public static string GetMedia(string Url, int Width, int Height, bool Compression)
        {
            return GetMedia(2, Url, null, Width, Height, null, Compression, null);
        }

        public static string GetMedia(string Url, int Width, int Height, string CssClass)
        {
            return GetMedia(2, Url, null, Width, Height, CssClass, true, null);
        }

        public static string GetMedia(string Url, int Width, string CssClass)
        {
            return GetMedia(2, Url, null, Width, 0, CssClass, true, null);
        }

        public static string GetMedia(string Url, int Width, int Height)
        {
            return GetMedia(2, Url, null, Width, Height, null, true, null);
        }

        public static string GetMedia(string Url, int Width)
        {
            return GetMedia(2, Url, null, Width, 0, null, true, null);
        }

        public static string GetMedia(string Url)
        {
            return GetMedia(2, Url, null, 0, 0, null, true, null);
        }

        public string GenCodeByNumber(string sCodeInt)
        {
            string sGetCodeDetail = string.Empty;
            switch (sCodeInt.Length)
            {
                case 1: sGetCodeDetail = "0000" + sCodeInt; break;
                case 2: sGetCodeDetail = "000" + sCodeInt; break;
                case 3: sGetCodeDetail = "00" + sCodeInt; break;
                case 4: sGetCodeDetail = "0" + sCodeInt; break;
            }

            return sGetCodeDetail;
        }

        public static DataTable GetDataByQueryString(dynamic serviceBase, string sQuery)
        {
            DataTable tblData = new DataTable();

            string sSQL = sQuery.Trim();
            if (string.IsNullOrEmpty(sSQL))
                return tblData;

            try
            {
                tblData = serviceBase.ExecuteDataTable(sSQL);
            }
            catch
            {
                return tblData;
            }

            return tblData;
        }

        // Cắt Url dựa vào tham số truyền vào
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

        /// <summary>
        /// Add by CanTV
        /// Lấy đường dẫn hiển thị lên các thẻ Html
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        public static string GetLinkFile(string File)
        {
            File = File.Replace("~/", string.Empty);

            if (!File.ToLower().StartsWith("http"))
                File = "/" + VSW.Core.Web.HttpRequest.ApplicationPath + HttpContext.Current.Server.UrlPathEncode(File);

            return File;
        }
    }
}
