<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Carrinho.aspx.cs" Inherits="FivesLivraria.Carrinho" %>
<%@ Import Namespace="System.Data.SqlTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 30px">
        <asp:GridView ID="gvCarrinho" runat="server" AutoGenerateColumns="False" DataKeyNames="idCarrinho"
            Width="90%" OnRowCommand="gvCarrinho_RowCommand" OnRowDataBound="gvCarrinho_RowDataBound"
            ShowFooter="true">
            <FooterStyle Font-Bold="true" />
            <Columns>
                <asp:TemplateField HeaderText="Descrição">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <table style="margin-left: 10px;">
                            <tr>
                                <td>
                                    <asp:Image ID="imagem" runat="server" Text="Imagem" ImageUrl='<%#"~/Images/"+ Eval("nmImagem") %>'
                                        Width="120px" Height="188px" />
                                </td>
                                <td>
                                    <asp:Label ID="nmTitulo" runat="server" Text='<%#Eval("nmTitulo")%>' />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Image" ImageUrl="~/Images/icon_recyclebin_16px.gif" 
                    CommandName="excluir" Text="Excluir" />
                <asp:TemplateField HeaderText="Quantidade">
                    <ItemTemplate>
                        <asp:TextBox ID="nrQtdProduto" runat="server" Text='<%#Eval("nrQtdProduto")%>' Width="40" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Preço Unitário">
                    <FooterTemplate>
                        Total:
                    </FooterTemplate>
                    <ItemTemplate>
                        <%# String.Format("{0:C}", ((SqlDecimal)Eval("vlPreco")).Value) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor Final">
                    <ItemTemplate>
                        <%# String.Format("{0:C}", ((SqlDecimal)Eval("vlFinal")).Value) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Button" CommandName="atualizar" Text="Atualizar" ControlStyle-CssClass="button" />
            </Columns>
            <EmptyDataTemplate>
                Não foi encontrado nenhum produto</EmptyDataTemplate>
        </asp:GridView>
        <br />
        <asp:Button ID="btnContinuarComprando" runat="server" Width="200px" Text="Continuar Comprando"
            CssClass="buttonAcao" OnClick="btnContinuarComprando_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFecharPedido" runat="server" Text="Fechar Pedido" CssClass="buttonAcao"
            OnClick="btnFecharPedido_Click" Width="200px" />
    </div>
    <br />
</asp:Content>
