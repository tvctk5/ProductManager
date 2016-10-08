<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    
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
<link rel="stylesheet" type="text/css" href="/Content/Add/SlideShows/0_style.css" />
<script type="text/javascript" src="/Content/Add/SlideShows/1_jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="/Content/Add/SlideShows/2_jquery.jcarousel.min.js"></script>
<link rel="stylesheet" type="text/css" href="/Content/Add/SlideShows/3_skin.css" />
<script language="javascript" type="text/javascript">
    function SelectTab(TabCurrent, Index) {
        $('li.active').removeAttr('class');
        $(TabCurrent).addClass("active");
        $('div.divSaleSub').css('display', 'none');
        $('#divSaleSub' + Index).css('display', 'block');
    }

    function mycarousel_initCallback(carousel) {
        // Disable autoscrolling if the user clicks the prev or next button.
        carousel.buttonNext.bind('click', function () {
            carousel.startAuto(0);
        });

        carousel.buttonPrev.bind('click', function () {
            carousel.startAuto(0);
        });

        // Pause autoscrolling if the user moves with the cursor over the clip.
        carousel.clip.hover(function () {
            carousel.stopAuto();
        }, function () {
            carousel.startAuto();
        });
    };

    function mycarousel_initCallback1(carousel) {
        // Disable autoscrolling if the user clicks the prev or next button.
        carousel.buttonNext.bind('click', function () {
            carousel.startAuto(0);
        });

        carousel.buttonPrev.bind('click', function () {
            carousel.startAuto(0);
        });

        // Pause autoscrolling if the user moves with the cursor over the clip.
        carousel.clip.hover(function () {
            carousel.stopAuto();
        }, function () {
            carousel.startAuto();
        });
    };

    $(document).ready(function () {
        $('#mycarousel').jcarousel({
            auto: 2,
            wrap: 'last',
            initCallback: mycarousel_initCallback
        });

        $('#Ul1').jcarousel({
            auto: 2,
            wrap: 'last',
            initCallback: mycarousel_initCallback1
        });

    });
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
                <% if (iCount == 3)
                   { %>
                <ul id="Ul1" class="jcarousel-skin-tango">
                    <li>
                        <img src="http://static.flickr.com/66/199481236_dc98b5abb3_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/75/199481072_b4a0d09597_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/57/199481087_33ae73a8de_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/77/199481108_4359e6b971_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/58/199481143_3c148d9dd3_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/72/199481203_ad4cdcf109_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/58/199481218_264ce20da0_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/69/199481255_fdfe885f87_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/60/199480111_87d4cb3e38_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/70/229228324_08223b70fa_s.jpg" width="75" height="75"
                            alt="" /></li>
                </ul>
                <%}
                   else
                   { %>
                <ul id="mycarousel" class="jcarousel-skin-tango">
                    <li>
                        <img src="http://static.flickr.com/66/199481236_dc98b5abb3_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/75/199481072_b4a0d09597_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/57/199481087_33ae73a8de_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/77/199481108_4359e6b971_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/58/199481143_3c148d9dd3_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/72/199481203_ad4cdcf109_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/58/199481218_264ce20da0_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/69/199481255_fdfe885f87_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/60/199480111_87d4cb3e38_s.jpg" width="75" height="75"
                            alt="" /></li>
                    <li>
                        <img src="http://static.flickr.com/70/229228324_08223b70fa_s.jpg" width="75" height="75"
                            alt="" /></li>
                </ul>
                <%} %>
            </div>
            <%iCount++;
              }%>
        </div>
    </div>
    <%--<div class="box-product"></div>--%>
</div>
