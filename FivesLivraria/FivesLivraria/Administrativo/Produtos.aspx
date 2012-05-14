<%@ Page Title="" Language="C#" MasterPageFile="~/Administrativo/Admin.Master" AutoEventWireup="true"
    CodeBehind="Produtos.aspx.cs" Inherits="FivesLivraria.Administrativo.Produtos" %>

<%@ Import Namespace="System.Data.SqlTypes" %>
<%@ Register Assembly="FivesLivraria.Controls" Namespace="FivesLivraria.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.msgBox.js"></script>
    <link rel="Stylesheet" type="text/css" href="../Styles/msgBoxLight.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="toolbar">
        <asp:Button ID="btnNew" runat="server" Text="Novo" ToolTip="Criar um novo usuário"
            OnClick="btnNew_Click" />
    </div>
    <br />
    <div class="divFilter">
        <div class="divFormHeader">
            &nbsp;&nbsp;Filtro
        </div>
        <div>
            <table style="width: 98%;" class="data">
                <colgroup>
                    <col width="5%" />
                    <col width="25%" />
                    <col width="70%" />
                </colgroup>
                <tr>
                    <td class="name">Categoria:</td>
                    <td style="text-align: left;">
                        &nbsp;<asp:DropDownList ID="dropCategorias" runat="server" Width="90%"
                            DataTextField="dsCategoria" DataValueField="idCategoria">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="90%"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="imgSearch" runat="server" ToolTip="Efetuar consulta" ImageUrl="~/Images/icon_search_16px.gif"
                            OnClick="imgSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div class="divForm">
        <div class="divFormHeader">
            &nbsp;&nbsp;Produtos
        </div>
        <div>
            <div style="padding-top: 7px;">
                <div style="text-align: left; font-weight: bold;">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecords" runat="server"></asp:Label></div>
                <cc1:PagedGridView ID="gridProdutos" runat="server" AutoGenerateColumns="false" Width="98%"
                    AllowPaging="true" OnRowDeleting="gridProdutos_RowDeleting" PageSize="10" OnPageIndexChanging="gridProdutos_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Nome do Produto</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("nmTitulo") %>
                                <asp:HiddenField ID="hdnIdProduto" runat="server" Value='<%# Eval("idProduto") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Autores</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("dsAutores") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Editora</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("nmEditora") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Ano
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("nrAno") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Qtde
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("qtdProduto") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgEdit" CommandArgument='<%# Eval("idProduto") %>' runat="server" OnClick="imgEdit_Click"
                                    ImageUrl="~/Images/Pen.gif" ToolTip="Editar" CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDelete" CausesValidation="false" CommandName="Delete" CommandArgument='<%# Eval("idProduto") %>'
                                    runat="server" ImageUrl="~/Images/icon_recyclebin_16px.gif" ToolTip="Apagar"
                                    OnClientClick="javascript:return confirm(this, 'Remoção de Produto', 'Deseja realmente remover o produto selecionado?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:PagedGridView>
            </div>
        </div>
    </div>
</asp:Content>
