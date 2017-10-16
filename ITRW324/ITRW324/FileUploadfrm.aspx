<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadfrm.aspx.cs" Inherits="ITRW324.FileUploadfrm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

        <style type="text/css">
    body
    {
             background:#ccc;
    align-content:center;
        font-family: Arial;
        font-size: 10pt;
    }
    .main_menu
    {
        width: 100px;
        background-color: #8AE0F2;
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
        background-color: #852B91;
        color: #fff;
    }
</style>

</head>
<body>
    <form id="form1" runat="server">
 
         <div> 
      
            &nbsp;<asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
<asp:Menu ID="Menu" runat="server" DataSourceID="SiteMapDataSource1" Orientation="Horizontal"
    OnMenuItemDataBound="OnMenuItemDataBound">
 
    <LevelMenuItemStyles>
        <asp:MenuItemStyle CssClass="main_menu" />
        <asp:MenuItemStyle CssClass="level_menu" />
    </LevelMenuItemStyles>
</asp:Menu>
            .<asp:SiteMapPath runat="server" ID="SiteMapPath1"></asp:SiteMapPath>
    
      
            <br />
            <br />
            <br />
             </div>
     <%   if (Session["user"] != null)
         {  %>
        <div>
        <asp:FileUpload ID="FileUploadVerify" runat="server" enctype="multipart/form-data"/>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit" />
        <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
    
    </div>
         <%  }%>
      <%   else     { %>

        <h2>Please Login by clicking the link below</h2>
        <asp:HyperLink ID="HyperLink1" Text="Login" runat="server" NavigateUrl="~/Login.aspx"></asp:HyperLink>

        <% } %>


    </form>
</body>
     
</html>
