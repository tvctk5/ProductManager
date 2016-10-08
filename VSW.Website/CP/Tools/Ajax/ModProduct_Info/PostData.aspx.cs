using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.MVC;
using VSW.Lib.Global;

namespace VSW.Website.CP.Tools.Ajax.Common.ModProduct_Info
{
    partial class PostData : System.Web.UI.Page
    {
        Tools.Common objCommon = new Tools.Common();
        DataOutput objDataOutput = new DataOutput();
        string sMessError = string.Empty;

        #region Tập các giá trị control được post lên để sử dụng
        int RecordID = 0;
        int ColorID = 0;
        string Code = string.Empty;
        string txtColor_Index = string.Empty;
        string txtColorCode = string.Empty;
        string txtColorName = string.Empty;
        int txtColor_CountNumber = 0;

        int SizeID = 0;
        string txtSize_Index = string.Empty;
        string txtSizeCode = string.Empty;
        string txtSizeName = string.Empty;
        int txtSize_CountNumber = 0;
        double txtSize_Price = 0;
        int MenuID = 0;
        #endregion

        #region Lấy các giá trị từ các control post
        /// <summary>
        /// Lấy các giá trị từ các control post
        /// </summary>
        private void GetValueControl()
        {
            RecordID = objCommon.ConvertToInt32(Request["RecordID"]);
            Code = objCommon.ConvertToString(Request["Code"]);
        }
        #endregion

        #region Methord Return Object
        /// <summary>
        /// Return Object
        /// </summary>
        /// <param name="objDataReturn"></param>
        private void RenderMessage(DataOutput objDataReturn)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string strJsonMessage = oSerializer.Serialize(objDataReturn);
            this.Page.Response.Clear();
            this.Page.Response.ContentType = "application/json";
            this.Page.Response.Write(strJsonMessage);
            this.Page.Response.End();
        }
        #endregion

        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string sType = Request.QueryString["Type"];

            // Lấy giá trị các control post lên
            GetValueControl();

            // Tùy từng trường hợp thao tác dữ liệu khác khau
            switch (sType)
            {
                // Check duplincate
                case "1": CheckDuplicate(); break;
                // Color Product
                case "colorAdd": AddColor(); break;
                case "colorSave": SaveColor(); break;
                case "colorDelete": DeleteColor(); break;
                // Size
                case "sizeAdd": AddSize(); break;
                case "sizeSave": SaveSize(); break;
                case "sizeDelete": DeleteSize(); break;
                // Chủng loại liên quan
                case "productGroupAdd": ProductGroupAdd(); break;
                case "productGroupDelete": ProductGroupDelete(); break;
                // Sản phẩm liên quan
                case "relativeAdd": RelativeAdd(); break;
                case "relativeDelete": RelativeDelete(); break;
                // Sản phẩm bán kèm
                case "attachAdd": AttachAdd(); break;
                case "attachDelete": AttachDelete(); break;

                // Đại lý
                case "agentAdd": AgentAdd(); break;
                case "agentDelete": AgentDelete(); break;

                // Khu vực bán sản phẩm
                case "areaAdd": AreaAdd(); break;
                case "areaDelete": AreaDelete(); break;

                // Thuộc tính lọc của sản phẩm
                case "filterAdd": FilterAdd(); break;
                case "filterDelete": FilterDelete(); break;

                // SlideShow Product
                case "imageSlideShowAdd": SlideShowAdd(); break;
                case "imageSlideShowSave": SlideShowSave(); break;
                case "imageSlideShowDelete": SlideShowDelete(); break;

                // Cập nhật thuộc tính cho sản phẩm
                case "properties_Save": PropertiesSave(); break;

                // Nội dung bình luận
                case "comment_Save": CommentSave(); break;
                case "comment_Approve": CommentApprove(); break;
                case "comment_UnApprove": CommentUnApprove(); break;
                case "comment_Delete": CommentDelete(); break;

                // Loại sản phẩm
                case "typesAdd": TypesAdd(); break;
                case "typesDelete": TypesDelete(); break;

                // Sản phẩm thêm vào trong đơn hàng
                case "formDkKyDaiLyDonHangAdd": FormDkKyDaiLyDonHangAdd(); break;
                case "formDkKyDaiLyDonHangDelete": FormDkKyDaiLyDonHangDelete(); break;
                case "donHang_SanPham_Save": FormDkKyDaiLyDonHangUpDate(); break;

                default:
                    break;
            }

            // Trả lại dữ liệu
            RenderMessage(objDataOutput);
        }

        /// <summary>
        /// Check duplicate
        /// </summary>
        private void CheckDuplicate()
        {
            // Kiểm tra mã xem có trùng với mã nào khác đã có không
            if (ModProduct_InfoService.Instance.DuplicateCode(Code, RecordID, ref sMessError))
            {
                if (string.IsNullOrEmpty(sMessError))
                {
                    objDataOutput.NotDuplicate = false;
                    objDataOutput.MessSuccess = CPViewControl.ShowMessDuplicate("Mã sản phẩm", Code);
                }
                else
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = sMessError;
                }
            }
        }

        #region Quản lý màu sắc
        /// <summary>
        /// AddColor
        /// </summary>
        private void AddColor()
        {
            ColorID = objCommon.ConvertToInt32(Request["ID"]);
            txtColor_Index = objCommon.ConvertToString(Request.QueryString["Index"]);
            txtColorCode = objCommon.ConvertToString(Request["txtColorCode" + txtColor_Index]);
            txtColorName = objCommon.ConvertToString(Request["txtColorName" + txtColor_Index]);
            txtColor_CountNumber = objCommon.ConvertToInt32(Request["txtColor_CountNumber" + txtColor_Index]);

            // Thêm thông tin màu
            ModProduct_Info_ColorEntity objInfo_ColorEntity = new ModProduct_Info_ColorEntity();
            objInfo_ColorEntity.Activity = true;
            objInfo_ColorEntity.ColorCode = txtColorCode;
            objInfo_ColorEntity.ColorName = txtColorName;
            objInfo_ColorEntity.CountNumber = txtColor_CountNumber;
            objInfo_ColorEntity.CreateDate = DateTime.Now;
            objInfo_ColorEntity.ProductInfoId = RecordID;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_ColorService.Instance.Save(objInfo_ColorEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = Color_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }

        /// <summary>
        /// SaveColor
        /// </summary>
        private void SaveColor()
        {
            ColorID = objCommon.ConvertToInt32(Request["ID"]);
            txtColor_Index = objCommon.ConvertToString(Request.QueryString["Index"]);
            txtColorCode = objCommon.ConvertToString(Request["txtColorCode" + txtColor_Index]);
            txtColorName = objCommon.ConvertToString(Request["txtColorName" + txtColor_Index]);
            txtColor_CountNumber = objCommon.ConvertToInt32(Request["txtColor_CountNumber" + txtColor_Index]);

            // Lấy bản ghi
            ModProduct_Info_ColorEntity objInfo_ColorEntity = ModProduct_Info_ColorService.Instance.GetByID(ColorID);

            if (objInfo_ColorEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin màu cần cập nhật";
                return;
            }

            // Cập nhật thông tin thay đổi
            objInfo_ColorEntity.ColorCode = txtColorCode;
            objInfo_ColorEntity.ColorName = txtColorName;
            objInfo_ColorEntity.CountNumber = txtColor_CountNumber;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_ColorService.Instance.Save(objInfo_ColorEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = Color_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình cập nhật";
                return;
            }
        }

        /// <summary>
        /// DeleteColor
        /// </summary>
        private void DeleteColor()
        {
            ColorID = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_ColorEntity objInfo_ColorEntity = ModProduct_Info_ColorService.Instance.GetByID(ColorID);

            if (objInfo_ColorEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin màu cần xóa";
                return;
            }

            // Lưu thay đổi
            try
            {
                ModProduct_Info_ColorService.Instance.Delete(objInfo_ColorEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = Color_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình xóa dữ liệu";
                return;
            }
        }

        private string Color_ReloadData()
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

        #region Quản lý kích thước
        /// <summary>
        /// AddSize
        /// </summary>
        private void AddSize()
        {
            SizeID = objCommon.ConvertToInt32(Request["ID"]);
            txtSize_Index = objCommon.ConvertToString(Request.QueryString["Index"]);
            txtSizeCode = objCommon.ConvertToString(Request["txtSizeCode" + txtSize_Index]);
            txtSizeName = objCommon.ConvertToString(Request["txtSizeName" + txtSize_Index]);
            txtSize_CountNumber = objCommon.ConvertToInt32(Request["txtSize_CountNumber" + txtSize_Index]);
            txtSize_Price = objCommon.ConvertToDouble(Request["txtSize_Price" + txtSize_Index]);

            // Thêm thông tin Size
            ModProduct_Info_SizeEntity objInfo_SizeEntity = new ModProduct_Info_SizeEntity();
            objInfo_SizeEntity.Activity = true;
            objInfo_SizeEntity.SizeCode = txtSizeCode;
            objInfo_SizeEntity.SizeName = txtSizeName;
            objInfo_SizeEntity.CountNumber = txtSize_CountNumber;
            objInfo_SizeEntity.CreateDate = DateTime.Now;
            objInfo_SizeEntity.ProductInfoId = RecordID;
            objInfo_SizeEntity.Price = txtSize_Price;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_SizeService.Instance.Save(objInfo_SizeEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = Size_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }

        /// <summary>
        /// SaveSize
        /// </summary>
        private void SaveSize()
        {
            SizeID = objCommon.ConvertToInt32(Request["ID"]);
            txtSize_Index = objCommon.ConvertToString(Request.QueryString["Index"]);
            txtSizeCode = objCommon.ConvertToString(Request["txtSizeCode" + txtSize_Index]);
            txtSizeName = objCommon.ConvertToString(Request["txtSizeName" + txtSize_Index]);
            txtSize_CountNumber = objCommon.ConvertToInt32(Request["txtSize_CountNumber" + txtSize_Index]);
            txtSize_Price = objCommon.ConvertToInt32(Request["txtSize_Price" + txtSize_Index]);

            // Lấy bản ghi
            ModProduct_Info_SizeEntity objInfo_SizeEntity = ModProduct_Info_SizeService.Instance.GetByID(SizeID);


            if (objInfo_SizeEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin Size cần cập nhật";
                return;
            }

            // Cập nhật thông tin thay đổi
            objInfo_SizeEntity.SizeCode = txtSizeCode;
            objInfo_SizeEntity.SizeName = txtSizeName;
            objInfo_SizeEntity.CountNumber = txtSize_CountNumber;
            objInfo_SizeEntity.Price = txtSize_Price;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_SizeService.Instance.Save(objInfo_SizeEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = Size_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình cập nhật";
                return;
            }
        }

        /// <summary>
        /// DeleteSize
        /// </summary>
        private void DeleteSize()
        {
            SizeID = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_SizeEntity objInfo_SizeEntity = ModProduct_Info_SizeService.Instance.GetByID(SizeID);

            if (objInfo_SizeEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin Size cần xóa";
                return;
            }

            // Lưu thay đổi
            try
            {
                ModProduct_Info_SizeService.Instance.Delete(objInfo_SizeEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = Size_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình xóa dữ liệu";
                return;
            }
        }

        /// <summary>
        /// CanTv
        /// Lấy danh sách các Size thuộc sản phẩm
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string Size_ReloadData()
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

        #region Quản lý chủng loại liên quan
        /// <summary>
        /// ProductGroup Add
        /// </summary>
        private void ProductGroupAdd()
        {
            MenuID = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_ProductGroupsEntity objInfo_GroupsEntity = new ModProduct_Info_ProductGroupsEntity();
            objInfo_GroupsEntity.ProductInfoId = RecordID;
            objInfo_GroupsEntity.MenuID = MenuID;
            //objInfo_GroupsEntity.ProductGroupsId = MenuID;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_ProductGroupsService.Instance.Save(objInfo_GroupsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// ProductGroup Delete
        /// </summary>
        private void ProductGroupDelete()
        {
            MenuID = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_ProductGroupsEntity objInfo_GroupsEntity = ModProduct_Info_ProductGroupsService.Instance.CreateQuery()
                .Where(p => p.ProductInfoId == RecordID && p.MenuID == MenuID).ToSingle();

            // Lưu thay đổi
            try
            {
                ModProduct_Info_ProductGroupsService.Instance.Delete(objInfo_GroupsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thực hiện";
                return;
            }
        }

        #endregion

        #region Quản lý sản phẩm liên quan
        /// <summary>
        /// Relative Add: Sản phẩm liên quan
        /// </summary>
        private void RelativeAdd()
        {
            int iProductIdAdd = objCommon.ConvertToInt32(Request["ID"]);

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            // Lấy bản ghi
            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);
            if (objModProduct_InfoEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy sản phẩm chính.";
                return;
            }

            // Lưu thay đổi
            try
            {
                // Thêm dấu nháy để có thể dễ tìm và remove
                if (string.IsNullOrEmpty(objModProduct_InfoEntity.ProductsConnection))
                    objModProduct_InfoEntity.ProductsConnection = "," + iProductIdAdd + ",";
                else
                    objModProduct_InfoEntity.ProductsConnection = "," + (objModProduct_InfoEntity.ProductsConnection.Trim(',') + "," + iProductIdAdd) + ",";

                // Lưu lại thay đổi
                ModProduct_InfoService.Instance.Save(objModProduct_InfoEntity);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = ProductRelative_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Relative Delete: Sản phẩm liên quan
        /// </summary>
        private void RelativeDelete()
        {
            int iProductIdDelete = objCommon.ConvertToInt32(Request["ID"]);

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            // Lấy bản ghi
            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);
            if (objModProduct_InfoEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy sản phẩm chính.";
                return;
            }

            // Lưu thay đổi
            try
            {
                // Thêm dấu nháy để có thể dễ tìm và remove
                if (string.IsNullOrEmpty(objModProduct_InfoEntity.ProductsConnection))
                    return;

                // Xóa sản phẩm liên quan đi
                objModProduct_InfoEntity.ProductsConnection = objModProduct_InfoEntity.ProductsConnection.Replace("," + iProductIdDelete + ",", ",");

                // Nếu chỉ có dấu ',' thì xóa trắng
                if (objModProduct_InfoEntity.ProductsConnection.Length == 1)
                    objModProduct_InfoEntity.ProductsConnection = string.Empty;

                // Lưu lại thay đổi
                ModProduct_InfoService.Instance.Save(objModProduct_InfoEntity);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = ProductRelative_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// CanTv
        /// Lấy danh sách các sản phẩm liên quan
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string ProductRelative_ReloadData()
        {
            string sData = string.Empty;

            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);
            if (objModProduct_InfoEntity == null || string.IsNullOrEmpty(objModProduct_InfoEntity.ProductsConnection))
                return sData;


            List<ModProduct_InfoEntity> lstProductInfo = ModProduct_InfoService.Instance.CreateQuery()
                                                    .WhereIn(p => p.ID, objModProduct_InfoEntity.ProductsConnection.Trim(','))
                                                    .OrderByAsc(o => o.Code)
                                                    .ToList();

            if (lstProductInfo == null || lstProductInfo.Count <= 0)
                return sData;

            // Lấy danh sách nhà sản xuất
            List<ModProduct_ManufacturerEntity> lstManufacturerEntity = ModProduct_ManufacturerService.Instance.CreateQuery().ToList();

            int iIndex = 0;
            foreach (ModProduct_InfoEntity item in lstProductInfo)
            {
                sData += "";
                sData += "<tr class='row" + iIndex % 2 + "'>";
                sData += "<td align='center'>" + (iIndex + 1) + "</td>";
                sData += "<td align='center'>" + item.ID + "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (item.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>" + Utils.GetMedia(item.File, 60, 60) + "</td>";
                sData += "<td align='left'>" + item.Code + " </td>";
                sData += "<td align='left'>" + item.Name + "</td>";
                sData += "<td align='right' nowrap='nowrap'>" + string.Format("{0:#,##0}", item.CountNumber) + "</td>";
                sData += "<td align='right'>" + string.Format("{0:#,##0}", item.Price.ToString()) + "</td>";
                sData += "<td align='left'>" + GetManufactureName(lstManufacturerEntity, item.ManufacturerId) + "</td>";
                sData += "<td align='center'>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) + "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid' href='javascript:void(0);' onclick='RelativeDelete_DeleteTr(urlProductRelative_Delete,this,\"" + item.ID + "\");return false;'>";
                sData += "<span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr> ";

                iIndex++;
            }

            return sData;
        }

        #endregion

        #region Quản lý sản phẩm bán kèm
        /// <summary>
        /// Attach Add: Sản phẩm liên quan
        /// </summary>
        private void AttachAdd()
        {
            int iProductIdAdd = objCommon.ConvertToInt32(Request["ID"]);

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            // Lấy bản ghi
            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);
            if (objModProduct_InfoEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy sản phẩm chính.";
                return;
            }

            // Lưu thay đổi
            try
            {
                // Thêm dấu nháy để có thể dễ tìm và remove
                if (string.IsNullOrEmpty(objModProduct_InfoEntity.ProductsAttach))
                    objModProduct_InfoEntity.ProductsAttach = "," + iProductIdAdd + ",";
                else
                    objModProduct_InfoEntity.ProductsAttach = "," + (objModProduct_InfoEntity.ProductsAttach.Trim(',') + "," + iProductIdAdd) + ",";

                // Lưu lại thay đổi
                ModProduct_InfoService.Instance.Save(objModProduct_InfoEntity);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = ProductAttach_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Attach Delete: Sản phẩm liên quan
        /// </summary>
        private void AttachDelete()
        {
            int iProductIdDelete = objCommon.ConvertToInt32(Request["ID"]);

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            // Lấy bản ghi
            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);
            if (objModProduct_InfoEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy sản phẩm chính.";
                return;
            }

            // Lưu thay đổi
            try
            {
                // Thêm dấu nháy để có thể dễ tìm và remove
                if (string.IsNullOrEmpty(objModProduct_InfoEntity.ProductsAttach))
                    return;

                // Xóa sản phẩm liên quan đi
                objModProduct_InfoEntity.ProductsAttach = objModProduct_InfoEntity.ProductsAttach.Replace("," + iProductIdDelete + ",", ",");

                // Nếu chỉ có dấu ',' thì xóa trắng
                if (objModProduct_InfoEntity.ProductsAttach.Length == 1)
                    objModProduct_InfoEntity.ProductsAttach = string.Empty;

                // Lưu lại thay đổi
                ModProduct_InfoService.Instance.Save(objModProduct_InfoEntity);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = ProductAttach_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// CanTv
        /// Lấy danh sách các sản phẩm liên quan
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string ProductAttach_ReloadData()
        {
            string sData = string.Empty;

            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);
            if (objModProduct_InfoEntity == null || string.IsNullOrEmpty(objModProduct_InfoEntity.ProductsAttach))
                return sData;


            List<ModProduct_InfoEntity> lstProductInfo = ModProduct_InfoService.Instance.CreateQuery()
                                                    .WhereIn(p => p.ID, objModProduct_InfoEntity.ProductsAttach.Trim(','))
                                                    .OrderByAsc(o => o.Code)
                                                    .ToList();

            if (lstProductInfo == null || lstProductInfo.Count <= 0)
                return sData;

            // Lấy danh sách nhà sản xuất
            List<ModProduct_ManufacturerEntity> lstManufacturerEntity = ModProduct_ManufacturerService.Instance.CreateQuery().ToList();

            int iIndex = 0;
            foreach (ModProduct_InfoEntity item in lstProductInfo)
            {
                sData += "";
                sData += "<tr class='row" + iIndex % 2 + "'>";
                sData += "<td align='center'>" + (iIndex + 1) + "</td>";
                sData += "<td align='center'>" + item.ID + "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (item.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>" + Utils.GetMedia(item.File, 60, 60) + "</td>";
                sData += "<td align='left'>" + item.Code + " </td>";
                sData += "<td align='left'>" + item.Name + "</td>";
                sData += "<td align='right' nowrap='nowrap'>" + string.Format("{0:#,##0}", item.CountNumber) + "</td>";
                sData += "<td align='right'>" + string.Format("{0:#,##0}", item.Price.ToString()) + "</td>";
                sData += "<td align='left'>" + GetManufactureName(lstManufacturerEntity, item.ManufacturerId) + "</td>";
                sData += "<td align='center'>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) + "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid' href='javascript:void(0);' onclick='AttachDelete_DeleteTr(urlProductAttach_Delete,this,\"" + item.ID + "\");return false;'>";
                sData += "<span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr> ";

                iIndex++;
            }

            return sData;
        }

        #endregion

        #region Lấy tên nhà sản xuất
        private string GetManufactureName(List<ModProduct_ManufacturerEntity> lstManufacturerEntity, int? iManufacturerID)
        {
            string sName = string.Empty;
            if (iManufacturerID == null || iManufacturerID <= 0)
                return sName;

            if (lstManufacturerEntity == null || lstManufacturerEntity.Count <= 0)
                return sName;

            ModProduct_ManufacturerEntity objModProduct_ManufacturerEntity = lstManufacturerEntity.Where(p => p.ID == iManufacturerID).SingleOrDefault();
            if (objModProduct_ManufacturerEntity == null)
                return sName;

            sName = objModProduct_ManufacturerEntity.Name;
            return sName;
        }

        #endregion

        #region Quản lý đại lý bán
        /// <summary>
        /// Agent Add
        /// </summary>
        private void AgentAdd()
        {
            int iCurrentAdd = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_AgentEntity objInfo_AgentEntity = new ModProduct_Info_AgentEntity();
            objInfo_AgentEntity.ProductInfoId = RecordID;
            objInfo_AgentEntity.ProductAgeId = iCurrentAdd;
            objInfo_AgentEntity.CreateDate = DateTime.Now;
            objInfo_AgentEntity.Activity = true;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_AgentService.Instance.Save(objInfo_AgentEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Agent Delete
        /// </summary>
        private void AgentDelete()
        {
            int iCurrentDelete = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_AgentEntity objInfo_GroupsEntity = ModProduct_Info_AgentService.Instance.CreateQuery()
                .Where(p => p.ProductInfoId == RecordID && p.ProductAgeId == iCurrentDelete).ToSingle();

            // Lưu thay đổi
            try
            {
                ModProduct_Info_AgentService.Instance.Delete(objInfo_GroupsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thực hiện";
                return;
            }
        }

        #endregion

        #region Quản lý khu vực bán
        /// <summary>
        /// Area Add
        /// </summary>
        private void AreaAdd()
        {
            int iCurrentAdd = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_AreaInNationalEntity objInfo_AreaInNational = new ModProduct_Info_AreaInNationalEntity();
            objInfo_AreaInNational.ProductInfoId = RecordID;
            objInfo_AreaInNational.ProductNationalAreaId = iCurrentAdd;
            objInfo_AreaInNational.CreateDate = DateTime.Now;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_AreaInNationalService.Instance.Save(objInfo_AreaInNational);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Area Delete
        /// </summary>
        private void AreaDelete()
        {
            int iCurrentDelete = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_AreaInNationalEntity objInfo_GroupsEntity = ModProduct_Info_AreaInNationalService.Instance.CreateQuery()
                .Where(p => p.ProductInfoId == RecordID && p.ProductNationalAreaId == iCurrentDelete).ToSingle();

            // Lưu thay đổi
            try
            {
                ModProduct_Info_AreaInNationalService.Instance.Delete(objInfo_GroupsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thực hiện";
                return;
            }
        }

        #endregion

        #region Quản lý thuộc tính lọc
        /// <summary>
        /// Filter Add
        /// </summary>
        private void FilterAdd()
        {
            int iCurrentAdd = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_PropertyFilterEntity objProduct_Info_PropertyFilter = new ModProduct_Info_PropertyFilterEntity();
            objProduct_Info_PropertyFilter.ProductInfoId = RecordID;
            objProduct_Info_PropertyFilter.ProductFilterId = iCurrentAdd;
            objProduct_Info_PropertyFilter.CreateDate = DateTime.Now;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_PropertyFilterService.Instance.Save(objProduct_Info_PropertyFilter);

                // Cập nhật lại danh sách trường dữ liệu trong Sản phẩm
                ModProduct_InfoEntity objProduct = ModProduct_InfoService.Instance.GetByID(RecordID);
                if (objProduct == null)
                    return;

                if (string.IsNullOrEmpty(objProduct.ProductFilterIds))
                    objProduct.ProductFilterIds = "," + iCurrentAdd + ",";
                else
                    objProduct.ProductFilterIds += iCurrentAdd + ",";

                // Lưu lại
                ModProduct_InfoService.Instance.Save(objProduct);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Filter Delete
        /// </summary>
        private void FilterDelete()
        {
            int iCurrentDelete = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_PropertyFilterEntity objInfo_GroupsEntity = ModProduct_Info_PropertyFilterService.Instance.CreateQuery()
                .Where(p => p.ProductInfoId == RecordID && p.ProductFilterId == iCurrentDelete).ToSingle();

            // Lưu thay đổi
            try
            {
                ModProduct_Info_PropertyFilterService.Instance.Delete(objInfo_GroupsEntity);

                // Cập nhật lại danh sách trường dữ liệu trong Sản phẩm
                ModProduct_InfoEntity objProduct = ModProduct_InfoService.Instance.GetByID(RecordID);
                if (objProduct == null)
                    return;

                if (string.IsNullOrEmpty(objProduct.ProductFilterIds))
                    return;

                // Xóa thuộc tính khỏi danh sách
                objProduct.ProductFilterIds = objProduct.ProductFilterIds.Replace("," + iCurrentDelete + ",", ",");

                // Nếu chỉ có dấu ","
                if (objProduct.ProductFilterIds.Length == 1)
                    objProduct.ProductFilterIds = string.Empty;

                // Lưu lại
                ModProduct_InfoService.Instance.Save(objProduct);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thực hiện";
                return;
            }
        }

        #endregion

        #region Quản lý danh sách ảnh sản phẩm
        int SlideShowID = 0;
        int SlideShowOrder = 0;
        string txtSlideShow_Index = string.Empty;
        string txtSlideShowName = string.Empty;
        string txtSlideShowUrl = string.Empty;
        /// <summary>
        /// AddSlideShow
        /// </summary>
        private void SlideShowAdd()
        {
            SlideShowID = objCommon.ConvertToInt32(Request["ID"]);
            txtSlideShow_Index = objCommon.ConvertToString(Request.QueryString["Index"]);
            txtSlideShowName = objCommon.ConvertToString(Request["txtSlideShowName" + txtSlideShow_Index]);
            txtSlideShowUrl = objCommon.ConvertToString(Request["txtSlideShowUrl" + txtSlideShow_Index]);
            SlideShowOrder = objCommon.ConvertToInt32(Request["txtSlideShowOrder" + txtSlideShow_Index]);

            ModProduct_SlideShowEntity objInfo_SlideShowEntity = new ModProduct_SlideShowEntity();
            if (string.IsNullOrEmpty(txtSlideShowName))
                txtSlideShowName = DateTime.Now.ToString("yyyyMMddHHmmssfffff");

            objInfo_SlideShowEntity.Name = txtSlideShowName;
            objInfo_SlideShowEntity.UrlFull = txtSlideShowUrl;
            objInfo_SlideShowEntity.UrlReSize = txtSlideShowUrl;
            objInfo_SlideShowEntity.Order = SlideShowOrder;
            objInfo_SlideShowEntity.CreateDate = DateTime.Now;
            objInfo_SlideShowEntity.ProductInfoId = RecordID;

            // Lưu thay đổi
            try
            {
                ModProduct_SlideShowService.Instance.Save(objInfo_SlideShowEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = SlideShow_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }

        /// <summary>
        /// SaveSlideShow
        /// </summary>
        private void SlideShowSave()
        {
            SlideShowID = objCommon.ConvertToInt32(Request["ID"]);
            txtSlideShow_Index = objCommon.ConvertToString(Request.QueryString["Index"]);
            txtSlideShowName = objCommon.ConvertToString(Request["txtSlideShowName" + txtSlideShow_Index]);
            txtSlideShowUrl = objCommon.ConvertToString(Request["txtSlideShowUrl" + txtSlideShow_Index]);
            SlideShowOrder = objCommon.ConvertToInt32(Request["txtSlideShowOrder" + txtSlideShow_Index]);

            // Lấy bản ghi
            ModProduct_SlideShowEntity objInfo_SlideShowEntity = ModProduct_SlideShowService.Instance.GetByID(SlideShowID);

            if (objInfo_SlideShowEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin màu cần cập nhật";
                return;
            }

            if (string.IsNullOrEmpty(txtSlideShowName))
                txtSlideShowName = DateTime.Now.ToString("yyyyMMddHHmmssfffff");

            objInfo_SlideShowEntity.Name = txtSlideShowName;
            objInfo_SlideShowEntity.UrlFull = txtSlideShowUrl;
            objInfo_SlideShowEntity.UrlReSize = txtSlideShowUrl;
            objInfo_SlideShowEntity.Order = SlideShowOrder;
            objInfo_SlideShowEntity.CreateDate = DateTime.Now;
            objInfo_SlideShowEntity.ProductInfoId = RecordID;

            // Lưu thay đổi
            try
            {
                ModProduct_SlideShowService.Instance.Save(objInfo_SlideShowEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = SlideShow_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình cập nhật";
                return;
            }
        }

        /// <summary>
        /// DeleteSlideShow
        /// </summary>
        private void SlideShowDelete()
        {
            SlideShowID = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_SlideShowEntity objInfo_SlideShowEntity = ModProduct_SlideShowService.Instance.GetByID(SlideShowID);

            if (objInfo_SlideShowEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin màu cần xóa";
                return;
            }

            // Lưu thay đổi
            try
            {
                ModProduct_SlideShowService.Instance.Delete(objInfo_SlideShowEntity);

                // Tải lại dữ liệu
                objDataOutput.MessSuccess = SlideShow_ReloadData();
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình xóa dữ liệu";
                return;
            }
        }

        private string SlideShow_ReloadData()
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

        #region Quản lý thuộc tính kỹ thuật của sản phẩm
        /// <summary>
        /// Cập nhật thuộc tính cho sản phẩm
        /// </summary>
        private void PropertiesSave()
        {
            // Lấy bản ghi
            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(RecordID);

            if (objModProduct_InfoEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin sản phẩm";
                return;
            }

            // Lấy thông tin Thuộc tính
            string sThuTu = Request.Form["ListPropertiesId"];
            string sPropertyGroupId = Request.Form["ListPropretyGroupId_ByPropertiesListId"];

            if (string.IsNullOrEmpty(sThuTu))
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Chưa có thuộc tính nào của sản phẩm.";
                return;
            }

            string[] ArrThuTuId = sThuTu.Split(',');
            string[] ArrPropertyGroupId = sPropertyGroupId.Split(',');

            ThuocTinhSanPham objThuocTinh = null;
            List<ThuocTinhSanPham> lstThuocTinh = new List<ThuocTinhSanPham>();

            for (int i = 0; i < ArrThuTuId.Length; i++)
            {
                objThuocTinh = new ThuocTinhSanPham();
                objThuocTinh.ProductInfoId = RecordID;
                objThuocTinh.PropertiesGroupsId = objCommon.ConvertToInt32(ArrPropertyGroupId[i]);
                objThuocTinh.PropertiesListId = objCommon.ConvertToInt32(ArrThuTuId[i]);
                objThuocTinh.Content = Request.Form["txtPropertyValue" + objThuocTinh.PropertiesListId];
                if (Request.Form["hidPropertySaveData" + objThuocTinh.PropertiesListId] == null || Request.Form["hidPropertySaveData" + objThuocTinh.PropertiesListId] != "1")
                    objThuocTinh.SaveData = false;
                else
                    objThuocTinh.SaveData = true;

                // Thêm vào danh sách
                lstThuocTinh.Add(objThuocTinh);
            }

            // Lưu thay đổi
            try
            {
                // Xóa dữ liệu cũ
                ModProduct_Info_DetailsService.Instance.Delete("ProductInfoId=" + RecordID);

                // Lấy danh sách insert 
                List<ModProduct_Info_DetailsEntity> lstModProduct_Info_DetailsEntity = new List<ModProduct_Info_DetailsEntity>();
                ModProduct_Info_DetailsEntity itemModProduct_Info_DetailsEntity = null;

                foreach (var itemThuocTinh in lstThuocTinh)
                {
                    itemModProduct_Info_DetailsEntity = new ModProduct_Info_DetailsEntity();
                    itemModProduct_Info_DetailsEntity.ProductInfoId = itemThuocTinh.ProductInfoId;
                    itemModProduct_Info_DetailsEntity.PropertiesGroupsId = itemThuocTinh.PropertiesGroupsId;
                    itemModProduct_Info_DetailsEntity.PropertiesListId = itemThuocTinh.PropertiesListId;
                    itemModProduct_Info_DetailsEntity.Content = itemThuocTinh.Content;

                    // Thêm vào list
                    lstModProduct_Info_DetailsEntity.Add(itemModProduct_Info_DetailsEntity);
                }

                // Insert vào DataBase
                ModProduct_Info_DetailsService.Instance.Save(lstModProduct_Info_DetailsEntity);

                // Thêm giá trị các thuộc tính vào bảng thuộc tính - giá trị
                List<ModProduct_PropertiesList_ValuesEntity> lstModProduct_PropertiesList_ValuesEntity = new List<ModProduct_PropertiesList_ValuesEntity>();
                ModProduct_PropertiesList_ValuesEntity itemPropertiesList_ValuesEntity = null;
                List<ModProduct_PropertiesList_ValuesEntity> lstModProduct_PropertiesList_ValuesEntity_Check = ModProduct_PropertiesList_ValuesService.Instance.CreateQuery().WhereIn(o => o.PropertiesListId, sThuTu).ToList();
                if (lstModProduct_PropertiesList_ValuesEntity_Check == null)
                    lstModProduct_PropertiesList_ValuesEntity_Check = new List<ModProduct_PropertiesList_ValuesEntity>();

                foreach (var itemThuocTinh in lstThuocTinh)
                {
                    if (itemThuocTinh.SaveData == false)
                        continue;

                    // Kiểm tra xem giá trị tồn tại chưa
                    itemPropertiesList_ValuesEntity = lstModProduct_PropertiesList_ValuesEntity_Check
                        .Where(p => p.PropertiesListId == itemThuocTinh.PropertiesListId && p.Content.ToUpper() == itemThuocTinh.Content.ToUpper()).SingleOrDefault();

                    // Nếu tồn tại rồi thì không thêm nữa
                    if (itemPropertiesList_ValuesEntity != null)
                        continue;

                    itemPropertiesList_ValuesEntity = new ModProduct_PropertiesList_ValuesEntity();

                    itemPropertiesList_ValuesEntity.PropertiesListId = itemThuocTinh.PropertiesListId;
                    itemPropertiesList_ValuesEntity.Content = itemThuocTinh.Content;

                    // Thêm vào danh sách
                    lstModProduct_PropertiesList_ValuesEntity.Add(itemPropertiesList_ValuesEntity);
                }

                // Insert vào DataBase
                ModProduct_PropertiesList_ValuesService.Instance.Save(lstModProduct_PropertiesList_ValuesEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình xóa dữ liệu";
                return;
            }
        }

        #endregion

        #region Quản lý nội dung bình luận
        /// <summary>
        /// Save comment
        /// </summary>
        private void CommentSave()
        {
            int ID = objCommon.ConvertToInt32(Request["ID"]);

            string txtCommentContent = objCommon.ConvertToString(Request["txtCommentContent" + ID]);

            ModProduct_CommentsEntity objCommentsEntity = ModProduct_CommentsService.Instance.GetByID(ID);
            if (objCommentsEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin bình luận";
                return;
            }

            // Cập nhật thông tin
            objCommentsEntity.Content = txtCommentContent;

            // Lưu thay đổi
            try
            {
                ModProduct_CommentsService.Instance.Save(objCommentsEntity);

                objDataOutput.MessSuccess = objCommentsEntity.Content.Replace(Environment.NewLine, "<br/>");
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }

        /// <summary>
        /// Approve comment
        /// </summary>
        private void CommentApprove()
        {
            int ID = objCommon.ConvertToInt32(Request["ID"]);

            ModProduct_CommentsEntity objCommentsEntity = ModProduct_CommentsService.Instance.GetByID(ID);
            if (objCommentsEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin bình luận";
                return;
            }

            // Cập nhật thông tin
            objCommentsEntity.Approved = true;

            // Lưu thay đổi
            try
            {
                ModProduct_CommentsService.Instance.Save(objCommentsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }

        /// <summary>
        /// UnApprove comment
        /// </summary>
        private void CommentUnApprove()
        {
            int ID = objCommon.ConvertToInt32(Request["ID"]);

            ModProduct_CommentsEntity objCommentsEntity = ModProduct_CommentsService.Instance.GetByID(ID);
            if (objCommentsEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin bình luận";
                return;
            }

            // Cập nhật thông tin
            objCommentsEntity.Approved = false;

            // Lưu thay đổi
            try
            {
                ModProduct_CommentsService.Instance.Save(objCommentsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        private void CommentDelete()
        {
            int ID = objCommon.ConvertToInt32(Request["ID"]);

            ModProduct_CommentsEntity objCommentsEntity = ModProduct_CommentsService.Instance.GetByID(ID);
            if (objCommentsEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin bình luận";
                return;
            }

            // Lưu thay đổi
            try
            {
                ModProduct_CommentsService.Instance.Delete(objCommentsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm mới";
                return;
            }
        }
        #endregion

        #region Quản lý loại sản phẩm
        /// <summary>
        /// Types Add
        /// </summary>
        private void TypesAdd()
        {
            int iCurrentAdd = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_TypesEntity objInfo_TypesEntity = new ModProduct_Info_TypesEntity();
            objInfo_TypesEntity.ProductInfoId = RecordID;
            objInfo_TypesEntity.ProductTypesId = iCurrentAdd;
            objInfo_TypesEntity.CreateDate = DateTime.Now;

            // Lưu thay đổi
            try
            {
                ModProduct_Info_TypesService.Instance.Save(objInfo_TypesEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Types Delete
        /// </summary>
        private void TypesDelete()
        {
            int iCurrentDelete = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_Info_TypesEntity objInfo_GroupsEntity = ModProduct_Info_TypesService.Instance.CreateQuery()
                .Where(p => p.ProductInfoId == RecordID && p.ProductTypesId == iCurrentDelete).ToSingle();

            // Lưu thay đổi
            try
            {
                ModProduct_Info_TypesService.Instance.Delete(objInfo_GroupsEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thực hiện";
                return;
            }
        }

        #endregion


        #region Quản lý Doanh thu - Đơn hàng - sản phẩm
        double doubTongSoLuong = 0;
        double doubTongTien = 0;
        double doubTongSauGiam = 0;

        /// <summary>
        /// Relative Add: Sản phẩm liên quan
        /// </summary>
        private void FormDkKyDaiLyDonHangAdd()
        {
            int iProductIdAdd = objCommon.ConvertToInt32(Request["ID"]);
            int iRecordID = objCommon.ConvertToInt32(Request["RecordID"]);

            int iSoLuong = objCommon.ConvertToInt32(Request["SoLuong"]);
            double iDonGia = objCommon.ConvertToDouble(Request["DonGia"] == null ? "0" : (Request["DonGia"].ToString().Replace(".", "").Replace(",", "")));

            // Lấy thông tin sản phẩm
            ModProduct_InfoEntity objModProduct_InfoEntity = ModProduct_InfoService.Instance.GetByID(iProductIdAdd);

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            // Đơn hàng
            VSW.Lib.LinqToSql.Mod_DT_Ky_DaiLy_DonHang objMod_DT_Ky_DaiLy_DonHang = ModDT_Ky_DaiLy_DonHangService.Instance.GetByIDLazy(iRecordID);
         
            ModDT_Ky_DaiLy_DonHang_SanPhamEntity objInsert = new ModDT_Ky_DaiLy_DonHang_SanPhamEntity();

            if (objMod_DT_Ky_DaiLy_DonHang != null)
            {
                objInsert.ModDTKyDaiLyDonHangId = objMod_DT_Ky_DaiLy_DonHang.ID;
                if (objMod_DT_Ky_DaiLy_DonHang.Mod_DT_Ky_DaiLy != null)
                {
                    objInsert.ModDTKyDaiLyId = objMod_DT_Ky_DaiLy_DonHang.Mod_DT_Ky_DaiLy.ID;
                    if (objMod_DT_Ky_DaiLy_DonHang.Mod_DT_Ky_DaiLy.Mod_DT_Ky != null)
                        objInsert.ModDtKyId = objMod_DT_Ky_DaiLy_DonHang.Mod_DT_Ky_DaiLy.Mod_DT_Ky.ID;
                }
            }

            objInsert.ID = 0;
            objInsert.ModProductId = objModProduct_InfoEntity.ID;
            objInsert.SoLuong = iSoLuong;
            objInsert.DonGia = iDonGia;
            objInsert.TongTien = objInsert.DonGia * objInsert.SoLuong;
            objInsert.TongSauGiam = objInsert.TongTien;
            objInsert.ChietKhau = 0;

            // Thêm mới
            ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.Save(objInsert);

            // Lưu thay đổi
            try
            {
                // Cập nhật lại đơn hàng
                UpdateDonHang(iRecordID);

                objDataOutput.TongSanPham = string.Format("{0:#,##0}", doubTongSoLuong);
                objDataOutput.TongTien = string.Format("{0:#,##0}", doubTongTien);
                objDataOutput.TongTienSauGiam = string.Format("{0:#,##0}", doubTongSauGiam);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = FormDkKyDaiLyDonHang_ReloadData(objInsert.ModDTKyDaiLyDonHangId);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Relative Delete: Sản phẩm liên quan
        /// </summary>
        private void FormDkKyDaiLyDonHangDelete()
        {
            int iProductIdDelete = objCommon.ConvertToInt32(Request["ID"]);
            int iRecordID = objCommon.ConvertToInt32(Request["RecordID"]);

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            // Lưu thay đổi
            try
            {
                string strDelete = "ModDTKyDaiLyDonHangId=" + iRecordID + " AND ModProductId = " + iProductIdDelete;
                ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.Delete(strDelete);

                // Cập nhật lại đơn hàng
                UpdateDonHang(iRecordID);

                objDataOutput.TongSanPham = string.Format("{0:#,##0}", doubTongSoLuong);
                objDataOutput.TongTien = string.Format("{0:#,##0}", doubTongTien);
                objDataOutput.TongTienSauGiam = string.Format("{0:#,##0}", doubTongSauGiam);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = FormDkKyDaiLyDonHang_ReloadData(iRecordID);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình xóa dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Relative Update: Sản phẩm liên quan
        /// </summary>
        private void FormDkKyDaiLyDonHangUpDate()
        {
            int iDonHangSanPham = objCommon.ConvertToInt32(Request["ID"]);
            int iDonHangID = objCommon.ConvertToInt32(Request["RecordID"]);

            int iSoLuong = objCommon.ConvertToInt32(Request["txtSoLuong"]);
            double iDonGia = objCommon.ConvertToDouble(Request["txtDonGia"] == null ? "0" : (Request["txtDonGia"].ToString().Replace(".", "").Replace(",", "")));

            bool bolToGetList = false;
            if (!string.IsNullOrEmpty(Request["GetList"]) && Request["GetList"] == "TRUE")
                bolToGetList = true;

            ModDT_Ky_DaiLy_DonHang_SanPhamEntity objInsert = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.GetByID(iDonHangSanPham);
            objInsert.SoLuong = iSoLuong;
            objInsert.DonGia = iDonGia;
            objInsert.TongTien = objInsert.DonGia * objInsert.SoLuong;
            objInsert.TongSauGiam = objInsert.TongTien;
            objInsert.ChietKhau = 0;

            // Lưu thay đổi
            try
            {
                // Cập nhật
                ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.Save(objInsert);

                UpdateDonHang(iDonHangID);

                objDataOutput.TongSanPham = string.Format("{0:#,##0}", doubTongSoLuong);
                objDataOutput.TongTien = string.Format("{0:#,##0}", doubTongTien);
                objDataOutput.TongTienSauGiam = string.Format("{0:#,##0}", doubTongSauGiam);

                // Lấy lại danh sách nếu có
                if (bolToGetList == true)
                    objDataOutput.MessSuccess = FormDkKyDaiLyDonHang_ReloadData(iDonHangID);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình thêm dữ liệu";
                return;
            }
        }

        /// <summary>
        /// Cập nhật thông tin đơn hàng
        /// </summary>
        /// <param name="iDonHangID"></param>
        private void UpdateDonHang(int iDonHangID)
        {
            doubTongSoLuong = 0;
            doubTongTien = 0;
            doubTongSauGiam = 0;

            // Cập nhật thông tin cho đơn hàng
            var DataCount = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.ExecuteDataTable(
                        @"SELECT SUM(SoLuong),SUM(TongTien) FROM dbo.Mod_DT_Ky_DaiLy_DonHang_SanPham
                            WHERE ModDTKyDaiLyDonHangId=" + iDonHangID);
            if (DataCount != null && DataCount.Rows.Count > 0)
            {
                // Lấy thông tin đơn hàng
                ModDT_Ky_DaiLy_DonHangEntity objModDT_Ky_DaiLy_DonHang = ModDT_Ky_DaiLy_DonHangService.Instance.GetByID(iDonHangID);
                if (objModDT_Ky_DaiLy_DonHang != null)
                {
                    objModDT_Ky_DaiLy_DonHang.TongSanPham = ConvertTool.ConvertToInt32(DataCount.Rows[0][0]);
                    objModDT_Ky_DaiLy_DonHang.TongTien = ConvertTool.ConvertToDouble(DataCount.Rows[0][1]);
                    objModDT_Ky_DaiLy_DonHang.TongSauGiam = objModDT_Ky_DaiLy_DonHang.TongTien - objModDT_Ky_DaiLy_DonHang.ChietKhau;
                    ModDT_Ky_DaiLy_DonHangService.Instance.Save(objModDT_Ky_DaiLy_DonHang);

                    // Lấy thông tin
                    doubTongSoLuong = objModDT_Ky_DaiLy_DonHang.TongSanPham;
                    doubTongTien = objModDT_Ky_DaiLy_DonHang.TongTien;
                    doubTongSauGiam = objModDT_Ky_DaiLy_DonHang.TongSauGiam;
                }
            }
        }
        /*
        /// <summary>
        /// CanTv
        /// Lấy danh sách các sản phẩm trong đơn hàng
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string FormDkKyDaiLyDonHang_ReloadData(int iModDTKyDaiLyDonHangId)
        {
            string sData = string.Empty;
            
            // Lấy danh sách các sản phẩm đã có trong đơn hàng
            string strListProductExists = string.Empty;
            List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity> lstCacSanPhamDaCoTrongDonHang = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                                .Where(o => o.ModDTKyDaiLyDonHangId == iModDTKyDaiLyDonHangId).ToList();
            if (lstCacSanPhamDaCoTrongDonHang != null && lstCacSanPhamDaCoTrongDonHang.Count > 0)
            {
                strListProductExists = VSW.Core.Global.Array.ToString(lstCacSanPhamDaCoTrongDonHang.Select(o => o.ModProductId).ToList().ToArray());
            }

            if (strListProductExists == null || string.IsNullOrEmpty(strListProductExists))
                return sData;


            List<ModProduct_InfoEntity> lstProductInfo = ModProduct_InfoService.Instance.CreateQuery()
                                                    .WhereIn(p => p.ID, strListProductExists)
                                                    .OrderByAsc(o => o.Code)
                                                    .ToList();

            if (lstProductInfo == null || lstProductInfo.Count <= 0)
                return sData;

            // Lấy danh sách nhà sản xuất
            List<ModProduct_ManufacturerEntity> lstManufacturerEntity = ModProduct_ManufacturerService.Instance.CreateQuery().ToList();

            int iIndex = 0;
            foreach (ModProduct_InfoEntity item in lstProductInfo)
            {
                sData += "";
                sData += "<tr class='row" + iIndex % 2 + "'>";
                sData += "<td align='center'>" + (iIndex + 1) + "</td>";
                sData += "<td align='center'>" + item.ID + "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (item.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>" + Utils.GetMedia(item.File, 60, 60) + "</td>";
                sData += "<td align='left'>" + item.Code + " </td>";
                sData += "<td align='left'>" + item.Name + "</td>";

                #region Lấy thông tin số lượng và đơn giá
                ModDT_Ky_DaiLy_DonHang_SanPhamEntity objSanPhan_DonHang = lstCacSanPhamDaCoTrongDonHang.Where(o => o.ModProductId == item.ID).SingleOrDefault();
                if (objSanPhan_DonHang == null)
                {
                    sData += "<td align='right' nowrap='nowrap'></td>";
                    sData += "<td align='right'></td>";
                }
                else
                {
                    sData += "<td align='right' nowrap='nowrap'>" + string.Format("{0:#,##0}", objSanPhan_DonHang.SoLuong) + "</td>";
                    sData += "<td align='right'>" + string.Format("{0:#,##0}", objSanPhan_DonHang.DonGia) + "</td>";
                }
                #endregion

                sData += "<td align='right'>" + string.Format("{0:#,##0}", objSanPhan_DonHang.DonGia * objSanPhan_DonHang.SoLuong) + "</td>";
                sData += "<td align='left' style='display:none;'>" + GetManufactureName(lstManufacturerEntity, item.ManufacturerId) + "</td>";
                sData += "<td align='center' style='display:none;'>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) + "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid' href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamDelete_DeleteTr(urlProductRelative_Delete,this,\"" + item.ID + "\");return false;'>";
                sData += "<span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr> ";

                iIndex++;
            }

            return sData;
        }
        */

        /// <summary>
        /// CanTv
        /// Lấy danh sách các sản phẩm trong đơn hàng
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string FormDkKyDaiLyDonHang_ReloadData(int iModDTKyDaiLyDonHangId)
        {
            string sData = string.Empty;

            // Lấy danh sách các sản phẩm đã có trong đơn hàng
            string strListProductExists = string.Empty;
            List<ModDT_Ky_DaiLy_DonHang_SanPhamEntity> lstCacSanPhamDaCoTrongDonHang = ModDT_Ky_DaiLy_DonHang_SanPhamService.Instance.CreateQuery()
                                .Where(o => o.ModDTKyDaiLyDonHangId == iModDTKyDaiLyDonHangId).ToList();
            if (lstCacSanPhamDaCoTrongDonHang != null && lstCacSanPhamDaCoTrongDonHang.Count > 0)
            {
                strListProductExists = VSW.Core.Global.Array.ToString(lstCacSanPhamDaCoTrongDonHang.Select(o => o.ModProductId).ToList().ToArray());
            }

            if (strListProductExists == null || string.IsNullOrEmpty(strListProductExists))
                return sData;


            List<ModProduct_InfoEntity> lstProductInfo = ModProduct_InfoService.Instance.CreateQuery()
                                                    .WhereIn(p => p.ID, strListProductExists)
                                                    .OrderByAsc(o => o.Code)
                                                    .ToList();

            if (lstProductInfo == null || lstProductInfo.Count <= 0)
                return sData;

            // Lấy danh sách nhà sản xuất
            List<ModProduct_ManufacturerEntity> lstManufacturerEntity = ModProduct_ManufacturerService.Instance.CreateQuery().ToList();

            int iIndex = 0;
            foreach (ModProduct_InfoEntity item in lstProductInfo)
            {
                sData += "";
                sData += "<tr class='row" + iIndex % 2 + "'>";
                sData += "<td align='center'>" + (iIndex + 1) + "</td>";
                sData += "<td align='center'>" + item.ID + "</td>";
                sData += "<td class='text-right' align='center' nowrap='nowrap'>";

                if (item.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";

                sData += "</td>";

                sData += "<td align='center'>" + Utils.GetMedia(item.File, 60, 60) + "</td>";
                sData += "<td align='left'>" + item.Code + " </td>";
                sData += "<td align='left'>" + item.Name + "</td>";
                #region Lấy thông tin số lượng và đơn giá
                ModDT_Ky_DaiLy_DonHang_SanPhamEntity objSanPhan_DonHang = lstCacSanPhamDaCoTrongDonHang.Where(o => o.ModProductId == item.ID).SingleOrDefault();
                if (objSanPhan_DonHang == null)
                {
                    sData += "<td align='right' nowrap='nowrap'></td>";
                    sData += "<td align='right'></td>";
                    sData += "<td align='right'></td>";
                }
                else
                {
                    string stxtSoLuong_Value = string.Format("{0:#,##0}", objSanPhan_DonHang.SoLuong);
                    string stxtDonGia_Value = string.Format("{0:#,##0}", objSanPhan_DonHang.DonGia);
                    string stxtTongTien_Value = string.Format("{0:#,##0}", objSanPhan_DonHang.DonGia * objSanPhan_DonHang.SoLuong);

                    sData += "<td align='right' nowrap='nowrap'><input type='text' style='width: 75% !important;' class='text_input txtSoLuong' id=txtSoLuong_" + objSanPhan_DonHang.ID +
                        " value='" + stxtSoLuong_Value + "' disabled='disabled' Value_Old='" + stxtSoLuong_Value + "' /></td>";
                    sData += "<td align='right'><input type='text' style='width: 75% !important;' class='text_input txtDonGia' id=txtDonGia_" + objSanPhan_DonHang.ID +
                        " value='" + stxtDonGia_Value + "' disabled='disabled' Value_Old='" + stxtDonGia_Value + "'  /></td>";
                    sData += "<td align='right'><label type='text' style='width: 75% !important;' class='text_input' id=lblTongTien_" + objSanPhan_DonHang.ID +
                        " value='" + stxtTongTien_Value + "' disabled='disabled' Value_Old='" + stxtTongTien_Value + "' >" + stxtTongTien_Value + "</label></td>";
                }
                #endregion

                sData += "<td align='left' style='display:none;'>" + GetManufactureName(lstManufacturerEntity, item.ManufacturerId) + "</td>";
                sData += "<td align='center' style='display:none;'>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) + "</td>";
                sData += "<td align='center'>";
                sData += "<a class='jgrid' edit href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamEdit(this);return false;' title='Chỉnh sửa'>";
                sData += "<span class='jgrid'><span class='state edit'></span></span></a>";
                sData += "<a class='jgrid hide' save href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamSave(urlSoLuong_DonGia_Save," + objSanPhan_DonHang.ID + ",txtSoLuong_" + objSanPhan_DonHang.ID + ",txtDonGia_" + objSanPhan_DonHang.ID + ", true);return false;' title='Lưu thay đổi'>";
                sData += "<span class='jgrid'><span class='state save'></span></span></a>";
                sData += "<a class='jgrid hide' cancel href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamCancel(this);return false;' title='Hủy cập nhật'>";
                sData += "<span class='jgrid'><span class='state deny'></span></span></a>";
                sData += "</td>";

                sData += "<td align='center'>";
                sData += "<a class='jgrid' href='javascript:void(0);' onclick='KyDaiLyDonHangSanPhamDelete_DeleteTr(urlSoLuong_DonGia_Delete,this,\"" + item.ID + "\", true);return false;'>";
                sData += "<span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";

                sData += "</tr> ";
                iIndex++;
            }

            return sData;
        }

        #endregion
    }

    /// <summary>
    /// Class, Data Output
    /// </summary>
    public class DataOutput : VSW.Website.CP.Tools.Ajax.Common.DataOutput
    {
        public string TongSanPham { get; set; }
        public string TongTien { get; set; }
        public string TongTienSauGiam { get; set; }

        public DataOutput()
        {
            TongSanPham = "0";
            TongTien = "0";
            TongTienSauGiam = "0";
        }
    }

    public class ThuocTinhSanPham
    {
        public string Content { get; set; }
        public int ProductInfoId { get; set; }
        public int PropertiesGroupsId { get; set; }
        public int PropertiesListId { get; set; }
        public bool SaveData { get; set; }
    }
}