using System;
using System.Web;
using System.ComponentModel;
using System.Data;

namespace VSW.Lib.Global
{
    public class EnumValue
    {
        public enum PRODUCT_INFO_ShowIcon
        {
            [Description("Không hiển thị")]
            NOT_SHOW = 0,
            [Description("Mới")]
            NEW = 1,
            [Description("Hot")]
            HOT = 2,
            [Description("Giảm giá")]
            SALE = 3
        }

        public enum PRODUCT_INFO_PriceTypeSale
        {
            [Description("Khuyến mãi tiền")]
            Mony = 0,
            [Description("Khuyến mãi %")]
            Percent = 1
        }

        public enum TypeProductSub
        {
            [Description("Sản phẩm liên quan")]
            Relative = 0,
            [Description("Sản phẩm cùng danh mục")]
            InCollection = 1
        }

        /// <summary>
        /// 0: Mới | 1: Đã duyệt | 2: Hoàn thành | 3: Từ chối
        /// </summary>
        public enum OrderStatus
        {
            [Description("Mới tạo")]
            MOI = 0,
            [Description("Đã duyệt")]
            DA_DUYET = 1,
            [Description("Hoàn thành")]
            HOAN_THANH = 2,
            [Description("Từ chối")]
            TU_CHOI = 3
        }

        /// <summary>
        /// 0: Chọn một | 1: chọn nhiều
        /// </summary>
        public enum TypeFilterGroup
        {
            [Description("Chọn một")]
            RADIO = 0,
            [Description("Chọn nhiều")]
            CHECKBOX = 1
        }

        public enum Activity
        {
            FALSE = 0,
            TRUE = 1
        }

        public enum LinkOnTitle
        {
            FALSE = 0,
            TRUE = 1
        }

        public enum ShowLinkViewAll
        {
            FALSE = 0,
            TRUE = 1
        }

        /// <summary>
        /// 0: Chọn một | 1: chọn nhiều
        /// </summary>
        public enum TypeSurveyGroup
        {
            [Description("Chọn một")]
            RADIO = 0,
            [Description("Chọn nhiều")]
            CHECKBOX = 1
        }

        /// <summary>
        /// Gán Icon cho bài viết
        /// </summary>
        public enum NewsState
        {
            [Description("Không gán")]
            KHONG_GAN = 0,
            [Description("Mới")]
            MOI = 1,
            [Description("Nổi bật")]
            NOI_BAT = 2
        }

        /// <summary>
        /// Loại tin
        /// </summary>
        public enum NewsType
        {
            [Description("Tin thường")]
            TIN_THUONG = 0,
            [Description("Mới")]
            MOI = 1,
            [Description("Nổi bật")]
            NOI_BAT = 2
        }

        /// <summary>
        /// Parameter
        /// </summary>
        public enum Parameter
        {
            [Description("Giao diện hiển thị")]
            TEMPLATE_ACTIVATE = 1,
            [Description("Mới")]
            MOI = 1,
            [Description("Nổi bật")]
            NOI_BAT = 2
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public static class EnumAttributesHelper
    {
        public static string GetDescription(this Enum value)
        {
            try
            {
                var da = (DescriptionAttribute[])(value.GetType().GetField(value.ToString())).GetCustomAttributes(typeof(DescriptionAttribute), false);
                return da.Length > 0 ? da[0].Description : value.ToString();
            }
            catch
            {
                return null;
            }
        }

        public static DataTable GetEnumText_PRODUCT_INFO_ShowIcon()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("value", typeof(int));
            dt.Columns.Add("text", typeof(string));
            DataRow dr;

            foreach (EnumValue.PRODUCT_INFO_ShowIcon item in Enum.GetValues(typeof(EnumValue.PRODUCT_INFO_ShowIcon)))
            {
                dr = dt.NewRow();
                dr[0] = ((int)item).ToString();
                dr[1] = GetDescription(item);
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
