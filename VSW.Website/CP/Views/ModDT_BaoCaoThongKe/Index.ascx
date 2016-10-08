<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.CPViewControl" %>
<%
    var model = ViewBag.Model as ModDT_BaoCaoThongKeModel;
    var listItem = ViewBag.Data as List<ModDT_Ky_DaiLyEntity>;

    // Hiển thị mô hình tổng quan cơ cấu các đại lý trong kỳ
    string strTree = string.Empty;
    string strTreeDaiLyItem = string.Empty;
    // Root
    strTreeDaiLyItem += "[{ v: '0', f: '<div style=\"color:blue; font-weight: bold;\">Root</div>' }, '', 'Root'],";

    if (listItem != null && listItem.Count > 0)
    {
        foreach (var item in listItem)
        {
            if (!item.ModProductAgentParentId.HasValue)
            {
                strTree += "{ \"id\": \"" + item.ModProductAgentId + "\", \"parent\": \"#\", \"text\": \"" + item.Name + "\", \"icon\" : \"/CP/Content/jstree/img/folder-parent.png\",\"state\" : {\"opened\": \"true\" }},";
                strTreeDaiLyItem += "[{ v: '" + item.ModProductAgentId + "', f: '" + item.Name + "' }, '0', '" + item.Name + "'],";
            }
            else
            {
                strTree += "{ \"id\": \"" + item.ModProductAgentId + "\", \"parent\": \"" + item.ModProductAgentParentId + "\", \"text\": \"" + item.Name + "\",\"state\" : {\"opened\": \"true\" }},";
                strTreeDaiLyItem += "[{ v: '" + item.ModProductAgentId + "', f: '" + item.Name + "' }, '" + item.ModProductAgentParentId + "', '" + item.Name + "'],";
            }
        }
    }
    // --- end: Hiển thị mô hình tổng quan cơ cấu các đại lý trong kỳ

    // Hiển thị mô hình thu nhập của các đại lý sau kỳ
    double doubTongTienHoaHong = 0;
    string strDaiLy_KyDoanhThu = string.Empty;
    string strDaiLy_ListName = string.Empty;
    string strDaiLy_ListValue = string.Empty;

    ModDT_KyEntity objModDT_KyEntity = ModDT_KyService.Instance.GetByID(model.ModDtKyId);
    if (objModDT_KyEntity != null)
        strDaiLy_KyDoanhThu += objModDT_KyEntity.Name;
    
    List<VSW.Lib.Global.ListItem.Item> listDaiLy = VSW.Lib.Global.ListItem.List.GetList_Dynamic(ModDT_Ky_DaiLyService.Instance, model.ModDtKyId <= 0 ? "" : ("ModDtKyId=" + model.ModDtKyId), "Name", "ModProductAgentId", "ModProductAgentParentId", "Name", "ID");

    if (listDaiLy != null && listDaiLy.Count > 0)
    {
        // Lấy thông tin các đại lý sau khi đã chốt kỳ
        List<ModDT_Ky_DaiLyEntity> lstModDT_Ky_DaiLyEntity = ModDT_Ky_DaiLyService.Instance.CreateQuery()
                                                            .Where(o => o.ModDtKyId == model.ModDtKyId).ToList_Cache();

        // Nội dung chi tiết
        int i = -1;
        foreach (var itemDaiLy in listDaiLy)
        {
            double doubThuNhap = 0;

            if (lstModDT_Ky_DaiLyEntity != null && lstModDT_Ky_DaiLyEntity.Count > 0)
            {
                ModDT_Ky_DaiLyEntity objKyDaiLy = lstModDT_Ky_DaiLyEntity.Where(o => o.ID == ConvertTool.ConvertToInt32(itemDaiLy.Value)).SingleOrDefault();
                if (objKyDaiLy != null)
                    doubThuNhap = objKyDaiLy.TongTienHoaHong;
            }

            // Tổng tiền
            doubTongTienHoaHong += doubThuNhap;
            
            if (string.IsNullOrEmpty(strDaiLy_ListName))
                strDaiLy_ListName += "'" + itemDaiLy.Name.Trim('-').Trim() + "'";
            else
                strDaiLy_ListName += ",'" + itemDaiLy.Name.Trim('-').Trim() + "'";

            if (string.IsNullOrEmpty(strDaiLy_ListValue))
                strDaiLy_ListValue += (doubThuNhap/1000).ToString();
            else
                strDaiLy_ListValue += "," + (doubThuNhap / 1000).ToString();
            
        }
    }
    // End: Hiển thị mô hình thu nhập của các đại lý sau kỳ
    
    string strCharTreeDaiLy =
                            @"var data = new google.visualization.DataTable();
                                    data.addColumn('string', 'Name');
                                    data.addColumn('string', 'Manager');
                                    data.addColumn('string', 'ToolTip');
                                    data.addRows([";
        strCharTreeDaiLy += strTreeDaiLyItem.Trim(',');
        strCharTreeDaiLy += @"]);
                            var chart = new google.visualization.OrgChart(document.getElementById('chart_div_tree_daily'));
                            chart.draw(data, { allowHtml: true, showRowNumber: true, width: '100%', height: '100%' });";
%>
<style type="text/css">
    .thongke-baocao-group
    {
        line-height: 30px;
        font-weight: bold;
        background-color: #E4E4AA;
        padding: 5px;
        font-size: 13pt;
        font-family: arial tahoma verdana sans-serif;
    }
    .bieu-do
    {
        overflow: scroll;
    }
</style>
<form id="vswForm" name="vswForm" method="post">
<script type="text/javascript" src="/{CPPath}/Content/add/jQuery/jquery-2.1.1.min.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/jstree/jstree.min.js"></script>
<link rel="stylesheet" href="/{CPPath}/Content/jstree/themes/default/style.min.css" />
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<!--highcharts-->
<script type="text/javascript" src="/{CPPath}/Content/add/highcharts/highcharts.js"></script>
<script type="text/javascript" src="/{CPPath}/Content/add/highcharts/modules/exporting.js"></script>
<script type="text/javascript">
    // Tree
    google.load('visualization', '1', { packages: ['orgchart'] });
    google.setOnLoadCallback(drawChartDaiLy);
    function drawChartDaiLy() {
        <%=strCharTreeDaiLy %>
    }
    
</script>
<script type="text/javascript">
    $(document).ready(function () {

        $('#treeDaiLy').jstree({ 'core': {
            'data': [<%=strTree %>]
        }
        });
    });
</script>
<script type="text/javascript">
    $(function () {
        $('#ky-thunhap-container').highcharts({
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Biểu đồ doanh thu của các đại lý sau kỳ'
            },
            subtitle: {
                text: 'Nguồn: td-mart.com'
            },
            xAxis: {
                categories: [<%=strDaiLy_ListName %>],
                title: {
                    text: 'Các đại lý'
                },
                labels: {
                    style: {"color": "blue"}
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'Thu nhập (Ngàn Đồng)',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify',
                    formatter: function () {
                    // 1.000.000,00
                        return Highcharts.numberFormat(this.value, 0, ',', ".");
                    }
                }
            },
            tooltip: {
                valueSuffix: ' (Ngàn Đồng)'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true
                    }
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 100,
                floating: true,
                borderWidth: 1,
                backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor || '#FFFFFF'),
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: '<%=strDaiLy_KyDoanhThu %>',
                data: [<%=strDaiLy_ListValue %>]
            }
//            , {
//                name: 'Year 1900',
//                data: [133, 156, 947, 408, 6]
//            }, {
//                name: 'Year 2008',
//                data: [973, 914, 4054, 732, 34]
//            }
            ]
        });
    });
    

		</script>
<input type="hidden" id="_vsw_action" name="_vsw_action" />
<input type="hidden" id="boxchecked" name="boxchecked" value="0" />
<div id="toolbar-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <div class="toolbar-list" id="toolbar">
            <%=GetDefaultListCommand()%>
        </div>
        <div class="pagetitle icon-48-generic">
            <h2>
                Doanh thu - Báo cáo thống kê</h2>
        </div>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
<div class="clr">
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $("#toolbar").remove();
    });
    var VSWController = 'ModDT_BaoCaoThongKe';

    var VSWArrVar = [
                        'limit', 'PageSize',
                        'ModDtKyId', 'ModDtKyId'
                   ];


    var VSWArrVar_QS = [
                        'filter_search', 'SearchText'
                      ];

    var VSWArrQT = [
                      '<%= model.PageIndex + 1 %>', 'PageIndex',
                      '<%= model.Sort %>', 'Sort'
                  ];

    var VSWArrDefault =
                  [
                    '1', 'PageIndex',
                    '20', 'PageSize'
                  ];
</script>
<%= ShowMessage()%>
<div id="element-box">
    <div class="t">
        <div class="t">
            <div class="t">
            </div>
        </div>
    </div>
    <div class="m">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <div id="table_sort_div">
                    </div>
                    <div id="chart_sort_div">
                    </div>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <div id='chart_div'>
                    </div>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2">
                    <table class="admintable">
                        <tr>
                            <td class="key" style="width: 15% !important">
                                <label>
                                    Kỳ doanh thu :</label>
                            </td>
                            <td style="width: 100% !important">
                                <select id="ModDtKyId" name="ModDtKyId" onchange="VSWRedirect();return false;" class="DropDownList">
                                    <%= Utils.ShowDDLList(ModDT_KyService.Instance, "", "[ID] DESC", model.ModDtKyId)%>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="thongke-baocao-group">
                                    Sơ đồ mô hình tổng quan cơ cấu các đại lý</div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="key" style="text-align: left !important;">
                                <div class="bieu-do">
                                    <div id="treeDaiLy">
                                    </div>
                                </div>
                            </td>
                            <td>
                                <div class="bieu-do">
                                    <div id='chart_div_tree_daily'>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div class="thongke-baocao-group">
                                    Sơ đồ thu nhập sau kỳ của các đại lý</div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="ky-thunhap-container" style="min-width: 310px; width: 100%; height: auto; margin: 0 auto;overflow:visible;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="clr">
        </div>
    </div>
    <div class="b">
        <div class="b">
            <div class="b">
            </div>
        </div>
    </div>
</div>
</form>
