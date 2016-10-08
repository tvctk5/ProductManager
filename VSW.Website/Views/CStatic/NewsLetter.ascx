<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
    var item = ViewBag.Data as ModProduct_InputEntity;
    var model = ViewBag.Model as StaticController_InputModel;
%>
<script language="javascript" type="text/javascript">
    function OnClickButton() {
        vsw_exec_cmd("CStatic-AddNewsLetter-NewsLetter");
    }
</script>
<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl pcenter">
            Dang ky nhan ban tin</h1>
    </div>
    <div class="boxvien">
        <form method="post">
        Name
        <input type="text" id="Code" name="Code" value="<%=item.Code %>" />
        <br />
        Email
        <input type="text" name="Email" id="Email" value="<%=model.Email %>" />
        <br />
        <input type="submit" name="_vsw_action[CStatic-AddNewsLetter-NewsLetter]" />
        </form>
    </div>
</div>
