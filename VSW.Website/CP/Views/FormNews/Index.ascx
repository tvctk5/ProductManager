<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<% 
    var model = ViewBag.Model as ModNewsModel;
    var listItem = ViewBag.Data as List<ModNewsEntity>;
 %>

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            
        </div>
        <div class="pagetitle icon-48-article">
            <h2>Bài viết</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'FormNews';

    var VSWArrVar = [
                    'filter_menu', 'MenuID',
                    'filter_state', 'State',
                    'filter_lang', 'LangID',
                    'limit', 'PageSize'
                   ];

    var VSWArrVar_QS = [
                        'filter_search', 'SearchText'
                      ];

    var VSWArrQT = [
                    '<%= model.PageIndex + 1 %>', 'PageIndex',
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
                     Lọc: <input type="text" value="<%= model.SearchText %>" id="filter_search" class="text_area" onchange="VSWRedirect();return false;" />
                    <button onclick="VSWRedirect();return false;">Tìm kiếm</button>
                </td>
                <td nowrap="nowrap">
                   Chuyên mục : 
                    <select id="filter_menu" onchange="VSWRedirect()" class="inputbox" size="1">
                         <option value="0">(Tất cả)</option>
                         <%= Utils.ShowDDLMenuByType("News", model.LangID, model.MenuID)%>
                    </select>
                   Vị trí :
                   <select id="filter_state" onchange="VSWRedirect()" class="inputbox" size="1">
                         <option value="0">(Tất cả)</option>
                         <%= Utils.ShowDDLByConfigkey("Mod.NewsState", model.State)%>
                    </select>
                   Ngôn ngữ :<%= ShowDDLLang(model.LangID)%>
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
                        Chọn
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tiêu đề", "Name")%>
                    </th>
                    <th style="width:40px" nowrap="nowrap">
                        <%= GetSortLink("Ảnh", "File")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Chuyên mục", "MenuID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Xuất bản", "Published")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Duyệt", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                         <%= GetSortLink("Sắp xếp", "Order")%>
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
                 { %>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center">
                        <a href="javascript:Close(<%= listItem[i].ID %>)">Chọn</a>
                    </td>
                    <td>
                        <%= listItem[i].Name%>
                    </td>
                    <td align="center">
                       <%= Utils.GetMedia(listItem[i].File, 40, 40)%>
                    </td>
                    <td align="center">
                       <%= GetName(listItem[i].getMenu()) %>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:dd/MM/yyyy HH:mm}", listItem[i].Published) %>
                    </td>
                    <td align="center">
                       <span class="jgrid">
                           <span class="state <%= listItem[i].Activity ? "publish" : "unpublish" %>"></span>
                       </span>
                    </td>
                    <td class="order">
                        <%= listItem[i].Order%>
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