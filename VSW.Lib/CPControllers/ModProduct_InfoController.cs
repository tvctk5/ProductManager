using System;
using System.Collections.Generic;
using System.Linq;
using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    [CPModuleInfo(Name = "Sản phẩm",
        Description = "Quản lý - Sản phẩm",
        Code = "ModProduct_Info",
        Access = 31,
        Order = 200,
        ShowInMenu = true,
        CssClass = "icon-16-component")]
    public class ModProduct_InfoController : CPController
    {
        private List<ModProduct_Info_OfficeEntity> listItem_ModProduct_Info_OfficeEntity { get; set; }
        private List<ModProduct_OfficeEntity> listItem_ModProduct_OfficeEntity { get; set; }

        public ModProduct_InfoController()
        {
            //khoi tao Service
            DataService = ModProduct_InfoService.Instance;
            CheckPermissions = true;
        }

        public void ActionIndex(ModProduct_InfoModel model)
        {
            // sap xep tu dong
            string orderBy = AutoSort(model.Sort);

            // tao danh sach
            var dbQuery = ModProduct_InfoService.Instance.CreateQuery()
                                .Where(!string.IsNullOrEmpty(model.SearchText), o => o.Name.Contains(model.SearchText))
                                .Where(o=>o.Deleted == false)
                                .WhereIn(o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", model.MenuID, model.LangID))
                                .Take(model.PageSize)
                                .OrderBy(orderBy)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            ViewBag.Model = model;
        }

        public void ActionAdd(ModProduct_InfoModel model)
        {
            model.ListIdProduct_BanKem = string.Empty;
            model.ListIdProduct_LienQuan = string.Empty;
            if (model.RecordID > 0)
            {
                item = ModProduct_InfoService.Instance.GetByID(model.RecordID);

                // khoi tao gia tri mac dinh khi update

                model.ListIdProduct_BanKem = item.ProductsAttach;
                model.ListIdProduct_LienQuan = item.ProductsConnection;
                model.ModPriceOld = item.Price;// Lưu lại giá cũ

                if (item.StartDate != null)
                {
                    model.ModDateTimeStart = item.StartDate.ToString("dd/MM/yyyy");
                    model.ModDateTimeFinish = item.FinishDate.ToString("dd/MM/yyyy");
                    model.ModHourStart = item.StartDate.Hour;
                    model.ModMinuteStart = item.StartDate.Minute;
                    model.ModHourFinish = item.FinishDate.Hour;
                    model.ModMinuteFinish = item.FinishDate.Minute;
                }

            #region Tải các thông tin cho Sản phẩm
            // Danh sách màu của sản phẩm
            model.ListColor = GetListColor(model.RecordID);

            // Danh sách size của sản phẩm
            model.ListSize = GetListSize(model.RecordID);

            // Danh sách các chủng loại liên quan
            model.ListProductGroups = GetListProductGroup(model.RecordID, item.MenuID, model.LangID);

            // Lấy các đại lý
            model.ListAgent = GetListAgent(model.RecordID);

            // Lấy các khu vực bán sản phẩm
            model.ListArea = GetListArea(model.RecordID);

            // Những thuộc tính lọc thuộc sản phẩm
            model.ListFilter = GetListFilter(model.RecordID);

            // Danh sách ảnh slide của sản phẩm
            model.ListSlideShow = GetListSlideShow(model.RecordID);

            // Danh sách các thuộc tính của sản phẩm
            model.ListProperties = GetListProperties(model.RecordID, item.MenuID, ref model);

            // Danh sách các bình luận của sản phẩm
            model.ListComments = GetListComment(model.RecordID);

            // Danh sách các loại của sản phẩm
            model.ListTypes = GetListTypes(model.RecordID);
            #endregion
            }
            else
            {
                item = new ModProduct_InfoEntity();

                // khoi tao gia tri mac dinh khi insert
                item.MenuID = model.MenuID;
                item.Activity = CPViewPage.UserPermissions.Approve;
                item.CreateDate = DateTime.Now;
                item.SaleOff = false;
                item.Status = true;
                item.StatusNote = "Sản phẩm mới 100%";
                item.VAT = true;
                item.PostDate = DateTime.Now;
                item.RuntimeDateStart = DateTime.Now;
            }



            LoadDataProductCurrent(ref model);

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        #region Tìm kiếm sản phẩm liên quan
        // Thêm sản phẩm liên quan
        public void ActionAddRelativeProduct(ModProduct_InfoModel model)
        {
            model.ProductCurrent = ModProduct_InfoService.Instance.GetByID(model.RecordID);
            //CPViewPage.Request["ModProductInfoSearch_SeachChildren"];
            // Danh sách các phẩm
            model.ListIdProduct_LienQuan = model.ProductCurrent.ProductsConnection;
            string sSanPhamLienQuan_Code = model.ModProductInfoSearch_Code_LienQuan;
            string sSanPhamLienQuan_Name = model.ModProductInfoSearch_Name_LienQuan;
            int iProductGroupsId = model.ModProductInfoSearch_ProductGroupsId;
            int iSanPhamLienQuan_ManufacturerId = model.ModSearchManufacturerId_LienQuan;
            bool bolModProductInfoSearch_SeachChildren = model.ModProductInfoSearch_SeachChildren;

            // tao danh sach

            var dbQuery = ModProduct_InfoService.Instance.CreateQuery()
                                .Where(p => p.ID != model.RecordID && p.LangID == model.LangID)
                                .Where(!string.IsNullOrEmpty(sSanPhamLienQuan_Code), o => o.Code.Contains(sSanPhamLienQuan_Code))
                                .Where(!string.IsNullOrEmpty(sSanPhamLienQuan_Name), o => o.Name.Contains(sSanPhamLienQuan_Name))
                                .Where(iProductGroupsId > 0 && bolModProductInfoSearch_SeachChildren == false, o => o.MenuID == iProductGroupsId)
                                .WhereIn(iProductGroupsId > 0 && bolModProductInfoSearch_SeachChildren == true,
                                            o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", iProductGroupsId, model.LangID))
                                .Where(iSanPhamLienQuan_ManufacturerId > 0, o => o.ManufacturerId == iSanPhamLienQuan_ManufacturerId)
                                .Take(model.PageSize)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            if (model.TotalRecord > 0)
                // Lấy danh sách tên nhà sản xuất
                model.GetListManufacture = ModProduct_ManufacturerService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            ViewBag.Model = model;
        }

        #endregion

        #region Quản lý sản phẩm bán kèm
        // Thêm sản phẩm BanKem
        public void ActionAddAttachProduct(ModProduct_InfoModel model)
        {
            model.ProductCurrent = ModProduct_InfoService.Instance.GetByID(model.RecordID);
            //CPViewPage.Request["ModProductInfoSearch_SeachChildren"];
            // Danh sách các phẩm
            model.ListIdProduct_BanKem = model.ProductCurrent.ProductsAttach;
            string sSanPhamBanKem_Code = model.ModProductInfoSearch_Code_BanKem;
            string sSanPhamBanKem_Name = model.ModProductInfoSearch_Name_BanKem;
            int iProductGroupsId = model.ModProductInfoSearch_ProductGroupsId;
            int iSanPhamBanKem_ManufacturerId = model.ModSearchManufacturerId_BanKem;
            bool bolModProductInfoSearch_SeachChildren = model.ModProductInfoSearch_SeachChildren;

            // tao danh sach
            var dbQuery = ModProduct_InfoService.Instance.CreateQuery()
                                .Where(p => p.ID != model.RecordID && p.LangID == model.LangID)
                                .Where(!string.IsNullOrEmpty(sSanPhamBanKem_Code), o => o.Code.Contains(sSanPhamBanKem_Code))
                                .Where(!string.IsNullOrEmpty(sSanPhamBanKem_Name), o => o.Name.Contains(sSanPhamBanKem_Name))
                                .Where(iProductGroupsId > 0 && bolModProductInfoSearch_SeachChildren == false, o => o.MenuID == iProductGroupsId)
                                .WhereIn(iProductGroupsId > 0 && bolModProductInfoSearch_SeachChildren == true,
                                            o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", iProductGroupsId, model.LangID))
                                .Where(iSanPhamBanKem_ManufacturerId > 0, o => o.ManufacturerId == iSanPhamBanKem_ManufacturerId)
                                .Take(model.PageSize)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            if (model.TotalRecord > 0)
                // Lấy danh sách tên nhà sản xuất
                model.GetListManufacture = ModProduct_ManufacturerService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            ViewBag.Model = model;
        }

        #endregion

        #region Lấy danh sách màu của sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các màu thuộc sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListColor(int RecordID)
        {
            string sData = string.Empty;

            List<ModProduct_Info_ColorEntity> lstInfo_Color = ModProduct_Info_ColorService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();

            if (lstInfo_Color == null)
                lstInfo_Color = new List<ModProduct_Info_ColorEntity>();

            int iMauSac = 0;
            foreach (ModProduct_Info_ColorEntity item in lstInfo_Color)
            {
                sData += "<tr class='row" + iMauSac % 2 + "'>";
                sData += "<td align='center'>";
                sData += iMauSac + 1;
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<input type='text' id='txtColorCode" + iMauSac + "' style='background-color: " + item.ColorCode + ";' name='txtColorCode" + iMauSac + "' value='" + item.ColorCode + "' />";
                sData += "</td>";
                sData += "<td>";
                sData += "<input type='text' id='txtColorName" + iMauSac + "' name='txtColorName" + iMauSac + "' value='" + item.ColorName + "' />";
                sData += "</td>";
                sData += "<td class='text-right' align='right' nowrap='nowrap'>";
                sData += "<input type='text' id='txtColor_CountNumber" + iMauSac + "' name='txtColor_CountNumber" + iMauSac + "' value='" + string.Format("{0:#,##0}", item.CountNumber) + "' />";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid color' title='Cập nhật' href='javascript:void(0);' onclick=\"colorSave(urlColor_Save,this,'" + iMauSac + "','" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state save'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid color' title='Xóa' href='javascript:void(0);' onclick=\"colorDelete(urlColor_Delete,'" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr>";
                iMauSac++;
            }

            #region Luôn tạo một dòng trắng
            sData += "<tr class='row" + iMauSac % 2 + "'>";
            sData += "<td align='center'>";
            sData += iMauSac + 1;
            sData += "</td>";
            sData += "<td align='left'>";
            sData += "<input type='text' id='txtColorCode" + iMauSac + "' name='txtColorCode" + iMauSac + "' value='' />";
            sData += "</td>";
            sData += "<td>";
            sData += "<input type='text' id='txtColorName" + iMauSac + "' name='txtColorName" + iMauSac + "' value='' />";
            sData += "</td>";
            sData += "<td class='text-right' align='right' nowrap='nowrap'>";
            sData += "<input type='text' id='txtColor_CountNumber" + iMauSac + "' name='txtColor_CountNumber" + iMauSac + "' value='0' />";
            sData += "</td>";
            sData += "<td align='center'>";
            sData += "<a class='jgrid color' title='Thêm mới' href='javascript:void(0);' onclick=\"colorAdd(urlColor_Add,this,'" + iMauSac + "'); return false;\"><span class='jgrid'>";
            sData += "<span class='state add'></span></span></a>";
            sData += "</td>";
            sData += "<td align='center'>";
            sData += "</td>";
            sData += "</tr>";
            #endregion

            return sData;
        }

        #endregion

        #region Lấy thông tin kích thức sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các Size thuộc sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListSize(int RecordID)
        {
            string sData = string.Empty;

            List<ModProduct_Info_SizeEntity> lstInfo_Size = ModProduct_Info_SizeService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();

            if (lstInfo_Size == null)
                lstInfo_Size = new List<ModProduct_Info_SizeEntity>();

            int iSize = 0;
            foreach (ModProduct_Info_SizeEntity item in lstInfo_Size)
            {
                sData += "<tr class='row" + iSize % 2 + "'>";
                sData += "<td align='center'>";
                sData += iSize + 1;
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<input type='text' id='txtSizeCode" + iSize + "' name='txtSizeCode" + iSize + "' value='" + item.SizeCode + "' />";
                sData += "</td>";
                sData += "<td>";
                sData += "<input type='text' id='txtSizeName" + iSize + "' name='txtSizeName" + iSize + "' value='" + item.SizeName + "' />";
                sData += "</td>";
                sData += "<td class='text-right' align='right' nowrap='nowrap'>";
                sData += "<input type='text' id='txtSize_CountNumber" + iSize + "' name='txtSize_CountNumber" + iSize + "' value='" + string.Format("{0:#,##0}", item.CountNumber) + "' />";
                sData += "</td>";
                sData += "<td class='text-right' align='right' nowrap='nowrap'>";
                sData += "<input type='text' id='txtSize_Price" + iSize + "' name='txtSize_Price" + iSize + "' value='" + string.Format("{0:#,##0}", item.Price) + "' />";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid Size' title='Cập nhật' href='javascript:void(0);' onclick=\"SizeSave(urlSize_Save,this,'" + iSize + "','" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state save'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid Size' title='Xóa' href='javascript:void(0);' onclick=\"SizeDelete(urlSize_Delete,'" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr>";
                iSize++;
            }

            #region Luôn tạo một dòng trắng
            sData += "<tr class='row" + iSize % 2 + "'>";
            sData += "<td align='center'>";
            sData += iSize + 1;
            sData += "</td>";
            sData += "<td align='left'>";
            sData += "<input type='text' id='txtSizeCode" + iSize + "' name='txtSizeCode" + iSize + "' value='' />";
            sData += "</td>";
            sData += "<td>";
            sData += "<input type='text' id='txtSizeName" + iSize + "' name='txtSizeName" + iSize + "' value='' />";
            sData += "</td>";
            sData += "<td class='text-right' align='right' nowrap='nowrap'>";
            sData += "<input type='text' id='txtSize_CountNumber" + iSize + "' name='txtSize_CountNumber" + iSize + "' value='0' />";
            sData += "</td>";
            sData += "<td class='text-right' align='right' nowrap='nowrap'>";
            sData += "<input type='text' id='txtSize_Price" + iSize + "' name='txtSize_Price" + iSize + "' value='0' />";
            sData += "</td>";
            sData += "<td align='center'>";
            sData += "<a class='jgrid Size' title='Thêm mới' href='javascript:void(0);' onclick=\"SizeAdd(urlSize_Add,this,'" + iSize + "'); return false;\"><span class='jgrid'>";
            sData += "<span class='state add'></span></span></a>";
            sData += "</td>";
            sData += "<td align='center'>";
            sData += "</td>";
            sData += "</tr>";
            #endregion

            return sData;
        }

        #endregion

        #region Danh sách các chủng loại liên quan
        int iRowIndex = -1;
        /// <summary>
        /// CanTv
        /// Lấy danh sách các chủng loại liên quan thuộc sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListProductGroup(int RecordID, int iProductGroupImportant, int LanguageId)
        {
            string sData = string.Empty;
            iRowIndex = -1;

            // LẤy danh sách tất cả chuyên mục product: Theo Product và theo Ngôn ngữ
            List<WebMenuEntity> lstWebMenuEntity = WebMenuService.Instance.CreateQuery().Where(p => p.Type == "Product" && p.LangID == LanguageId).ToList();
            if (lstWebMenuEntity == null)
                return sData;

            // Lấy danh sách
            List<ModProduct_Info_ProductGroupsEntity> lstInfo_ProductGroups = ModProduct_Info_ProductGroupsService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();
            if (lstInfo_ProductGroups == null)
                lstInfo_ProductGroups = new List<ModProduct_Info_ProductGroupsEntity>();

            // Cấp cha ngoài cùng
            WebMenuEntity objWebMenuEntity_Parent = lstWebMenuEntity.Where(p => p.ParentID == 0).SingleOrDefault();
            if (objWebMenuEntity_Parent == null)
                return sData;

            // Cấp cha thứ 2
            List<WebMenuEntity> lstWebMenuEntity_Parent = lstWebMenuEntity.Where(p => p.ParentID == objWebMenuEntity_Parent.ID).OrderBy(o => o.Order).ToList();
            if (lstWebMenuEntity_Parent == null)
                return sData;

            ModProduct_Info_ProductGroupsEntity objSelected;
            bool bolSeleted = false;
            string sParentName = string.Empty;

            foreach (WebMenuEntity itemWebMenu in lstWebMenuEntity_Parent)
            {
                // Đã chọn nhóm này trong danh sách các
                bolSeleted = false;
                objSelected = lstInfo_ProductGroups.Where(p => p.MenuID == itemWebMenu.ID).FirstOrDefault();
                if (objSelected != null)
                    bolSeleted = true;

                sParentName = "Danh mục sản phẩm >> " + itemWebMenu.Name;

                sData += "<tr class='row" + (++iRowIndex) % 2 + "'>";
                sData += "<td align='center'>";
                sData += "<label>" + (iRowIndex + 1) + "</label>";
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<label>" + sParentName + "</label>";
                sData += "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (itemWebMenu.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>";
                // NẾu Item đang xét là Danh mục chủng loại gốc của sản phẩm
                if (itemWebMenu.ID == iProductGroupImportant)
                    sData += "<span class='jgrid'><span class='state activate' title='Là chủng loại chính của sản phẩm'></span></span>";
                sData += "</td>";

                sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                if (itemWebMenu.ID != iProductGroupImportant)
                {
                    sData += "    <a class='jgrid Add" + (bolSeleted ? " a-hide" : string.Empty) + "' title='Thêm' href='javascript:void(0);' onclick=\"ProductGroupsAdd(urlProductGroup_Add,this,'" + itemWebMenu.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state add'></span></span></a>";
                }
                sData += "</td>";
                sData += "<td align='center'>";
                if (itemWebMenu.ID != iProductGroupImportant)
                {
                    sData += "    <a class='jgrid Delete" + (bolSeleted ? string.Empty : " a-hide") + "' title='Xóa' href='javascript:void(0);' onclick=\"ProductGroupsDelete(urlProductGroup_Delete,this,'" + itemWebMenu.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state delete'></span></span></a>";
                }
                sData += "</td>";
                sData += "</tr>";

                // Tìm những con
                sData += GetRowChildenProductGroup(itemWebMenu.ID, sParentName, iProductGroupImportant, lstWebMenuEntity, lstInfo_ProductGroups);
            }

            return sData;
        }

        private string GetRowChildenProductGroup(int iParentId, string sParentName, int iProductGroupImportant, List<WebMenuEntity> lstWebMenuEntity, List<ModProduct_Info_ProductGroupsEntity> lstInfo_ProductGroups)
        {
            string sData = string.Empty;
            List<WebMenuEntity> lstWebMenuEntity_Childen = lstWebMenuEntity.Where(p => p.ParentID == iParentId).OrderBy(o => o.Order).ToList();
            if (lstWebMenuEntity_Childen == null || lstWebMenuEntity_Childen.Count <= 0)
                return sData;

            ModProduct_Info_ProductGroupsEntity objSelected;
            bool bolSeleted = false;

            foreach (WebMenuEntity itemChilden in lstWebMenuEntity_Childen)
            {
                // Đã chọn nhóm này trong danh sách các
                bolSeleted = false;
                objSelected = lstInfo_ProductGroups.Where(p => p.MenuID == itemChilden.ID).FirstOrDefault();
                if (objSelected != null)
                    bolSeleted = true;

                sData += "<tr class='row" + (++iRowIndex) % 2 + "'>";
                sData += "<td align='center'>";
                sData += "<label>" + (iRowIndex + 1) + "</label>";
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<label>" + sParentName + " >> " + itemChilden.Name + "</label>";
                sData += "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (itemChilden.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>";
                // NẾu Item đang xét là Danh mục chủng loại gốc của sản phẩm
                if (itemChilden.ID == iProductGroupImportant)
                    sData += "<span class='jgrid'><span class='state activate' title='Là chủng loại chính của sản phẩm'></span></span>";
                sData += "</td>";

                sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                if (itemChilden.ID != iProductGroupImportant)
                {
                    sData += "    <a class='jgrid Add" + (bolSeleted ? " a-hide" : string.Empty) + "' title='Thêm' href='javascript:void(0);' onclick=\"ProductGroupsAdd(urlProductGroup_Add,this,'" + item.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state add'></span></span></a>";
                }
                sData += "</td>";
                sData += "<td align='center'>";
                if (itemChilden.ID != iProductGroupImportant)
                {
                    sData += "    <a class='jgrid Delete" + (bolSeleted ? string.Empty : " a-hide") + "' title='Xóa' href='javascript:void(0);' onclick=\"ProductGroupsDelete(urlProductGroup_Delete,this,'" + item.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state delete'></span></span></a>";
                }
                sData += "</td>";
                sData += "</tr>";

                // Tìm những con
                sData += GetRowChildenProductGroup(itemChilden.ID, sParentName + " >> " + itemChilden.Name, iProductGroupImportant, lstWebMenuEntity, lstInfo_ProductGroups);
            }

            return sData;
        }

        #endregion

        #region Thông tin Đại lý bán sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các đại lý bán sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListAgent(int RecordID)
        {
            string sData = string.Empty;
            List<ModProduct_AgentEntity> lstInfo_Agent_All = ModProduct_AgentService.Instance.CreateQuery().OrderByAsc(o => o.Name).ToList();
            if (lstInfo_Agent_All == null || lstInfo_Agent_All.Count <= 0)
                return sData;

            List<ModProduct_Info_AgentEntity> lstInfo_Agent_In = ModProduct_Info_AgentService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();

            if (lstInfo_Agent_In == null)
                lstInfo_Agent_In = new List<ModProduct_Info_AgentEntity>();

            int iAgent = 0;
            bool bolCheckExist = false;
            foreach (ModProduct_AgentEntity item in lstInfo_Agent_All)
            {
                sData += "<tr class='row" + iAgent % 2 + "'>";
                sData += "<td align='center'>";
                sData += iAgent + 1;
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<label>" + item.Code + "</label>";
                sData += "</td>";
                sData += "<td>";
                sData += "<label>" + item.Name + "</label>";
                sData += "</td>";
                sData += "<td nowrap='nowrap'>";
                sData += "<label>" + item.CountryName + "</label>";
                sData += "</td>";
                sData += "<td>";
                sData += "<label>" + item.CityName + "</label>";
                sData += "</td>";
                sData += "<td>";
                sData += "<label>" + item.Address + "</label>";
                sData += "</td>";

                // Kiểm tra sự tồn tại
                bolCheckExist = CheckExistAgent(lstInfo_Agent_In, item.ID);

                sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                sData += "    <a class='jgrid Add" + (bolCheckExist ? " a-hide" : string.Empty) + "' title='Thêm' href='javascript:void(0);' onclick=\"AgentAdd(urlAgent_Add,this,'" + item.ID + "'); return false;\">";
                sData += "        <span class='jgrid'><span class='state add'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "    <a class='jgrid Delete" + (bolCheckExist ? string.Empty : " a-hide") + "' title='Xóa' href='javascript:void(0);' onclick=\"AgentDelete(urlAgent_Delete,this,'" + item.ID + "'); return false;\">";
                sData += "        <span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr>";
                iAgent++;
            }
            return sData;
        }

        /// <summary>
        /// True: Đã tồn tại | False: Chưa tồn tại
        /// </summary>
        /// <param name="lstInfo_Agent_All"></param>
        /// <param name="iCurrentID"></param>
        /// <returns></returns>
        private bool CheckExistAgent(List<ModProduct_Info_AgentEntity> lstInfo_Agent_In, int iCurrentID)
        {
            if (iCurrentID <= 0 || (lstInfo_Agent_In == null || lstInfo_Agent_In.Count <= 0))
                return false;

            ModProduct_Info_AgentEntity objExist = lstInfo_Agent_In.Where(p => p.ProductAgeId == iCurrentID).SingleOrDefault();
            if (objExist == null)
                return false;

            return true;
        }

        #endregion

        #region Thông tin khu vực bán sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các khu vực bán sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListArea(int RecordID)
        {
            string sData = string.Empty;
            List<ModProduct_National_AreaEntity> lstInfo_Agent_All = ModProduct_National_AreaService.Instance.CreateQuery().OrderByAsc(o => o.Name).ToList();
            if (lstInfo_Agent_All == null || lstInfo_Agent_All.Count <= 0)
                return sData;

            List<ModProduct_Info_AreaInNationalEntity> lstInfo_Area_In = ModProduct_Info_AreaInNationalService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();

            if (lstInfo_Area_In == null)
                lstInfo_Area_In = new List<ModProduct_Info_AreaInNationalEntity>();

            // Lất hết quốc gia đang sử dụng
            List<ModProduct_NationalEntity> lstNationalEntity_All = ModProduct_NationalService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            int iArea = -1;
            int iSTT = 0;
            bool bolCheckExist = false;
            List<ModProduct_National_AreaEntity> lstInfo_Agent_Filter = null;

            foreach (var itemNational in lstNationalEntity_All)
            {
                // Quyết định màu của dòng
                iArea++;

                // Dòng quốc gia
                #region Dòng quốc gia
                sData += "<tr class='row" + iArea % 2 + "'>";
                sData += "<td colspan='5'>";
                sData += "<div><img class='img-icon-category' border='0' /> <label style='font-weight:bold;'>" + itemNational.Name + "</label></div>";
                sData += "</td></tr>";
                #endregion


                lstInfo_Agent_Filter = lstInfo_Agent_All.Where(p => p.ProductNationalId == itemNational.ID).ToList();
                if (lstInfo_Agent_Filter == null || lstInfo_Agent_Filter.Count <= 0)
                    continue;

                foreach (ModProduct_National_AreaEntity item in lstInfo_Agent_Filter)
                {
                    // Quyết định màu của dòng
                    iArea++;

                    sData += "<tr class='row" + iArea % 2 + "'>";
                    sData += "<td align='center'>";
                    sData += (++iSTT);
                    sData += "</td>";
                    sData += "<td align='left'>";
                    sData += "<label>" + item.Code + "</label>";
                    sData += "</td>";
                    sData += "<td>";
                    sData += "<label>" + item.Name + "</label>";
                    sData += "</td>";

                    // Kiểm tra sự tồn tại
                    bolCheckExist = CheckExistArea(lstInfo_Area_In, item.ID);

                    sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                    sData += "    <a class='jgrid Add" + (bolCheckExist ? " a-hide" : string.Empty) + "' title='Thêm' href='javascript:void(0);' onclick=\"AreaAdd(urlArea_Add,this,'" + item.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state add'></span></span></a>";
                    sData += "</td>";
                    sData += "<td align='center'>";
                    sData += "    <a class='jgrid Delete" + (bolCheckExist ? string.Empty : " a-hide") + "' title='Xóa' href='javascript:void(0);' onclick=\"AreaDelete(urlArea_Delete,this,'" + item.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state delete'></span></span></a>";
                    sData += "</td>";
                    sData += "</tr>";
                }
            }


            return sData;
        }

        /// <summary>
        /// True: Đã tồn tại | False: Chưa tồn tại
        /// </summary>
        /// <param name="lstInfo_Agent_All"></param>
        /// <param name="iCurrentID"></param>
        /// <returns></returns>
        private bool CheckExistArea(List<ModProduct_Info_AreaInNationalEntity> lstInfo_Area_In, int iCurrentID)
        {
            if (iCurrentID <= 0 || (lstInfo_Area_In == null || lstInfo_Area_In.Count <= 0))
                return false;

            ModProduct_Info_AreaInNationalEntity objExist = lstInfo_Area_In.Where(p => p.ProductNationalAreaId == iCurrentID).SingleOrDefault();
            if (objExist == null)
                return false;

            return true;
        }

        #endregion

        #region Danh sách các thuộc tính lọc
        /// <summary>
        /// CanTv
        /// Lấy danh sách các thuộc tính lọc bán sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListFilter(int RecordID)
        {
            string sData = string.Empty;
            List<ModProduct_FilterEntity> lstProduct_Filter_All = ModProduct_FilterService.Instance.CreateQuery().OrderByAsc(o => o.Order).ToList();
            if (lstProduct_Filter_All == null || lstProduct_Filter_All.Count <= 0)
                return sData;

            List<ModProduct_Info_PropertyFilterEntity> lstProduct_Info_PropertyFilter_In = ModProduct_Info_PropertyFilterService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();

            if (lstProduct_Info_PropertyFilter_In == null)
                lstProduct_Info_PropertyFilter_In = new List<ModProduct_Info_PropertyFilterEntity>();

            // Lất hết quốc gia đang sử dụng
            List<ModProduct_FilterGroupsEntity> lstProduct_FilterGroups_All = ModProduct_FilterGroupsService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            int iFilter = -1;
            int iSTT = 0;
            bool bolCheckExist = false;
            List<ModProduct_FilterEntity> lstInfo_Agent_Filter = null;

            foreach (var itemFilterGroup in lstProduct_FilterGroups_All)
            {
                // Quyết định màu của dòng
                iFilter++;

                // Dòng Nhóm thuộc tính lọc
                #region Dòng Nhóm thuộc tính lọc
                sData += "<tr class='row" + iFilter % 2 + "'>";
                sData += "<td colspan='6'>";
                sData += "<div><img class='img-icon-category' border='0' /> <label style='font-weight:bold;'>" + itemFilterGroup.Name + "</label></div>";
                sData += "</td></tr>";
                #endregion

                lstInfo_Agent_Filter = lstProduct_Filter_All.Where(p => p.FilterGroupsId == itemFilterGroup.ID).ToList();
                if (lstInfo_Agent_Filter == null || lstInfo_Agent_Filter.Count <= 0)
                    continue;

                foreach (ModProduct_FilterEntity item in lstInfo_Agent_Filter)
                {
                    // Quyết định màu của dòng
                    iFilter++;

                    sData += "<tr class='row" + iFilter % 2 + "'>";
                    sData += "<td align='center'>";
                    sData += (++iSTT);
                    sData += "</td>";
                    sData += "<td align='left'>";
                    sData += "<label>" + item.Value + "</label>";
                    sData += "</td>";
                    sData += "<td>";
                    sData += "<label>" + item.Note + "</label>";
                    sData += "</td>";

                    sData += "<td>";
                    sData += Utils.GetMedia(item.File, 40, 40);
                    sData += "</td>";

                    // Kiểm tra sự tồn tại
                    bolCheckExist = CheckExistFilter(lstProduct_Info_PropertyFilter_In, item.ID);

                    sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                    sData += "    <a class='jgrid Add" + (bolCheckExist ? " a-hide" : string.Empty) + "' title='Thêm' href='javascript:void(0);' onclick=\"FilterAdd(urlFilter_Add,this,'" + item.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state add'></span></span></a>";
                    sData += "</td>";
                    sData += "<td align='center'>";
                    sData += "    <a class='jgrid Delete" + (bolCheckExist ? string.Empty : " a-hide") + "' title='Xóa' href='javascript:void(0);' onclick=\"FilterDelete(urlFilter_Delete,this,'" + item.ID + "'); return false;\">";
                    sData += "        <span class='jgrid'><span class='state delete'></span></span></a>";
                    sData += "</td>";
                    sData += "</tr>";
                }
            }

            return sData;
        }

        /// <summary>
        /// True: Đã tồn tại | False: Chưa tồn tại
        /// </summary>
        /// <param name="lstInfo_Agent_All"></param>
        /// <param name="iCurrentID"></param>
        /// <returns></returns>
        private bool CheckExistFilter(List<ModProduct_Info_PropertyFilterEntity> lstInfo_Filter_In, int iCurrentID)
        {
            if (iCurrentID <= 0 || (lstInfo_Filter_In == null || lstInfo_Filter_In.Count <= 0))
                return false;

            ModProduct_Info_PropertyFilterEntity objExist = lstInfo_Filter_In.Where(p => p.ProductFilterId == iCurrentID).SingleOrDefault();
            if (objExist == null)
                return false;

            return true;
        }
        #endregion

        #region Danh sách các ảnh của sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các ảnh sildeshow của sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListSlideShow(int RecordID)
        {
            string sData = string.Empty;

            List<ModProduct_SlideShowEntity> lstInfo_Color = ModProduct_SlideShowService.Instance
                .CreateQuery().Where(p => p.ProductInfoId == RecordID).OrderByAsc(o => o.Order).ToList();

            if (lstInfo_Color == null)
                lstInfo_Color = new List<ModProduct_SlideShowEntity>();

            int iImg = 0;
            foreach (ModProduct_SlideShowEntity item in lstInfo_Color)
            {
                sData += "<tr class='row" + iImg % 2 + "'>";
                sData += "<td align='center'>";
                sData += iImg + 1;
                sData += "</td>";
                sData += "<td align='center'>";
                sData += Utils.GetMedia(item.UrlFull, 100, 80, string.Empty, true, "id='img_view_" + iImg + "'");
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<input type='text' class='text_input' id='txtSlideShowName" + iImg + "' name='txtSlideShowName" + iImg + "' value='" + item.Name + "' />";
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<input type='text' class='text_input' readonly='readonly' style='width:75%;' id='txtSlideShowUrl" + iImg + "' name='txtSlideShowUrl" + iImg + "' value='" + item.UrlFull + "' />";
                sData += "<input type='button' class='text_input button-function' style='width:16%;' onclick=\"ShowFileForm('txtSlideShowUrl" + iImg + "');return false;\" value='Chọn ảnh' ";
                sData += "</td>";
                sData += "<td align='right'>";
                sData += "<input type='text' class='text_input' id='txtSlideShowOrder" + iImg + "' name='txtSlideShowOrder" + iImg + "' value='" + item.Order + "' />";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid color' title='Cập nhật' href='javascript:void(0);' onclick=\"ImageSlideShowSave(urlImageSlideShow_Save,this,'" + iImg + "','" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state save'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid color' title='Xóa' href='javascript:void(0);' onclick=\"ImageSlideShowDelete(urlImageSlideShow_Delete,'" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr>";
                iImg++;
            }

            #region Luôn tạo một dòng trắng
            sData += "<tr class='row" + iImg % 2 + "'>";
            sData += "<td align='center'>";
            sData += iImg + 1;
            sData += "</td>";
            sData += "<td>";
            sData += "</td>";
            sData += "<td align='left'>";
            sData += "<input type='text' class='text_input' id='txtSlideShowName" + iImg + "' name='txtSlideShowName" + iImg + "' value='' />";
            sData += "</td>";
            sData += "<td align='left'>";
            sData += "<input type='text' class='text_input' readonly='readonly' style='width:75%;' id='txtSlideShowUrl" + iImg + "' name='txtSlideShowUrl" + iImg + "' value='' />";
            sData += "<input type='button' class='text_input button-function' style='width:16%;' onclick=\"ShowFileForm('txtSlideShowUrl" + iImg + "');return false;\" value='Chọn ảnh' ";
            sData += "</td>";
            sData += "<td align='right'>";
            sData += "<input type='text' class='text_input' id='txtSlideShowOrder" + iImg + "' name='txtSlideShowOrder" + iImg + "' value='" + iImg + "' />";
            sData += "</td>";
            sData += "<td align='center'>";
            sData += "<a class='jgrid color' title='Thêm mới' href='javascript:void(0);' onclick=\"ImageSlideShowAdd(urlImageSlideShow_Add,this,'" + iImg + "'); return false;\"><span class='jgrid'>";
            sData += "<span class='state add'></span></span></a>";
            sData += "</td>";
            sData += "<td align='center'>";
            sData += "</td>";
            sData += "</tr>";
            #endregion

            return sData;
        }

        #endregion

        #region Danh sách các thuộc tính kỹ thuật của sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các thuộc tính (thông tin kỹ thuật) sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListProperties(int RecordID, int iMenuId, ref ModProduct_InfoModel model)
        {
            string sData = string.Empty;

            // Chủng loại
            WebMenuEntity objMenu = WebMenuService.Instance.GetByID(iMenuId);
            if (objMenu == null || objMenu.ProductAreaId == null)
                return sData;

            // Lĩnh vực của sản phẩm
            ModProduct_AreaEntity objProduct_Area = ModProduct_AreaService.Instance.GetByID((int)objMenu.ProductAreaId);
            if (objProduct_Area == null)
                return sData;

            #region Lấy tất cả các dữ liệu nhóm theo lĩnh vực
            // Lấy danh sách các nhóm thuộc tính
            List<ModProduct_Area_PropretyGroupEntity> lstArea_PropretyGroup_All = ModProduct_Area_PropretyGroupService.Instance.CreateQuery()
                                                                                .Where(p => p.ProductAreaId == objProduct_Area.ID)
                                                                                .ToList();
            if (lstArea_PropretyGroup_All == null || lstArea_PropretyGroup_All.Count <= 0)
                return sData;

            // Lấy danh sách các chuỗi Nhóm thuộc tính
            string sListPropretyGroupId_All = string.Empty;
            var Area_PropretyGroup_All = lstArea_PropretyGroup_All.Select(o => o.PropertiesGroupId).ToList();
            if (Area_PropretyGroup_All != null)
            {
                foreach (var itemString in Area_PropretyGroup_All)
                {
                    sListPropretyGroupId_All += "," + itemString;
                }

                sListPropretyGroupId_All = sListPropretyGroupId_All.Trim(',');
            }

            // Lấy tất cả nhóm thuộc tính
            List<ModProduct_PropertiesGroupsEntity> lstPropretyGroup_All = ModProduct_PropertiesGroupsService.Instance.CreateQuery()
                .Where(p => p.Activity == true)
                .WhereIn(p => p.ID, sListPropretyGroupId_All).OrderByAsc(o => o.Order).ToList();
            if (lstPropretyGroup_All == null || lstPropretyGroup_All.Count <= 0)
                return sData;

            // Lấy tất cả thuộc tính thuộc các nhóm trên
            List<ModProduct_PropertiesListEntity> lstPropreties_All = ModProduct_PropertiesListService.Instance.CreateQuery()
                .Where(p => p.Activity == true)
                .WhereIn(p => p.PropertiesGroupsId, sListPropretyGroupId_All)
                .OrderByAsc(o => o.Order).ToList();
            if (lstPropreties_All == null)
                lstPropreties_All = new List<ModProduct_PropertiesListEntity>();

            string sPropertiesListId = string.Empty;
            string sListPropretyGroupId_ByPropertiesListId = string.Empty;
            foreach (var itemString in lstPropreties_All)
            {
                sPropertiesListId += "," + itemString.ID;
                sListPropretyGroupId_ByPropertiesListId += "," + itemString.PropertiesGroupsId;
            }

            sPropertiesListId = sPropertiesListId.Trim(',');
            sListPropretyGroupId_ByPropertiesListId = sListPropretyGroupId_ByPropertiesListId.Trim(',');

            // Lưu lại danh sách ID các thuộc tính
            model.ListPropertiesId = sPropertiesListId;
            model.ListPropretyGroupId_ByPropertiesListId = sListPropretyGroupId_ByPropertiesListId;

            // Lấy tất cả những giá trị cũ của các thuộc tính
            List<ModProduct_PropertiesList_ValuesEntity> lstPropertiesList_Values_All = null;
            if (!string.IsNullOrEmpty(sPropertiesListId))
                lstPropertiesList_Values_All = ModProduct_PropertiesList_ValuesService.Instance.CreateQuery().WhereIn(p => p.PropertiesListId, sPropertiesListId).ToList();

            if (lstPropertiesList_Values_All == null)
                lstPropertiesList_Values_All = new List<ModProduct_PropertiesList_ValuesEntity>();
            #endregion


            // Lấy danh sách các giá trị của nhóm thuộc tính
            List<ModProduct_Info_DetailsEntity> lstProduct_Info_Details = ModProduct_Info_DetailsService.Instance.CreateQuery()
                                                                                .Where(p => p.ProductInfoId == RecordID)
                                                                                .WhereIn(p => p.PropertiesGroupsId, sListPropretyGroupId_All)
                                                                                .ToList();
            if (lstProduct_Info_Details == null)
                lstProduct_Info_Details = new List<ModProduct_Info_DetailsEntity>();

            int iProperties = -1;
            int iSTT = 0;
            List<ModProduct_PropertiesListEntity> lstPropreties_Filter = null;

            foreach (var itemPropertiesGroup in lstPropretyGroup_All)
            {
                // Quyết định màu của dòng
                iProperties++;

                // Dòng Nhóm thuộc tính
                #region Dòng Nhóm thuộc tính
                sData += "<tr class='row" + iProperties % 2 + "'>";
                sData += "<td colspan='6' style='background-color: #7CAEFA;color:white;'>";
                sData += "<div><img class='img-icon-category' border='0' /> <label style='font-weight:bold;'>" + itemPropertiesGroup.Name + "</label></div>";
                sData += "</td></tr>";
                #endregion

                lstPropreties_Filter = lstPropreties_All.Where(p => p.PropertiesGroupsId == itemPropertiesGroup.ID)
                    .OrderBy(o => o.Order)
                    .ToList();
                if (lstPropreties_Filter == null || lstPropreties_Filter.Count <= 0)
                    continue;

                foreach (ModProduct_PropertiesListEntity itemPropertiesList in lstPropreties_Filter)
                {
                    // Quyết định màu của dòng
                    iProperties++;

                    sData += "<tr class='row" + iProperties % 2 + "'>";
                    // Số thứ tự
                    sData += "<td align='center'>";
                    sData += (++iSTT);
                    sData += "</td>";

                    // Tên thuộc tính
                    sData += "<td align='left' nowrap='nowrap'>";
                    sData += "<label>" + itemPropertiesList.Name + "</label>";
                    sData += "</td>";

                    // Dropdown Cho chọn dữ liệu cũ
                    sData += "<td align='left'>";
                    sData += GetDropDownList_PropertiesValue(lstPropertiesList_Values_All, itemPropertiesList.ID);
                    sData += "</td>";

                    // Ô nhập giá trị cho thuộc tính
                    sData += "<td>";
                    sData += GetValue_PropertiesValue(lstProduct_Info_Details, itemPropertiesList.ID);
                    sData += "</td>";

                    // Đơn vị tính
                    sData += "<td align='center'>";
                    sData += "<label>" + itemPropertiesList.Unit + "</label>";
                    sData += "</td>";

                    // Lưu dữ liệu
                    sData += "<td align='center'>";
                    sData += "<input type='checkbox' name='chkPropertySaveData" + itemPropertiesList.ID + "' checked='checked'>";
                    sData += "<input type='hidden' name='hidPropertySaveData" + itemPropertiesList.ID + "' value='1'>";
                    sData += "</td>";
                    sData += "</tr>";
                }
            }

            if (lstPropreties_All != null && lstPropreties_All.Count > 0)
            {
                #region Dòng chức năng
                iProperties++;
                sData += "<tr class='row" + iProperties % 2 + "'>";
                sData += "<td colspan='6' align='right'>";
                sData += "";
                sData += "<div style='text-align: right; margin-top: 5px; margin-bottom: 5px;'>";
                sData += "    <input id='btnPropertiesSave' value='Lưu thông tin' class='text_input button-function button-background-image-save' style='width: 150px' type='button'>";
                sData += "</div>";
                sData += "</td></tr>";
                #endregion
            }

            return sData;
        }

        /// <summary>
        /// Lấy danh sách các giá trị cũ của thuộc tính lên dropdownlist để người dùng có thể lựa chọn
        /// </summary>
        /// <param name="lstPropertiesList_Values_All"></param>
        /// <param name="iPropertyId"></param>
        /// <returns></returns>
        private string GetDropDownList_PropertiesValue(List<ModProduct_PropertiesList_ValuesEntity> lstPropertiesList_Values_All, int iPropertyId)
        {
            string sData = "<select controlset='txtPropertyValue" + iPropertyId + "' class='DropDownList'>";

            List<ModProduct_PropertiesList_ValuesEntity> lstPropertiesList_Values_Filter = lstPropertiesList_Values_All.Where(p => p.PropertiesListId == iPropertyId).OrderBy(o => o.Content).ToList();
            if (lstPropertiesList_Values_Filter == null || lstPropertiesList_Values_Filter.Count <= 0)
            {
                sData += "</select>";
                return sData;
            }

            sData += "<option>--- Chọn dữ liệu ---</option>";
            foreach (ModProduct_PropertiesList_ValuesEntity itemPropertiesList_Value in lstPropertiesList_Values_Filter)
            {
                sData += "<option>" + itemPropertiesList_Value.Content + "</option>";
            }

            sData += "</select>";

            return sData;
        }

        /// <summary>
        /// Lấy giá trị thuộc tính đưa lên textbox (Nếu có)
        /// </summary>
        /// <param name="lstProduct_Info_Details"></param>
        /// <param name="iPropertyId"></param>
        /// <returns></returns>
        private string GetValue_PropertiesValue(List<ModProduct_Info_DetailsEntity> lstProduct_Info_Details, int iPropertyId)
        {
            string sValue = string.Empty;
            ModProduct_Info_DetailsEntity Product_Info_Detail = lstProduct_Info_Details.Where(p => p.PropertiesListId == iPropertyId).SingleOrDefault();
            if (Product_Info_Detail != null)
                sValue = Product_Info_Detail.Content;

            return "<input type='text' class='text_input' name='txtPropertyValue" + iPropertyId + "' id='txtPropertyValue" + iPropertyId + "' maxlength='500' style='width: 98%;' value='" + sValue + "'></input>";
        }

        #endregion

        #region Danh sách các bình luận của sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách các bình luận của sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListComment(int RecordID)
        {
            string sData = string.Empty;

            List<ModProduct_CommentsEntity> lstProduct_Comments = ModProduct_CommentsService.Instance
                .CreateQuery().Where(p => p.ProductInfoId == RecordID).OrderByAsc(o => o.CreateDate).ToList();

            if (lstProduct_Comments == null)
                lstProduct_Comments = new List<ModProduct_CommentsEntity>();

            int iComment = 0;
            foreach (ModProduct_CommentsEntity item in lstProduct_Comments)
            {
                sData += "<tr class='row" + iComment % 2 + "'>";
                sData += "<td align='center' nowrap='nowrap'>";
                sData += iComment + 1;
                sData += "</td>";
                sData += "<td>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate);
                sData += "</td>";
                sData += "<td align='left' nowrap='nowrap'>";
                sData += "<label>" + item.Name + "</label>";
                sData += "</td>";
                sData += "<td align='left' nowrap='nowrap'>";
                sData += "<label>" + item.PhoneNumber + "</label>";
                sData += "</td>";
                sData += "<td align='left' nowrap='nowrap'>";
                sData += "<label>" + item.Email + "</label>";
                sData += "</td>";
                sData += "<td align='left'>";
                sData += "<label id='lblCommentContent" + item.ID + "'>" + item.Content.Replace(Environment.NewLine, "<br/>") + "</label>";
                sData += "<textarea rows='4' class='text_input hide' style='width:98%;' id='txtCommentContent" + item.ID + "' name='txtCommentContent" + item.ID + "'>" + item.Content + "</textarea>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid comment-edit' title='Chỉnh sửa' href='javascript:void(0);' controlshow='txtCommentContent" + item.ID + "' controlhide='lblCommentContent" + item.ID + "'><span class='jgrid'>";
                sData += "<span class='state edit'></span></span></a>";
                sData += "<a class='jgrid comment-save hide' title='Cập nhật' href='javascript:void(0);' onclick=\"CommentSave(urlComment_Save,this,'" + item.ID + "','txtCommentContent" + item.ID + "','lblCommentContent" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state save'></span></span></a>";
                sData += "<a class='jgrid comment-cancel hide' title='Hủy' href='javascript:void(0);' controlshow='lblCommentContent" + item.ID + "' controlhide='txtCommentContent" + item.ID + "'><span class='jgrid'>";
                sData += "<span class='state publish_r'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";

                sData += "<a class='jgrid comment-approve" + (item.Approved ? " hide" : "") + "' title='Duyệt' href='javascript:void(0);' onclick=\"CommentApprove(urlComment_Approve,this,'" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state activate'></span></span></a>";
                sData += "<a class='jgrid comment-unapprove" + (item.Approved ? "" : " hide") + "' title='Hủy duyệt' href='javascript:void(0);' onclick=\"CommentUnApprove(urlComment_UnApprove,this,'" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state publish_r'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid color' title='Xóa bình luận' href='javascript:void(0);' onclick=\"CommentDelete(urlComment_Delete,this,'" + item.ID + "'); return false;\"><span class='jgrid'>";
                sData += "<span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr>";
                iComment++;
            }
            return sData;
        }
        #endregion

        #region Thông tin loại của sản phẩm
        /// <summary>
        /// CanTv
        /// Lấy danh sách loại mà sản phẩm thuộc
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string GetListTypes(int RecordID)
        {
            string sData = string.Empty;
            List<ModProduct_TypesEntity> lstInfo_Types_All = ModProduct_TypesService.Instance.CreateQuery().OrderByAsc(o => o.Name).ToList();
            if (lstInfo_Types_All == null || lstInfo_Types_All.Count <= 0)
                return sData;

            List<ModProduct_Info_TypesEntity> lstInfo_Types_In = ModProduct_Info_TypesService.Instance.CreateQuery().Where(p => p.ProductInfoId == RecordID).ToList();

            if (lstInfo_Types_In == null)
                lstInfo_Types_In = new List<ModProduct_Info_TypesEntity>();

            int iTypes = 0;
            bool bolCheckExist = false;
            foreach (ModProduct_TypesEntity item in lstInfo_Types_All)
            {
                sData += "<tr class='row" + iTypes % 2 + "'>";
                sData += "<td align='center'>";
                sData += iTypes + 1;
                sData += "</td>";
                sData += "<td align='left' nowrap='nowrap'>";
                sData += "<label>" + item.Code + "</label>";
                sData += "</td>";
                sData += "<td nowrap='nowrap'>";
                sData += "<label>" + item.Name + "</label>";
                sData += "</td>";

                // Kiểm tra sự tồn tại
                bolCheckExist = CheckExistTypes(lstInfo_Types_In, item.ID);

                sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                sData += "    <a class='jgrid Add" + (bolCheckExist ? " a-hide" : string.Empty) + "' title='Thêm' href='javascript:void(0);' onclick=\"TypesAdd(urlTypes_Add,this,'" + item.ID + "'); return false;\">";
                sData += "        <span class='jgrid'><span class='state add'></span></span></a>";
                sData += "</td>";
                sData += "<td align='center'>";
                sData += "    <a class='jgrid Delete" + (bolCheckExist ? string.Empty : " a-hide") + "' title='Xóa' href='javascript:void(0);' onclick=\"TypesDelete(urlTypes_Delete,this,'" + item.ID + "'); return false;\">";
                sData += "        <span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr>";
                iTypes++;
            }
            return sData;
        }

        /// <summary>
        /// True: Đã tồn tại | False: Chưa tồn tại
        /// </summary>
        /// <param name="lstInfo_Types_All"></param>
        /// <param name="iCurrentID"></param>
        /// <returns></returns>
        private bool CheckExistTypes(List<ModProduct_Info_TypesEntity> lstInfo_Types_In, int iCurrentID)
        {
            if (iCurrentID <= 0 || (lstInfo_Types_In == null || lstInfo_Types_In.Count <= 0))
                return false;

            ModProduct_Info_TypesEntity objExist = lstInfo_Types_In.Where(p => p.ProductTypesId == iCurrentID).SingleOrDefault();
            if (objExist == null)
                return false;

            return true;
        }

        #endregion

        public void ActionSave(ModProduct_InfoModel model)
        {
            if (ValidSave(model))
                SaveRedirect();
        }

        public void ActionApply(ModProduct_InfoModel model)
        {
            if (ValidSave(model))
                ApplyRedirect(model.RecordID, item.ID);
        }

        public void ActionSaveNew(ModProduct_InfoModel model)
        {
            if (ValidSave(model))
                SaveNewRedirect(model.RecordID, item.ID);
        }

        public void ActionSaleGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@Sale", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        public void ActionNewGX(int[] arrID)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            DataService.Update("[ID]=" + arrID[0],
                        "@New", arrID[1]);

            //thong bao
            CPViewPage.SetMessage("Đã thực hiện thành công.");
            CPViewPage.RefreshPage();
        }

        public void ActionProperties()
        {
            CPViewPage.Response.Redirect(CPViewPage.Request.RawUrl.Replace("ModProduct_Info", "ModProduct_Info_Details"));
        }

        public override void ActionCancel()
        {
            string sUrl = CPViewPage.Request.RawUrl.Replace("Add.aspx", "Index.aspx")
                .Replace("Properties.aspx", "Index.aspx");

            int iIndex = sUrl.LastIndexOf('/');
            sUrl = sUrl.Substring(0, iIndex);

            iIndex = sUrl.LastIndexOf('/');
            sUrl = sUrl.Substring(0, iIndex);

            CPViewPage.Response.Redirect(sUrl);
        }

        public void ActionSearchProduct_LienQuan(ModProduct_InfoModel model)
        {
            TryUpdateModel(item);
            model.ModProductInfoSearch_Code_LienQuan = model.ModProductInfoSearch_Code_LienQuan.Trim();
            model.ModProductInfoSearch_Name_LienQuan = model.ModProductInfoSearch_Name_LienQuan.Trim();

            dynamic DbQuery = null;
            if (!string.IsNullOrEmpty(model.ModProductInfoSearch_Code_LienQuan) && !string.IsNullOrEmpty(model.ModProductInfoSearch_Name_LienQuan))
            {
                if (model.ModSearchManufacturerId_LienQuan != 0)
                {
                    DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                            .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_LienQuan) &&
                                o.Name.Contains(model.ModProductInfoSearch_Name_LienQuan) &&
                                o.ManufacturerId == model.ModSearchManufacturerId_LienQuan)
                                .Take(model.PageSize)
                                .Skip(model.PageIndex * model.PageSize)
                                ;
                }
                else
                {
                    DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_LienQuan) &&
                            o.Name.Contains(model.ModProductInfoSearch_Name_LienQuan))
                            .Take(model.PageSize)
                            .Skip(model.PageIndex * model.PageSize)
                            ;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.ModProductInfoSearch_Code_LienQuan))
                {
                    if (model.ModSearchManufacturerId_LienQuan != 0)
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_LienQuan) && o.ManufacturerId == model.ModSearchManufacturerId_LienQuan)
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                    else
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_LienQuan))
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                }
                else
                {
                    if (model.ModSearchManufacturerId_LienQuan != 0)
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Name.Contains(model.ModProductInfoSearch_Name_LienQuan))
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                    else
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Name.Contains(model.ModProductInfoSearch_Name_LienQuan) && o.ManufacturerId == model.ModSearchManufacturerId_LienQuan)
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                }
            }

            LoadDataProductCurrent(ref model);
            model.listItem_LienQuan = DbQuery.ToList();
            model.TotalRecord = DbQuery.TotalRecord;
            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSearchProduct_BanKem(ModProduct_InfoModel model)
        {
            TryUpdateModel(item);
            model.ModProductInfoSearch_Code_BanKem = model.ModProductInfoSearch_Code_BanKem.Trim();
            model.ModProductInfoSearch_Name_BanKem = model.ModProductInfoSearch_Name_BanKem.Trim();

            dynamic DbQuery = null;
            if (!string.IsNullOrEmpty(model.ModProductInfoSearch_Code_BanKem) && !string.IsNullOrEmpty(model.ModProductInfoSearch_Name_BanKem))
            {
                if (model.ModSearchManufacturerId_BanKem != 0)
                {
                    DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                            .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_BanKem) &&
                                o.Name.Contains(model.ModProductInfoSearch_Name_BanKem) &&
                                o.ManufacturerId == model.ModSearchManufacturerId_BanKem)
                                .Take(model.PageSize)
                                .Skip(model.PageIndex * model.PageSize)
                                ;
                }
                else
                {
                    DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_BanKem) &&
                            o.Name.Contains(model.ModProductInfoSearch_Name_BanKem))
                            .Take(model.PageSize)
                            .Skip(model.PageIndex * model.PageSize)
                            ;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(model.ModProductInfoSearch_Code_BanKem))
                {
                    if (model.ModSearchManufacturerId_BanKem != 0)
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_BanKem) && o.ManufacturerId == model.ModSearchManufacturerId_BanKem)
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                    else
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Code.Contains(model.ModProductInfoSearch_Code_BanKem))
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                }
                else
                {
                    if (model.ModSearchManufacturerId_BanKem != 0)
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Name.Contains(model.ModProductInfoSearch_Name_BanKem))
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                    else
                    {
                        DbQuery = ModProduct_InfoService.Instance.CreateQuery()
                        .Where(o => o.Name.Contains(model.ModProductInfoSearch_Name_BanKem) && o.ManufacturerId == model.ModSearchManufacturerId_BanKem)
                        .Take(model.PageSize)
                        .Skip(model.PageIndex * model.PageSize)
                        ;
                    }
                }
            }

            LoadDataProductCurrent(ref model);
            model.listItem_BanKem = DbQuery.ToList();
            model.TotalRecord = DbQuery.TotalRecord;
            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void LoadDataProductCurrent(ref ModProduct_InfoModel model)
        {
            model.listItem_BanKem_Current = null;
            model.listItem_LienQuan_Current = null;

            model.ListIdProduct_BanKem = string.IsNullOrEmpty(model.ListIdProduct_BanKem) ? string.Empty : model.ListIdProduct_BanKem.Trim().Trim(',');
            model.ListIdProduct_LienQuan = string.IsNullOrEmpty(model.ListIdProduct_LienQuan) ? string.Empty : model.ListIdProduct_LienQuan.Trim().Trim(',');

            if (!string.IsNullOrEmpty(model.ListIdProduct_BanKem))
                model.listItem_BanKem_Current = ModProduct_InfoService.Instance.CreateQuery().WhereIn(o => o.ID, model.ListIdProduct_BanKem).ToList();

            if (!string.IsNullOrEmpty(model.ListIdProduct_LienQuan))
                model.listItem_LienQuan_Current = ModProduct_InfoService.Instance.CreateQuery().WhereIn(o => o.ID, model.ListIdProduct_LienQuan).ToList();

            model.GetListManufacture = ModProduct_ManufacturerService.Instance.CreateQuery()
            .Where(o => o.Activity == true)
            .ToList();

            int iRecordID = model.RecordID;

            if (model.RecordID > 0)
            {
                // Link xem lịch sử thay đổi giá
                model.UrlHistory = "tb_show('', '" + CPViewPage.Request.RawUrl.Replace("ModProduct_Info", "FormProduct_Price_History").Replace("Add", "Index").Replace("RecordID", "ProductInfoId") + "?TB_iframe=true;height=400;width=700;', ''); return false;";

                // Link tải ảnh SlideShow
                ModProduct_SlideShowEntity objSlideShowEntity = ModProduct_SlideShowService.Instance.CreateQuery()
                                                                .Where(o => o.ProductInfoId == iRecordID)
                                                                .ToSingle();

                model.UrlSlideShow = "tb_show('', '"
                    + Utils.GetUrl(CPViewPage.Request.RawUrl, "ModProduct_Info")
                    + "/FormProduct_SlideShow/Add.aspx/RecordID/"
                    + (objSlideShowEntity == null ? 0 : objSlideShowEntity.ID).ToString()
                    + "?TB_iframe=true;height=400;width=700;', ''); return false;";

                // Link View Properties
                //ModProduct_Info_DetailsEntity objInfo_Details = ModProduct_Info_DetailsService.Instance.CreateQuery()
                //                                .Where(o => o.ProductInfoId == iRecordID)
                //                                .ToSingle();

                model.UrlProperties = "tb_show('', '"
                    + Utils.GetUrl(CPViewPage.Request.RawUrl, "ModProduct_Info")
                    + "/FormProduct_Info_Details/Add.aspx/ProductInfoId/"
                    + model.RecordID.ToString()
                    + "?TB_iframe=true;height=500;width=850;', ''); return false;";

                // Link to Comment Product 
                model.UrlComment = "tb_show('', '"
                    + Utils.GetUrl(CPViewPage.Request.RawUrl, "ModProduct_Info")
                    + "/FormProduct_Comments/Index.aspx/ProductInfoId/"
                    + model.RecordID
                    + "?TB_iframe=true;height=500;width=850;', ''); return false;";

                // History SaleOff
                model.UrlPriceSaleOffHistory = "tb_show('', '"
                    + CPViewPage.Request.RawUrl.Replace("ModProduct_Info", "FormProduct_PriceSale_History").Replace("Add", "Index").Replace("RecordID", "ProductInfoId")
                    + "?TB_iframe=true;height=450;width=800;', ''); return false;";
            }
        }

        public void ActionSelectedProduct_LienQuan(ModProduct_InfoModel model)
        {
            TryUpdateModel(item);

            if (!string.IsNullOrEmpty(model.ListIdProduct_LienQuan))
                model.ListIdProduct_LienQuan += ",";

            if (!string.IsNullOrEmpty(model.Selected_Product_LienQuan.ToString()))
                model.ListIdProduct_LienQuan += model.Selected_Product_LienQuan;

            ActionSearchProduct_LienQuan(model);

            LoadDataProductCurrent(ref model);

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        public void ActionSelectedProduct_BanKem(ModProduct_InfoModel model)
        {
            TryUpdateModel(item);

            if (!string.IsNullOrEmpty(model.ListIdProduct_BanKem))
                model.ListIdProduct_BanKem += ",";

            if (!string.IsNullOrEmpty(model.Selected_Product_BanKem.ToString()))
                model.ListIdProduct_BanKem += model.Selected_Product_BanKem;

            ActionSearchProduct_BanKem(model);

            LoadDataProductCurrent(ref model);

            ViewBag.Data = item;
            ViewBag.Model = model;
        }

        #region private func

        private ModProduct_InfoEntity item = null;

        private bool ValidSave(ModProduct_InfoModel model)
        {
            TryUpdateModel(item);

            //chong hack
            item.ID = model.RecordID;

            ViewBag.Data = item;
            ViewBag.Model = model;

            CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;

            //kiem tra quyen han
            if ((model.RecordID < 1 && !CPViewPage.UserPermissions.Add) || (model.RecordID > 0 && !CPViewPage.UserPermissions.Edit))
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");

            //kiem tra chuyen muc
            //if (item.MenuID < 1)
            //    //CPViewPage.Message.ListMessage.Add("Yêu cầu chọn chuyên mục.");
            //    item.MenuID = 871;

            // Kiểm tra thời gian nhập vào
            //if (item.RuntimeDateStart.Value.Year == 0001)
            //    item.RuntimeDateStart = null;

            //if (item.RuntimeDateFinish.Value.Year == 0001)
            //    item.RuntimeDateFinish = null;

            if (CPViewPage.Message.ListMessage.Count == 0)
            {
                //neu khong nhap code -> tu sinh
                if (item.Code.Trim() == string.Empty)
                    item.Code = Data.GetCode(item.Name);

                // Kiểm tra mã xem có trùng với mã nào khác đã có không
                string sMessError = string.Empty;
                if (ModProduct_InfoService.Instance.DuplicateCode(item.Code, model.RecordID, ref sMessError))
                {
                    if (string.IsNullOrEmpty(sMessError))
                        CPViewPage.Message.ListMessage.Add(CPViewControl.ShowMessDuplicate("Mã sản phẩm", item.Code));
                    else
                        CPViewPage.Message.ListMessage.Add("Lỗi phát sinh: " + sMessError);
                    return false;
                }

                // Nếu không có nhà sản xuất
                if (model.ModelManufacturerId == 0)
                    item.ManufacturerId = null;
                else
                    item.ManufacturerId = model.ModelManufacturerId;

                try
                {
                    bool bolCreateSlideShow = true;
                    if (item.ID > 0)
                        bolCreateSlideShow = false;

                    if (string.IsNullOrEmpty(item.File.Trim()))
                        item.File = "~/Data/upload/files/Products/Empty.gif";

                    //item.ProductsAttach = model.ListIdProduct_BanKem;
                    //item.ProductsConnection = model.ListIdProduct_LienQuan;
                    item.ModifiedDate = DateTime.Now;
                    item.UserId = VSW.Core.Global.Convert.ToInt(Cookies.GetValue("CP.UserID", true));

                    // Cập nhật lại khuyến mãi
                    //if (item.SaleOff)
                    //{
                    //    DateTime dStart = Convert.ToDateTime(model.ModDateTimeStart + " " + model.ModHourStart + ":" + model.ModMinuteStart + ":00");
                    //    DateTime dFinish = Convert.ToDateTime(model.ModDateTimeFinish + " " + model.ModHourFinish + ":" + model.ModMinuteFinish + ":00");

                    //    item.StartDate = dStart;
                    //    item.FinishDate = dFinish;

                    //    UpdatePriceSaleOff(ref item);
                    //}

                    // Kiểm tra ngôn ngữ
                    if (item.LangID <= 0)
                        item.LangID = 1; // Mặc định Việt Nam

                    //save - Lưu thông tin thay đổi
                    ModProduct_InfoService.Instance.Save(item);
                     
                    // update
                    // Cập nhật lịch sử thay đổi tiền
                    UpdatePriceHistory(item, model);
                     
                    // Cập nhật lịch sử giá khuyến mãi
                    UpdateSaleOff_History(item); 
                }
                catch (Exception ex)
                {
                    Global.Error.Write(ex);
                    CPViewPage.Message.ListMessage.Add(ex.Message);
                    return false;
                }

                return true;
            }

            return false;
        }

        public override void ActionCopy(int id)
        {
            if (CheckPermissions && !CPViewPage.UserPermissions.Approve)
            {
                //thong bao
                CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                return;
            }

            dynamic item = DataService.GetByID(id);

            item.ID = 0;
            item.Name = item.Name + " - (Bản sao)";

            DataService.Save(item);
            
            //thong bao
            CPViewPage.SetMessage("Sao chép thành công.");
            CPViewPage.RefreshPage();
        }

        public void UpdatePriceHistory(ModProduct_InfoEntity it, ModProduct_InfoModel md)
        {
            ModProduct_Price_HistoryEntity objPriceHistory = new ModProduct_Price_HistoryEntity();
            int _UserID = VSW.Core.Global.Convert.ToInt(Cookies.GetValue("CP.UserID", true));
            bool type = false;
            double dAfterPrice = item.Price;

            // Nếu cập nhật
            if (md.RecordID > 0)
            {
                objPriceHistory.UserId = _UserID;
                objPriceHistory.ProductInfoId = md.RecordID;
                objPriceHistory.BeforePrice = md.ModPriceOld;

                if (dAfterPrice - md.ModPriceOld >= 0)
                    type = true;

                objPriceHistory.AfterPrice = dAfterPrice;
                objPriceHistory.Type = type;
                objPriceHistory.CreateDate = DateTime.Now;
            }
            else
            {
                objPriceHistory.UserId = _UserID;
                objPriceHistory.ProductInfoId = item.ID;
                objPriceHistory.BeforePrice = 0;
                objPriceHistory.AfterPrice = dAfterPrice;
                objPriceHistory.Type = true; // Thêm mới sản phẩm, mặc định là tăng
                objPriceHistory.CreateDate = DateTime.Now;
            }

            ModProduct_Price_HistoryService.Instance.Save(objPriceHistory);
        }

        private void UpdateProduct_Agent(int iProductId, string ArrAgentIn)
        {
            ArrAgentIn = ArrAgentIn.Trim(',');
            string sQueryDelete = "[ProductInfoId]=" + iProductId;

            // Xóa dữ liệu cũ
            ModProduct_Info_AgentService.Instance.Delete(sQueryDelete);

            if (string.IsNullOrEmpty(ArrAgentIn))
                return;

            string[] lstAgentIn = ArrAgentIn.Split(',');
            if (lstAgentIn == null || lstAgentIn.Length <= 0)
                return;

            ModProduct_Info_AgentEntity objProduct_AgentEntity = null;
            foreach (string item in lstAgentIn)
            {
                objProduct_AgentEntity = new ModProduct_Info_AgentEntity();
                objProduct_AgentEntity.ProductInfoId = iProductId;
                objProduct_AgentEntity.ProductAgeId = Convert.ToInt32(item);
                ModProduct_Info_AgentService.Instance.Save(objProduct_AgentEntity);
            }
        }

        private void UpdateProduct_PropertiesGroups(int iProductId, string ArrPropertiesGroupsIn)
        {
            ArrPropertiesGroupsIn = ArrPropertiesGroupsIn.Trim(',');
            string sQueryDelete = "[ProductInfoId]=" + iProductId;

            // Xóa dữ liệu cũ
            ModProduct_Groups_PropertiesGroupsService.Instance.Delete(sQueryDelete);

            if (string.IsNullOrEmpty(ArrPropertiesGroupsIn))
                return;

            string[] lstPropertiesGroupsIn = ArrPropertiesGroupsIn.Split(',');
            if (lstPropertiesGroupsIn == null || lstPropertiesGroupsIn.Length <= 0)
                return;

            ModProduct_Groups_PropertiesGroupsEntity objProductGroupsEntity = null;
            foreach (string item in lstPropertiesGroupsIn)
            {
                objProductGroupsEntity = new ModProduct_Groups_PropertiesGroupsEntity();
                //objProductGroupsEntity.ProductInfoId = iProductId;
                objProductGroupsEntity.PropertiesGroupsId = Convert.ToInt32(item);
                ModProduct_Groups_PropertiesGroupsService.Instance.Save(objProductGroupsEntity);
            }
        }

        private void UpdateSaleOff_History(ModProduct_InfoEntity it)
        {
            // Không giảm giá
            if (!it.SaleOff)
                return;

            // Giảm giá
            ModProduct_PriceSale_HistoryEntity objPriceSale_History = new ModProduct_PriceSale_HistoryEntity();
            objPriceSale_History.ProductInfoId = item.ID;
            objPriceSale_History.UserId = VSW.Core.Global.Convert.ToInt(Cookies.GetValue("CP.UserID", true)); ;
            objPriceSale_History.Price = it.Price;
            objPriceSale_History.SaleOffType = it.SaleOffType;
            objPriceSale_History.SaleOffValue = it.SaleOffValue;
            objPriceSale_History.PriceSale = it.PriceSale;
            objPriceSale_History.StartDate = it.StartDate;
            objPriceSale_History.FinishDate = it.FinishDate;
            objPriceSale_History.CreateDate = DateTime.Now;

            ModProduct_PriceSale_HistoryService.Instance.Save(objPriceSale_History);
        }

        private void UpdatePriceSaleOff(ref ModProduct_InfoEntity item)
        {
            // Kiểu giảm theo Giá tiền
            if (!item.SaleOffType)
                item.PriceSale = item.Price - item.SaleOffValue;

            // Giảm giá theo %
            else
            {
                double dSaleOffValueModified = (double)(item.Price * item.SaleOffValue) / 100;
                item.PriceSale = item.Price - dSaleOffValueModified;
            }
        }
        #endregion

        #region Tìm kiếm sản phẩm thêm vào đơn hàng (Bên quản lý doanh thu)
        // Thêm sản phẩm liên quan
        public void ActionFormDkKyDaiLyDonHang(ModProduct_InfoModel model)
        {
            model.ProductCurrent = new ModProduct_InfoEntity();
            // Lấy danh sách các sản phẩm đã có trong đơn hàng
            string strListProductExists = string.Empty;
            List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity> lstCacSanPhamDaCoTrongDonHang = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                                .Where(o => o.ModDTKyDaiLyDonHangId == model.RecordID).ToList();
            if (lstCacSanPhamDaCoTrongDonHang != null && lstCacSanPhamDaCoTrongDonHang.Count > 0)
            {
                strListProductExists = VSW.Core.Global.Array.ToString(lstCacSanPhamDaCoTrongDonHang.Select(o => o.ModProductId).ToList().ToArray());
            }

            // Các sản phẩm có trong đơn hàng
            model.ProductCurrent.ProductsConnection = strListProductExists;

            // Danh sách các phẩm
            model.ListIdProduct_LienQuan = model.ProductCurrent.ProductsConnection;
            string sSanPhamLienQuan_Code = model.ModProductInfoSearch_Code_LienQuan;
            string sSanPhamLienQuan_Name = model.ModProductInfoSearch_Name_LienQuan;
            int iProductGroupsId = model.ModProductInfoSearch_ProductGroupsId;
            int iSanPhamLienQuan_ManufacturerId = model.ModSearchManufacturerId_LienQuan;
            bool bolModProductInfoSearch_SeachChildren = model.ModProductInfoSearch_SeachChildren;

            // tao danh sach

            var dbQuery = ModProduct_InfoService.Instance.CreateQuery()
                                .Where(p => p.LangID == model.LangID)
                                .Where(!string.IsNullOrEmpty(sSanPhamLienQuan_Code), o => o.Code.Contains(sSanPhamLienQuan_Code))
                                .Where(!string.IsNullOrEmpty(sSanPhamLienQuan_Name), o => o.Name.Contains(sSanPhamLienQuan_Name))
                                .Where(iProductGroupsId > 0 && bolModProductInfoSearch_SeachChildren == false, o => o.MenuID == iProductGroupsId)
                                .WhereIn(iProductGroupsId > 0 && bolModProductInfoSearch_SeachChildren == true,
                                            o => o.MenuID, WebMenuService.Instance.GetChildIDForCP("Product", iProductGroupsId, model.LangID))
                                .Where(iSanPhamLienQuan_ManufacturerId > 0, o => o.ManufacturerId == iSanPhamLienQuan_ManufacturerId)
                                .Take(model.PageSize)
                                .Skip(model.PageIndex * model.PageSize);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            if (model.TotalRecord > 0)
                // Lấy danh sách tên nhà sản xuất
                model.GetListManufacture = ModProduct_ManufacturerService.Instance.CreateQuery().Where(p => p.Activity == true).ToList();

            ViewBag.Model = model;
        }

        #endregion

        public override void ActionDelete(int[] arrID)
        {
            string sMess = string.Empty;
            try
            {
                if (CheckPermissions && !CPViewPage.UserPermissions.Delete)
                {
                    //thong bao
                    CPViewPage.Message.MessageType = Message.MessageTypeEnum.Error;
                    CPViewPage.Message.ListMessage.Add("Quyền hạn chế.");
                    return;
                }

                //DataService.Delete("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")");
                DataService.Update("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")", "@Deleted", true);

                // ModProduct_CustomersGroupsService.Instance.Update("[ID] IN (" + VSW.Core.Global.Array.ToString(arrID) + ")", "@Deleted", true);

                // thành công
                sMess = "Đã xóa thành công.";
            }
            catch (Exception ex)
            {
                // Lỗi phát sinh
                sMess = "Xóa thất bại. Lỗi phát sinh trong quá trình xóa hoặc tồn tại ràng buộc dữ liệu:" + ex.Message;
            }

            //thong bao
            CPViewPage.SetMessage(sMess);
            CPViewPage.RefreshPage();
        }
    }

    public class ModProduct_InfoModel : DefaultModel
    {
        private int _LangID = 1;
        public int LangID
        {
            get { return _LangID; }
            set { _LangID = value; }
        }

        public int MenuID { get; set; }
        public string SearchText { get; set; }
        public int ProductGroupsId { get; set; }
        public List<ModProduct_ManufacturerEntity> GetListManufacture { get; set; }
        public ModProduct_InfoEntity ProductCurrent { get; set; }

        public string ModProductGroupsInId { get; set; }
        public string ModProductGroupsOutId { get; set; }

        public string ModProductAgentInId { get; set; }
        public string ModProductAgentOutId { get; set; }

        public string PropertiesGroupsInId { get; set; }
        public string PropertiesGroupsOutId { get; set; }

        public int ModelManufacturerId { get; set; }
        public string ModProductInfoSearch_Code_BanKem { get; set; }
        public string ModProductInfoSearch_Name_BanKem { get; set; }
        public int ModSearchManufacturerId_BanKem { get; set; }
        public bool bolProductSearch_LienQuan { get; set; }
        public bool bolProductSearch_BanKem { get; set; }
        public int Selected_Product_LienQuan { get; set; }
        public int Selected_Product_BanKem { get; set; }

        // Danh sách sản phẩm liên quan tìm kiếm đc
        public List<ModProduct_InfoEntity> listItem_LienQuan { get; set; }
        public List<ModProduct_InfoEntity> listItem_LienQuan_Current { get; set; }
        public string ListIdProduct_LienQuan { get; set; }
        public string ModProductInfoSearch_Code_LienQuan { get; set; }
        public string ModProductInfoSearch_Name_LienQuan { get; set; }
        public string ModProductInfoSearch_MA_PHAN_CAP_LienQuan { get; set; }
        public int ModSearchManufacturerId_LienQuan { get; set; }
        public int ModProductInfoSearch_ProductGroupsId { get; set; }

        private bool _ModProductInfoSearch_SeachChildren = true;
        public bool ModProductInfoSearch_SeachChildren
        {
            get
            {
                return _ModProductInfoSearch_SeachChildren;
            }
            set
            {
                _ModProductInfoSearch_SeachChildren = value;
            }
        }

        public string ListIdProduct_BanKem { get; set; }

        // Danh sách màu của sản phẩm: Dạng tr
        public string ListColor { get; set; }

        // Danh sách size của sản phẩm: Dạng tr
        public string ListSize { get; set; }

        // Danh sách các chủng loại liên quan: Dạng tr
        public string ListProductGroups { get; set; }

        // Danh sách các đại lý bán sản phẩm: Dạng tr
        public string ListAgent { get; set; }

        // Danh sách các khu vực bán sản phẩm: Dạng tr
        public string ListArea { get; set; }

        // Danh sách các thuộc tính lọc sản phẩm: Dạng tr
        public string ListFilter { get; set; }

        // Danh sách các ảnh slideshow của sản phẩm: Dạng tr
        public string ListSlideShow { get; set; }

        // Danh sách các properties của sản phẩm: Dạng tr
        public string ListProperties { get; set; }
        // Danh sách chuỗi các thuộc tính cách nhau bằng dấu "," (phục vụ cập nhật thuộc tính cho sản phẩm)
        public string ListPropertiesId { get; set; }
        // Danh sách chuỗi các nhóm thuộc tính (theo thuộc tính) cách nhau bằng dấu "," (phục vụ cập nhật thuộc tính cho sản phẩm)
        public string ListPropretyGroupId_ByPropertiesListId { get; set; }

        // Danh sách các bình luận của sản phẩm: Dạng tr
        public string ListComments { get; set; }

        // Danh sách các loại của sản phẩm: Dạng tr
        public string ListTypes { get; set; }

        // Danh sách sản phẩm bán kèm tìm thấy
        public List<ModProduct_InfoEntity> listItem_BanKem { get; set; }
        public List<ModProduct_InfoEntity> listItem_BanKem_Current { get; set; }
        public List<ModProduct_Price_HistoryEntity> listItem_History { get; set; }

        public int TabIndexCurrent { get; set; }
        public string UrlHistory { get; set; }
        public string UrlSlideShow { get; set; }
        public string UrlProperties { get; set; }
        public string UrlComment { get; set; }
        public string UrlPriceSaleOffHistory { get; set; }
        public double ModPriceOld { get; set; }

        public string ModDateTimeStart { get; set; }
        public int ModHourStart { get; set; }
        public int ModMinuteStart { get; set; }

        public string ModDateTimeFinish { get; set; }
        public int ModHourFinish { get; set; }
        public int ModMinuteFinish { get; set; }

    }

    public class Mod_Product_Info_Office_Full : ModProduct_OfficeEntity
    {
        public int ProductInfoId { get; set; }
        public int ProductOfficeId { get; set; }
        public int CountNumber { get; set; }
        public bool Using { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}

