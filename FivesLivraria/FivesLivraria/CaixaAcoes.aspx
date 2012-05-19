<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CaixaAcoes.aspx.cs" Inherits="FivesLivraria.CaixaAcoes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <div align="center">
        <asp:Button ID="btnAbrirCaixa" runat="server" Text="Abrir Caixa" OnClick="btnAbrirCaixa_onClick"
            Width="200px" Height="60px" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnReducao" runat="server" Text="Fechar Caixa | Redução Z" OnClick="btnReducao_onClick"
            Width="200px" Height="60px" />
    </div>
    <br />
    <br />
    <div align="center">
        <asp:Button ID="btnLeitura" runat="server" Text="Leitura X" OnClick="btnLeitura_onClick"
            Width="200px" Height="60px" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnHistorico" runat="server" Text="Historico do Caixa" OnClick="btnHistorico_onClick"
            Width="200px" Height="60px" />
    </div>
    <br />
    <br />
</asp:Content>
