using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace FivesLivraria.Data
{
    [Serializable]
    public class Endereco
    {
        public SqlInt32 idEndereco { get; set; }
        public SqlString dsEndereco { get; set; }
        public SqlString nrEndereco { get; set; }
        public SqlString compEndereco { get; set; }
        public SqlString cep { get; set; }
        public SqlString dsBairro { get; set; }
        public SqlInt32 idMunicipio { get; set; }
        public SqlString idCliente { get; set; }
    }

    [Serializable]
    [XmlRoot("EnderecoCollection")]
    public class EnderecoCollection : List<Endereco>
    {
        public static EnderecoCollection ListByCliente(int idCliente)
        {
            return SqlXmlGet<EnderecoCollection>.Select("spLista_endereco", new SqlXmlParams("idCliente", idCliente));
        }
    }
}
