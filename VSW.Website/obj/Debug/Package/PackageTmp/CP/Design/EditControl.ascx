<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Core.Design.ViewControlDesign" %>
<script src="/{CPPath}/Content/plugins/jQuery/jquery-3.1.1.min.js" type="text/javascript"></script>
<div id="to_hlid_<%= ViewControlName%>$<%=CphName %>" style="height: 10px; width: 100%"
    ondragenter="return dragEnter(event)" ondrop="return dragDrop(event)" ondragover="return dragOver(event)">
</div>
<div class="border-control">
    <table id="<%= ViewControlName%>$<%=CphName %>" <%if (!AloneMode){ %>style="cursor: move;"
        draggable="true" ondragstart="return dragStart(event)" <%} %> class="admintable"
        style="width: 100%;">
        <tr>
            <td class="key" style="width: 100%; text-align: center;" colspan="2">
                <a onclick="do_display('tbl_<%= ViewControlName%>');" href="javascript:;">
                    <%= ViewControlID%>
                    <%if (CurrentModule != null)
                      { %>
                    -
                    <%= ((VSW.Lib.MVC.ModuleInfo)CurrentModule).Name %><%}
                      else if (ErrorMode)
                      { %>
                    - Không tồn tại
                    <%} %></a>
            </td>
        </tr>
    </table>
    <table id="tbl_<%= ViewControlName%>" class="admintable" style="width: 100%; display: none;">
        <%
            string LayoutDefine = string.Empty;
            string LayoutValue = string.Empty;
            bool bolMutiline = false;
            bool bolCheckBox = false;

            for (int i = 0; CurrentObject != null && i < CurrentObject.GetFields_Name().Length; i++)
            {
                string fieldName = CurrentObject.GetFields_Name()[i];
                string currentTitle = fieldName;
                bolMutiline = false;
                bolCheckBox = false;

                List<VSW.Lib.Global.ListItem.Item> _List = null;

                System.Reflection.FieldInfo _PropertyInfo = CurrentObject.GetFieldInfo(fieldName);

                object[] attributes = _PropertyInfo.GetCustomAttributes(typeof(VSW.Core.MVC.PropertyInfo), true);
                if (attributes == null || attributes.GetLength(0) == 0)
                    continue;

                VSW.Core.MVC.PropertyInfo propertyInfo = (VSW.Core.MVC.PropertyInfo)attributes[0];

                currentTitle = propertyInfo.Key;

                if (fieldName == "LayoutDefine")
                {
                    LayoutDefine = currentTitle;
                    continue;
                }

                if (propertyInfo.Value is string)
                    _List = VSW.Lib.Global.ListItem.List.GetListByText(propertyInfo.Value.ToString());

                if (_List != null && VSW.Lib.Global.ListItem.List.FindByName(_List, "ConfigKey").Value != "")
                {
                    // Kiểm tra xem loại hiển thị config này có hiển thị dạng check box không
                    if (VSW.Lib.Global.ListItem.List.FindByName(_List, "SelectType").Value == "Checkbox")
                        bolCheckBox = true;
                    
                    _List = VSW.Lib.Global.ListItem.List.GetListByText("," + VSW.Core.Global.Config.GetValue(VSW.Lib.Global.ListItem.List.FindByName(_List, "ConfigKey").Value));
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
                    if (ViewPageDesign.PageViewState["EditControl_PageID_" + LangID] != null)
                    {
                        _List = ViewPageDesign.PageViewState["EditControl_PageID_" + LangID] as List<VSW.Lib.Global.ListItem.Item>;
                    }
                    else
                    {
                        _List = VSW.Lib.Global.ListItem.List.GetList(SysPageService.Instance, LangID);
                        _List.Insert(0, new VSW.Lib.Global.ListItem.Item(string.Empty, string.Empty));

                        ViewPageDesign.PageViewState["EditControl_PageID_" + LangID] = _List;
                    }
                }

                if (fieldName.IndexOf("MenuID") > -1)
                {
                    string typeMenu = string.Empty;
                    if (_List != null)
                        typeMenu = VSW.Lib.Global.ListItem.List.FindByName(_List, "Type").Value;

                    string keyViewState = "EditControl_MenuID_" + typeMenu + "_" + LangID;

                    if (ViewPageDesign.PageViewState[keyViewState] != null)
                    {
                        _List = ViewPageDesign.PageViewState[keyViewState] as List<VSW.Lib.Global.ListItem.Item>;
                    }
                    else
                    {
                        _List = VSW.Lib.Global.ListItem.List.GetList(WebMenuService.Instance, LangID, typeMenu);
                        _List.Insert(0, new VSW.Lib.Global.ListItem.Item(string.Empty, string.Empty));

                        ViewPageDesign.PageViewState[keyViewState] = _List;
                    }
                } 
                
                string currentID = ViewControlID + "_" + fieldName;
                string currentValue = GetValueCustom(fieldName);

                if (currentValue == string.Empty)
                    currentValue = (CurrentObject.GetField(fieldName) == null ? string.Empty : CurrentObject.GetField(fieldName).ToString());
                
        %>
        <tr id="tr_<%= currentID%>">
            <td class="key" style="font-size: 12px; font-weight: normal; width: 35%;">
                <%= currentTitle%>
                :
            </td>
            <td>
                <%if (_List != null)
                  { %>
                  <% if (bolCheckBox == true)
                     {%>
                            <%
                                List<string> Values = string.IsNullOrEmpty(currentValue) ? new List<string>() : currentValue.Trim(',').Split(',').ToList();
                                for (int j = 0; j < _List.Count; j++)
                              {
                                  if (string.IsNullOrEmpty(_List[j].Value))
                                      continue;
                                  bool bolExists = Values.Where(o => o == _List[j].Value).Any();
                                    %>
                                <input type="checkbox" id="<%= j %>" checkboxvalue="checkboxvalue" data-id="<%= currentID%>" data-name="<%= currentID%>" value='<%=_List[j].Value %>' <%if(bolExists) {%>checked<%} %> onchange="checkboxChange(this);"/> <%= _List[j].Name%> 
                            <%} %>
                            <input type="text" name="<%= currentID%>" id="<%= currentID%>" value="<%=currentValue %>" />
                  <%}
                     else
                     { %>
                        <select id="<%= currentID%>" name="<%= currentID%>" style="width: 99%;" class="text_input">
                            <%for (int j = 0; j < _List.Count; j++)
                              { %>
                            <option <%if(currentValue==_List[j].Value) {%>selected<%} %> value='<%= _List[j].Value%>'>
                                <%= _List[j].Name%></option>
                            <%} %>
                        </select>
                <%}
                  }
                  else
                  { %>
                <%if (fieldName.IndexOf("Text") > -1)
                  { %>
                <input id="<%= currentID%>" name="<%= currentID%>" type="text" class="text_input"
                    style="width: 100px" value="<%=currentValue %>" />
                <%}
                  else
                  {
                      if (bolMutiline)
                      {%>
                        <textarea rows="3" cols="15"  id="<%= currentID%>" name="<%= currentID%>" class="text_input"><%=currentValue %></textarea>
                      <%}else {%>
                        <input id="<%= currentID%>" name="<%= currentID%>" type="text" class="text_input"
                            style="width: 80%;" value="<%=currentValue %>" />
                            <%} %>
                <%} %>
                <%if (fieldName.IndexOf("NewsID") > -1)
                  { %>
                <input type="image" class="vsw-icon" onclick="ShowNewsForm('<%= currentID%>',<%=currentValue %>);return false;"
                    src="/{CPPath}/Content/add/img/search-16.gif" />
                <%} %>
                <%if (fieldName.IndexOf("Text") > -1)
                  {
                      VSW.Lib.Global.Session.SetValue(currentID, currentValue);
                          
                %>
                <input type="image" class="vsw-icon" onclick="ShowTextForm('<%= currentID%>','');return false;"
                    src="/{CPPath}/Content/add/img/search-16.gif" />
                <%} %>
                <%if (fieldName.IndexOf("File") > -1)
                  { %>
                <input type="image" class="vsw-icon" onclick="ShowFileForm('<%= currentID%>','<%=currentValue %>');return false;"
                    src="/{CPPath}/Content/add/img/search-16.gif" />
                <%} %>
                <%} %>
            </td>
        </tr>
        <%} %>
        <%if (CurrentModule != null)
          {
              string path = Server.MapPath("~/Views/" + CurrentModule.Code);
              if (System.IO.Directory.Exists(path))
              {
                  string[] ArrFiles = System.IO.Directory.GetFiles(path);

                  List<VSW.Lib.Global.ListItem.Item> _List = new List<VSW.Lib.Global.ListItem.Item>();
                  foreach (string file in ArrFiles)
                  {
                      string s = System.IO.Path.GetFileNameWithoutExtension(file);
                      _List.Add(new VSW.Lib.Global.ListItem.Item(s, s));
                  }

                  string currentValue = GetValueCustom("ViewLayout");

                  if (currentValue == string.Empty)
                      currentValue = (CurrentObject.GetProperty("ViewLayout") == null ? string.Empty : CurrentObject.GetProperty("ViewLayout").ToString());

                  LayoutValue = currentValue;  
        %>
        <tr id="tr_<%= ViewControlID%>_ViewLayout">
            <td class="key" style="font-size: 12px; font-weight: normal; width: 35%;">
                Cách hiển thị :
            </td>
            <td>
                <select id="<%= ViewControlID%>_ViewLayout" name="<%= ViewControlID%>_ViewLayout"
                    <%if(LayoutDefine != ""){ %>onchange="layout_change('<%= ViewControlID%>','<%=LayoutDefine %>',this.value)"
                    <%} %> style="width: 99%;" class="text_input">
                    <%for (int j = 0; j < _List.Count; j++)
                      { %>
                    <option <%if(currentValue==_List[j].Value) {%>selected<%} %> value='<%= _List[j].Value%>'>
                        <%= _List[j].Name%></option>
                    <%} %>
                </select>
            </td>
        </tr>
        <%}
          } %>
        <tr>
            <td align="right" style="padding-top: 2px; padding-right: 5px;" class="cmd-control"
                colspan="2">
                <%if (!AloneMode)
                  { %>
                <%if ((PosID & 1) != 1)
                  { %>
                <a href="javascript:cp_update('<%= ViewControlID%>|up');">
                    <img alt="Up" src="/{CPPath}/Content/add/img/up-16.gif" />
                </a>
                <%} %>
                <%if ((PosID & 4) != 4)
                  { %>
                <a href="javascript:cp_update('<%= ViewControlID%>|down');">
                    <img alt="Down" src="/{CPPath}/Content/add/img/down-16.gif" />
                </a>
                <%} %>
                <%} %>
                <%if (!ReadOnlyMode)
                  { %>
                <a href="javascript:cp_update('<%= ViewControlID%>|save');">
                    <img alt="Save" src="/{CPPath}/Content/add/img/save-16.gif" />
                </a>
                <%} %>
                <%if (!AloneMode)
                  { %>
                <a onclick="return confirm('Bạn chắc là mình muốn xóa chứ !');" href="javascript:cp_update('<%= ViewControlID%>|delete');">
                    <img alt="Delete" src="/{CPPath}/Content/add/img/delete-16.gif" />
                </a>
                <%} %>
            </td>
        </tr>
    </table>
</div>
<%if (LayoutDefine != string.Empty)
  { %>
<script type="text/javascript">
    window.parent.ListOnLoad.push({ pid: '<%= ViewControlID%>', list_param: '<%=LayoutDefine %>', layout: '<%=LayoutValue %>' });
//    $(document).ready(function () {
//        $(".boder-template").on("change", "input", function () {
//            alert("awrara");
//        });
//    });

    
</script>
<%} %>
