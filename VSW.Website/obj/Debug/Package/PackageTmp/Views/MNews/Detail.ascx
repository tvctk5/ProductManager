<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl" %>
<% 
    var item = ViewBag.Data as ModNewsEntity;
    var listOther = ViewBag.Other as List<ModNewsEntity>;

    //var listComment = ViewBag.Comment as List<ModCommentEntity>;
    //var model = ViewBag.Model as MNewsModel;

    var itemComment = ViewBag.AddComment as ModCommentEntity;
    if (itemComment == null)
    {
        itemComment = new ModCommentEntity();

        itemComment.Name = Cookies.GetValue("Web.Comment.Name", true);
        itemComment.Email = Cookies.GetValue("Web.Comment.Email", true);
    }

    // Danh sách bình luận
    var ListComment = ViewBag.ListComment as string;

    var Url = ViewPage.GetURL(item.MenuID, item.Code);
%>
<div class="div-module">
    <div class="module-title hide">
        <%= ViewPage.CurrentPage.Name %></div>
    <div class="module-content">
        <div class="news-detail-title">
            <div class="news-detail-title-datetime">
                Ngày đăng:&nbsp;<%=item.Published.ToString("dd/MM/yyyy HH:mm")%>
                &nbsp;-&nbsp;Số lượt xem:&nbsp;<%=item.CountViewed%>&nbsp;-&nbsp;Số lượt bình luận:&nbsp;<%=item.CountComment%></div>
            <div class="news-detail-title-text">
                <%=item.Name%></div>
            <div class="news-detail-description-text">
                <h2>
                    <%=item.Summary%></h2>
            </div>
        </div>
        <div class="news-detail-content">
            <%= Utils.GetHTMLForSeo(item.Content) %>
        </div>
        <div class="news-detail-share">
            <%
                #region Lấy link chia sẻ face book
                string LinkLikeShare_FaceBook = string.Empty;
                string LinkLikeShare_FaceBook_Replated = string.Empty;
                string Host = string.Empty;
                string LinkPage = string.Empty;

                // Link trang hiện tại
                LinkPage = ViewPage.ReturnPath;

                var lstResource = WebResourceService.Instance.CreateQuery().Where(o => o.Code == "Web_Facebook" || o.Code == "Web_Host").ToList();

                var objResource = lstResource.Where(o => o.Code == "Web_Host").SingleOrDefault();
                if (objResource != null)
                    Host = Server.HtmlEncode(objResource.Value);

                objResource = lstResource.Where(o => o.Code == "Web_Facebook").SingleOrDefault();
                if (objResource != null)
                    LinkLikeShare_FaceBook = objResource.Value;

                LinkLikeShare_FaceBook_Replated = LinkLikeShare_FaceBook.Replace("LINK", Host + LinkPage);

                #endregion
            %>
            <b>Chia sẻ :</b><span><%=LinkLikeShare_FaceBook_Replated %></span>
        </div>
        <div class="news-detail-tag">
            <p>
                <b>Tags </b>:
                <% if (!string.IsNullOrEmpty(item.Tags))
                   {
                       string[] ArrTag = item.Tags.Split(',');%>
                <%for (int i = 0; i < ArrTag.Length; i++)
                  {
                      ArrTag[i] = ArrTag[i].Trim();%>
                <%if (i > 0)
                  { %>
                ,
                <%} %>
                <a href="<%= ViewPage.GetURL("Tag", Data.GetCode(ArrTag[i])) %>" title="<%= ArrTag[i] %>">
                    <%= ArrTag[i]%></a>
                <%} %>
                <%} %>
            </p>
        </div>
        <div class="news-detail-comment">
            <form method="post">
            <div class="news-detail-comment-title">
                <%
                    var countComment = item.CountComment;//item.getCountComment();
                    if (countComment <= 0)
                    {%>
                <h4>
                    Chưa có bình luận nào cho bài viết</h4>
                <%}
                    else
                    {%>
                Danh sách các bình luận của bài viết
                <%} %>
            </div>
            <div class="news-detail-comment-list">
                <%=ListComment%>
            </div>
            <div class="news-detail-comment-sentcomment">
                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#btnSentComment").click(function () {
                            var Comment_Name = $("#Name");
                            var Comment_Email = $("#Email");
                            var Comment_Content = $("#Content");
                            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

                            if (Comment_Name == null || Comment_Name.val() == "") {
                                alert("Yêu cầu nhập Họ và tên");
                                Comment_Name.focus();
                                return false;
                            }

                            if (Comment_Email == null || Comment_Email.val() == "") {
                                alert("Yêu cầu nhập Email");
                                Comment_Email.focus();
                                return false;
                            }
                            else {
                                if (!filter.test(Comment_Email.val())) {
                                    alert("Email không đúng định dạng");
                                    Comment_Email.focus();
                                    return false;
                                }
                            }

                            if (Comment_Content == null || Comment_Content.val() == "") {
                                alert("Yêu cầu nhập nội dung bình luận");
                                Comment_Content.focus();
                                return false;
                            }
                        });
                    });
                </script>
                <table class="adminlist">
                    <tr>
                        <th colspan="4">
                            <div class="news-detail-comment-sentcomment-title">
                                GỬI BÌNH LUẬN</div>
                        </th>
                    </tr>
                    <tr>
                        <td nowrap="nowrap">
                            Họ và tên
                        </td>
                        <td>
                            <input name="Name" id="Name" value="<%=itemComment.Name %>" size="28" tabindex="1"
                                type="text" />
                            &nbsp;(<span class="require">*</span>)
                        </td>
                        <td nowrap="nowrap">
                            Email
                        </td>
                        <td>
                            <input name="Email" id="Email" value="<%=itemComment.Email %>" size="28" tabindex="1"
                                type="text" />&nbsp;(<span class="require">*</span>)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Nội dung
                        </td>
                        <td colspan="3">
                            <textarea name='Content' id='Content' cols='60' rows='3' tabindex='4'><%=itemComment.Content%></textarea>&nbsp;(<span
                                class="require">*</span>)
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <input name="_vsw_action[AddCommentPOST]" type="submit" id="btnSentComment" value="Gửi bình luận" />
                        </td>
                    </tr>
                </table>
            </div>
            </form>
        </div>
        <div class="news-detail-other">
            <div class="news-detail-other-title">
                Các tin khác</div>
            <div class="news-detail-other-list">
                <ul>
                    <%for (int i = 0; listOther != null && i < listOther.Count; i++)
                      {
                          string urlDetail = ViewPage.GetURL(listOther[i].MenuID, listOther[i].Code);
                    %>
                    <li>
                        <div class="font_titleother">
                            <a href="<%=urlDetail %>">
                                <%=listOther[i].Name%></a>
                            <% 
                          string imgIcon = string.Empty;
                          string imgTitle = string.Empty;
                          switch (listOther[i].State)
                          {
                              case (int)EnumValue.NewsState.KHONG_GAN: imgIcon = "news-icon-default";
                                  imgTitle = "Bài viết thường";
                                  break;
                              case (int)EnumValue.NewsState.MOI: imgIcon = "news-icon-new";
                                  imgTitle = "Bài viết mới";
                                  break;
                              case (int)EnumValue.NewsState.NOI_BAT: imgIcon = "news-icon-hot";
                                  imgTitle = "Bài viết nổi bật";
                                  break;
                          }
                            %>
                            <img alt="<%=imgTitle %>" title="<%=imgTitle %>" class="<%=imgIcon %>" />
                            <span class="div-news-title-info-date">(<%=listOther[i].Published.ToString("dd/MM/yyyy HH:mm")%>)</span>
                        </div>
                    </li>
                    <%} %></ul>
            </div>
        </div>
    </div>
    <div class="module-footer">
    </div>
</div>
