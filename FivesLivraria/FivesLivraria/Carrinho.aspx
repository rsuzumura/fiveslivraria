﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Carrinho.aspx.cs" Inherits="FivesLivraria.Carrinho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 30px">
    <asp:GridView ID="gvCarrinho" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="idCarrinho" 
        onrowcommand="gvCarrinho_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Descrição">
                <ItemTemplate>
                    <asp:Image ID="imagem" runat="server" Text="Imagem" src='<%#"/Images/"+ Eval("nmImagem") %>'  Width="120px" Height="188px"/>
                    <asp:Label ID="nmTitulo" runat="server" Text='<%#Eval("nmTitulo")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/subjectselect_delete.gif" CommandName="excluir" Text="Excluir" />
             <asp:TemplateField HeaderText="Quantidade">
                <ItemTemplate>
                    <asp:TextBox ID="nrQtdProduto" runat="server" Text='<%#Eval("nrQtdProduto")%>' width="40" />                 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="vlPreco" HeaderText="Preço Unitário" SortExpression="idProduto" />
            <asp:BoundField DataField="vlFinal" HeaderText="Valor Final" SortExpression="nmTitulo" />
            <asp:ButtonField ButtonType="Button" CommandName="atualizar" Text="Atualizar" ControlStyle-CssClass="button"/>
        </Columns>
        <EmptyDataTemplate>Não foi encontrado nenhum produto</EmptyDataTemplate>
    </asp:GridView>
    <br />
    <asp:Button ID="btnContinuarComprando" runat="server" 
        Text="Continuar Comprando" CssClass="buttonAcao" 
        onclick="btnContinuarComprando_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnFecharPedido" runat="server" Text="Fechar Pedido" 
        CssClass="buttonAcao" onclick="btnFecharPedido_Click"/>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblTotal" runat="server" Text="Total: " />
    <asp:Label ID="txtTotal" runat="server" />
    </div>
</asp:Content>
