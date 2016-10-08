using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Website.CP.Tools.Ajax.Common.ModProduct_National
{
    partial class PostData : System.Web.UI.Page
    {
        Tools.Common objCommon = new Tools.Common();
        DataOutput objDataOutput = new DataOutput();
        string sMessError = string.Empty;

        #region Tập các giá trị control được post lên để sử dụng
        int RecordID = 0;
        string Code = string.Empty;
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
                case "1": CheckDuplicate(); break;
                case "areaDelete": AreaDelete(); break;
                case "areaGetList": AreaGetList(); break;
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
            if (ModProduct_NationalService.Instance.DuplicateCode(Code, RecordID, ref sMessError))
            {
                if (string.IsNullOrEmpty(sMessError))
                {
                    objDataOutput.NotDuplicate = false;
                    objDataOutput.MessSuccess = CPViewControl.ShowMessDuplicate("Mã đại lý", Code);
                }
                else
                {
                    objDataOutput.Error = true;
                    objDataOutput.MessError = sMessError;
                }
            }
        }

        /// <summary>
        /// Area Delete: Khu vực thuộc quốc gia
        /// </summary>
        private void AreaDelete()
        {
            int iAreaIdDelete = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            ModProduct_National_AreaEntity objModProduct_National_AreaEntity =
                ModProduct_National_AreaService.Instance.CreateQuery().Where(p => p.ProductNationalId == RecordID && p.ID == iAreaIdDelete).ToSingle();
            if (objModProduct_National_AreaEntity == null)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Không tìm thấy thông tin vùng cần xóa";
                return;
            }

            // Lưu thay đổi
            try
            {
                // Lưu lại thay đổi
                ModProduct_National_AreaService.Instance.Delete(objModProduct_National_AreaEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình xóa dữ liệu";
                return;
            }
        }

        /// <summary>
        /// AreaGetList: Khu vực thuộc quốc gia
        /// </summary>
        private void AreaGetList()
        {
            int iProductNationalId = objCommon.ConvertToInt32(Request["ID"]);

            // Lấy bản ghi
            List<ModProduct_National_AreaEntity> objModProduct_NationalEntity = ModProduct_National_AreaService.Instance.CreateQuery().Where(p => p.ProductNationalId == iProductNationalId).ToList();

            // Lấy thông tin
            try
            {
                objDataOutput.Error = false;
                objDataOutput.MessSuccess = Area_ReloadData(objModProduct_NationalEntity);
            }
            catch
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = "Phát sinh lỗi trong quá trình lấy dữ liệu";
                return;
            }
        }

        /// <summary>
        /// CanTv
        /// Lấy danh sách các khu vực thuộc quốc gia
        /// </summary>
        /// <param name="RecordID"></param>
        /// <returns></returns>
        private string Area_ReloadData(List<ModProduct_National_AreaEntity> objModProduct_NationalEntity)
        {
            string sData = string.Empty;

            if (objModProduct_NationalEntity == null || objModProduct_NationalEntity.Count <= 0)
                return sData;

            int iIndex = 0;
            foreach (ModProduct_National_AreaEntity item in objModProduct_NationalEntity)
            {
                sData += "";
                sData += "<tr class='row" + iIndex % 2 + "'>";
                sData += "<td align='center'>" + (iIndex + 1) + "</td>";
                //sData += "<td align='center'>" + item.ID + "</td>";
                sData += "<td align='left' nowrap='nowrap'>" + item.Code + "</td>";
                sData += "<td align='left'>" + item.Name + "</td>";
                sData += "<td align='center'>" + string.Format("{0:dd/MM/yyyy HH:mm}", item.CreateDate) + "</td>";

                sData += "<td class='text-right' align='center' nowrap='nowrap'>";
                if (item.Activity == false)
                    sData += "<span class='jgrid'><span class='state unpublish' title='Không sử dụng'></span></span>";
                else
                    sData += "<span class='jgrid'><span class='state activate' title='Đang sử dụng'></span></span>";
                sData += "</td>";

                sData += "<td align='center'>";
                sData += "<a class='jgrid' title=\"Sửa khu vực\" href='javascript:void(0);' onclick=\"tb_show('', '/CP/FormProduct_Area/Add.aspx/RecordID/" + item.ID + "/ProductNationalId/" + item.ProductNationalId + "TB_iframe=true;height=300;width=850;', ''); return false;\">";
                sData += "<span class='jgrid'><span class='state edit'></span></span></a>";
                sData += "</td>";

                sData += "<td align='center'>";
                sData += "<a class='jgrid' title=\"Xóa khu vực\" href='javascript:void(0);' onclick=\"AreaDelete_DeleteTr(urlArea_Delete,this,'" + item.ID + "');return false;\">";
                sData += "<span class='jgrid'><span class='state delete'></span></span></a>";
                sData += "</td>";
                sData += "</tr> ";


                iIndex++;
            }

            return sData;
        }
    }

    /// <summary>
    /// Class, Data Output
    /// </summary>
    public class DataOutput : VSW.Website.CP.Tools.Ajax.Common.DataOutput
    {
        public DataOutput()
        {

        }
    }
}