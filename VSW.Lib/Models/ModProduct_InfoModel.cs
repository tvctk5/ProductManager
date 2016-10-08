using System;
using System.Collections.Generic;

using VSW.Core.Models;

namespace VSW.Lib.Models
{
    public class ModProduct_InfoEntity : EntityBase
    {

        #region Autogen by VSW

        [DataInfo]
        public override int ID { get; set; }

        [DataInfo]
        public int? ManufacturerId { get; set; }

        [DataInfo]
        public int UserId { get; set; }

        [DataInfo]
        public int Type { get; set; }

        [DataInfo]
        public int MenuID { get; set; }

        [DataInfo]
        public int LangID { get; set; }

        [DataInfo]
        public string PageTitle { get; set; }

        [DataInfo]
        public string PageDescription { get; set; }

        [DataInfo]
        public string PageKeywords { get; set; }

        [DataInfo]
        public string Code { get; set; }

        [DataInfo]
        public override string Name { get; set; }

        [DataInfo]
        public int CountNumber { get; set; }

        [DataInfo]
        public double PriceInput { get; set; }

        [DataInfo]
        public double Price { get; set; }

        [DataInfo]
        public string PriceTextSaleView { get; set; }

        [DataInfo]
        // Hiển thị thuế Giá hay không
        public bool ShowPrice { get; set; }

        [DataInfo]
        public bool VAT { get; set; }

        [DataInfo]
        // Hiển thị thuế AVT
        public bool ShowVAT { get; set; }

        [DataInfo]
        public bool SaleOff { get; set; }

        [DataInfo]
        public bool SaleOffType { get; set; }

        [DataInfo]
        public double SaleOffValue { get; set; }

        [DataInfo]
        public double PriceSale { get; set; }

        [DataInfo]
        public DateTime StartDate { get; set; }

        [DataInfo]
        public DateTime FinishDate { get; set; }

        [DataInfo]
        public string InfoBasic { get; set; }

        [DataInfo]
        public string Gifts { get; set; }

        [DataInfo]
        // Khuyến mãi
        public string Promotion { get; set; }

        [DataInfo]
        // Chính sách
        public string Policy { get; set; }

        [DataInfo]
        // Thông tin nổi bật
        public string InfoHighlight { get; set; }

        [DataInfo]
        public string File { get; set; }

        [DataInfo]
        // Số lượt xem
        public int Preview { get; set; }

        [DataInfo]
        // Hiển thị số lượt xem
        public bool ShowPreview { get; set; }

        [DataInfo]
        // Số lượt mua
        public int BuyCount { get; set; }

        [DataInfo]
        // Hiển thị số lượt mua
        public bool ShowBuyCount { get; set; }

        [DataInfo]
        // Số lượt bình luận
        public int CommentCount { get; set; }

        [DataInfo]
        // Hiển thị số bình luận hay không
        public bool ShowCommentCount { get; set; }

        [DataInfo]
        // Yêu cầu gọi điện thoại
        public bool ShowCallRequire { get; set; }

        [DataInfo]
        // Số lượt yêu cầu gọi
        public int CallRequireCount { get; set; }

        [DataInfo]
        // Hiển thị số lượt thích
        public bool ShowLike { get; set; }

        [DataInfo]
        // Số lượt thích
        public int LikeCount { get; set; }

        [DataInfo]
        public string Warranty { get; set; }

        [DataInfo]
        public bool Status { get; set; }

        [DataInfo]
        public string StatusNote { get; set; }

        [DataInfo]
        // Hiển thị xem sản phẩm mới hay cũ
        public bool ShowStatus { get; set; }

        [DataInfo]
        // Hiển thị xem hiển thị Icon gì: 0: Không hiển thị | 1: New | 2: Hot | 3: Khuyến mại
        public int ShowIcon { get; set; }

        [DataInfo]
        public string NewsPost { get; set; }

        [DataInfo]
        public string SizeInfo { get; set; }

        [DataInfo]
        public string ProductsConnection { get; set; }

        [DataInfo]
        public string ProductsAttach { get; set; }

        [DataInfo]
        public int ProductType { get; set; }

        [DataInfo]
        // Danh sách các thuộc tính ID của sản phẩm cách nhau bởi dấu ,
        public string ProductFilterIds { get; set; }

        [DataInfo]
        // Ngày đăng
        public DateTime PostDate { get; set; }

        [DataInfo]
        // Ngày bắt đầu đăng
        public DateTime RuntimeDateStart { get; set; }

        [DataInfo]
        // Ngày kết thúc đăng
        public DateTime RuntimeDateFinish { get; set; }

        [DataInfo]
        public DateTime CreateDate { get; set; }

        [DataInfo]
        public DateTime ModifiedDate { get; set; }

        [DataInfo]
        public bool Activity { get; set; }

        [DataInfo]
        public bool Deleted { get; set; }

        [DataInfo]
        public DateTime DeleteDate { get; set; }

        #endregion

        private WebMenuEntity _oMenu = null;
        public WebMenuEntity getMenu()
        {
            if (_oMenu == null && MenuID > 0)
                _oMenu = WebMenuService.Instance.GetByID_Cache(MenuID);

            if (_oMenu == null)
                _oMenu = new WebMenuEntity();

            return _oMenu;
        }

    }

    public class ModProduct_InfoService : ServiceBase<ModProduct_InfoEntity>
    {

        #region Autogen by VSW

        private ModProduct_InfoService()
            : base("[Mod_Product_Info]")
        {

        }

        private static ModProduct_InfoService _Instance = null;
        public static ModProduct_InfoService Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new ModProduct_InfoService();

                return _Instance;
            }
        }

        #endregion

        public ModProduct_InfoEntity GetByID(int id)
        {
            return base.CreateQuery()
               .Where(o => o.ID == id)
               .ToSingle();
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// CanTV       2012/09/21      Tạo mới
        /// </summary>
        /// <param name="sCode">Mã kiểm tra</param>
        /// <param name="IdUpdate">Id bản ghi: Nếu thêm mới thì Id=0</param>
        /// <returns>True: Nếu Duplicate | False: nếu không Duplicate</returns>
        public bool DuplicateCode(string sCode, int IdUpdate, ref string sMess)
        {
            try
            {
                // Có mã trùng
                List<ModProduct_InfoEntity> lstEntity =
                base.CreateQuery()
                        .Where(o => o.ID != IdUpdate && o.Code == sCode)
                        .ToList();

                if (lstEntity == null)
                    return false;

                if (lstEntity.Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                sMess = ex.ToString();
                return true;
            }
        }

    }
}