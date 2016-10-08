<%@ Page Language="C#" AutoEventWireup="true" %>
<script runat="server">
            
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.HttpRuntime.UnloadAppDomain();
        Response.Write("OK");
    }
    
</script>