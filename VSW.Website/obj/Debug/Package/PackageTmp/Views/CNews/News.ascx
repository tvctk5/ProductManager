<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
        
    var listItem = ViewBag.Data as List<ModNewsEntity>;
%>

<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl">
            <%=ViewBag.Title%></h1>
    </div>
    <div class="boxvien" style="padding-left: 8px;">
        <div class="box50 al">
            <%for (int i = 0; listItem != null && i < listItem.Count && i < 1;i++ )
    {
        string Url = ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code);
        %>
            <h1 class="font_titlesp" style="padding-top: 10px;">
                <a href="<%=Url %>"> <%=listItem[i].Name%></a></h1>
            <div class="baivietMainBox-img200" style="font-family: Arial, Helvetica, sans-serif;
                font-size: 12px; width: 200px; margin: 0px 15px 5px 0px; float: left; position: relative;
                text-align: left; background-color: rgb(255, 255, 255);">
                 <%if (!string.IsNullOrEmpty(listItem[i].File))
                  { %>
                  <img  src="<%=Utils.GetResizeFile(listItem[i].File, 2, 90, 80)%>" />
                <%} %>
            </div>
            <div class="baiviet-TopContent200" style="font-family: Arial, Helvetica, sans-serif;
                font-size: 12px; float: left; width: 323px; text-align: left; background-color: rgb(255, 255, 255);">
                <%=listItem[i].Summary%>
            </div>
            <div class="font_titlect">
                <a href="<%=Url %>">{RS:Web_MoreNews}</a></div><%} %>
        </div>
        <div class="box50 cham">
            <h1 class="font_titlesp pl u"> {RS:Web_OtherNews}</h1>
            <div class="boxconnect">
                <ul>
                    <%for (int i = 1; listItem != null && i < listItem.Count;i++ ){
                        string Url = ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code);
                        %>
                            <li class="icon_c"><a href="<%=Url %>"><%=listItem[i].Name%></a></li><%} %>
                </ul>
            </div>
        </div>
    </div>
</div>