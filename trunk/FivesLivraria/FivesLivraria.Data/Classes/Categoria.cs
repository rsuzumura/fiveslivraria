using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using System.Xml.Serialization;
using FivesLivraria.Data.Utils;

namespace FivesLivraria.Data.Classes
{
    public class Categoria
    {
        public SqlInt32 idCategoria { get; set; }
        public SqlString dsCategoria { get; set; }

    }

    [XmlRoot("ListaCategoria")]
    public class ListaCategoria : List<Categoria>
    {
        public static DataSet List()
        {
            return Dataset.ConverteListParaDataTable(SqlXmlGet<ListaCategoria>.Select("spLista_categorias", new SqlXmlParams("nada",0)));
        }
    }
}
