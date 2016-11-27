<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% var listItem = ViewBag.Data as List<ModAdvEntity>; %>

<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl pcenter"> <%=ViewBag.Title%></h1>
    </div>
    <div class="boxvien">
       <%for (int i = 0; listItem != null && i < listItem.Count; i++ )
       { %>
           <%= Utils.GetCodeAdv(listItem[i])%>
       <%} %>
    </div>
</div>