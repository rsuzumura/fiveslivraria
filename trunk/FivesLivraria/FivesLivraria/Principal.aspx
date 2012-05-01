<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Principal.aspx.cs" Inherits="FivesLivraria.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label ID="lblBusca" runat="server" Text="Buscar por nome: "></asp:Label>
        <asp:TextBox ID="txtBusca" runat="server" Width="400px"></asp:TextBox>
        <asp:DropDownList ID="ddlCategoria" runat="server" Width="170px">
        </asp:DropDownList>
        <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="button" 
            onclick="btnBusca_Click"/>
        <br /><br />
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="4"
            onitemcommand="DataList1_ItemCommand">
            <ItemTemplate >
                <asp:Label ID="idProduto" runat="server" Text='<%# Eval("idProduto") %>'  Visible="false"/>
                <asp:Image ID="imagem" runat="server" Text="Imagem" src='<%#"/Images/"+ Eval("nmImagem") %>'  width="200"/>
                <br />
                <asp:Button ID="btnComprar" runat="server" Text="Comprar" CssClass="button" align="center" />
                <br /><br />
                <asp:Label ID="nmTitulo" runat="server" Text='<%# Eval("nmTitulo") %>' width="250" CssClass="dsProduto"/>
                <br />
                <asp:Label ID="vlPreco" runat="server" Text='<%#"Por apenas: R$"+ Eval("vlPreco") %>' width="250" CssClass="dsProduto"/>
                <br /><br />
            </ItemTemplate>
        </asp:DataList>
        <br /><br /><br />
        Page
        <asp:Label ID="currentPageLabel" runat="server" />
        of
        <asp:Label ID="howManyPagesLabel" runat="server" />
        |
        <asp:HyperLink ID="previousLink" runat="server">Previous</asp:HyperLink>
        <asp:Repeater ID="pagesRepeater" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hyperlink" runat="server" Text='<%# Eval("Page") %>' NavigateUrl='<%# Eval("Url") %>' />
            </ItemTemplate>
        </asp:Repeater>
        <asp:HyperLink ID="nextLink" runat="server">Next</asp:HyperLink>
    </p>
</asp:Content>
