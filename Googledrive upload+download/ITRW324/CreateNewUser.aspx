<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewUser.aspx.cs" Inherits="ITRW324.CreateNewUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    body
    {
        background:#fff;
        font-style : normal;
        align-content:center;
        font-family: Arial;
        font-size: 12pt;
    }
    .formclass
    {
        padding:10px;
        margin:auto;
        background:#fff;
        width:200px;
         
    }
    
    .main_menu
    {
        width: 100px;
        background-color: #0094ff;
        font-style : normal;
        font-weight : bold;
        color: #000;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-right: 5px;
    }
    .level_menu
    {
        width: 110px;
        background-color: #000;
        color: #fff;
        text-align: center;
        height: 30px;
        line-height: 30px;
        margin-top: 5px;
    }
    .selected
    {
        background-color: #ff0000;
        color: #fff;
    }

  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
<asp:Menu ID="Menu" runat="server" BorderStyle = "Outset" DataSourceID="SiteMapDataSource1" Orientation="Horizontal"
    OnMenuItemDataBound="OnMenuItemDataBound">
 
    <LevelMenuItemStyles>
        <asp:MenuItemStyle CssClass="main_menu" />
        <asp:MenuItemStyle CssClass="level_menu" />
    </LevelMenuItemStyles>
</asp:Menu>
        <br />
           <asp:SiteMapPath runat="server" ID="SiteMapPath1"></asp:SiteMapPath>
            <br />
    <h2 style="margin-left: 80px">Register new User:</h2>
       <table>
           <tr>
               <td class="auto-style1">Enter Username:</td>
               <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox1" ForeColor="Red" ErrorMessage="Username Required"></asp:RequiredFieldValidator>
               </td>
           </tr>
           <tr>
               <td class="auto-style1">Enter Password:</td>
               <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox2" ForeColor="Red" ErrorMessage="Password required"></asp:RequiredFieldValidator>
               </td>
           </tr>
  <tr>
               <td class="auto-style1">Confirm Password:</td>
               <td><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                   <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox2" ForeColor="Red" ControlToValidate="TextBox3" Display="Dynamic" ErrorMessage="Passwords do not match"></asp:CompareValidator>
               </td>
           </tr>
             <tr>
               <td class="auto-style1">Enter Email:</td>
               <td><asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox4" ForeColor="Red" ErrorMessage="Email required"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox4" ForeColor="Red" ErrorMessage="Please enter a valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                 </td>
           </tr>

           <tr>
               <td>
                   <br />
               </td>
           </tr>

           <tr>
               <td class="auto-style1">
                   <asp:Button ID="Button1" runat="server" Text="Create User" BorderStyle = "Outset" ForeColor ="Green" OnClick="Button1_Click" />
               &nbsp;
               </td>
           </tr>
           

           <tr>
               <td class="auto-style2">
                   <br />
                   <asp:Label ID="Label1" runat="server"></asp:Label>
               </td>
           </tr>
           

           <tr>
               <td class="auto-style1">
                   &nbsp;</td>
           </tr>
           

       </table>
        
    </div>
    </form>
</body>
</html>

