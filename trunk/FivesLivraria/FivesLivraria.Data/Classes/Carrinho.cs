using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using FivesLivraria.Data.Utils;
using System.Xml.Serialization;

namespace FivesLivraria.Data.Classes
{
    public class Carrinho
    {
        public SqlInt32 idCarrinho { get; set; } 
        public SqlInt32 idProduto { get; set; }
        public SqlString nmTitulo { get; set; }
        public SqlString nmTituloOriginal { get; set; }
        public SqlString dsProduto { get; set; }
        public SqlString ISBN { get; set; }
        public SqlString dsAutores { get; set; }
        public SqlString nmEditora { get; set; }
        public SqlInt32 nrAno { get; set; }
        public SqlString dsEdicao { get; set; }
        public SqlInt32 qtdProduto { get; set; }
        public SqlString nmImagem { get; set; }
        public SqlDecimal vlPreco { get; set; }
        public SqlInt32 idUsuario { get; set; }
        public SqlInt32 nrQtdProduto { get; set; }
        public SqlDecimal vlFinal { get; set; }
        public SqlString dvStatus { get; set; }

        public static void Insert(int idProduto, int idUsuario)
        {
            SqlXmlRun.Execute("spInsere_item_carrinho", new SqlXmlParams("idProduto", idProduto, "idUsuario", idUsuario));
        }
        
        public static void Update(int idCarrinho, int nrQtdProduto)
        {
            SqlXmlRun.Execute("spAltera_item_carrinho", new SqlXmlParams("idCarrinho", idCarrinho, "nrQtdProduto", nrQtdProduto));
        }

        public static void Delete(int idCarrinho)
        {
            SqlXmlRun.Execute("spExclui_item_carrinho", new SqlXmlParams("idCarrinho", idCarrinho));
        }

    }

    [XmlRoot("ListaCarrinho")]
    public class ListaCarrinho : List<Carrinho>
    {
        public static DataSet List(int idUsuario)
        {
            return Dataset.ConverteListParaDataTable(SqlXmlGet<ListaCarrinho>.Select("spLista_item_carrinho", new SqlXmlParams("idUsuario", idUsuario)));
        }
    }
}
