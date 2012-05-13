<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CaixaAcoes.aspx.cs" Inherits="FivesLivraria.CaixaAcoes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="contextBtns">
    <p> 
        <asp:Button ID="btnAbrirCaixa" runat="server" Text="Abrir Caixa"
          style="position:absolute; top: 228px; left: 422px;" 
            OnClick="btnAbrirCaixa_onClick"/> 
    
    </p>
    <p> 
        <asp:Button ID="btnReducao" runat="server" Text="Fechar Caixa | Redução Z"
          style="position:absolute; top: 225px; left: 569px; margin-top: 4px;" 
            OnClick="btnReducao_onClick"/> 
    </p>
    <p> 
        <asp:Button ID="btnLeitura" runat="server" Text="Leitura X"
          style="position:absolute; top: 323px; left: 427px;" 
            OnClick="btnLeitura_onClick"/> 
    </p>
    <p>
        <asp:Button ID="btnHistorico" runat="server" Text="Historico do Caixa"
          style="position:absolute; top: 323px; left: 604px;" 
            OnClick="btnHistorico_onClick"/> 
    </p>
</div>
</asp:Content>
