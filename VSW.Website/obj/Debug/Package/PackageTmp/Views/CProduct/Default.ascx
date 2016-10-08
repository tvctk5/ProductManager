<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>

<%
    // Không hiển thị module
    if (!ViewBag.ShowModule)
        return;
    var listItem = ViewBag.Data as List<ModProductEntity>;
%>

<script src="/Content/js/wz_tooltip.js" type="text/javascript"></script>
<script type="text/javascript">
    //Alert MsgAd
    clicksor_enable_MsgAlert = true;
    //default pop-under house ad url
    clicksor_enable_pop = true; clicksor_frequencyCap = 0.1;
    durl = '';
    //default banner house ad url
    clicksor_default_url = '';
    clicksor_banner_border = '#000f30'; clicksor_banner_ad_bg = '#FFFFFF';
    clicksor_banner_link_color = '#0c15ff'; clicksor_banner_text_color = '#da0041';
    clicksor_banner_image_banner = true; clicksor_banner_text_banner = true;
    clicksor_layer_border_color = '';
    clicksor_layer_ad_bg = ''; clicksor_layer_ad_link_color = '';
    clicksor_layer_ad_text_color = ''; clicksor_text_link_bg = '';
    clicksor_text_link_color = '#0c59ff'; clicksor_enable_text_link = true;
    clicksor_enable_VideoAd = true;
</script>

<div class="main_body">
    <div class="module-duong-top">
        <p> <%= ViewBag.Title %></p>
    </div>
    <div class="image-sp">
        <div class="" align="center">
            <div style="width: 590px;" align="center">
                
                <table style="width: 570px;" border="0">
                    <tr>
                       <%for(int i = 0; listItem != null && i < listItem.Count; i++)
                        {
                        string sURL = ViewPage.GetURL(listItem[i].MenuID, listItem[i].Code);%>
                        <%if (i > 0 && i % 2 == 0)
                          { %></tr><tr><%} %>
                        <td>
                            <div style="padding: 0; margin: 0; background: #FFF">
                            <span>
                                <div class="item_style">
                                    <div class="name-product">
                                        <br>
                                        <%= listItem[i].Name %><br>
                                    </div>
                                    <div class="bg_product">
                                         <%if (!string.IsNullOrEmpty(listItem[i].File))
                                          { %>
                                        <div class="bg_product_01 hoverborder" onmouseout="UnTip()" onmouseover="Tip('<img src=<%= Utils.GetResizeFile(listItem[i].File, 2, 0, 0)%>  />',ABOVE, true)">
                                            <a href="<%=sURL %>">
                                               <img src="<%= Utils.GetResizeFile(listItem[i].File, 2, 145, 120)%>" class="border_img" alt="<%= listItem[i].Name %>" />
                                            </a>
                                        </div><%} %>
                                        <div class="bg_product_02">
                                            <br />
                                            <b> Giá</b> :<b style="color:Red"> <%= string.Format("{0:#,##0}", listItem[i].Price)%></b>
                                            <br />
                                        </div>
                                    </div>
                                    <div class="bottom_product">
                                        <span style="padding-left: 35px;">
                                          <a href="<%=sURL %>">
                                            <img src="/Content/images/btchitiet.jpg" alt="" class="border_img">
                                         </a></span>
                                         <span style="padding-left: 35px;"><a href="/view-cart/Add.aspx/ProductID/<%= listItem[i].ID %>">
                                            <input name="btnorder" src="/Content/images/btdathang.jpg" alt="Dat hang" id="btnoder" type="image"></a>
                                        </span>
                                    </div>
                                </div>
                            </span>
                            </div>
                        </td>
                        <%}%>
                    </tr>
                   
                </table>

            </div>
        </div>
    </div>
    <div class="module-duong-bottom"></div>
</div>




