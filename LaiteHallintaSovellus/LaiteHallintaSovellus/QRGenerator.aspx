<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRGenerator.aspx.cs" Inherits="LaiteHallintaSovellus.QRGenerator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Image runat="server" ID="QRImage" />
        <asp:Button runat="server" ID="Button1" Text="Luo PDF" OnClick="CreatePDF_OnClick" />
    </div>
    </form>
</body>
</html>
