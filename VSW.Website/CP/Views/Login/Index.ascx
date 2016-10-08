<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl"%>

<div class="padding">
    <div class="clr"></div>
    <div id="element-box" class="login">
        <div class="t">
            <div class="t"><div class="t"></div></div>
        </div>
        <div class="m wbg">
            <h1> {RS:Login_LoginTitle}</h1>
            <div id="section-box">
                <div class="t">
                    <div class="t"><div class="t"></div></div>
                </div>
                <div class="m">
                    <%= ShowMessage()%>
                    <form id="loginForm" name="loginForm" method="post">
                    <input type="hidden" id="_vsw_action" name="_vsw_action" value="Login" />
                    <input type="submit" class="hidebtn" value="Log in" />
                    <fieldset class="loginform">
                        <label id="mod-login-username-lbl" for="mod-login-username"> {RS:Login_UserName}</label>
                        <input name="UserName" id="mod-login-username" type="text" class="inputbox" size="15" />
                        <label id="mod-login-password-lbl" for="mod-login-password"> {RS:Login_Password}</label>
                        <input name="Password" id="mod-login-password" type="password" class="inputbox" size="15" />
                        <label id="mod-login-language-lbl" for="lang"> {RS:Login_Language}</label>
                        <select id="lang" name="lang" class="inputbox" onchange="document.getElementById('_vsw_action').value='[ChangeLang]['+this.value+']';loginForm.submit();">
                            <option <%if(CPViewPage.CurrentLang.Code == "vi-VN") {%> selected <%} %> value="vi-VN">Việt Nam</option>
                            <option <%if(CPViewPage.CurrentLang.Code == "en-US") {%> selected <%} %> value="en-US">English</option>
                        </select>
                        <div class="clr"></div>
                        <div class="button-holder">
                            <div class="button1">
                                <div class="next">
                                    <a href="#" onclick="loginForm.submit();">{RS:Login_LoginSubmit}</a>
                                </div>
                            </div>
                        </div>
                        <div class="clr"></div>
                    </fieldset>
                    </form>
                    <div class="clr"></div>
                </div>
                <div class="b">
                    <div class="b"><div class="b"></div></div>
                </div>
            </div>
            <div id="lock"></div>
            <div class="clr"></div>
        </div>
        <div class="b"><div class="b"><div class="b"></div></div></div>
    </div>
    <div class="clr"></div>
</div>

