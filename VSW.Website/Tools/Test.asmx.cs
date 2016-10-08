using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using VSW.Lib.Models;

namespace VSW.Website.Tools
{
    /// <summary>
    /// Summary description for Test
    /// </summary>  
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Test : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string DisplayMessage(string NameInfo)
        {
            return "Welcome " + NameInfo + " to the world of JQuery AJax";
        }

        [WebMethod]
        public string GetDataTable(string iTop)
        {
            try
            {
                DataTable objDataTable = new DataTable();

                objDataTable.Columns.Add("Column1");
                objDataTable.Columns.Add("Column2");
                //DataRow objDataRow = objDataTable.NewRow();
                //objDataRow[0] = "Giá trị số 1.1";
                //objDataRow[1] = "Giá trị số 1.2";
                //objDataTable.Rows.Add(objDataRow);

                //objDataRow = objDataTable.NewRow();
                //objDataRow[0] = "Giá trị số 2.1";
                //objDataRow[1] = "Giá trị số 2.2";
                //objDataTable.Rows.Add(objDataRow);

                //string ans = JsonConvert.SerializeObject(objDataTable, Formatting.Indented);

                //string script = "{\"DataRow\": " + ans + "}";
                ////script += "for(i = 0;i<employeeList.Employee.length;i++)";
                ////script += "{";
                ////script += "alert ('Name : ='+employeeList.Employee[i].Name+' Age : = '+employeeList.Employee[i].Age);";
                ////script += "}";

                ////JsonSerializerSettings objJsonSerializerSettings = new JsonSerializerSettings();
                ////DataTable tblGetData = (DataTable)JsonConvert.DeserializeObject(script);
                //return script;





                /////            test
                ModProduct_ManufacturerEntity objModProduct_Manufacturer = ModProduct_ManufacturerService.Instance.CreateQuery().OrderByAsc(p => p.ID).ToSingle();

                DataRow objDataRow = objDataTable.NewRow();
                objDataRow[0] = objModProduct_Manufacturer.ID;
                objDataRow[1] = objModProduct_Manufacturer.Name;
                objDataTable.Rows.Add(objDataRow);

                string ans = JsonConvert.SerializeObject(objDataTable, Formatting.Indented);
                string script = "{\"DataRow\": " + ans + "}";
                return script;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
