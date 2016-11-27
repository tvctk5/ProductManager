<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl">
            <%= ViewPage.CurrentPage.Name %></h1>
    </div>
    <div class="boxvien" style="padding: 10px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td style="border-bottom: #999999 dotted 1px; padding-bottom: 15px; padding-top: 10px;">
                     <%= Utils.GetHTMLForSeo(ViewPage.CurrentPage.Content) %>
                </td>
            </tr>
        </table>
    </div>
</div>


