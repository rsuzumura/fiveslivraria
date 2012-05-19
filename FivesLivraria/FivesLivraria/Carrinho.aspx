<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Carrinho.aspx.cs" Inherits="FivesLivraria.Carrinho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 30px">
        <asp:GridView ID="gvCarrinho" runat="server" AutoGenerateColumns="False" DataKeyNames="idCarrinho"
            OnRowCommand="gvCarrinho_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Descrição">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="imagem" runat="server" Text="Imagem" src='<%#"/Images/"+ Eval("nmImagem") %>'
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
                <asp:BoundField DataField="vlPreco" HeaderText="Preço Unitário" SortExpression="idProduto" />
                <asp:BoundField DataField="vlFinal" HeaderText="Valor Final" SortExpression="nmTitulo" />
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTotal" runat="server" Text="Total: " />
        <asp:Label ID="txtTotal" runat="server" />
    </div>
    <br />
</asp:Content>
