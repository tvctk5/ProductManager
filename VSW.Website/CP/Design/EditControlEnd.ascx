<%@ Control Language="C#" AutoEventWireup="true"%>

<script runat="server">
    
    private string _CphName = string.Empty;
    public string CphName
    {
        get { return _CphName; }
        set { _CphName = value; }
    }

    List<VSW.Lib.MVC.ModuleInfo> listModule = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        listModule = VSW.Lib.Web.Application.Modules.Where(o => o.IsControl == true).OrderBy(o => o.Order).ToList();
    }
</script>

<div id="to_cph_<%= CphName%>" style="min-height:10px;width:100%" ondragenter="return dragEnter(event)" ondrop="return dragDrop(event)" 
     ondragover="return dragOver(event)">

</div>

<div class="border-control">
<table class="admintable" style="width: 100%;">
   <tr>
      <td class="key" style="width:100%;text-align:center;" colspan="2"><a onclick="do_display('tbl_<%= CphName%>');" href="javascript:;">Thêm mới điều khiển</a></td>
   </tr>
</table>
<table id="tbl_<%= CphName%>" class="admintable" style="width: 100%;display:none;">
  <tr>
     <td class="key" style="font-size:12px;font-weight:normal;width:35%;">Điều khiển :</td>
     <td>
         <select class="text_input" name="vsw_cph_<%= CphName %>_control_code" id="vsw_cph_<%= CphName %>_control_code">
            <%for (int i = 0; listModule != null && i < listModule.Count; i++)
                { %>
                <option value='<%= listModule[i].Code%>'><%= listModule[i].Name%></option>
            <%} %> 
            <option value='VSWMODULE'>VSWMODULE</option>
        </select>
       </td>
  </tr>
  <tr>
     <td class="key" style="font-size:12px;font-weight:normal;">Mã :</td>
     <td>
        <input id="vsw_cph_<%= CphName %>_VSWID" name="vsw_cph_<%= CphName %>_VSWID" value="" style="width:94%;" class="text_input" />
     </td>
  </tr>
  <tr>
     <td colspan="2" align="right" style="padding-right:5px;" > 
        <a href="javascript:cp_update('vsw_cph_<%= CphName %>|add');">
           <img alt="Add" src="/{CPPath}/Content/add/img/save-16.gif" />
        </a>
     </td>
  </tr>
</table>
</div>

<div style="padding-top:10px"></div>

</div>