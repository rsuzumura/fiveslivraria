<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true"
    CodeBehind="LostPassword.aspx.cs" Inherits="FivesLivraria.LostPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divCadastro">
        <div class="header">
            &nbsp;&nbsp;Recuperação de Senha
        </div>
        <asp:ValidationSummary ID="validationResume" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:" />
        <asp:Panel ID="pnlMessage" runat="server" Width="100%" Visible="false">
            <br />
            <asp:Literal ID="ltMessage" runat="server" Text="O usuário informado não foi encontrado na nossa base de dados."></asp:Literal>
            <br />
            <br />
            <asp:Button ID="btnBack" runat="server" Text="Voltar" OnClick="btnBack_Click" ToolTip="Voltar para a página de autenticação." />
        </asp:Panel>
        <asp:Panel ID="pnlData" runat="server" Width="100%">
            <table class="data">
                <colgroup>
                    <col width="15%" />
                    <col width="85%" />
                </colgroup>
                <tr>
                    <td class="name">
                        Pergunta:
                    </td>
                    <td style="text-align: left; ">
                        &nbsp;&nbsp;&nbsp;<asp:Literal ID="ltQuestion" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="name">
                        <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ErrorMessage="A resposta é obrigatória."
                            ControlToValidate="txtAnswer" Text="*"></asp:RequiredFieldValidator>
                        Resposta:
                    </td>
                    <td style="text-align: left; ">
                        &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <br />
                        <asp:Button ID="btnRequestPassword" runat="server" Text="Solicitar&#10;Senha" CssClass="button"
                            CausesValidation="false" Width="130px" Height="50px" OnClick="btnRequestPassword_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
