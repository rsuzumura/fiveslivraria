using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data;
using System.Xml.Serialization;
using FivesLivraria.Data.Utils;

namespace FivesLivraria.Data
{
    [Serializable]
    public class Categoria
    {
        public SqlInt32 idCategoria { get; set; }
        public SqlString dsCategoria { get; set; }
        public SqlBoolean stCategoria { get; set; }

        #region METHODS
        public void Add()
        {
            SqlXmlRun.Execute("spCadastra_categoria", this);
        }

        public void Update()
        {
            SqlXmlRun.Execute("spUpdate_categoria", this);
        }

        public static bool Delete(int idCategoria)
        {
            bool r = false;
            SqlXmlRun.Execute("spRemove_categoria", out r, new SqlXmlParams("idCategoria", idCategoria));
            return r;
        }

        public static Categoria Get(int idCategoria)
        {
            return SqlXmlGet<Categoria>.Select("spGet_categoria", new SqlXmlParams("idCategoria", idCategoria));
        }
        #endregion
    }

    [XmlRoot("ListaCategoria")]
    public class ListaCategoria : List<Categoria>
    {
        public static ListaCategoria List(string dsCategoria, bool enabledOnly, int pageIndex, int pageSize, out int totalRowCount)
        {
            dsCategoria = string.Format("%{0}%", dsCategoria);
            return SqlXmlGet<ListaCategoria>.Select("spLista_categoriasByFilter", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("dsCategoria", dsCategoria, "enabledOnly", enabledOnly));
        }

        public static DataSet List()
        {
            return Dataset.ConverteListParaDataTable(SqlXmlGet<ListaCategoria>.Select("spLista_categorias", new SqlXmlParams()));
        }
    }
}
