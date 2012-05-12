<%@ Page Title="" Language="C#" MasterPageFile="~/Administrativo/Admin.Master" AutoEventWireup="true"
    CodeBehind="Usuarios.aspx.cs" Inherits="FivesLivraria.Administrativo.Usuarios" %>

<%@ Register Assembly="FivesLivraria.Controls" Namespace="FivesLivraria.Controls"
    TagPrefix="cc1" %>

<%@ Import Namespace="System.Data.SqlTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
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
            <table style="width: 98%;">
                <colgroup>
                    <col width="20%" />
                    <col width="80%" />
                </colgroup>
                <tr>
                    <td><asp:DropDownList ID="dropRoles" runat="server" Width="90%"></asp:DropDownList></td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="90%"></asp:TextBox>
                        &nbsp;
                        <asp:ImageButton ID="imgSearch" runat="server" ToolTip="Efetuar consulta"
                            ImageUrl="~/Images/icon_search_16px.gif" onclick="imgSearch_Click" />
                    </td>
                </tr>
            </table>            
        </div>
    </div>
    <br />
    <div class="divForm">
        <div class="divFormHeader">
            &nbsp;&nbsp;Usuários
        </div>
        <div>
            <div style="padding-top: 7px;">
                <div>
                    <asp:ValidationSummary ID="validationResume" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:"
                        CssClass="validationMessage" />
                </div>
                <div style="text-align: left; font-weight: bold;">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRecords" runat="server"></asp:Label></div>
                <cc1:PagedGridView ID="gridUsuarios" runat="server" AutoGenerateColumns="false" Width="98%"
                    AllowPaging="true" OnRowCancelingEdit="gridUsuarios_RowCancelingEdit" OnRowDeleting="gridUsuarios_RowDeleting"
                    OnRowEditing="gridUsuarios_RowEditing" OnRowUpdating="gridUsuarios_RowUpdating"
                    PageSize="10" OnPageIndexChanging="gridUsuarios_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>Nome do Usuário</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("nmUsuario") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("nmUsuario") %>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Login</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("dsLogin") %>
                                <asp:HiddenField ID="hdnLogin" runat="server" Value='<%# Eval("dsLogin") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <%# Eval("dsLogin")%>
                                <asp:HiddenField ID="hdnLogin" runat="server" Value='<%# Eval("dsLogin") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>Email</HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("Email") %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEmail" runat="server" Text='<%# Eval("Email") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="O email é obrigatório."
                                    ControlToValidate="txtEmail" Text="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="O formato do email está inválido"
                                    ControlToValidate="txtEmail" Text="*" ValidationExpression="[\w-]+(\.[\w-]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Permissão
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# Eval("RoleName") %>
                                <asp:HiddenField ID="hdnRole" runat="server" Value='<%# Eval("RoleName") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="hdnRole" runat="server" Value='<%# Eval("RoleName") %>' />
                                <asp:DropDownList ID="dropRoles" runat="server"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                Bloqueado
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%# ((SqlBoolean)Eval("IsLocked")).Value ? "Sim" : "Não" %>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:CheckBox ID="ckbLocked" runat="server" Checked='<%# ((SqlBoolean)Eval("IsLocked")).Value %>' />
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
                                <asp:ImageButton ID="imgDelete" CausesValidation="false" CommandName="Delete" runat="server" ImageUrl="~/Images/icon_recyclebin_16px.gif"
                                    ToolTip="Apagar" OnClientClick="javascript:return confirm(this);" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="imgCancel" CommandName="Cancel" CausesValidation="false" runat="server" ImageUrl="~/Images/subjectselect_delete.gif"
                                    ToolTip="Cancelar" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:PagedGridView>
            </div>
        </div>
    </div>
</asp:Content>
