<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadfrm.aspx.cs" Inherits="ITRW324.FileUploadfrm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:FileUpload ID="FileUploadVerify" runat="server" enctype="multipart/form-data"/>
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Submit" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
