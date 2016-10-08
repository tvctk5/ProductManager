<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<% 
    var model = ViewBag.Model as ModNewsModel;
    var listItem = ViewBag.Data as List<ModNewsEntity>;
%>
<form id="vswForm" name="vswForm" method="post">
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-article">
            <h2>
                Bài viết</h2>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
<div class="clr">
</div>
<script type="text/javascript">

    var VSWController = 'ModNews';

    var VSWArrVar = [
                    'filter_menu', 'MenuID',
                    'filter_state', 'State',
                    'filter_lang', 'LangID',
                    'limit', 'PageSize'
                   ];

    var VSWArrVar_QS = [
                        'filter_search', 'SearchText'
                      ];

    var VSWArrQT = [
                    '<%= model.PageIndex + 1 %>', 'PageIndex',
                    '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '1', 'LangID',
                    '20', 'PageSize'
                  ];

    $(document).ready(function () {
        $(".sent-mail-letter").click(function () {
            if (!confirm("Gửi email cho danh sách người đăng ký nhận tin?"))
                return false;

            var NewsId = $(this).attr("newsid");

            $(".news-sentmail-loading").show();
            // Gửi email
            var urlSentMailNewsLetter = '<%=ResolveUrl("~/CP/Tools/Ajax/ModNews/PostData.aspx?Type=sentmailnewsletter")%>';
            SentMailNewsLetter(urlSentMailNewsLetter, NewsId);

            //alert("Đang thực hiện gửi email. Xin hãy chờ!");
        });
    });

    // Gửi email
    function SentMailNewsLetter(linkPage, RecordID) {

        var pageUrl = linkPage + "&RecordID=" + RecordID;
        var dataPostBack = null;

        $.post(pageUrl,
        dataPostBack,
    function (data) {
        if (data.Error) {
            alert(data.MessError);
            return false;
        }
        else {
            // Thành công
            alert(data.MessSuccess)
        }
    })
    .done(function () {
    })
    .fail(function () {
        alert("Phát sinh lỗi trong quá trình thực hiện. Hãy thử lại!");
        return null;
    })
    .always(function () {
        $(".news-sentmail-loading").hide();
    })
    ;
    }
</script>
<%= ShowMessage()%>
<div class="news-sentmail-loading">
    <div>
        <p>
            <img class="news-sentmail-loading-img" alt="Đang tìm kiếm" />&nbsp;&nbsp;Đang gửi
            email, xin hãy chờ!</p>
    </div>
</div>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <table>
            <tr>
                <td width="100%">
                    Lọc:
                    <input type="text" value="<%= model.SearchText %>" id="filter_search" class="text_area"
                        onchange="VSWRedirect();return false;" />
                    <button onclick="VSWRedirect();return false;">
                        Tìm kiếm</button>
                </td>
                <td nowrap="nowrap">
                    Chuyên mục :
                    <select id="filter_menu" onchange="VSWRedirect()" class="inputbox" size="1">
                        <option value="0">(Tất cả)</option>
                        <%= Utils.ShowDDLMenuByType("News", model.LangID, model.MenuID)%>
                    </select>
                    Vị trí :
                    <select id="filter_state" onchange="VSWRedirect()" class="inputbox" size="1">
                        <option value="0">(Tất cả)</option>
                        <%= Utils.ShowDDLByConfigkey("Mod.NewsState", model.State)%>
                    </select>
                    Ngôn ngữ :<%= ShowDDLLang(model.LangID)%>
                </td>
            </tr>
        </table>
        <table class="adminlist" cellspacing="1">
            <thead>
                <tr>
                    <th width="1%">
                        #
                    </th>
                    <th width="1%" nowrap="nowrap" class="hide">
                        <%= GetSortLink("ID", "ID")%>
                    </th>
                    <th width="1%">
                        <input type="checkbox" name="toggle" value="" onclick="checkAll(<%= model.PageSize %>);" />
                    </th>
                    <th class="title">
                        <%= GetSortLink("Tiêu đề", "Name")%>
                    </th>
                    <th style="width: 40px" nowrap="nowrap">
                        <%= GetSortLink("Ảnh", "File")%>
                    </th>
                    <th nowrap="nowrap">
                        <%= GetSortLink("Chuyên mục", "MenuID")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Xuất bản", "Published")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Duyệt", "Activity")%>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        <%= GetSortLink("Sắp xếp", "Order")%>
                        <a href="javascript:vsw_exec_cmd('saveorder')" class="saveorder" title="Lưu sắp xếp">
                        </a>
                    </th>
                    <th width="1%" nowrap="nowrap">
                        Gửi email
                    </th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="15">
                        <del class="container">
                            <%= GetPagination(model.PageIndex, model.PageSize, model.TotalRecord)%>
                        </del>
                    </td>
                </tr>
            </tfoot>
            <tbody>
                <%for (int i = 0; listItem != null && i < listItem.Count; i++)
                  { %>
                <tr class="row<%= i%2 %>">
                    <td align="center">
                        <%= i + 1%>
                    </td>
                    <td align="center" class="hide">
                        <%= listItem[i].ID%>
                    </td>
                    <td align="center">
                        <%= GetCheckbox(listItem[i].ID, i)%>
                    </td>
                    <td>
                        <a href="javascript:VSWRedirect('Add', <%= listItem[i].ID %>)">
                            <%= listItem[i].Name%></a>
                        <p class="smallsub">
                            (<span>Mã</span>:
                            <%= listItem[i].Code%>)</p>
                    </td>
                    <td align="center">
                        <%= Utils.GetMedia(listItem[i].File, 40, 40)%>
                    </td>
                    <td align="center">
                        <%= GetName(listItem[i].getMenu()) %>
                    </td>
                    <td align="center">
                        <%= string.Format("{0:dd-MM-yyyy HH:mm}", listItem[i].Published) %>
                    </td>
                    <td align="center">
                        <%= GetPublish(listItem[i].ID, listItem[i].Activity)%>
                    </td>
                    <td class="order">
                        <%= GetOrder(listItem[i].ID, listItem[i].Order)%>
                    </td>
                    <td>
                        <a href="javascript:void(0);" class="sent-mail-letter" newsid="<%=listItem[i].ID %>">
                            <img /></a>
                    </td>
                </tr>
                <%} %>
            </tbody>
        </table>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
</form>
