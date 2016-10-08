<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    var listProductGroups = ModProduct_GroupsService.Instance.CreateQuery()
        .Where(o => o.Activity == true)
        .OrderByAsc(o => o.ID).ToList();// Danh sách các chủng loại đang được sử dụng

    //var listItem = ViewBag.Data as List<ModProduct_InfoEntity>;
    var listItemSale = ModProduct_InfoService.Instance.CreateQuery().Where(o => o.SaleOff == true).ToList();// Danh sách tất cả các sản phẩm khuyến mãi
    var listItemSaleByGroup = new List<ModProduct_InfoEntity>(); // Khởi tạo danh sách SP khuyến mãi trong một nhóm nào đó

    int iCount = 0;// Đếm lần lượt phần tử trong nhóm, lấy đối tượng đầu tiên
    int iCountItem = 0;// Đếm lần lượt số lượng phần tử trong số SP thuộc nhóm.
    int iItemInPage = 8;// Số lượng SP thuộc một trang (Mặc định là 8)
%>
<script language="javascript" type="text/javascript">
    function SelectTab(TabCurrent, Index) {
        $('li.active').removeAttr('class');
        $(TabCurrent).addClass("active");
        $('div.divSaleSub').css('display', 'none');
        $('#divSaleSub' + Index).css('display', 'block');
    }
</script>
<style type="text/css">
    .divSaleSub
    {
        display: none;
    }
</style>
<div class="title">
    <%= ViewBag.Title %></div>
<div>
    <div id="wrapper" style="width: 100%">
        <div class="box-product-header">
            header</div>
        <ol class="tab">
            <%--<li onclick="SelectTab(this,1);" class="active">Trang chủ</li>
            <li onclick="SelectTab(this,2);">Giới thiệu</li>
            <li onclick="SelectTab(this,3);">Sản phẩm</li>
            <li onclick="SelectTab(this,4);">Tin tức</li>
            <li onclick="SelectTab(this,5);">Liên hệ</li>
            <li onclick="SelectTab(this,6);">Giới thiệu</li>
            <li onclick="SelectTab(this,7);">Sản phẩm</li>
            <li onclick="SelectTab(this,8);">Tin tức</li>--%>
            <%
                if (listProductGroups != null && listProductGroups.Count > 0)
                {
                    foreach (var item in listProductGroups)
                    {%>
            <li onclick="SelectTab(this,<%= item.ID.ToString() %>);" <%= iCount==0?"class='active'":string.Empty %>>
                <span>
                    <%=Server.HtmlEncode(item.Name)%></span></li>
            <% iCount++;
                    }%>
            <%} %>
        </ol>
        <div id="content">
            <%if (listProductGroups == null || listProductGroups.Count <= 0)
                  return;
              iCount = 0;
              foreach (var itemProductGroups in listProductGroups)
              {
                  if (listItemSale == null || listItemSale.Count <= 0)
                      break;

                  listItemSaleByGroup = listItemSale.Where(o => o.ProductGroupsId == itemProductGroups.ID).ToList();
                  if (listItemSaleByGroup == null || listItemSaleByGroup.Count <= 0)
                      continue;
            %>
            <div class="divSaleSub" id="<%= "divSaleSub" +itemProductGroups.ID.ToString()  %>"
                style="<%= iCount==0?"display:block;": string.Empty %>">
                    <%
                      iCountItem = 0;
                      string sCreateTable = "<table class='tblView' style='width: 100%;'><tr>";
                      for (int i = 0; i < 8; i++)
                      {
                          if (i < listItemSaleByGroup.Count)
                          {
                              sCreateTable += "<td style='width:25%;' valign='top'><div><div>";
                              sCreateTable += listItemSaleByGroup[i].Name;
                              sCreateTable += listItemSaleByGroup[i].Code;
                              sCreateTable += listItemSaleByGroup[i].CreateDate;
                              sCreateTable += listItemSaleByGroup[i].FinishDate;
                              sCreateTable += listItemSaleByGroup[i].InfoBasic;
                              sCreateTable += listItemSaleByGroup[i].NewsPost;
                          }
                          else
                              sCreateTable += "<td style='width:25%;display:none;'><div><div>";

                          sCreateTable += "</div></div></td>";

                          if (iCountItem == 3)
                              sCreateTable += "</tr><tr>";
                    %>
                    <% iCountItem++;
                      } sCreateTable += "</tr></table>";
                      // write ra bảng đã tạo  %>
                    <%=sCreateTable %>
            </div>
            <%iCount++;
                  }%>
        </div>
    </div>
    <%--<div class="box-product"></div>--%>
</div>
