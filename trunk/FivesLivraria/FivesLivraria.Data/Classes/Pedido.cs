using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FivesLivraria.Data.Classes
{
    [Serializable]
    public class Pedido
    {
        public int codigoPedido { get; set; }
        public FormaPagamento pagamento { get; set; }
        public double valorTotalPedido { get; set; }
        public ItemPedido itens { get; set; }

        //public Pedido()
        //{
        //   this.codigoPedido = 1000;
        //}

        public static void finalizaPedido(string dsPagamento, int idUsuario, int nrParcelas)
        {
            SqlXmlRun.Execute("spFinaliza_compra", new SqlXmlParams("dsPagamento", dsPagamento, "idUsuario", idUsuario, "nrParcelas", nrParcelas));
        }

        public static void finalizaPedido(string dsPagamento, int idUsuario, int nrParcelas, int idEnderecoEntrega, int idEnderecoCobranca)
        {
            SqlXmlRun.Execute("spFinaliza_compra2", new SqlXmlParams("dsPagamento", dsPagamento, "idUsuario", idUsuario, "nrParcelas", nrParcelas, "idEnderecoEntrega", idEnderecoEntrega, "idEnderecoCobranca", idEnderecoCobranca));
        }

        public double getValorTotalPedido()
        {
            this.valorTotalPedido = this.itens.totalizarItens();
            return valorTotalPedido;
        }
    }

    [XmlRoot("ListaPedido")]
    public class ListaPedido : List<Pedido>
    {
        public static ListaPedido List(int idCliente)
        {
            return SqlXmlGet<ListaPedido>.Select("spLista_pedido", new SqlXmlParams("idCliente", idCliente));
        }
    }
}
