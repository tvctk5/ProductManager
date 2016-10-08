<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<%
    var model = ViewBag.Model as ModProduct_SlideShowModel;
    var listItem = ViewBag.Data as List<ModProduct_SlideShowEntity>;
%>

<form id="vswForm" name="vswForm" method="post">

<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />

<div id="toolbar-box">
    <div class="t"><div class="t"><div class="t"></div></div></div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>Product_ slide show</h2>
        </div>
        <div class="clr"></div>
    </div>
    <div class="b"><div class="b"> <div class="b"></div></div></div>
</div>
<div class="clr"></div>

<script type="text/javascript">

    var VSWController = 'ModProduct_SlideShow';

    var VSWArrVar = [ 
                        'limit', 'PageSize'
                   ];


    var VSWArrQT = [
                      '<%= model.PageIndex + 1 %>', 'PageIndex', 
                      '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
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
                </td>
                <td nowrap="nowrap">
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
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Product info id", "ProductInfoId")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_01", "Image_01")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_02", "Image_02")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_03", "Image_03")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_04", "Image_04")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_05", "Image_05")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_06", "Image_06")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_07", "Image_07")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_08", "Image_08")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_09", "Image_09")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Image_10", "Image_10")%>
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
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].ID%>
                    </td>
                    <td align="center">
                       <%= string.Format("{0:#,##0}", listItem[i].ProductInfoId)%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_01%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_02%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_03%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_04%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_05%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_06%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_07%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_08%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_09%>
                    </td>
                    <td align="center">
                       <%= listItem[i].Image_10%>
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
