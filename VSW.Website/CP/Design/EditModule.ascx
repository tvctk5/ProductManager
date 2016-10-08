<%@ Control Language="C#" AutoEventWireup="true"%>

<script runat="server">
    public VSW.Core.Global.Class CurrentObject { get; set; }
    public SysPageEntity CurrentPage { get; set; }
    public VSW.Lib.MVC.ModuleInfo CurrentModule { get; set; }
    public int LangID { get; set; }
</script>

<div class="col width-100">
  <table class="admintable">
     <%for (int i = 0; CurrentObject != null && i < CurrentObject.GetFields_Name().Length; i++)
        {
            string fieldName = CurrentObject.GetFields_Name()[i];

            System.Reflection.FieldInfo _FieldInfo = CurrentObject.GetFieldInfo(fieldName);
            object[] attributes = _FieldInfo.GetCustomAttributes(typeof(VSW.Core.MVC.PropertyInfo), true);
            if (attributes == null || attributes.GetLength(0) == 0)
                continue;

            VSW.Core.MVC.PropertyInfo propertyInfo = (VSW.Core.MVC.PropertyInfo)attributes[0];

            string currentID = CurrentModule.Code + "_" + fieldName;
            string currentValue = string.Empty;
            bool bolMutiline = false;

            if (CurrentPage != null)
                currentValue = CurrentPage.Items.GetValue(CurrentModule.Code + "." + fieldName).ToString();

            if (currentValue == string.Empty)
                currentValue = (CurrentObject.GetField(fieldName) == null ? string.Empty : CurrentObject.GetField(fieldName).ToString());

            List<VSW.Lib.Global.ListItem.Item> _List = null;
           
            if (propertyInfo.Value is string)
                _List = VSW.Lib.Global.ListItem.List.GetListByText(propertyInfo.Value.ToString());
            
            if (_List != null && VSW.Lib.Global.ListItem.List.FindByName(_List, "ConfigKey").Value != string.Empty)
            {
                _List = VSW.Lib.Global.ListItem.List.GetListByText("," + VSW.Core.Global.Config.GetValue(VSW.Lib.Global.ListItem.List.FindByName(_List, "ConfigKey").Value).ToString());
            }

            // Add by CanTV
            // Nếu là dạng kiểu danh mục: định dạng sẽ là: List|"Table Name"#"List Field Select (Insert &)"#"Where"#"Order by"
            // ex: [VSW.Core.MVC.PropertyInfo("TEST", "List|Mod_Product_Manufacturer#Name&ID#1=1#Name")]

            if (_List != null && VSW.Lib.Global.ListItem.List.FindByName(_List, "List").Value != "")
            {
                string listName = string.Empty;
                if (_List != null)
                    listName = VSW.Lib.Global.ListItem.List.FindByName(_List, "List").Value;

                string[] ArrListName = listName.Split('#');

                _List = VSW.Lib.Global.ListItem.List.GetList_TypeList(ArrListName[0], ArrListName[1].Replace("&", ","), ArrListName[2], ArrListName[3]);
            }

            // Kiểu Mutiline
            if (_List != null && VSW.Lib.Global.ListItem.List.FindByName(_List, "Mutiline").Value != "")
            {
                bolMutiline = true;
                _List = null;
            }

            if (fieldName.IndexOf("PageID") > -1)
            {
                _List = VSW.Lib.Global.ListItem.List.GetList(SysPageService.Instance);
                _List.Insert(0, new VSW.Lib.Global.ListItem.Item(string.Empty, string.Empty));
            }

            if (fieldName.IndexOf("MenuID") > -1)
            {
                bool HasContinue = false;
                string menuType = string.Empty;
                if (_List != null)
                {
                    menuType = VSW.Lib.Global.ListItem.List.FindByName(_List, "Type").Value;
                    HasContinue = !VSW.Core.Global.Convert.ToBool(VSW.Lib.Global.ListItem.List.FindByName(_List, "Show").Value);
                }

                if (HasContinue)
                    continue;
                else
                {
                    _List = VSW.Lib.Global.ListItem.List.GetList(WebMenuService.Instance, LangID, menuType);
                    _List.Insert(0, new VSW.Lib.Global.ListItem.Item(string.Empty, string.Empty));
                }
            } %>

            <tr id='tr_<%= currentID %>'>
               <td class="key"><%= propertyInfo.Key %> :</td>
               <td>

                   <%if (_List != null)
                    {%>
                        <select id="<%= currentID %>" name="<%= currentID %>" style="  width: 81.5% !important;" class="text_input" />
                            <%for (int j = 0; j < _List.Count; j++)
                            {%>
                                <option <%=(currentValue == _List[j].Value ? "selected" : "") %> value='<%= _List[j].Value%>'><%= _List[j].Name %></option>
                            <%}%>
                        </select>
                    <%}
                    else
                    {
                     if (bolMutiline)
                      {%>
                        <textarea rows="3" cols="10"  id='<%= currentID%>' name="<%= currentID%>" class="text_input"
                        style="width:80% !important;"><%=currentValue %></textarea>
                      <%}else {%>
                        <input id="<%= currentID%>" name="<%= currentID%>" type="text" class="text_input"
                            style="width: 80%;" value="<%=currentValue %>" />
                            <%} %>

                        <%if (fieldName.StartsWith("NewsID")){%>
                            &nbsp;<input type="image" class="vsw-icon" onclick="ShowNewsForm('<%= currentID %>',<%= currentValue %>);return false;" src="/{CPPath}/Content/add/img/search-16.gif"/>
                        <%}else if (fieldName.StartsWith("Text")){
                              VSW.Lib.Global.Session.SetValue(currentID, currentValue);
                              %>
                            &nbsp;<input type="image" class="vsw-icon" onclick="ShowTextForm('<%= currentID %>','<%= currentValue %>');return false;" src="/{CPPath}/Content/add/img/search-16.gif" />
                        <%}else if (fieldName.StartsWith("File")){%>
                            &nbsp;<input type="image" class="vsw-icon" onclick="ShowFileForm('<%= currentID %>','<%= currentValue %>');return false;" src="/{CPPath}/Content/add/img/search-16.gif"  />
                        <%}%>

                    <%}%>

                   <input type="image" class="vsw-icon" onclick="UpdateCustom('<%= currentID %>','');return false;" src="/{CPPath}/Content/add/img/save-16.gif" title="Cập nhật lên mã thiết kế"/>
              </td>
          </tr>
      <%} %>
  </table>
</div>