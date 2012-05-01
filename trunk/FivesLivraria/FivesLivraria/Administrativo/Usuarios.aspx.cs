using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;

namespace FivesLivraria.Administrativo
{
    public partial class Usuarios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ShowMessage(MessageType.Info, "Teste", "Teste");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}