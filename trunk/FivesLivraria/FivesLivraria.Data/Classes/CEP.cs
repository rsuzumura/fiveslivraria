using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace FivesLivraria.Data
{
    public class CEP
    {
        public SqlString cep { get; set; }
        public SqlString endereco { get; set; }
        public SqlString bairro { get; set; }
        public SqlString cidade { get; set; }
        public SqlInt32 idEstado { get; set; }

        public static CEP Get(string cep)
        {
            return SqlXmlGet<CEP>.Select("spGetEnderecoByCEP", new SqlXmlParams("cep", cep));
        }
    }
}
