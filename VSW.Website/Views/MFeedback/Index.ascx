<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModFeedbackEntity;
    if (item == null)
        item = new ModFeedbackEntity();
%>

<div class="box100 mg">
    <div class="boxtitle">
        <h1 class="font_title pl"><%= ViewPage.CurrentPage.Name %></h1>
    </div>
    <div class="boxvien" style="padding: 10px;">
        <form method="post">
        <table width="100%" cellspacing="0" style="font-family: Arial; font-size: 12px;">
            <tr>
                <td colspan="2" style="padding-bottom: 10px; padding-top: 5px; padding-left: 5px;">
                    <div style="text-align: center;">
                         {RS:Web_Feedback} 
                     </div>
                </td>
            </tr>
            <tr>
                <td width="25%" class="key">
                    {RS:Web_FB_Name}:<sup><font color="#FF0000">(*)</font></sup>
                </td>
                <td>
                    <input type="text" name="Name" value="<%=item.Name %>" class="required" size="45" />
                </td>
            </tr>
            <tr>
                <td class="key">
                    {RS:Web_FB_Title}:<sup><font color="#FF0000">(*)</font></sup>
                </td>
                <td>
                    <input type="text" name="Title" value="<%=item.Title %>" class="required" size="45" />
                </td>
            </tr>
            <tr>
                <td class="key" style="vertical-align: top;">
                    {RS:Web_FB_Content}:<sup><font color="#FF0000">(*)</font></sup>
                </td>
                <td>
                    <textarea name="Content" class="required" rows="8" cols="47"><%=item.Content %></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="2">
                    <input type="submit" name="_vsw_action[AddPOST]" value="{RS:Web_FB_Send}" />
                    <input type="reset" name="Reset" value="{RS:Web_FB_Reset}" />
                </td>
            </tr>
        </table>
        </form>
    </div>
</div>
