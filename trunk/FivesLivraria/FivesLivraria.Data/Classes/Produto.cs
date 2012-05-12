using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;
using System.Reflection;
using System.Data;
using FivesLivraria.Data.Utils;

namespace FivesLivraria.Data
{
    [Serializable]
    public class Produto
    {
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
        public SqlInt32 idCategoria { get; set; }
        public SqlString dsCategoria { get; set; }

        //[XmlIgnore]
        //public int codigo { get; set; }
        //[XmlIgnore]
        //public string nome { get; set; }
        //[XmlIgnore]
        //public double valor { get; set; }

        //public Produto(int codigo, string nome, double valor)
        //{
        //    this.codigo = codigo;
        //    this.nome = nome;
        //    this.valor = valor;
        //}
                
        public override string ToString()
        {
            return "codigo: " + (idProduto.IsNull ? 0 : idProduto.Value) + " nome: " + (nmTitulo.IsNull ? ""  : nmTitulo.Value) + " valor: " + vlPreco;
        }

        public void Insert()
        {
            int t = 0;
            SqlXmlRun.Execute("spCadastra_produto", this, "idProduto", out t);
            idProduto = t;
        }

        public void Update()
        {
            SqlXmlRun.Execute("spUpdate_produto", this);
        }

        public static void Delete(int idProduto)
        {
            SqlXmlRun.Execute("spRemove_produto", new SqlXmlParams("idProduto", idProduto));
        }

        public static Produto Get(int idProduto)
        {
            return SqlXmlGet<Produto>.Select("spGet_produto", new SqlXmlParams("idProduto", idProduto));
        }
    }

    [XmlRoot("ListaProduto")]
    public class ListaProdutos : List<Produto>
    {
        public static ListaProdutos ListByFilter(int idCategoria, string nmProduto, int pageIndex, int pageSize, out int totalRowCount)
        {
            return SqlXmlGet<ListaProdutos>.Select("spLista_produto", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("categoria", idCategoria, "nmProduto", nmProduto));
        }

        public static DataSet List(int categoria, string nmProduto, int pageIndex, int pageSize, out int totalRowCount)
        {
            return Dataset.ConverteListParaDataTable(SqlXmlGet<ListaProdutos>.Select("spLista_produtos", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("categoria", categoria, "nmProduto", nmProduto)));
        }
    }
}
