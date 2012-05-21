<%@ Page Title="" Language="C#" MasterPageFile="~/Administrativo/Admin.Master" AutoEventWireup="true"
    CodeBehind="CadastroUsuario.aspx.cs" Inherits="FivesLivraria.Administrativo.CadastroUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="toolbar">
        <asp:Button ID="btnSave" runat="server" Text="Cadastrar" CssClass="button" OnClick="btnSave_Click"
            TabIndex="9" CausesValidation="false" />
        <asp:Button ID="btnCancel" runat="server" Text="Voltar" CausesValidation="false"
            ToolTip="Criar um novo usuário" OnClick="btnCancel_Click" TabIndex="10" />
    </div>
    <br />
    <asp:ValidationSummary ID="validationResume" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:"
        CssClass="validationMessage" />
    <div class="divForm" style="height: 300px;">
        <div class="divFormHeader">
            &nbsp;&nbsp;Cadastro de Usuários
        </div>
        <table class="data">
            <colgroup>
                <col width="15%" />
                <col width="85%" />
            </colgroup>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="O nome é obrigatório."
                        ControlToValidate="txtUser" Text="*"></asp:RequiredFieldValidator>
                    Nome:
                </td>
                <td>
                    <asp:TextBox ID="txtUser" runat="server" Width="90%" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Endereço:
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Width="90%" TabIndex="2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="O email é obrigatório."
                        ControlToValidate="txtEmailAddress" Text="*"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="O formato do email está inválido"
                        ControlToValidate="txtEmailAddress" Text="*" ValidationExpression="[\w-]+(\.[\w-]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    Email:
                </td>
                <td>
                    <asp:TextBox ID="txtEmailAddress" runat="server" Width="90%" TabIndex="3"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ErrorMessage="O login é obrigatório."
                        ControlToValidate="txtLogin" Text="*"></asp:RequiredFieldValidator>
                    Login:
                </td>
                <td>
                    <asp:TextBox ID="txtLogin" runat="server" Width="90%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="A senha é obrigatória."
                        ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator>
                    Senha:
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="90%" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="txtPassword"
                        ControlToCompare="txtConfirmPassword" ErrorMessage="A senha e a confirmação não correspondem."
                        Text="*"></asp:CompareValidator>
                    Confirmação de senha:
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="90%"
                        TabIndex="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ErrorMessage="A pergunta é obrigatória."
                        ControlToValidate="txtQuestion" Text="*"></asp:RequiredFieldValidator>
                    Pergunta:
                </td>
                <td>
                    <asp:TextBox ID="txtQuestion" runat="server" Width="90%" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ErrorMessage="A resposta é obrigatória."
                        ControlToValidate="txtAnswer" Text="*"></asp:RequiredFieldValidator>
                    Resposta:
                </td>
                <td>
                    <asp:TextBox ID="txtAnswer" runat="server" Width="90%" TabIndex="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Tipo de permissão:
                </td>
                <td>
                    <asp:DropDownList ID="dropRoles" runat="server" Width="90%" TabIndex="8">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; height: 240px; clear: both;">
    </div>
</asp:Content>
