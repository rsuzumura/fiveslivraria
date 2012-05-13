<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CaixaBalcao.aspx.cs" Inherits="FivesLivraria.CaixaBalcao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #TextArea1
        {
            width: 524px;
            top: 151px;
            left: 687px;
            height: 266px;
        }
        #areaTEF
        {
            top: 553px;
            left: 797px;
            height: 213px;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="Pedido">
        <asp:Label ID="lbl_NomeCliente" runat="server"  
            style="position:absolute; top: 226px; left: 144px; margin-top: 0px;"> Nome: </asp:Label>

        <asp:TextBox ID="box_NomeCliente" runat="server" 
            style="position:absolute; top: 224px; left: 202px; margin-top: 0px;"> </asp:TextBox>
        
        <asp:Label ID="lbl_CPFCliente" runat="server" 
            style="position:absolute; top: 258px; left: 147px; margin-top: 20px;"> CPF: </asp:Label>

        <asp:TextBox ID="box_CPFCliente" runat="server" 
            
            style="position:absolute; top: 252px; left: 198px; margin-top: 20px; height: 20px;"> </asp:TextBox>
        
        <asp:Button ID="btnPedido" runat="server" Text="Gerar Pedido"
            style="position:absolute; top: 176px; left: 581px;" 
            OnClick="btnPedido_onClick" />

        <asp:RadioButtonList ID="ListFrmPgto" runat="server" 
            style="position:absolute; top: 210px; left: 424px; margin-top: 4px;" 
            onselectedindexchanged="ListFrmPgto_SelectedIndexChanged">
                <asp:ListItem Value="1" Selected="True">Dinheiro</asp:ListItem>
                <asp:ListItem Value="2">Crédito</asp:ListItem>
                <asp:ListItem Value="3">Débito</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lbl_Pgto" runat="server" Text="Forma de Pagamento" 
            style="position:absolute; top: 181px; left: 388px;"/>

        <asp:TextBox ID="txtNumCartao" runat="server" Visible="false" MaxLength="16" 
            style="position:absolute; top: 307px; left: 362px; width: 204px;"/>

        <asp:TextBox ID="txtCodCartao" runat="server" Visible="false" MaxLength="3"
            style="position:absolute; top: 304px; left: 608px; width: 47px;"/>
</div>
<div id="itensPedido">
        <asp:Label ID="Label1" runat="server" Text="Label" 
            
            style="position:absolute; top: 370px; left: 188px; width: 136px; height: 15px; right: 701px;"> Escolha o Item: </asp:Label>

       <asp:Button id="btnItem" runat="server" Text="Adicionar Item" style="position:absolute; top: 364px; left: 570px;" 
            OnClick="btnItem_onClick" />

        <asp:DropDownList ID="listProdutosTeste" runat="server" 
            style="position:absolute; top: 367px; left: 312px; width: 182px; margin-top: 0px;" >
            <asp:ListItem Selected="True" Value="1">Prod 01</asp:ListItem>
            <asp:ListItem Value="2">Prod 02</asp:ListItem>
            <asp:ListItem Value="3">Prod 03</asp:ListItem>
            <asp:ListItem Value="4">Prod 04</asp:ListItem>
            <asp:ListItem Value="5">Prod 05</asp:ListItem>
        </asp:DropDownList>

</div>

<div id="areas">
        <textarea id="area_TEF" name="S2" cols="30" readonly="readonly" rows="1000" runat="server"
            style="background-color: #66CCFF; position: absolute; top: 503px; left: 821px; height: 115px;" 
            title="Comprovantes TEF" visible="True"></textarea>

        <textarea id="area_Cupom" name="S1" readonly="readonly" rows="1000" cols="50" 
            title="Cupons Impressos" runat="server"
            
            style="background-color: #FFFFCC; position: absolute; top: 146px; left: 735px; width: 464px; height: 344px;" 
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

    <asp:GridView ID="gvTable" runat="server" AutoGenerateColumns="False" 
            style="position:absolute; top: 405px; left: 149px; width: 182px; margin-top: 0px;"
        DataKeyNames="idTable" 
        onrowcommand="gvTable_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Descrição">
                <ItemTemplate>
                    <asp:Image ID="imagem" runat="server" Text="Imagem" src='<%#"/Images/"+ Eval("nmImagem") %>'  width="170"/>
                    <asp:Label ID="nmTitulo" runat="server" Text='<%#Eval("nmTitulo")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" CommandName="excluir" Text="Excluir" />
             <asp:TemplateField HeaderText="Quantidade">
                <ItemTemplate>
                    <asp:TextBox ID="nrQtdProduto" runat="server" Text='<%#Eval("nrQtdProduto")%>' width="40" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Button" CommandName="atualizar" Text="Atualizar" />
            <asp:BoundField DataField="vlPreco" HeaderText="Preço Unitário" SortExpression="idProduto" />
            <asp:BoundField DataField="vlFinal" HeaderText="Valor Final" SortExpression="nmTitulo" />
        </Columns>
    </asp:GridView>
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
