<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FivesLivraria.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Login ID="Login1" runat="server">
        <LayoutTemplate>
            <div align="center" class="divLogin">
                <div class="header">
                    &nbsp;&nbsp;Login
                </div>
                <br />
                <div>
                    <table cellspacing="2" cellpadding="2" class="data">
                        <tr>
                            <td class="name">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Usuário:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server" CssClass="input" TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                    ControlToValidate="UserName" ErrorMessage="O nome de usuário é obrigatório." 
                                    ToolTip="Nome de usuário obrigatório." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" CssClass="input" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                    ControlToValidate="Password" ErrorMessage="A senha é obrigatória." 
                                    ToolTip="Senha obrigatória." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:LinkButton ID="LostPassword" runat="server" ToolTip="Recuperação de senha esquecida" TabIndex="4"
                                    Text="Esqueci minha senha" OnClick="LostPassword_Click"></asp:LinkButton>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <br />
                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Entrar" TabIndex="3"
                                    ValidationGroup="Login1" CssClass="button" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table class="data" cellpadding="2" cellspacing="2">
                        <tr>
                            <td class="label">&nbsp;&nbsp;&nbsp;Ainda não sou cliente</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnClientRegister" runat="server" Text="Cadastrar" 
                                    CssClass="button" onclick="btnClientRegister_Click" TabIndex="5" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </LayoutTemplate>
        <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
    </asp:Login>
</asp:Content>
