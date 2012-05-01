using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace FivesLivraria.Data
{
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

        public static Usuario Get(string dsLogin)
        {
            return SqlXmlGet<Usuario>.Select("spGet_usuario", new SqlXmlParams("dsLogin", dsLogin));
        }
    }
}
