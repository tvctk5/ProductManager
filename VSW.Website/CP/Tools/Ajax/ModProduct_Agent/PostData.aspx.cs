using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.MVC;

namespace VSW.Website.CP.Tools.Ajax.Common.ModProduct_Agent
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
                case "2": GetTinhThanh(); break;
                case "3": break;
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
            if (ModProduct_AgentService.Instance.DuplicateCode(Code, RecordID, ref sMessError))
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
        /// Lấy danh sách tỉnh thành gửi về cho Client
        /// </summary>
        private void GetTinhThanh()
        {
            int iQuocGia = 0;
            try
            {
                iQuocGia = Convert.ToInt32(Request.Form["QuocGia"]);
                if (iQuocGia <= 0)
                    return;
            }
            catch (Exception ex)
            {
                objDataOutput.Error = true;
                objDataOutput.MessError = ex.ToString();
            }

            objDataOutput.MessSuccess = ShowTinhThanh(iQuocGia, 0);
        }

        /// <summary>
        /// Hiển thị tỉnh thành
        /// </summary>
        /// <param name="QuocGiaID"></param>
        /// <param name="TinhThanhID"></param>
        /// <returns></returns>
        public string ShowTinhThanh(int QuocGiaID, int TinhThanhID)
        {
            List<ModProduct_CityEntity> lstModProduct_CityEntity = ModProduct_CityService.Instance.CreateQuery().Where(p => p.ProductNationalId == QuocGiaID && p.Activity == true).ToList();
            if (lstModProduct_CityEntity == null || lstModProduct_CityEntity.Count <= 0)
                return string.Empty;

            string sReturn = string.Empty;

            foreach (var item in lstModProduct_CityEntity)
            {
                if (item.ID == TinhThanhID)
                    sReturn += "<option value=\"" + item.ID + "\" selected=\"selected\">" + item.Name + "</option>";
                else
                    sReturn += "<option value=\"" + item.ID + "\">" + item.Name + "</option>";
            }

            return sReturn;
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