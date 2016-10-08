<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<script runat="server">
   
    private List<EntityBase> AutoGetMap(SysPageModel model)
    {
        List<EntityBase> list = new List<EntityBase>();

        int _id = model.ParentID;
        while (_id > 0)
        {
            SysPageEntity _Menu = SysPageService.Instance.GetByID(_id);

            if (_Menu == null)
                break;

            _id = _Menu.ParentID;

            list.Insert(0, _Menu);
        }

        return list;
    }
</script>

<% 
    var model = ViewBag.Model as SysPageModel;
    var listItem = ViewBag.Data as List<SysPageEntity>;
 %>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%= GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-menu">
            <h2>Trang</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'SysPage';

    var VSWArrVar = [
                    'filter_lang', 'LangID',
                    'limit', 'PageSize'
                   ];

    var VSWArrQT = [
                    '<%= model.PageIndex + 1 %>', 'PageIndex',
                    '<%= model.ParentID %>', 'ParentID',
                    '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '1', 'LangID',
                    '20', 'PageSize'
                  ];
</script>
        
<%= ShowMessage()%>

<div id="element-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
                        
        <table>
            <tr>
                <td width="100%">
                    <%= ShowMap(AutoGetMap(model))%>
                </td>
                <td nowrap="nowrap">
                    Ngôn ngữ : <%= ShowDDLLang(model.LangID)%>
                </td>
            </tr>
        </table>

        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        #
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th width="1%">
                        #
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tên", "Name")%>
                    </th>
                    <%if (model.ParentID > 0)
                      {%>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Mã", "Code")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Chức năng", "ModuleCode")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Mẫu giao diện", "TemplateID")%>
                    </th>
                    <%} %>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Duyệt", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Menu", "ViewInMenu")%>
                    </th>
                                        <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ViewInSiteMap", "ViewInSiteMap")%>
                    </th>
                    
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Sắp xếp", "Order")%>
                        <a href="javascript:vsw_exec_cmd('saveorder')" class="saveorder" title="Lưu sắp xếp"></a>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="15">
                        <del class="container">
                            <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord)%>
                        </del>
                    </td>
                </tr>
            </tfoot>
            <tbody>
               <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                 { 
                     var moduleInfo = SysModuleService.Instance.VSW_Core_GetByCode(listItem[i].ModuleCode) as VSW.Lib.MVC.ModuleInfo;%>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="center">
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                          <img src="/{CPPath}/Content/add/img/edit_f2.png" width="15px" />
                        </a>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Index', <%= listItem[i].ID %>, 'ParentID')"><%= listItem[i].Name%></a>
                    </td>
                    <%if (model.ParentID > 0)
                      {%>
                    <td align="center">
                       <%= listItem[i].Code %>
                    </td>
                    <td align="center">
                       <%= moduleInfo == null ? string.Empty : moduleInfo.Name%>
                    </td>
                    <td align="center">
                       <%= GetName(SysTemplateService.Instance.GetByID(listItem[i].TemplateID))%>
                    </td>
                    <%} %>
                    <td align="center">
                       <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td align="center">
                       <%= GetViewInMenu(listItem[i].ID, listItem[i].ViewInMenu)%>
                    </td>  
                    <td align="center">
                       <%= GetViewInSiteMap(listItem[i].ID, listItem[i].ViewInSiteMap)%>
                    </td>    
                                     
                    <td class="order">
                        <%= GetOrder(listItem[i].ID, listItem[i].Order)%>
                    </td>
                    <td align="center">
                        <%= listItem[i].ID%>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
                            
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"><div class="b"></div></div></div>
</div>

</form>