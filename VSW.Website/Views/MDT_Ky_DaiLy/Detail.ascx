﻿<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<% 
    var item = ViewBag.Data as ModDT_Ky_DaiLyEntity;
    var listOther = ViewBag.Other as List<ModDT_Ky_DaiLyEntity>;
%>

<div class="title"><%= ViewPage.CurrentPage.Name %></div>

<div class="item">
    <p class="item-id">ID : <%= item.ID %></p>
    <p class="item-moddtkyid">Mod dt ky id : <%= string.Format("{0:#,##0}", item.ModDtKyId) %></p>
    <p class="item-modproductagentparentid">Mod product agent parent id : <%= string.Format("{0:#,##0}", item.ModProductAgentParentId) %></p>
    <p class="item-modproductagentid">Mod product agent id : <%= string.Format("{0:#,##0}", item.ModProductAgentId) %></p>
    <p class="item-code">Mã : <%= item.Code %></p>
    <p class="item-name">Tên : <%= item.Name %></p>
    <p class="item-type">Loại : <%= string.Format("{0:#,##0}", item.Type) %></p>
    <p class="item-value">Giá trị : <%= string.Format("{0:#,##0}", item.Value) %></p>
    <p class="item-totalfirst">Total first : <%= string.Format("{0:#,##0}", item.TotalFirst) %></p>
    <p class="item-totallast">Total last : <%= string.Format("{0:#,##0}", item.TotalLast) %></p>
    <p class="item-modloaidailycode">Mod loai dai ly code : <%= item.ModLoaiDaiLyCode %></p>
    <p class="item-modloaidailyname">Mod loai dai ly name : <%= item.ModLoaiDaiLyName %></p>
    <p class="item-modloaidailytype">Mod loai dai ly type : <%= string.Format("{0:#,##0}", item.ModLoaiDaiLyType) %></p>
    <p class="item-modloaidailyvalue">Mod loai dai ly value : <%= string.Format("{0:#,##0}", item.ModLoaiDaiLyValue) %></p>
    <p class="item-activity">Duyệt : <%= item.Activity ? "Có" : "Không" %></p>
    <p class="item-createdate">Create date : <%= string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) %></p>
</div>

<div class="list-other">
<%for(int i = 0; listOther != null && i < listOther.Count; i++)
{
string Url = ViewPage.GetURLCurrentPage(listOther[i].Code);%>
    <p class="list-other-name"><a href="<%=Url %>"><%= listOther[i].Name %></a></p>
<%}%>
</div>
