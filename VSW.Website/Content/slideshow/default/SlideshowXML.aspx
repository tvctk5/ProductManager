<%@ Page Language="C#" AutoEventWireup="true" %>
<script runat="server">
    
    List<ModAdvEntity> ArrAdv = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "text/xml";
        
        int MenuID = VSW.Core.Web.HttpQueryString.GetValue("m").ToInt();
        ArrAdv = ModAdvService.Instance.CreateQuery()
                    .Where(o => o.Activity == true && o.MenuID == MenuID)
                    .OrderByAsc(o => o.Order)
                    .ToList_Cache();
    }
    
</script>
<?xml version="1.0" encoding="ISO-8859-1"?>
<imageshow>
    <title>JSN ImageShow Module #53 XML Data</title>
    <slideshow path="<%= ArrAdv != null ? System.IO.Path.GetDirectoryName(ArrAdv[0].File).Replace("\\","/").Replace("~/","/") : "" %>/">
      <%for (int i = 0; ArrAdv != null && i < ArrAdv.Count; i++)
         {%><image><%=System.IO.Path.GetFileName(ArrAdv[i].File)%></image><%}%>
    </slideshow>
</imageshow>