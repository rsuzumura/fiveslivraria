using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Data.SqlTypes;

namespace FivesLivraria.Data
{
    [Serializable]
    public class Pedido
    {
        public SqlInt32 idPedido { get; set; }
        public SqlDateTime dtPedido { get; set; }
        public SqlInt32 idCliente { get; set; }
        public SqlInt32 idUsuario { get; set; }
        public SqlDateTime dtEntregaPrevista { get; set; }
        public SqlDateTime dtEntregaReal { get; set; }
        public SqlInt32 idEnderecoEntrega { get; set; }
        public SqlString EnderecoEntrega { get; set; }
        public SqlInt32 idEnderecoCobranca { get; set; }
        public SqlString EnderecoCobranca { get; set; }
        public SqlInt32 tpPagamento { get; set; }
        public SqlInt32 nrParcelas { get; set; }

        public static Pedido Get(int idPedido)
        {
            return SqlXmlGet<Pedido>.Select("spGet_pedido", new SqlXmlParams("idPedido", idPedido));
        }
    }

    [XmlRoot("ListaPedido")]
    public class ListaPedido : List<Pedido>
    {
        public static ListaPedido List(int idCliente, int pageIndex, int pageSize, out int totalRowCount)
        {
            return SqlXmlGet<ListaPedido>.Select("spLista_pedido", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("idCliente", idCliente));
        }
    }
}
