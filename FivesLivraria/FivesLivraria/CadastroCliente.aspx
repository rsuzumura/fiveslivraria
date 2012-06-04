<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.Master" AutoEventWireup="true"
    CodeBehind="CadastroCliente.aspx.cs" Inherits="FivesLivraria.CadastroCliente" %>

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
            <br />
            <eo:TabStrip ID="TabStripCadastro" runat="server" ControlSkinID="None" Width="98%"
                MultiPageID="tabs">
                <LookItems>
                    <eo:TabItem Height="21" HoverStyle-CssText="position: relative; top: 2px; background-image: url(00010502); background-repeat: repeat-x"
                        ItemID="_Default" LeftIcon-HoverUrl="00010506" LeftIcon-SelectedUrl="00010508"
                        LeftIcon-Url="00010504" NormalStyle-CssText="position: relative; top: 2px; background-image: url(00010501); background-repeat: repeat-x"
                        RightIcon-HoverUrl="00010507" RightIcon-SelectedUrl="00010509" RightIcon-Url="00010505"
                        SelectedStyle-CssText="background-image: url(00010503); background-repeat: repeat-x"
                        Text-Padding-Bottom="2" Text-Padding-Top="1">
                        <SubGroup ItemSpacing="1" Style-CssText="background-image:url(00010510);background-position-y:bottom;background-repeat:repeat-x;color:black;cursor:hand;font-family:'Microsoft Sans Serif',Verdana;font-size:8.25pt;">
                        </SubGroup>
                    </eo:TabItem>
                </LookItems>
                <TopGroup>
                    <Items>
                        <eo:TabItem Text-Html="Dados Cadastrais" PageViewID="cadastro">
                        </eo:TabItem>
                        <eo:TabItem Text-Html="Endereços de Entrega" PageViewID="endereco">
                        </eo:TabItem>
                        <eo:TabItem Text-Html="Dados de Acesso" PageViewID="acesso">
                        </eo:TabItem>
                    </Items>
                </TopGroup>
            </eo:TabStrip>
            <eo:MultiPage ID="tabs" runat="server" Width="100%">
                <eo:PageView runat="server" ID="cadastro" Width="100%">
                    <table class="data">
                        <colgroup>
                            <col width="20%" />
                            <col width="80%" />
                        </colgroup>
                        <tr>
                            <td class="name">
                                Tipo de Cliente:
                            </td>
                            <td style="text-align: left;">
                                <asp:RadioButtonList ID="rdoTipoCliente" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rdoTipoCliente_SelectedIndexChanged"
                                    Font-Names="Verdana,Tahoma" Font-Size="10pt">
                                    <asp:ListItem Text="Pessoa Física" Value="false" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pessoa Jurídica" Value="true"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlCustomer" runat="server" Width="100%">
                                    <table class="data">
                                        <colgroup>
                                            <col width="22%" />
                                            <col width="78%" />
                                        </colgroup>
                                        <tr>
                                            <td class="name">
                                                <asp:RequiredFieldValidator ID="rfvUser" runat="server" ErrorMessage="O nome é obrigatório."
                                                    ControlToValidate="txtUser" Text="*" ValidationGroup="cadastro"></asp:RequiredFieldValidator>
                                                Nome:&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                <asp:RequiredFieldValidator ID="rfvCPF" runat="server" Text="*" ErrorMessage="O CPF é obrigatório."
                                                    ValidationGroup="cadastro" ControlToValidate="txtCPF"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rexCPF" runat="server" Text="*" ControlToValidate="txtCPF"
                                                    ValidationExpression="^\d{2,3}.\d{3}.\d{3}-\d{2}$" ValidationGroup="cadastro"
                                                    ErrorMessage="O formato do CPF está inválido.Utilize o formato {999.999.999-99}"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="cvCPF" runat="server" Text="*" ErrorMessage="CPF inválido, dígito verificador não confere."
                                                    ValidationGroup="cadastro" OnServerValidate="cvCPF_ServerValidate"></asp:CustomValidator>
                                                CPF:&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <script type="text/javascript" src="Scripts/keyScripts.js" ></script>
                                                <asp:TextBox ID="txtCPF" runat="server" onkeypress="return validateCPF(this, event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                RG:&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtRG" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                Data de Nascimento:&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <eo:DatePicker ID="datePickerNascimento" runat="server" Width="150px" ControlSkinID="None"
                                                    DayCellHeight="16" DayCellWidth="19" DayHeaderFormat="FirstLetter" DisabledDates=""
                                                    OtherMonthDayVisible="True" SelectedDates="" TitleLeftArrowImageUrl="DefaultSubMenuIconRTL"
                                                    TitleRightArrowImageUrl="DefaultSubMenuIcon">
                                                    <PickerStyle CssText="font-family:Courier New; padding-left:5px; padding-right: 5px;" />
                                                    <CalendarStyle CssText="background-color: white; border-right: #7f9db9 1px solid; padding-right: 4px; border-top: #7f9db9 1px solid; padding-left: 4px; font-size: 9px; padding-bottom: 4px; border-left: #7f9db9 1px solid; padding-top: 4px; border-bottom: #7f9db9 1px solid; font-family: tahoma" />
                                                    <TitleStyle CssText="background-color:#9ebef5;font-family:Tahoma;font-size:12px;padding-bottom:2px;padding-left:6px;padding-right:6px;padding-top:2px;" />
                                                    <TitleArrowStyle CssText="cursor:hand" />
                                                    <MonthStyle CssText="font-family: tahoma; font-size: 12px; margin-left: 14px; cursor: hand; margin-right: 14px" />
                                                    <DayHeaderStyle CssText="font-family: tahoma; font-size: 12px; border-bottom: #aca899 1px solid" />
                                                    <DayStyle CssText="font-family: tahoma; font-size: 12px; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <DayHoverStyle CssText="font-family: tahoma; font-size: 12px; border-right: #fbe694 1px solid; border-top: #fbe694 1px solid; border-left: #fbe694 1px solid; border-bottom: #fbe694 1px solid" />
                                                    <TodayStyle CssText="font-family: tahoma; font-size: 12px; border-right: #bb5503 1px solid; border-top: #bb5503 1px solid; border-left: #bb5503 1px solid; border-bottom: #bb5503 1px solid" />
                                                    <SelectedDayStyle CssText="font-family: tahoma; font-size: 12px; background-color: #fbe694; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <DisabledDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                    <OtherMonthDayStyle CssText="font-family: tahoma; font-size: 12px; color: gray; border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid" />
                                                </eo:DatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                Nome da Mãe:&nbsp;&nbsp;
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtNmMae" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlCompany" runat="server" Width="100%">
                                    <table class="data">
                                        <colgroup>
                                            <col width="20%" />
                                            <col width="80%" />
                                        </colgroup>
                                        <tr>
                                            <td class="name">
                                                <asp:RequiredFieldValidator ID="rfvRazaoSocial" runat="server" ControlToValidate="txtRazaoSocial"
                                                    ValidationGroup="cadastro" Text="*" ErrorMessage="A Razão Social é obrigatória."></asp:RequiredFieldValidator>
                                                Razão Social:
                                            </td>
                                            <td style="text-align: left;">
                                                &nbsp;&nbsp;<asp:TextBox ID="txtRazaoSocial" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                <asp:RequiredFieldValidator ID="rfvCNPJ" runat="server" ControlToValidate="txtCNPJ"
                                                    ValidationGroup="cadastro" Text="*" ErrorMessage="O CNPJ é obrigatório."></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rexCNPJ" runat="server" ControlToValidate="txtCNPJ"
                                                    ValidationGroup="cadastro" Text="*" ValidationExpression="^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$"
                                                    ErrorMessage="O formato do CNPJ está inválido. Utilize o formato {99.999.999/9999-99}"></asp:RegularExpressionValidator>
                                                <asp:CustomValidator ID="cvCNPJ" runat="server" Text="*" ValidationGroup="cadastro"
                                                    ErrorMessage="CJPJ inválido, dígito verificador não confere." OnServerValidate="cvCNPJ_ServerValidate"></asp:CustomValidator>
                                                CNPJ:
                                            </td>
                                            <td style="text-align: left;">
                                                <script type="text/javascript" src="Scripts/keyScripts.js" ></script>
                                                &nbsp;&nbsp;<asp:TextBox ID="txtCNPJ" runat="server" onkeypress="return validateCNPJ(this, event);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                Inscrição Municipal:
                                            </td>
                                            <td style="text-align: left;">
                                                &nbsp;&nbsp;<asp:TextBox ID="txtInscricaoMunicipal" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">
                                                Inscrição Estadual:
                                            </td>
                                            <td style="text-align: left;">
                                                &nbsp;&nbsp;<asp:TextBox ID="txtInscricaoEstadual" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </eo:PageView>
                <eo:PageView ID="endereco" runat="server" Width="100%">
                    <asp:UpdatePanel ID="UpdateEndereco" runat="server">
                        <ContentTemplate>
                            <table class="data">
                                <colgroup>
                                    <col width="15%" />
                                    <col width="25%" />
                                    <col width="10%" />
                                    <col width="15%" />
                                    <col width="10%" />
                                    <col width="17%" />
                                    <col width="9%" />
                                </colgroup>
                                <tr>
                                    <td class="name">
                                        <asp:RequiredFieldValidator ID="rfvEnderecoCliente" runat="server" Text="*" ErrorMessage="O endereço é obrigatório"
                                            ControlToValidate="txtEnderecoCliente" ValidationGroup="Endereco"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvEndereco" runat="server" Text="*" ErrorMessage="É necessário cadastrar pelo menos um endereço de entrega."
                                            ValidationGroup="cadastro" OnServerValidate="cvEndereco_ServerValidate"></asp:CustomValidator>
                                        Endereço:
                                    </td>
                                    <td colspan="4" style="text-align: left;">
                                        <asp:TextBox ID="txtEnderecoCliente" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="name">
                                        Nº:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtNumero" runat="server" Width="74%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="name">
                                        Complemento:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtComplemento" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="name">
                                        CEP:
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtCEP" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td class="name">
                                        Bairro:
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <asp:TextBox ID="txtBairro" runat="server" Width="92%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="name">
                                        Município:
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <asp:DropDownList ID="dropMunicipio" runat="server" DataTextField="nmMunicipio" DataValueField="idMunicipio"
                                            Width="100%">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="name">
                                        Estado:
                                    </td>
                                    <td colspan="3" style="text-align: left;">
                                        <asp:DropDownList ID="dropEstado" runat="server" DataTextField="nmEstado" DataValueField="idEstado"
                                            Width="96%" AutoPostBack="true" OnSelectedIndexChanged="dropEstado_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr align="center">
                                    <td colspan="7">
                                        <asp:Button ID="btnAddEndereco" runat="server" Text="Adicionar Endereço" Width="200px"
                                            OnClick="btnAddEndereco_Click" ValidationGroup="Endereco" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                            <div style="width: 98%; height: 100px; overflow-y: scroll;">
                                <asp:GridView ID="gridEnderecos" runat="server" Width="99%" AutoGenerateColumns="false"
                                    OnRowDeleting="gridEnderecos_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="dsEndereco" HeaderText="Endereço" />
                                        <asp:BoundField DataField="nrEndereco" HeaderText="Nº" />
                                        <asp:BoundField DataField="dsBairro" HeaderText="Bairro" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/icon_recyclebin_16px.gif"
                                                    CausesValidation="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </eo:PageView>
                <eo:PageView ID="acesso" runat="server" Width="98%">
                    <table class="data">
                        <colgroup>
                            <col width="15%" />
                            <col width="85%" />
                        </colgroup>
                        <tr>
                            <td class="name">
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="O email é obrigatório."
                                    ControlToValidate="txtEmailAddress" Text="*" ValidationGroup="cadastro"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="O formato do email está inválido"
                                    ValidationGroup="cadastro" ControlToValidate="txtEmailAddress" Text="*" ValidationExpression="[\w-]+(\.[\w-]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                Email:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">
                                <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ErrorMessage="O login é obrigatório."
                                    ValidationGroup="cadastro" ControlToValidate="txtLogin" Text="*"></asp:RequiredFieldValidator>
                                Login:
                            </td>
                            <td>
                                <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="A senha é obrigatória."
                                    ValidationGroup="cadastro" ControlToValidate="txtPassword" Text="*"></asp:RequiredFieldValidator>
                                Senha:
                            </td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="name">
                                <asp:CompareValidator ID="cmpPassword" runat="server" ControlToValidate="txtPassword"
                                    ValidationGroup="cadastro" ControlToCompare="txtConfirmPassword" ErrorMessage="A senha e a confirmação não correspondem."
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
                                    ValidationGroup="cadastro" ControlToValidate="txtQuestion" Text="*"></asp:RequiredFieldValidator>
                                Pergunta:
                            </td>
                            <td>
                                <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
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
                </eo:PageView>
            </eo:MultiPage>
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:UpdatePanel ID="updateSubmit" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSave" runat="server" Text="Cadastrar" CssClass="button" OnClick="btnSave_Click"
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
