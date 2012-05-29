<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CaixaOpcoes.aspx.cs" Inherits="FivesLivraria.CaixaOpcoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Funcionários - Menu Geral
    </h2>
    <p>
        &nbsp;</p>
    <div align="center" style="margin-bottom: 5px;">
        <asp:Button ID="btnCaixa" runat="server" Text="Ações de Caixa" OnClick="btnCaixa_Click"
            Width="200px" Height="50px" />
    </div>
    <div align="center" style="margin-bottom: 5px;">
        <asp:Button ID="btnBalcao" runat="server" Text="Caixa / Balcão" OnClick="btnBalcao_Click"
            Width="200px" Height="50px" />
    </div>
    <div align="center" style="margin-bottom: 5px;">
        <asp:Button ID="btnPedidos" runat="server" Text="Consulta dos Pedidos" OnClick="btnPedidos_Click"
            Width="200px" Height="50px" />
    </div>
</asp:Content>
