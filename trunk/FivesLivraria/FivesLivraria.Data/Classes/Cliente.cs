using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace FivesLivraria.Data
{
    public class Cliente
    {
        public SqlInt32 idCliente { get; set; }
        public SqlString nmCliente { get; set; }
        public SqlInt32 idUsuario { get; set; }

        [XmlArray("EnderecoCollection")]
        [XmlArrayItem("Endereco")]
        public EnderecoCollection Enderecos;

        public static Cliente Get(int idUsuario)
        {
            return SqlXmlGet<Cliente>.Select("spGet_cliente", new SqlXmlParams("idUsuario", idUsuario));
        }
    }

    [XmlRoot("ListaCliente")]
    public class ListaCliente : List<Cliente>
    {
        public static ListaCliente List(string nmCliente, int pageIndex, int pageSize, out int totalRowCount)
        {
            return SqlXmlGet<ListaCliente>.Select("spLista_cliente", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("nmCliente", nmCliente));
        }
    }

    public class Pessoa : Cliente
    {
        public SqlInt32 idPessoa { get; set; }
        public SqlString cpf { get; set; }
        public SqlString rg { get; set; }
        public SqlDateTime dtNascimento { get; set; }
        public SqlString nmMae { get; set; }

        public void Insert()
        {
            int id = 0;
            SqlXmlRun.Execute("spCadastra_pessoa", this, "idCliente", out id);
        }

        public static Pessoa Get(int idUsuario)
        {
            return SqlXmlGet<Pessoa>.Select("spGet_pessoa", new SqlXmlParams("idUsuario", idUsuario));
        }
    }

    public class ListaPessoa : List<Pessoa>
    {
        public static ListaPessoa List()
        {
            return SqlXmlGet<ListaPessoa>.Select("", new SqlXmlParams());
        }
    }

    public class Empresa : Cliente
    {
        public SqlInt32 idEmpresa { get; set; }
        public SqlString nmRazaoSocial { get; set; }
        public SqlString cnpj { get; set; }
        public SqlString inscricaoEstadual { get; set; }
        public SqlString inscricaoMunicipal { get; set; }

        public void Insert()
        {
            int id = 0;
            SqlXmlRun.Execute("spCadastra_pessoa", this, "idCliente", out id);
        }

        public static Empresa Get(int idUsuario)
        {
            return SqlXmlGet<Empresa>.Select("spGet_empresa", new SqlXmlParams("idUsuario", idUsuario));
        }
    }
}
