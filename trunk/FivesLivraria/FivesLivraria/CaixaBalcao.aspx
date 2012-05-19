<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CaixaBalcao.aspx.cs" Inherits="FivesLivraria.CaixaBalcao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div>
        <div class="formHeader">
            Atendimento - Balcão
        </div>
        <div>
            <table class="data">
                <colgroup>
                    <col width="10%" />
                    <col width="60%" />
                    <col width="10%" />
                    <col width="20%" />
                </colgroup>
                <tr>
                    <td class="name">
                        Cliente:
                    </td>
                    <td>
                        <asp:TextBox ID="box_NomeCliente" runat="server" ReadOnly="true" Width="90%"></asp:TextBox>
                    </td>
                    <td class="name">
                        CPF/CNPJ:
                    </td>
                    <td>
                        <asp:TextBox ID="box_CPFCliente" runat="server" Width="80%"> </asp:TextBox>
                        <asp:ImageButton ID="btnSearchCustomer" runat="server" ToolTip="Buscar Cliente" ImageUrl="~/Images/icon_search_16px.gif" />
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div>
            <asp:Button ID="btnAddProduto" runat="server" Text="Adicionar Produto" Width="200px" />
        </div>
        <br />
        <div style="height: 200px; overflow-y: scroll;">
            Produtos
            <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" Style="width: 182px;
                margin-top: 0px;" DataKeyNames="idTable" OnRowCommand="gvTable_RowCommand" EmptyDataText="Nenhum produto foi selecionado.">
                <Columns>
                    <asp:TemplateField HeaderText="Descrição">
                        <ItemTemplate>
                            <asp:Image ID="imagem" runat="server" Text="Imagem" src='<%#"/Images/"+ Eval("nmImagem") %>'
                                Width="170" />
                            <asp:Label ID="nmTitulo" runat="server" Text='<%#Eval("nmTitulo")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Button" CommandName="excluir" Text="Excluir" />
                    <asp:TemplateField HeaderText="Quantidade">
                        <ItemTemplate>
                            <asp:TextBox ID="nrQtdProduto" runat="server" Text='<%#Eval("nrQtdProduto")%>' Width="40" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:ButtonField ButtonType="Button" CommandName="atualizar" Text="Atualizar" />
                    <asp:BoundField DataField="vlPreco" HeaderText="Preço Unitário" SortExpression="idProduto" />
                    <asp:BoundField DataField="vlFinal" HeaderText="Valor Final" SortExpression="nmTitulo" />
                </Columns>
            </asp:GridView>
        </div>        
    </div>
    <div id="Pedido">
        <div align="left">
            <table class="data">
                <colgroup>
                    <col width="10%" />
                    <col width="20%" />
                    <col width="70%" />
                </colgroup>
                <tr>
                    <td class="name">
                        Forma de pagamento:
                    </td>
                    <td>
                        <asp:RadioButtonList ID="ListFrmPgto" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ListFrmPgto_SelectedIndexChanged">
                            <asp:ListItem Value="1" Selected="True">Dinheiro</asp:ListItem>
                            <asp:ListItem Value="2">Crédito</asp:ListItem>
                            <asp:ListItem Value="3">Débito</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <table id="tblCartao" runat="server" visible="false">
                            <tr>
                                <td>
                                    Nº do Cartão:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumCartao" runat="server" Visible="false" MaxLength="16" Style="width: 204px;" />
                                </td>
                                <td>
                                    Código de segurança:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodCartao" runat="server" Visible="false" MaxLength="3" Style="width: 47px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnPedido" runat="server" Text="Gerar Pedido" OnClick="btnPedido_onClick" />
    </div>
    <div id="itensPedido">
        <asp:Label ID="Label1" runat="server" Text="Label" Style="width: 136px; height: 15px;"> Escolha o Item: </asp:Label>
        <asp:Button ID="btnItem" runat="server" Text="Adicionar Item" OnClick="btnItem_onClick" />
        <asp:DropDownList ID="listProdutosTeste" runat="server" Style="width: 182px; margin-top: 0px;">
            <asp:ListItem Selected="True" Value="1">Prod 01</asp:ListItem>
            <asp:ListItem Value="2">Prod 02</asp:ListItem>
            <asp:ListItem Value="3">Prod 03</asp:ListItem>
            <asp:ListItem Value="4">Prod 04</asp:ListItem>
            <asp:ListItem Value="5">Prod 05</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div id="areas">
        <textarea id="area_TEF" name="S2" cols="30" readonly="readonly" rows="1000" runat="server"
            style="background-color: #66CCFF; height: 115px;" title="Comprovantes TEF" visible="True"></textarea>
        <textarea id="area_Cupom" name="S1" readonly="readonly" rows="1000" cols="50" title="Cupons Impressos"
            runat="server" style="background-color: #FFFFCC; width: 464px; height: 344px;"
            visible="True">
       </textarea>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />        
    </div>
</asp:Content>
