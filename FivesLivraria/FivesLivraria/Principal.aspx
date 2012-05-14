<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Principal.aspx.cs" Inherits="FivesLivraria.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label ID="lblBusca" runat="server" Text="Buscar por nome: "></asp:Label>
        <asp:TextBox ID="txtBusca" runat="server" Width="400px"></asp:TextBox>
        <asp:DropDownList ID="ddlCategoria" runat="server" Width="170px" DataTextField="dsCategoria"
            DataValueField="idCategoria">
        </asp:DropDownList>
        <asp:Button ID="btnBusca" runat="server" Text="Buscar" CssClass="button" OnClick="btnBusca_Click" />
        <br />
        <br />
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="4" OnItemCommand="DataList1_ItemCommand"
            OnItemDataBound="DataList1_ItemDataBound">
            <ItemTemplate>
                <asp:Label ID="idProduto" runat="server" Text='<%# Eval("idProduto") %>' Visible="false" />
                <asp:Image ID="imagem" runat="server" Text="Imagem" ImageUrl='<%#"~/Images/"+ Eval("nmImagem") %>'
                    Width="120px" Height="188px" />
                <br />
                <asp:Button ID="btnComprar" runat="server" Text="Comprar" CssClass="button" align="center" />
                <br />
                <br />
                <asp:Label ID="nmTitulo" runat="server" Text='<%# Eval("nmTitulo") %>' Width="250"
                    CssClass="dsProduto" />
                <br />
                <asp:Label ID="vlPreco" runat="server" Text='<%#"Por apenas: R$"+ Eval("vlPreco") %>'
                    Width="250" CssClass="dsProduto" />
                <br />
                <br />
            </ItemTemplate>
        </asp:DataList>
        <br />
        <br />
        <br />
        Página
        <asp:Label ID="currentPageLabel" runat="server" />
        de
        <asp:Label ID="howManyPagesLabel" runat="server" />
        |
        <asp:HyperLink ID="previousLink" runat="server">Anterior</asp:HyperLink>
        <asp:Repeater ID="pagesRepeater" runat="server">
            <ItemTemplate>
                <asp:HyperLink ID="hyperlink" runat="server" Text='<%# Eval("Page") %>' NavigateUrl='<%# Eval("Url") %>' />
            </ItemTemplate>
        </asp:Repeater>
        <asp:HyperLink ID="nextLink" runat="server">Próximo</asp:HyperLink>
    </p>
</asp:Content>
