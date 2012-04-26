<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true" CodeBehind="CadastroCliente.aspx.cs" Inherits="FivesLivraria.CadastroCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                Nome:
                <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="O nome é obrigatório."
                    ControlToValidate="txtUser" Text="*"></asp:RequiredFieldValidator>
            </td>
            <td><asp:TextBox ID="txtUser" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Email:</td>
            <td><asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Login:</td>
            <td><asp:TextBox ID="txtLogin" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                Senha:<asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="txtPassword"
                    ControlToCompare="txtConfirmPassword" ErrorMessage="A senha e a confirmação não correspondem."
                    Text="*"></asp:CompareValidator>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Confirmação de senha:</td>
            <td><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Pergunta:</td>
            <td><asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Resposta:</td>
            <td><asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" Text="Cadastrar" CssClass="button" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
