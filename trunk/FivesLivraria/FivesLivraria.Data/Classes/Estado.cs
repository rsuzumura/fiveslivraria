using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace FivesLivraria.Data
{
    public class Estado
    {
        public SqlInt32 idEstado { get; set; }
        public SqlString nmEstado { get; set; }
        public SqlString siglaEstado { get; set; }
    }

    [XmlRoot("EstadoCollection")]
    public class EstadoCollection : List<Estado>
    {
        public static EstadoCollection List()
        {
            return SqlXmlGet<EstadoCollection>.Select("spLista_estado", new SqlXmlParams());
        }
    }
}
