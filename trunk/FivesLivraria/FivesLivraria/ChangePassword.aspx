<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="FivesLivraria.ChangePassword" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <div>
        <asp:UpdatePanel ID="updatePanelValidate" runat="server">
            <ContentTemplate>
                <asp:ValidationSummary ID="validationResume" runat="server" DisplayMode="List" HeaderText="Atenção: verifique os seguintes itens:"
                    CssClass="validationMessage" ValidationGroup="cadastro" />
                <asp:ValidationSummary ID="ValidationSummaryEndereco" runat="server" DisplayMode="List"
                    HeaderText="Atenção: verifique os seguintes itens:" ValidationGroup="Endereco"
                    CssClass="validationMessage" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="divCadastro">
        <div class="header">
            &nbsp;&nbsp;Cadastro do Cliente
        </div>
        <div>
            <table class="data">
                <colgroup>
                    <col width="15%" />
                    <col width="85%" />
                </colgroup>
                <tr id="trPassword" runat="server">
                    <td class="name">
                        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="A senha atual é obrigatória."
                            ValidationGroup="cadastro" ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator>
                        Senha Antiga:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trConfirmPassword" runat="server">
                    <td class="name">
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="A nova senha é obrigatória."
                            ValidationGroup="cadastro" ControlToValidate="txtNewPassword" Text="*"></asp:RequiredFieldValidator>
                        Nova senha:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trQuestion" runat="server">
                    <td class="name">
                        <asp:RequiredFieldValidator ID="rfvQuestion" runat="server" ErrorMessage="A pergunta é obrigatória."
                            ValidationGroup="cadastro" ControlToValidate="txtQuestion" Text="*"></asp:RequiredFieldValidator>
                        Pergunta:
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trAnswer" runat="server">
                    <td class="name">
                        <asp:RequiredFieldValidator ID="rfvAnswer" runat="server" ErrorMessage="A resposta é obrigatória."
                            ValidationGroup="cadastro" ControlToValidate="txtAnswer" Text="*"></asp:RequiredFieldValidator>
                        Resposta:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAnswer" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:UpdatePanel ID="updateSubmit" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="Cadastrar" CssClass="button" OnClick="btnSave_Click"
                                    CausesValidation="false" />&nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Voltar" CssClass="button" OnClick="btnBack_Click"
                                    CausesValidation="false" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="ModalUpdateProgress">
        <asp:UpdateProgress ID="up_progress" DisplayAfter="1" runat="server">
            <ProgressTemplate>
                <div id="updateDiv" class="loading">
                    <img alt="" src="Images/ajax-loader.gif" />
                    <br />
                    Carregando...
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="modalProgress" runat="server" TargetControlID="panelUpdateProgress"
        BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" RepositionMode="RepositionOnWindowResizeAndScroll">
    </ajaxToolkit:ModalPopupExtender>
    <script type="text/javascript">
        var ModalProgress = '<%= modalProgress.ClientID %>';
        var panelUpdateProgress = '<%= panelUpdateProgress.ClientID %>';
    </script>
    <script type="text/javascript" src="Scripts/jsUpdateProgress.js"></script>
</asp:Content>
