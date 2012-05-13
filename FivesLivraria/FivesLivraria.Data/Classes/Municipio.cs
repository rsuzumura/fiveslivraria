using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace FivesLivraria.Data
{
    public class Municipio
    {
        public SqlInt32 idMunicipio { get; set; }
        public SqlString nmMunicipio { get; set; }
        public SqlInt32 idEstado { get; set; }
    }

    [XmlRoot("MunicipioCollection")]
    public class MunicipioCollection : List<Municipio>
    {
        public static MunicipioCollection List(int idEstado)
        {
            return SqlXmlGet<MunicipioCollection>.Select("spLista_municipio", new SqlXmlParams("idEstado", idEstado));
        }
    }
}
