<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
        
  %>
<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl pcenter">
            {RS:Web_SupportOnline}</h1>
    </div>
    <div class="boxvien">
        {RS:Web_Support}
    </div>
</div>