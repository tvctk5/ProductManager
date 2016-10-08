using System;
using System.Collections.Generic;
using System.Linq;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.Controllers
{
    [ModuleInfo(Name = "MO : Product_ info", Code = "MProduct_Info", Order = 50)]
    public class MProduct_InfoController : Controller
    {

        [VSW.Core.MVC.PropertyInfo("Chuyên mục", "Type|Product")]
        public int MenuID;

        [VSW.Core.MVC.PropertyInfo("Số lượng")]
        public int PageSize = 10;

        public void ActionIndex(MProduct_InfoModel model)
        {
            if (ViewPage.CurrentPage.MenuID > 0)
                MenuID = ViewPage.CurrentPage.MenuID;

            var dbQuery = ModProduct_InfoService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.Deleted == false)
                            .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product_Info", MenuID, ViewPage.CurrentLang.ID))
                            .OrderByDesc(o => o.ID)
                            .Take(PageSize)
                            .Skip(PageSize * model.Page);

            ViewBag.Data = dbQuery.ToList();
            model.TotalRecord = dbQuery.TotalRecord;
            model.PageSize = PageSize;
            ViewBag.Model = model;

            // Lấy thông tin trang index hiện tại (danh sách các sản phẩm)
            var GetCurrentPage = ViewPage.GetCurrentPage();
            ViewPage.CurrentPage.PageTitle = GetCurrentPage.PageTitle;
            ViewPage.CurrentPage.PageDescription = GetCurrentPage.PageDescription;
            ViewPage.CurrentPage.PageKeywords = GetCurrentPage.PageKeywords;

            Session.Remove("ProductId");

            //Global.Cookies.SetValue("Product.Detail.ProductId", "");
        }

        public void ActionDetail(string endCode)
        {
            var item = ModProduct_InfoService.Instance.CreateQuery()
                            .Where(o => o.Activity == true && o.Code == endCode)
                //.WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product_Info", MenuID, ViewPage.CurrentLang.ID))
                            .ToSingle();

            if (item != null)
            {
                // Tăng số lượt xem 
                item.Preview = item.Preview + 1;
                // Lưu lại
                ModProduct_InfoService.Instance.Save(item);

                // Lấy danh sách ảnh sản phẩm
                List<ModProduct_SlideShowEntity> lstSlideShow = ModProduct_SlideShowService.Instance.CreateQuery().Where(p => p.ProductInfoId == item.ID).ToList();

                // Lấy danh sách thuộc tính : Dạng table html
                string ListProperties = ThuocTinh(item);

                // Lấy danh sách đại lý : Dạng table html
                string ListAgent = DaiLy(item);

                // Lấy danh sách bình luận
                string ListComment = BinhLuan(item);

                //ViewBag.Other = ModProduct_InfoService.Instance.CreateQuery()
                //                        .Where(o => o.Activity == true)
                //                        .Where(o => o.ID < item.ID)
                //                        .WhereIn(MenuID > 0, o => o.MenuID, WebMenuService.Instance.GetChildIDForWeb_Cache("Product_Info", MenuID, ViewPage.CurrentLang.ID))
                //                        .OrderByDesc(o => o.ID)
                //                        .Take(PageSize)
                //                        .ToList();

                ViewBag.Data = item;
                ViewBag.SlideShow = lstSlideShow;
                ViewBag.ListProperties = ListProperties;
                ViewBag.ListAgent = ListAgent;
                ViewBag.ListComment = ListComment;
                //ViewPage.CurrentPage.PageTitle = item.Name;

                //for SEO
                ViewPage.CurrentPage.PageTitle = string.IsNullOrEmpty(item.PageTitle) ? item.Name : item.PageTitle;
                ViewPage.CurrentPage.PageDescription = item.PageDescription;
                ViewPage.CurrentPage.PageKeywords = item.PageKeywords;

                //Global.Cookies.SetValue("Product.Detail.ProductId", item.ID.ToString());
                Session.SetValue("ProductId", item.ID);
            }
            else
            {
                ViewPage.Error404();
            }
        }

        /// <summary>
        /// Danh sách các thuộc tính của sản phẩm
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string ThuocTinh(ModProduct_InfoEntity item)
        {
            string sData = string.Empty;

            // Lấy thông tin chủng loại
            WebMenuEntity objWebMenuService = WebMenuService.Instance.GetByID(item.MenuID);

            List<ModProduct_Area_PropretyGroupEntity> lstArea_PropretyGroup_All = ModProduct_Area_PropretyGroupService.Instance
                                      .CreateQuery().Where(p => p.ProductAreaId == objWebMenuService.ProductAreaId).ToList();

            #region Lấy tất cả các dữ liệu nhóm theo lĩnh vực
            // Lấy danh sách các nhóm thuộc tính 
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

            // Lấy tất cả những giá trị cũ của các thuộc tính
            List<ModProduct_PropertiesList_ValuesEntity> lstPropertiesList_Values_All = null;
            if (!string.IsNullOrEmpty(sPropertiesListId))
                lstPropertiesList_Values_All = ModProduct_PropertiesList_ValuesService.Instance.CreateQuery().WhereIn(p => p.PropertiesListId, sPropertiesListId).ToList();

            if (lstPropertiesList_Values_All == null)
                lstPropertiesList_Values_All = new List<ModProduct_PropertiesList_ValuesEntity>();
            #endregion


            // Lấy danh sách các giá trị của nhóm thuộc tính
            List<ModProduct_Info_DetailsEntity> lstProduct_Info_Details = ModProduct_Info_DetailsService.Instance.CreateQuery()
                                                                                .Where(p => p.ProductInfoId == item.ID)
                                                                                .WhereIn(p => p.PropertiesGroupsId, sListPropretyGroupId_All)
                                                                                .ToList();
            if (lstProduct_Info_Details == null)
                lstProduct_Info_Details = new List<ModProduct_Info_DetailsEntity>();

            int iProperties = -1;
            int iSTT = 0;
            List<ModProduct_PropertiesListEntity> lstPropreties_Filter = null;

            sData = "<table class='tbl-properties'>";

            foreach (var itemPropertiesGroup in lstPropretyGroup_All)
            {
                // Quyết định màu của dòng
                iProperties++;

                // Dòng Nhóm thuộc tính
                #region Dòng Nhóm thuộc tính
                sData += "<tr class='row" + iProperties % 2 + "'>";
                sData += "<td colspan='4'>";
                sData += "<div class='div-properties-group'><span>" + itemPropertiesGroup.Name + "</span></div>";
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

                    sData += "<td align='center' class='td-stt'>";
                    sData += "</td>";

                    // Tên thuộc tính
                    sData += "<td align='left' nowrap='nowrap' class='td-label'>";
                    sData += "<label>" + itemPropertiesList.Name + "</label>";
                    sData += "</td>";

                    sData += "<td class='td-space'>:";
                    sData += "</td>";

                    // Ô nhập giá trị cho thuộc tính
                    sData += "<td class='td-value'>";
                    sData += GetValue_PropertiesValue(lstProduct_Info_Details, itemPropertiesList.ID);

                    // Đơn vị tính
                    if (!string.IsNullOrEmpty(itemPropertiesList.Unit))
                        sData += "&nbsp;<span>(" + itemPropertiesList.Unit + ")</span>";
                    sData += "</td>";

                    sData += "</tr>";
                }
            }

            sData += "</table>";

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

            return "<span name='txtPropertyValue" + iPropertyId + "' id='txtPropertyValue" + iPropertyId + "' style='width: 90%;'>" + sValue + "</span>";
        }

        /// <summary>
        /// Lấy danh sách đại lý
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string DaiLy(ModProduct_InfoEntity item)
        {
            string sData = string.Empty;
            List<ModProduct_Info_AgentEntity> lstInfo_Agent_In = ModProduct_Info_AgentService.Instance.CreateQuery()
                .Where(p => p.ProductInfoId == item.ID).ToList();

            if (lstInfo_Agent_In == null || lstInfo_Agent_In.Count <= 0)
                return sData;

            // Lấy danh sách ID
            string ListId = VSW.Lib.Global.ConvertTool.ConvertListStringToString(lstInfo_Agent_In.Select(p => p.ProductAgeId).ToList());

            List<ModProduct_AgentEntity> lstInfo_Agent = ModProduct_AgentService.Instance.CreateQuery()
                .WhereIn(p => p.ID, ListId)
                .OrderByAsc(o => o.Name).ToList();
            if (lstInfo_Agent == null || lstInfo_Agent.Count <= 0)
                return sData;

            sData += "<div class='div-agent'>";
            sData += "<div class='div-group-info-detail'>";
            sData += "<div class='group-info-detail'>";
            sData += "Đại lý bán sản phẩm:";
            sData += "</div>";

            foreach (var itemAgent in lstInfo_Agent)
            {
                sData += "<div class='list-info-detail'>";
                sData += itemAgent.Name + " - " + itemAgent.Address;
                sData += "</div>";
            }

            sData += "</div>";
            sData += "</div>";
            return sData;
        }

        /// <summary>
        /// Lấy các thông tin bình luận
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string BinhLuan(ModProduct_InfoEntity item)
        {
            string sData = "<div class='div-no-comment'>Chưa có bình luận nào cho sản phẩm</div>";
            if (item == null)
                return sData;

            // LẤy các comment đã được duyệt
            var lstDataComment = ModProduct_CommentsService.Instance.CreateQuery().Where(o => o.ProductInfoId == item.ID && o.Approved == true)
                                                              .OrderByDesc(o => o.CreateDate).ToList();

            if (lstDataComment == null || lstDataComment.Count <= 0)
                return sData;

            sData = string.Empty;

            foreach (var itemComment in lstDataComment)
            {
                sData += "<div class='div-comment-group'>";
                sData += "<div class='div-comment-group-info-user'><span class='div-comment-group-info'>" + itemComment.Name + "</span><span class='div-comment-group-info-email'>" + (string.IsNullOrEmpty(itemComment.Email) ? string.Empty : " - ") + itemComment.Email + "</span>";
                sData += "<span class='div-comment-group-info-address'>" + (string.IsNullOrEmpty(itemComment.Address) ? string.Empty : " - ") + itemComment.Address + "</span><span class='div-comment-group-info-date'>(" + itemComment.CreateDate.ToString("dd/MM/yyyy HH:mm") + ")</span></div>";

                // nội dung comment
                sData += "<div class='div-comment-content'>";
                sData += itemComment.Content;
                sData += "</div>";
                sData += "</div>";
            }

            return sData;
        }
    }

    public class MProduct_InfoModel
    {
        private int _Page = 0;
        public int Page
        {
            get { return _Page; }
            set { _Page = value - 1; }
        }

        public int PageSize { get; set; }
        public int TotalRecord { get; set; }
    }
}
