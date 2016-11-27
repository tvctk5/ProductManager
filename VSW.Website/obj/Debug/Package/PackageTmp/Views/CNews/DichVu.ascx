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
    <div class="boxvien">
      <%for (int i = 0; listItem != null && i < listItem.Count;i++ )
    {
        string Url = ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code);
        %>
        <div class="boxsp pcenter">
            <h1 class="font_titlesp">
                <a href="<%=Url %>"><%=listItem[i].Name%></a>
            </h1>
            <a href="<%=Url %>">
             <%if (!string.IsNullOrEmpty(listItem[i].File))
              { %>
              <img src="<%=Utils.GetResizeFile(listItem[i].File, 2, 189, 125)%>" border="0" width="189" height="125" />
             <%} %>
            </a>
        </div>
       <%} %>
    </div>
</div>