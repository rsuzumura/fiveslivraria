<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true"
    CodeBehind="CadastroCliente.aspx.cs" Inherits="FivesLivraria.CadastroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:ValidationSummary ID="validationResume" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:" CssClass="validationMessage" />
    </div>
    <div class="divCadastro">
        <div class="header">
            &nbsp;&nbsp;Cadastro de Usuário
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
                    <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    Endereço:
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
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
                    <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ErrorMessage="O login é obrigatório."
                        ControlToValidate="txtLogin" Text="*"></asp:RequiredFieldValidator>
                    Login:
                </td>
                <td>
                    <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="A senha é obrigatória."
                        ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator>                    
                    Senha:
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="txtPassword"
                        ControlToCompare="txtConfirmPassword" ErrorMessage="A senha e a confirmação não correspondem."
                        Text="*"></asp:CompareValidator>
                    Confirmação<br />
                    de senha:
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ErrorMessage="A pergunta é obrigatória."
                        ControlToValidate="txtQuestion" Text="*"></asp:RequiredFieldValidator>
                    Pergunta:
                </td>
                <td>
                    <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="name">
                    <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ErrorMessage="A resposta é obrigatória."
                        ControlToValidate="txtAnswer" Text="*"></asp:RequiredFieldValidator>
                    Resposta:
                </td>
                <td>
                    <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Cadastrar" CssClass="button" OnClick="btnSave_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
