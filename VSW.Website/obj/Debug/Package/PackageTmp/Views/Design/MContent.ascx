<%@ Control Language="C#" AutoEventWireup="true"%>

<script runat="server">
    public VSW.Core.Global.Class CurrentObject { get; set; }
    public SysPageEntity CurrentPage { get; set; }
    public VSW.Lib.MVC.ModuleInfo CurrentModule { get; set; }
    public int LangID { get; set; }

    public string GetCurrentValue(string fieldName)
    {
        string currentValue = string.Empty;

        if (CurrentPage != null)
            currentValue = CurrentPage.Items.GetValue(CurrentModule.Code + "." + fieldName).ToString();

        if (currentValue == string.Empty)
            currentValue = (CurrentObject.GetField(fieldName) == null ? string.Empty : CurrentObject.GetField(fieldName).ToString());

        return currentValue;
    }

    public VSW.Core.MVC.PropertyInfo GetPropertyInfo(string fieldName)
    {
        System.Reflection.FieldInfo _FieldInfo = CurrentObject.GetFieldInfo(fieldName);
        object[] attributes = _FieldInfo.GetCustomAttributes(typeof(VSW.Core.MVC.PropertyInfo), true);
        if (attributes == null || attributes.GetLength(0) == 0)
            return null;

        return (VSW.Core.MVC.PropertyInfo)attributes[0];
    }
</script>

<div class="col width-100">
  <textarea style="width:100%;height:500px" name="Content" id="Content"><%=CurrentPage.Content%></textarea>
</div>