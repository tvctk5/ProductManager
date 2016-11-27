<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModNewsEntity;
    var listOther = ViewBag.Other as List<ModNewsEntity>;

    var listComment = ViewBag.Comment as List<ModCommentEntity>;
    var model = ViewBag.Model as MNewsModel;
    
    var itemComment = ViewBag.AddComment as ModCommentEntity;
    if (itemComment == null)
    {
        itemComment = new ModCommentEntity();

        itemComment.Name = Cookies.GetValue("Web.Comment.Name", true);
        itemComment.Email = Cookies.GetValue("Web.Comment.Email", true);
    }

    var Url = ViewPage.GetURL(item.MenuID, item.Code);
%>

<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl">
            <%= ViewPage.CurrentPage.Name %></h1>
    </div>
    <div class="boxvien" style="padding: 10px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td style="border-bottom: #999999 dotted 1px; padding-bottom: 15px; padding-top: 10px;">
                    <h1><%=item.Name%></h1>
                    <br />
                     <%= Utils.GetHTMLForSeo(item.Content) %>

                     <p> <b>Tags </b>: 
                       <% if (!string.IsNullOrEmpty(item.Tags))
                       {
                           string[] ArrTag = item.Tags.Split(',');%>
                            <%for (int i = 0; i < ArrTag.Length; i++)
                            {
                                ArrTag[i] = ArrTag[i].Trim();%>
                                <%if (i > 0)
                                { %> , <%} %>
                                <a href="<%= ViewPage.GetURL("Tag", Data.GetCode(ArrTag[i])) %>" title="<%= ArrTag[i] %>"><%= ArrTag[i]%></a>
                            <%} %>
                       <%} %>
                    </p>

                    <form method="post" >
                    <table width="100%">
                      <tr>
                         <td colspan="2" align="center"><h3> <b><%if (item.getCountComment() > 0)
                                                { %><%= item.getCountComment()%> <%}
                                                else
                                                { %>chưa có<%} %> bình luận &#8220;<%= item.Name%>&#8221;</b></h3></td>
                      </tr>
                      <%for (int i = 0; listComment != null && i < listComment.Count; i++)
                        {%>
                      <tr>
                        <td><b><%=listComment[i].Name%></b> viết:</td>
                        <td><%=listComment[i].Content.Replace("\n","<br />")%></td>
                      </tr>
                      <%} %>  
                      <tr>
                        <td colspan="2" style="padding-top: 6px;" class="navigation">
                           <div id="paginationControl">
                           <%if (model.TotalRecord > model.PageSize)
                           {%>
                              <%= GetPagination(Url, model.Page, model.PageSize, model.TotalRecord)%>
                          <%} %></div></td>
                      </tr>
                      <tr>
                         <td colspan="2" align="center"><h3> <b>Viết bình luận</b></h3></td>
                      </tr>
                      <tr>
                        <td>Họ và tên </td>
                        <td><input name="Name" id="Name" value="<%=itemComment.Name %>" size="28" tabindex="1" type="text"> (bắt buộc)</td>
                      </tr>
                      <tr>
                        <td>Email </td>
                        <td><input name="Email" id="Email" value="<%=itemComment.Email %>" size="28" tabindex="1" type="text"> (bắt buộc)</td>
                      </tr>
                      <tr>
                        <td>Nội dung </td>
                        <td><textarea name='Content' id='Content' cols='60' rows='10' tabindex='4'><%=itemComment.Content%></textarea> (bắt buộc)</td>
                      </tr>
                      <tr>
                         <td colspan="2" align="center"><input name="_vsw_action[AddCommentPOST]" type="submit" value="Gửi bình luận" /></td>
                      </tr>
                    </table>
                    </form>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 6px;">
                    <div class="title_tintuc_cl">
                        <b>{RS:Web_OtherNews}</b></div>
                    <div style="padding: 11px 0 9px 27px;">
                        <%for (int i = 0; listOther != null && i < listOther.Count; i++)
                        {
                            string urlDetail = ViewPage.GetURL(listOther[i].MenuID, listOther[i].Code);
                            %>
                        <li><div class="font_titleother"><a href="<%=urlDetail %>"> <%=listOther[i].Name%> </a></div></li>
                        <%} %>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>