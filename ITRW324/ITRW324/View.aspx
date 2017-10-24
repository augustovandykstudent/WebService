﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="ITRW324.View" %> 

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
        <style type="text/css">
    body
    {
        background: #fff;
        background-image: url('/Picture/Nimri new.jpg');
        font-style : normal;
        align-content:center;
        font-family: Arial;
        font-size: 12pt;
        color: aquamarine;
    }
    .main_menu
    {
        width: 100px;   
        background-color: #c9e483;
        font-style : normal;
        font-weight : bold;
        color: black;
        text-align: center;      
        height: 30px;
        line-height: 30px;
        margin-right: 5px;
        border-radius: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
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
        background-color: #0094ff;
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
           <asp:SiteMapPath runat="server" ID="SiteMapPath1" ForeColor="Black"></asp:SiteMapPath>
          </div> 
     <%   if (Session["user"] != null) 
         {  %> 
        <div> 
                 <br /> 
                 <asp:Label ID="Label1" runat="server"></asp:Label> 
 
      <asp:GridView ID="grid" runat="server" OnSelectedIndexChanged="grid_SelectedIndexChanged" Height="348px" Width="772px">            
             <Columns> 
        
        <asp:TemplateField ItemStyle-HorizontalAlign="Center"> 
            <ItemTemplate> 
                <asp:LinkButton ID="lnkView" runat="server" Text="View" OnClick="view" CommandArgument='<%# Eval("Hash") %>'></asp:LinkButton> 
            </ItemTemplate> 
        </asp:TemplateField> 
    </Columns> 
        </asp:GridView> 
                 <br /> 
         
         <%  }%> 
         <%   else     { %> 
        <p><b>Please Login by clicking the link below</b></p> 
        <asp:HyperLink ID="HyperLink1" Text="Login" runat="server" NavigateUrl="~/Login.aspx"></asp:HyperLink> 
 
        <% } %> 
         </div> 
  
    </form>
</body>
</html>

