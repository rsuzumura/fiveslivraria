<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Pedido.aspx.cs" Inherits="FivesLivraria.Pedido" %>

<%@ Register Assembly="FivesLivraria.Controls" Namespace="FivesLivraria.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <br />
        <div class="titleHeader">
            &nbsp;&nbsp;<asp:Literal ID="ltPedido" runat="server"></asp:Literal>
        </div>
        <div align="center">
            <table class="data" style="width: 90%;">
                <colgroup>
                    <col width="20%" />
                    <col width="80%" />
                </colgroup>
                <tr>
                    <td class="name">
                        Data do pedido:
                    </td>
                    <td style="text-align: left;">
                        <asp:Literal ID="ltDtPedido" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="name">
                        Data de entrega prevista:
                    </td>
                    <td style="text-align: left;">
                        <asp:Literal ID="ltDataPrevista" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="name">
                        Endereço de entrega:
                    </td>
                    <td style="text-align: left;">
                        <asp:Literal ID="ltEnderecoEntrega" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="name">
                        Endereço de cobrança:
                    </td>
                    <td style="text-align: left;">
                        <asp:Literal ID="ltEnderecoCobranca" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; font-weight: bold;">
                        Produtos
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <cc1:PagedGridView ID="gridProdutos" runat="server" AutoGenerateColumns="false" Width="98%"
                            ShowFooter="true" AllowPaging="true" PageSize="10" 
                            OnPageIndexChanging="gridPedidos_PageIndexChanging" 
                            onrowdatabound="gridProdutos_RowDataBound">
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
                                                    <%#Eval("nmTitulo")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantidade">
                                    <ItemTemplate>
                                        <%#Eval("qtdProduto")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Preço Unitário">
                                    <ItemTemplate>
                                        <%# Eval("vlPreco") %>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="true" />
                                    <FooterTemplate>
                                        Total:
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="vlFinal" HeaderText="Valor Final" FooterStyle-Font-Bold="true"/>
                            </Columns>
                        </cc1:PagedGridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
