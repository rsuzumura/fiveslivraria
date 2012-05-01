using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace FivesLivraria.Data
{
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

        public int codigo;
        public string nome;
        public double valor;
                
        //public Produto(int codigo, string nome, double valor)
        //{
        //    this.codigo = codigo;
        //    this.nome = nome;
        //    this.valor = valor;
        //}

        override
        public string ToString()
        {
            return "codigo: " + codigo + "\n nome: " + nome + "\n valor: " + valor;
        }
    }

    [XmlRoot("ListaProduto")]
    public class ListaProdutos : List<Produto>
    {
        public static ListaProdutos List(string categoria, string nmProduto, int pageIndex, int pageSize, out int totalRowCount)
        {
            return SqlXmlGet<ListaProdutos>.Select("spLista_produtos", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("categoria", categoria, "nmProduto", nmProduto));
        }
    }
}
