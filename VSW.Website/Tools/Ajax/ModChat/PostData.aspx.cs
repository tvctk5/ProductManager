using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VSW.Lib.Models;
using VSW.Lib.Global;

public partial class PostData : System.Web.UI.Page
{
    DataOutput objDataOutput = new DataOutput();
    List<string> lstBoxChat = null;
    const string sIsMe = "Tôi";
    string sAction = string.Empty;
    string sUserName = string.Empty;
    string sUserFrom = string.Empty;
    string sUserTo = string.Empty;
    string sMessager = string.Empty;

    /// <summary>
    /// 0: Bắt đầu phiên của khác vãng lai | Bắt đầu phiên của quản trị
    /// </summary>
    int iType = 0;
    string sSessionFrom = string.Empty;
    string sSessionTo = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        sAction = Request["action"];

        if (string.IsNullOrEmpty(sAction))
            Response.Write(string.Empty);

        switch (sAction)
        {
            case "chatheartbeat": chatHeartbeat();
                break;

            case "sendchat": sendChat();
                break;

            case "openchatbox": openChat();
                break;

            case "closechat": closeChat();
                break;

            case "startchatsession": startChatSession();
                break;
        }

        // Trả lại dữ liệu
        RenderMessage(objDataOutput);
    }

    private void chatHeartbeat()
    {
        sUserFrom = ConvertTool.ConvertToString(Request.Cookies["UserName_Chat"].Value);
        var History = ModChat_HistoryService.Instance.CreateQuery().Where(o => o.To_UserName == sUserFrom && o.Activity == false)
                                        .OrderBy("From_UserName,CreateDate").ToList();

        if (History == null || History.Count <= 0)
        {
            objDataOutput.ListChatItem = new List<BoxChatItem>();
            return;
        }

        #region Kiểm tra xem, người gửi gần nhất có phải là người gửi tiếp theo không
        sUserTo = History.FirstOrDefault().From_UserName;

        var objHistoryCheck_List = ModChat_HistoryService.Instance.CreateQuery().Where(o => ((o.From_UserName == sUserFrom && o.To_UserName == (sUserTo))
            || (o.From_UserName == (sUserTo) && o.To_UserName == (sUserFrom))) && o.Activity == true)
            .OrderByDesc(o => o.CreateDate).ToList();

        var objHistoryCheck = new ModChat_HistoryEntity();
        if (objHistoryCheck_List == null || objHistoryCheck_List.Count <= 0)
            objHistoryCheck = null;
        else
            objHistoryCheck = objHistoryCheck_List[0];
        #endregion

        // Duyệt, tạo list
        BoxChatItem itemBox = null;
        List<BoxChatItem> lstListHistory = new List<BoxChatItem>();
        string sFromUserName_Check = string.Empty;
        sUserName = ConvertTool.ConvertToString(Request.Cookies["UserName_Chat"].Value);

        foreach (var item in History)
        {
            itemBox = new BoxChatItem();
            itemBox.f = item.From_UserName;

            // Kiểm tra lần đầu dựa vào dữ liệu tìm thấy trong db
            if (lstListHistory.Count <= 0)
            {
                if (objHistoryCheck == null || objHistoryCheck.From_UserName.Equals(item.From_UserName) == false)
                    itemBox.s = 1; // Mặc định hiển thị UserName
                else
                {
                    itemBox.s = 2; // Không hiển thị UserName (Messager hiển thị thành một khối)
                    objDataOutput.UserChatContiue = true;
                }
            }
            // Kiểm tra những lần sau, dựa vào dữ liệu mới gần nhất
            else
            {
                if (string.IsNullOrEmpty(sFromUserName_Check) || sFromUserName_Check.Equals(item.From_UserName) == false)
                    itemBox.s = 1;
                else
                    itemBox.s = 2;
            }

            itemBox.m = item.Message;
            itemBox.datetime = "Tin nhắn gửi vào lúc " + item.CreateDate.ToString("HH:mm - dd/MM/yyyy");
            lstListHistory.Add(itemBox);

            // Lưu lại lịch sử vào Session
            BoxChatItem NewItem = new BoxChatItem();
            NewItem = itemBox;
            NewItem.boxTitle = item.From_UserName;
            updateChatHistory(NewItem);

            sFromUserName_Check = item.From_UserName;

            // Cập nhật lại trạng thái messager
            item.Activity = true;

            // Cập nhật lại trong db
            ModChat_HistoryService.Instance.Save(item);
        }

        // Gán lại danh sách
        objDataOutput.ListChatItem = lstListHistory;
    }

    private void sendChat()
    {
        // Lấy thông tin đăng nhập
        GetDataUser();

        sMessager = FormatText(ConvertTool.ConvertToString(Request["message"]));
        sUserFrom = ConvertTool.ConvertToString(Request.Cookies["UserName_Chat"].Value);
        sUserTo = ConvertTool.ConvertToString(Request["to"]);

        #region Kiểm tra xem, người gửi gần nhất có phải là người gửi tiếp theo không
        var objHistoryCheck_List = ModChat_HistoryService.Instance.CreateQuery().Where(o => (o.From_UserName == (sUserFrom) && o.To_UserName == (sUserTo))
            || (o.From_UserName == (sUserTo) && o.To_UserName == (sUserFrom))
            )
            .OrderByDesc(o => o.CreateDate).ToList();

        var objHistoryCheck = new ModChat_HistoryEntity();
        if (objHistoryCheck_List == null || objHistoryCheck_List.Count <= 0)
            objHistoryCheck = null;
        else
            objHistoryCheck = objHistoryCheck_List[0];
        #endregion

        ModChat_HistoryEntity objHistory = new ModChat_HistoryEntity();
        objHistory.From_UserName = sUserFrom;
        objHistory.To_UserName = sUserTo;
        objHistory.Message = sMessager;
        objHistory.IP = Context.Request.ServerVariables["REMOTE_ADDR"];
        objHistory.Activity = false;
        objHistory.CreateDate = DateTime.Now;

        ModChat_HistoryService.Instance.Save(objHistory);

        // Lưu vào lịch sử
        BoxChatItem objBoxChatItem = new BoxChatItem();
        objBoxChatItem.boxTitle = objHistory.To_UserName;
        objBoxChatItem.f = sIsMe; //objHistory.From_UserName;

        if (objHistoryCheck == null || objHistoryCheck.From_UserName.Equals(objHistory.From_UserName) == false)
            objBoxChatItem.s = 1; // Mặc định hiển thị UserName
        else
        {
            objBoxChatItem.s = 2; // Không hiển thị UserName (Messager hiển thị thành một khối)
            objDataOutput.UserChatContiue = true;
        }

        objBoxChatItem.m = objHistory.Message;
        objBoxChatItem.datetime = "Tin nhắn gửi vào lúc " + objHistory.CreateDate.ToString("HH:mm - dd/MM/yyyy");

        // Ngày gửi tin nhắn
        objDataOutput.datetime = objBoxChatItem.datetime;
        objDataOutput.MessSuccess = sMessager;

        // Lưu lại lịch sử
        updateChatHistory(objBoxChatItem);
    }

    private string FormatText(string input)
    {
        input = HttpUtility.HtmlEncode(input);
        input = input.Replace("\n\r", "\n");
        input = input.Replace("\n", "<br>");

        return input;
    }

    private void closeChat()
    {
        string sChatboxName = Request["chatbox"];
        List<string> lstListBoxChat = null;

        if (Session["ListBoxChat"] == null)
            return;

        lstListBoxChat = (List<string>)Session["ListBoxChat"];

        // Xem đã tồn tại chưa, nếu chưa thì thêm vào
        if (lstListBoxChat.Contains(sChatboxName) == false)
            return;

        lstListBoxChat.Remove(sChatboxName);

        // Gán lại
        Session["ListBoxChat"] = lstListBoxChat;
    }

    private void openChat()
    {
        string sChatboxName = Request["chatbox"];
        List<string> lstListBoxChat = null;

        if (Session["ListBoxChat"] == null)
            lstListBoxChat = new List<string>();
        else
            lstListBoxChat = (List<string>)Session["ListBoxChat"];

        // Xem đã tồn tại chưa, nếu chưa thì thêm vào
        if (lstListBoxChat.Contains(sChatboxName) == false)
            lstListBoxChat.Add(sChatboxName);

        // Gán lại
        Session["ListBoxChat"] = lstListBoxChat;
    }

    private void startChatSession()
    {
        // Lấy thông tin người đăng nhập
        GetDataUser();

        if (Session["ListBoxChat"] != null)
            lstBoxChat = (List<string>)Session["ListBoxChat"];

        if (lstBoxChat == null)
            lstBoxChat = new List<string>();

        List<BoxChatItem> lstBoxChatItem = null;
        if (Session["ChatHistory"] != null)
        {
            try
            {
                lstBoxChatItem = (List<BoxChatItem>)Session["ChatHistory"];
            }
            catch
            {
                lstBoxChatItem = new List<BoxChatItem>();
            }
        }

        if (lstBoxChatItem == null)
            lstBoxChatItem = new List<BoxChatItem>();

        objDataOutput.BoxChats = lstBoxChat;
        objDataOutput.ListChatItem = lstBoxChatItem;
        objDataOutput.username = sUserName;

        Session["ChatHistory"] = lstBoxChatItem;
    }

    /// <summary>
    /// Lấy thông tin người đăng nhập
    /// </summary>
    private void GetDataUser()
    {
        if (Request.Cookies["UserName_Chat"] != null && string.IsNullOrEmpty(Request.Cookies["UserName_Chat"].Value) == false)
            sUserName = ConvertTool.ConvertToString(Request.Cookies["UserName_Chat"].Value);
        else
        {
            sUserName = "User_" + RandomNumber(100, 10000) + DateTime.Now.ToString("fff");
            HttpCookie aCookie = new HttpCookie("UserName_Chat");
            aCookie.Value = sUserName;

            // Mặc định: Hạn là 1 tiếng
            aCookie.Expires = DateTime.Now.AddMinutes(60);
            Response.Cookies.Add(aCookie);

            // Refresh dữ liệu
            Session["ListBoxChat"] = null;
            Session["ChatHistory"] = null;
        }

        Session["UserName_Chat"] = sUserName;
    }

    private void updateChatHistory(BoxChatItem objBoxChatItem)
    {
        List<BoxChatItem> lstBoxChatItem = null;
        if (Session["ChatHistory"] == null)
            lstBoxChatItem = new List<BoxChatItem>();
        else
        {
            try
            {
                lstBoxChatItem = (List<BoxChatItem>)Session["ChatHistory"];
            }
            catch
            {
                lstBoxChatItem = new List<BoxChatItem>();
            }
        }

        lstBoxChatItem.Add(objBoxChatItem);

        // Lưu lại
        Session["ChatHistory"] = lstBoxChatItem;
    }

    private int RandomNumber(int min, int max, int seed = 0)
    {
        Random random = new Random((int)DateTime.Now.Ticks + seed);
        return random.Next(min, max);
    }

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
}

/// <summary>
/// Class, Data Output
/// </summary>
public class DataOutput
{
    public bool Error { get; set; }
    public string MessError { get; set; }
    public string MessSuccess { get; set; }
    public string username { get; set; }
    public List<string> ListMessage { get; set; }
    public List<string> BoxChats { get; set; }
    public List<BoxChatItem> ListChatItem = null;
    public bool UserChatContiue { get; set; }
    public string datetime { get; set; }

    public DataOutput()
    {
        Error = false;
        ListMessage = new List<string>();
        BoxChats = new List<string>();
        ListChatItem = new List<BoxChatItem>();
        UserChatContiue = false;
    }
}

public class BoxChatItem
{
    public string boxTitle { get; set; }

    /// <summary>
    /// Type: 1: Dòng tin nhắn thuộc Nhóm tin mới | 2: Dòng tin nhắn thuộc Nhóm tin cũ
    /// </summary>
    public int s { get; set; }

    /// <summary>
    /// Title box name
    /// </summary>
    public string f { get; set; }

    /// <summary>
    /// Messager
    /// </summary>
    public string m { get; set; }

    /// <summary>
    /// DateTime
    /// </summary>
    public string datetime { get; set; }
}