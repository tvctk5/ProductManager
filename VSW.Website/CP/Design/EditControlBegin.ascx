<%@ Control Language="C#" AutoEventWireup="true"%>
<script runat="server">
    
    private string _CphName = string.Empty;
    public string CphName
    {
        get { return _CphName; }
        set { _CphName = value; }
    }

</script>

<div class="boder-template" style="margin-top:20px;">
<div style="height:20px;width:100%;font-weight:bold;text-align:center;background-color:GrayText;color:White;padding-top:5px;">
  <%= CphName %>
</div>
