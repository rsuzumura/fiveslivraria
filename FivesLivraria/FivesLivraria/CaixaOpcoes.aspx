<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CaixaOpcoes.aspx.cs" Inherits="FivesLivraria.CaixaOpcoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p> Página com as opções para o funcionário </p>
    <p> &nbsp;</p>
    <p> 
        <asp:HyperLink ID="Balcao" runat="server" NavigateUrl="~/CaixaBalcao.aspx"> Caixa / Balcão </asp:HyperLink>
    </p>
    <p> 
        <asp:HyperLink ID="Pedidos" runat="server" NavigateUrl="~/CaixaPedidos.aspx"> Consulta dos Pedidos </asp:HyperLink>
    </p>
    <p> 
        <asp:HyperLink ID="Caixa" runat="server" NavigateUrl="~/CaixaAcoes.aspx"> Ações de Caixa </asp:HyperLink>
    </p>

</asp:Content>

