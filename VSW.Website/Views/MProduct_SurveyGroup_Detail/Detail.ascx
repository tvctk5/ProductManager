<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModProduct_SurveyGroup_DetailEntity;
    var listOther = ViewBag.Other as List<ModProduct_SurveyGroup_DetailEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-surveygroupid">Survey group id : <%= string.Format("{0:#,##0}", item.SurveyGroupId) %></p>
    <p class="item-code">Mã : <%= item.Code %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-vote">Vote : <%= string.Format("{0:#,##0}", item.Vote) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].Code);%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
<%}%>
</div>
