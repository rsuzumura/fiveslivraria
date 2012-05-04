using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace FivesLivraria.Data
{
    [Serializable]
    public class Usuario
    {
        public SqlInt32 idUsuario { get; set; }
        public SqlString nmUsuario { get; set; }
        public SqlString dsEndereco { get; set; }
        public SqlString dsLogin { get; set; }
        public SqlString Email { get; set; }
        public SqlBoolean IsLocked { get; set; }
        public SqlString RoleName { get; set; }

        public void Insert()
        {
            int id = 0;
            SqlXmlRun.Execute("spCadastra_usuario", this, "idUsuario", out id);
            this.idUsuario = id;
        }
        public void Update()
        {
            SqlXmlRun.Execute("spUpdate_usuario", this);
        }
        public static bool Delete(string dsLogin)
        {
            bool r = false;
            SqlXmlRun.Execute("spRemove_usuario", out r, new SqlXmlParams("dsLogin", dsLogin));
            return r;
        }

        public static Usuario Get(string dsLogin)
        {
            return SqlXmlGet<Usuario>.Select("spGet_usuario", new SqlXmlParams("dsLogin", dsLogin));
        }

        public static bool VerificaEmail(string dsLogin, string email)
        {
            bool r = false;
            SqlXmlRun.Execute("spVerifica_email", out r, new SqlXmlParams("dsLogin", dsLogin, "email", email));
            return r;
        }
    }

    [XmlRoot("ListaUsuarios")]
    public class ListaUsuario : List<Usuario>
    {
        public static ListaUsuario List(string nmUsuario, string roleName, int pageIndex, int pageSize, out int totalRowCount)
        {
            return SqlXmlGet<ListaUsuario>.Select("spLista_usuario", pageIndex, pageSize, out totalRowCount, new SqlXmlParams("nmUsuario", string.Format("%{0}%", nmUsuario), "roleName", roleName));
        }
    }
}
