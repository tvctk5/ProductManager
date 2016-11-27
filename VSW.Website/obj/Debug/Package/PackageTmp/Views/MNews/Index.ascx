<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<%
    var listItem = ViewBag.Data as List<ModNewsEntity>;
    var model = ViewBag.Model as MNewsModel;
%>

<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl">
            <%= ViewPage.CurrentPage.Name %></h1>
    </div>
    <div class="boxvien" style="padding: 10px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
          <%for (int i = 0; listItem != null && i < listItem.Count; i++ )
                        {
                            string Url = ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code);
                            %>
            <tr>
                <td width="130" style="border-bottom: #999999 dotted 1px; padding-bottom: 5px; padding-top: 5px;">
                    <a href="<%=Url %>">
                      <%if (!string.IsNullOrEmpty(listItem[i].File))
                            { %>
                            <img src="<%=Utils.GetResizeFile(listItem[i].File, 2, 120, 100)%>" width="120" height="100" border="0" />
                        <%} %>
                    </a>
                </td>
                <td style="border-bottom: #999999 dotted 1px; padding-bottom: 15px; padding-top: 10px;">
                    <p class="font_title">
                        <a href="<%=Url %>"><%=listItem[i].Name%></a>
                    </p>
                    <p><%=listItem[i].Summary%></p>
                    <div class="font_titlect">
                        <a href="<%=Url %>">{RS:Web_MoreNews}</a></div>
                </td>
            </tr>
            <%} %>  
            <tr>
                <td colspan="2" style="padding-top: 6px;" class="navigation">
                    <div id="paginationControl">
                        <%= GetPagination(model.Page, model.PageSize, model.TotalRecord)%>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>