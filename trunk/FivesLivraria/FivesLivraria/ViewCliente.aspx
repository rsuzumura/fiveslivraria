<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ViewCliente.aspx.cs" Inherits="FivesLivraria.ViewCliente" %>
<%@ Import Namespace="System.Data.SqlTypes" %>
<%@ Register Assembly="FivesLivraria.Controls" Namespace="FivesLivraria.Controls"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="toolbar">
        <asp:Button ID="btnSave" runat="server" Text="Alterar Cadastro" CssClass="button"
            CausesValidation="false" Width="150px" OnClick="btnSave_Click" />
            &nbsp;&nbsp;
        <asp:Button ID="btnChangePassword" runat="server" Text="Alterar Senha" CssClass="button"
            CausesValidation="false" Width="150px" OnClick="btnChangePassword_Click" />
    </div>
    <br />
    <div class="main">
        <div class="titleHeader">
            &nbsp;&nbsp;Cadastro do Cliente
        </div>
        <div align="center">
            <table class="data" style="width: 90%;">
                <colgroup>
                    <col width="20%" />
                    <col width="30%" />
                    <col width="20%" />
                    <col width="30%" />
                </colgroup>
                <tr>
                    <td class="name">Nome do cliente:</td>
                    <td style="text-align: left;"><asp:Literal ID="ltNome" runat="server"></asp:Literal></td>
                    <td class="name"><asp:Literal ID="ltLabelTaxId" runat="server">CPF</asp:Literal></td>
                    <td style="text-align: left;"><asp:Literal ID="ltTaxId" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center; font-weight: bold;">Pedidos</td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <cc1:PagedGridView ID="gridPedidos" runat="server" AutoGenerateColumns="false" Width="98%"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="gridPedidos_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="idPedido" HeaderText="Nº do Pedido" />
                                <asp:TemplateField>
                                    <HeaderTemplate>Data do Pedido</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# ((SqlDateTime)Eval("dtPedido")).Value.ToShortDateString() %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlkDetalhes" runat="server" Text="Detalhes" NavigateUrl='<%# "~/Pedido.aspx?idpedido=" + Eval("idPedido") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </cc1:PagedGridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
