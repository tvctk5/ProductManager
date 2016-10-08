using System;

using VSW.Lib.MVC;
using VSW.Lib.Models;
using VSW.Lib.Global;

namespace VSW.Lib.CPControllers
{
    public class FormProduct_AreaController : ModProduct_National_AreaController
    {
        public override void ActionCopy(int id)
        {

        }
        public override void ActionPublish(int[] arrID)
        {

        }
        public override void ActionUnPublish(int[] arrID)
        {

        }
        public override void ActionDelete(int[] arrID)
        {

        }
        public override void ActionSaveOrder(int[] arrID)
        {

        }
        public override void ActionPublishGX(int[] arrID)
        {

        }

        /// <summary>
        /// Kiểm tra sự tồn tại của một sản phẩm trong danh sách liên quan
        /// </summary>
        /// <param name="sListProduct"></param>
        /// <param name="iIDProduct"></param>
        /// <returns></returns>
        public static bool CheckExisted(string sListProduct, int iIDProduct)
        {
            if (string.IsNullOrEmpty(sListProduct))
                return false;

            // Tồn tại
            if (sListProduct.Contains("," + iIDProduct + ","))
                return true;

            return false;
        }
    }
}
