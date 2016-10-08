<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="VSW.Website.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
         #tblView td div
         { 
             background-color: #F2F2F2;
             padding-top: 1px;
             padding-bottom: 3px;
             width: 88%;
             }
        #tblView td div div
        {
            margin-top: -3px !important;
            margin-left: -2px !important;
            padding: 5px 0px;
            background-color: white;
            border: 1px solid #D7D7D7;
            width: 98%;
        }
        
        #tblView td
        {
            padding-right: 10px;
            min-height:400px;
        }
        #tblView td div div:hover
        { border: 1px solid Red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <table id="tblView" style="width: 50%">
                <tr>
                    <td style="width: 25%">
                        <div>
                            <div>
                                1 <br />1 <br />1 <br />1 <br />1 <br />1 <br /></div>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div>
                            <div>
                                2 <br />2 <br />2 <br />2 <br />2 <br />2 <br /></div>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div>
                            <div>
                                3 <br />3 <br />3 <br />3 <br />3 <br />3 <br /></div>
                        </div>
                    </td>
                    <td style="width: 25%">
                        <div>
                            <div>
                                4 <br />4 <br />4 <br />4 <br />4 <br />4 <br /></div>
                        </div>
                    </td>
                </tr>
            </table>
        </center>
    </div>
    </form>
</body>
</html>
