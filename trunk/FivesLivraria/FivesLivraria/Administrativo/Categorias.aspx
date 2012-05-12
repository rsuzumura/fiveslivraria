<%@ Page Title="" Language="C#" MasterPageFile="~/Administrativo/Admin.Master" AutoEventWireup="true"
    CodeBehind="Categorias.aspx.cs" Inherits="FivesLivraria.Administrativo.Categorias" %>

<%@ Import Namespace="System.Data.SqlTypes" %>
<%@ Register Assembly="FivesLivraria.Controls" Namespace="FivesLivraria.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="toolbar">
        <asp:Button ID="btnNew" runat="server" Text="Novo" ToolTip="Criar uma nova categoria"
            OnClick="btnNew_Click" />
        <asp:Button ID="btnSave" runat="server" Text="Salvar" ToolTip="Salva a categoria"
            OnClick="btnSave_Click" Visible="false" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" ToolTip="Cancela a inclusão de categoria"
            OnClick="btnCancel_Click" Visible="false" />
    </div>
    <br />
    <div class="divFilter" id="divFilter" runat="server">
        <div class="divFormHeader">
            &nbsp;&nbsp;Filtro
        </div>
        <div>
            <table style="width: 98%;">
                <tr>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="90%"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="imgSearch" runat="server" ToolTip="Efetuar consulta" ImageUrl="~/Images/icon_search_16px.gif"
                            OnClick="imgSearch_Click" Width="16px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:ValidationSummary ID="validationResume" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:"
        CssClass="validationMessage" />
    <div class="divFilter" id="divInsert" runat="server" visible="false">
        <div class="divFormHeader">
            &nbsp;&nbsp;Inclusão de Categoria
        </div>
        <div>
            <table class="data" style="width: 98%;">
                <colgroup>
                    <col width="10%" />
                    <col width="80%" />
                    <col width="10%" />
                </colgroup>
                <tr>
                    <td class="name">
                        Descrição:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCategoria" runat="server" Width="90%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:CheckBox ID="ckbAtivo" runat="server" Text="Ativo" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <br />
    <div class="divForm">
        <div class="divFormHeader">
            &nbsp;&nbsp;Categorias de Produto
        </div>
        <div>
            <div style="padding-top: 7px;">
                <div>
                    <div style="text-align: left; font-weight: bold;">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecords" runat="server"></asp:Label></div>
                    <cc1:PagedGridView ID="gridCategorias" runat="server" AutoGenerateColumns="false"
                        Width="98%" AllowPaging="true" OnRowCancelingEdit="gridCategorias_RowCancelingEdit"
                        OnRowDeleting="gridCategorias_RowDeleting" OnRowEditing="gridCategorias_RowEditing"
                        OnRowUpdating="gridCategorias_RowUpdating" PageSize="10" OnPageIndexChanging="gridCategorias_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Descrição da Categoria</HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("dsCategoria") %>
                                    <asp:HiddenField ID="hdnIdCategoria" runat="server" Value='<%# Eval("idCategoria") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescricao" runat="server" Width="90%" Text='<%# Eval("dsCategoria") %>'></asp:TextBox>
                                    <asp:HiddenField ID="hdnIdCategoria" runat="server" Value='<%# Eval("idCategoria") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    Status</HeaderTemplate>
                                <ItemTemplate>
                                    <%# ((SqlBoolean)Eval("stCategoria")).Value ? "Ativo" : "Inativo" %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="ckbStatus" runat="server" Checked='<%# ((SqlBoolean)Eval("stCategoria")).Value %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Pen.gif"
                                        ToolTip="Editar" CausesValidation="false" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgSave" CommandName="Update" runat="server" ImageUrl="~/Images/Save.gif"
                                        ToolTip="Salvar" CausesValidation="false" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgDelete" CausesValidation="false" CommandName="Delete" runat="server"
                                        ImageUrl="~/Images/icon_recyclebin_16px.gif" ToolTip="Apagar" OnClientClick="javascript:return confirm(this);" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="imgCancel" CommandName="Cancel" CausesValidation="false" runat="server"
                                        ImageUrl="~/Images/subjectselect_delete.gif" ToolTip="Cancelar" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:PagedGridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
