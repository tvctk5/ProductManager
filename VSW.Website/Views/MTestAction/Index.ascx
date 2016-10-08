<%@ Control Language="C#" AutoEventWireup="true" Inherits="VSW.Lib.MVC.ViewControl"%>

<form method=post>
<h1> Cách gọi 1</h1>

 
<input type="text" name="Name" value="" /><br />
<input type="text" name="Address" value="" /><br />

<input type="submit" name="_vsw_action[A_CanTV]" text="Action_A" value="Action_A" />
<input type="submit" name="_vsw_action[B_CanTV]" text="Action_B" value="Action_B" />

<h1> Cách gọi 2</h1>
</form>