using System;
using System.Collections.Generic;
using System.Data;
using VSW.Lib.Models;


namespace VSW.Lib.Global.ListItem
{
    public class List
    {
        public static List<Item> GetListByConfigkey(string Configkey)
        {
            return GetListByText(VSW.Core.Global.Config.GetValue(Configkey).ToString());
        }

        public static List<Item> GetListByText(string ListText)
        {
            List<Item> list = new List<Item>();

            string[] _Items = ListText.Split(',');
            for (int i = 0; i < _Items.Length; i++)
            {
                if (_Items[i].IndexOf('|') == -1)
                    list.Add(new Item(_Items[i], _Items[i]));
                else
                {
                    string _Name = _Items[i].Split('|')[0];
                    string _Value = _Items[i].Split('|')[1];

                    list.Add(new Item(_Name, _Value));
                }
            }

            return list;
        }

        public static int GetLevel(string _text)
        {
            int level = 0;
            for (int i = 0; i < _text.Length; i++)
            {
                if ((int)_text[i] == 45)
                    level++;
                else
                    break;
            }
            return level / 4;
        }

        public static List<Item> GetListForEdit(List<Item> list, int parent_id)
        {
            List<Item> _list = new List<Item>();

            bool _found = false;
            int level = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (_found && level < GetLevel(list[i].Name))
                    continue;

                if (!_found && list[i].Value == parent_id.ToString())
                {
                    _found = true;
                    level = GetLevel(list[i].Name);

                    continue;
                }

                _list.Add(list[i]);
            }

            return _list;
        }

        public static Item FindByName(List<Item> list, string name)
        {
            Item obj = list.Find(s => s.Name == name);

            return obj ?? new Item(string.Empty, string.Empty);
        }

        private static List<Item> _list = null;
        public static List<Item> GetList(object serviceBase)
        {
            return GetList(serviceBase, 0, string.Empty, string.Empty);
        }

        public static List<Item> GetList(object serviceBase, int langID)
        {
            return GetList(serviceBase, langID, string.Empty, string.Empty);
        }

        public static List<Item> GetList(object serviceBase, int langID, string type)
        {
            return GetList(serviceBase, langID, type, string.Empty);
        }

        public static List<Item> GetList(object serviceBase, int langID, string type, bool bolOrderBy_MA_PHAN_CAP)
        {
            return GetList(serviceBase, langID, type, string.Empty);
        }

        public static List<Item> GetList(dynamic serviceBase,
            int langID,
            string type,
            string where)
        {
            _list = new List<Item>();

            bool langUnAbc = VSW.Core.Global.Config.GetValue("Mod.LangUnABC").ToBool();

            string sSQL = "SELECT [ID],[Name] " + (langUnAbc ? "+ ' [' + ISNULL([Code],'-') + ']' AS [Name]" : "") + ",[ParentID],[Order] FROM " + serviceBase.TableName + " WHERE Activity=1";

            if (langID > 0)
                sSQL += " AND [LangID]=" + langID;

            if (type != string.Empty)
                sSQL += " AND [Type]='" + type + "'";

            if (where != string.Empty)
                sSQL += " AND " + where;

            sSQL += " ORDER BY [ID]";

            DataTable dtData = serviceBase.ExecuteDataTable(sSQL);

            BuildItem(dtData, 0, string.Empty);

            return _list;
        }

        public static List<Item> GetList(dynamic serviceBase,
            int langID,
            string type,
            string where, bool bolOrderBy_MA_PHAN_CAP)
        {
            _list = new List<Item>();

            bool langUnAbc = VSW.Core.Global.Config.GetValue("Mod.LangUnABC").ToBool();

            string sSQL = "SELECT [CurrentCode],[Name] " + (langUnAbc ? "+ ' [' + ISNULL([Code],'-') + ']' AS [Name]" : "") + ",[ParentID],[Order] FROM " + serviceBase.TableName + " WHERE 1=1";

            if (langID > 0)
                sSQL += " AND [LangID]=" + langID;

            if (type != string.Empty)
                sSQL += " AND [Type]='" + type + "'";

            if (where != string.Empty)
                sSQL += " AND " + where;

            sSQL += " ORDER BY [CurrentCode]";

            DataTable dtData = serviceBase.ExecuteDataTable(sSQL);

            BuildItem(dtData, "0", string.Empty);

            return _list;
        }

        public static List<Item> GetList(dynamic serviceBase, DataTable tblData)
        {
            _list = new List<Item>();

            BuildItem(tblData, 0, string.Empty);

            return _list;
        }

        /// <summary>
        /// Add by CanTV: Lấy ra danh sách danh mục theo tên
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static List<Item> GetList_TypeList(string table, string selectField, string where, string order)
        {
            _list = new List<Item>();

            string sSQL = "SELECT " + selectField + " FROM " + table;

            if (!string.IsNullOrEmpty(where))
                sSQL += " WHERE " + where;

            if (!string.IsNullOrEmpty(order))
                sSQL += " ORDER BY " + order;

            DataTable dtData = WebMenuService.Instance.ExecuteDataTable(sSQL);

            // selectField: Name,Value
            BuildItem_TypeList(selectField, dtData);

            return _list;
        }

        /// <summary>
        /// Lấy danh mục phân cấp theo ID truyền vào
        /// </summary>
        /// <param name="serviceBase"></param>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public static List<Item> GetList(object serviceBase, string strQuery)
        {
            _list = new List<Item>();
            DataTable dtData = WebMenuService.Instance.ExecuteDataTable(strQuery);

            // selectField: Name,Value
            BuildItem(dtData, 0, string.Empty);

            return _list;
        }

        /// <summary>
        /// Create By CanTV: Tạo danh sách các bản ghi (Danh sách thuần, không phân cấp)
        /// </summary>
        /// <param name="selectField">Có cấu trúc: Nam,Value</param>
        /// <param name="dtData"></param>
        private static void BuildItem_TypeList(string selectField, DataTable dtData)
        {
            if (dtData == null)
                return;

            string[] ArrField = selectField.Split(',');
            if (ArrField == null || ArrField.Length < 1)
                return;

            string sFieldName = ArrField[0];
            string sFieldValue = ArrField[1];

            try
            {
                // Mặc định là không có bản ghi nào được chọn
                _list.Add(new Item("----- Chọn -----", "0"));

                foreach (DataRow item in dtData.Rows)
                {
                    _list.Add(new Item(VSW.Core.Global.Convert.ToString(item[sFieldName]),
                                            VSW.Core.Global.Convert.ToString(item[sFieldValue])));
                }
            }
            catch
            {
                return;
            }
        }

        private static void BuildItem(DataTable dtData, int parentID, string space)
        {
            DataRow[] dr = dtData.Select("ParentID=" + parentID, "Order");

            for (int i = 0; i < dr.Length; i++)
            {
                string _text = space + " " + dr[i]["Name"].ToString().Trim();

                parentID = VSW.Core.Global.Convert.ToInt(dr[i]["ID"]);

                _list.Add(new Item(_text, parentID.ToString()));

                BuildItem(dtData, parentID, space + "----");
            }
        }

        /// <summary>
        /// Add by CanTV
        /// </summary>
        private static void BuildItem(DataTable dtData, string parentCode, string space)
        {
            DataRow[] dr = dtData.Select("ParentCode='" + parentCode + "'", "Order");

            for (int i = 0; i < dr.Length; i++)
            {
                string _text = space + " " + dr[i]["Name"].ToString().Trim();

                parentCode = VSW.Core.Global.Convert.ToString(dr[i]["ParentCode"]);

                _list.Add(new Item(_text, parentCode));

                BuildItem(dtData, parentCode, space + "----");
            }
        }

        private static void BuildItem(DataTable dtData, int parentID, string space, string Order)
        {
            DataRow[] dr = dtData.Select("ParentID=" + parentID, string.IsNullOrEmpty(Order) ? "ID" : Order);

            for (int i = 0; i < dr.Length; i++)
            {
                string _text = space + " " + dr[i]["Name"].ToString().Trim();

                parentID = VSW.Core.Global.Convert.ToInt(dr[i]["ID"]);

                _list.Add(new Item(_text, parentID.ToString()));

                BuildItem(dtData, parentID, space + "----", Order);
            }
        }

        private static void BuildItem(DataTable dtData, int parentID, string space, string Order, string strID, string strParentID)
        {
            DataRow[] dr = dtData.Select(parentID <= 0 ? (strParentID + " IS NULL OR " + strParentID + "=" + parentID) : strParentID + "=" + parentID, string.IsNullOrEmpty(Order) ? strID : Order);

            for (int i = 0; i < dr.Length; i++)
            {
                string _text = space + " " + dr[i]["Name"].ToString().Trim();

                parentID = VSW.Core.Global.Convert.ToInt(dr[i][strID]);

                _list.Add(new Item(_text, parentID.ToString()));

                BuildItem(dtData, parentID, space + "----", Order, strID, strParentID);
            }
        }

        private static void BuildItem(DataTable dtData, int parentID, string space, string Order, string strID, string strParentID, string strTextName, string strValueName)
        {
            if (string.IsNullOrEmpty(strTextName))
                strTextName = "Name";

            if (string.IsNullOrEmpty(strValueName))
                strValueName = "ID";


            DataRow[] dr = dtData.Select(parentID <= 0 ? (strParentID + " IS NULL OR " + strParentID + "=" + parentID) : strParentID + "=" + parentID, 
                string.IsNullOrEmpty(Order) ? strID : Order);

            for (int i = 0; i < dr.Length; i++)
            {
                string _text = space + " " + dr[i][strTextName].ToString().Trim();

                parentID = VSW.Core.Global.Convert.ToInt(dr[i][strID]);

                _list.Add(new Item(_text, dr[i][strValueName].ToString()));

                BuildItem(dtData, parentID, space + "----", Order, strID, strParentID, strTextName, strValueName);
            }
        }

        public static List<Item> GetList_Modified(dynamic serviceBase, string where, string order)
        {
            _list = new List<Item>();

            string sSQL = "SELECT [ID],[Name],[ParentID],[CodeParent] FROM " + serviceBase.TableName
                + " WHERE Activity=1 "
                + " AND 1=1";

            if (where != string.Empty)
                sSQL += " AND " + where;

            sSQL += " ORDER BY [ID]";

            DataTable dtData = serviceBase.ExecuteDataTable(sSQL);

            BuildItem(dtData, 0, string.Empty, "CodeParent");

            return _list;
        }

        public static List<Item> GetList_Dynamic(dynamic serviceBase, string where, string order)
        {
            if (string.IsNullOrEmpty(order))
                order = "[ID]";

            _list = new List<Item>();

            string sSQL = "SELECT [ID],[Name],[ParentID] FROM " + serviceBase.TableName
                + " WHERE Activity=1 "
                + " AND 1=1";

            if (where != string.Empty)
                sSQL += " AND " + where;

            sSQL += " ORDER BY " + order;

            DataTable dtData = serviceBase.ExecuteDataTable(sSQL);

            BuildItem(dtData, 0, string.Empty, order);

            return _list;
        }


        public static List<Item> GetList_Dynamic(dynamic serviceBase, string where, string order, 
            string strID, string strParentID)
        {
            if (string.IsNullOrEmpty(order))
                order = "[ID]";

            _list = new List<Item>();

            string sSQL = "SELECT [" + strID + "],[Name],[" + strParentID + "] FROM " + serviceBase.TableName
                + " WHERE 1=1";

            if (where != string.Empty)
                sSQL += " AND " + where;

            sSQL += " ORDER BY " + order;

            DataTable dtData = serviceBase.ExecuteDataTable(sSQL);

            BuildItem(dtData, 0, string.Empty, order, strID, strParentID);

            return _list;
        }

        public static List<Item> GetList_Dynamic(dynamic serviceBase, string where, string order,
            string strID, string strParentID, string strTextName, string strValueName)
        {
            if (string.IsNullOrEmpty(order))
                order = "[ID]";

            _list = new List<Item>();

            string sSQL = "SELECT * FROM " + serviceBase.TableName
                + " WHERE 1=1";

            if (where != string.Empty)
                sSQL += " AND " + where;

            sSQL += " ORDER BY " + order;

            DataTable dtData = serviceBase.ExecuteDataTable(sSQL);

            BuildItem(dtData, 0, string.Empty, order, strID, strParentID, strTextName, strValueName);

            return _list;
        }
    }
}