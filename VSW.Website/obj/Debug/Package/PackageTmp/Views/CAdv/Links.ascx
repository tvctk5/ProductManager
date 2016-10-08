<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<% // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModAdvEntity>; %>

<form action="/" method="post">
    ﻿<div class="top-right">
        Liên kết website
    </div>
    <div class="bg-right">
        <br />
        <script language="JavaScript">
            function DD_jumpMenu(targ, selObj, restore) {
                var s = selObj.options[selObj.selectedIndex].value;
                window.open(s);
                if (restore) selObj.selectedIndex = 0;
            }
        </script>
        <select name="dllink" id="dllink" class="inputbox" style="width: 180px;" onchange="DD_jumpMenu('parent',this,0)">
            <option selected="selected" value="0">- Liên kết website -</option>
             <%for (int i = 0; listItem != null && i < listItem.Count; i++ )
            { %><option value="<%=listItem[i].URL%>"><%=listItem[i].Name%></option><%} %>
        </select>
    </div>
    <div class="bottom-righ"></div>
</form>