<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ITRW324.About" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
    body
    {
        background:#fff;
        font-style : normal;
        align-content:center;
        font-family: Arial;
        font-size: 12pt;
    }
    .main_menu
    {
        width: 100px;
        font-style : normal;
        font-weight : bold;
        background-color: #0094ff;
        color: #000;
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
        font-weight : bold;
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
        font-weight : bold;
        color: #fff;
    }
            #TextArea1 {
                height: 507px;
                width: 624px;
            }
            #txtArea1 {
                height: 470px;
                width: 567px;
            }
        </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
<asp:Menu ID="Menu" runat="server" DataSourceID="SiteMapDataSource1" Orientation="Horizontal"
    OnMenuItemDataBound="OnMenuItemDataBound" BorderStyle = "Outset">
 
    <LevelMenuItemStyles>
        <asp:MenuItemStyle CssClass="main_menu"/>
        <asp:MenuItemStyle CssClass="level_menu"/>
    </LevelMenuItemStyles>
</asp:Menu>
        <br />
           <asp:SiteMapPath runat="server" ID="SiteMapPath1"></asp:SiteMapPath>
        <br />
    </div>
    </form>
    <p>
        &nbsp;</p>

<h1> About our Website: </h1>
 
<p> This is a Document Verification System that utilizes block chain technology and cryptography <br />
    that can verify the existence, integrity of a document. It accomplished by taking the entire <br />
    document and its contents and converting it to cryptographic digits. These cryptographic digits <br />
    are also called a hash in this case. The hash is then validated and then added to the end of the <br />
    block chain for storage and future verification. After being added to the block chain, if the <br />
    same document is presented, the hash value obtained should match an already valid entry inside <br />
    of the block chain. Since the entire content of the document, this includes metadata, is used <br />
    to obtain the cryptographic digits/hash. If any alteration took place to the document or inside <br />
    of the document the hash value will differ to show it doesn’t match or it’s not the original. <br />
    Because of this, only unalterable document types like PDF can be used for this verification method.<br />
    The use of block chain technology allows for a decentralized verification method for documents. <br />
    This process also allows for document time stamping or to demonstrate dataownership without <br />
    revealing the actual data.    </p>

</body>
</html>

