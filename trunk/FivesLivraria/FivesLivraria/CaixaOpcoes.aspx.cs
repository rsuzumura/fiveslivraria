using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FivesLivraria
{
   public partial class CaixaOpcoes : System.Web.UI.Page
   {
      protected void Page_Load(object sender, EventArgs e)
      {

      }

      protected void btnBalcao_Click(object sender, EventArgs e)
      {
          Response.Redirect("~/CaixaBalcao.aspx", false);
      }

      protected void btnPedidos_Click(object sender, EventArgs e)
      {
          Response.Redirect("~/CaixaPedidos.aspx", false);
      }

      protected void btnCaixa_Click(object sender, EventArgs e)
      {
          Response.Redirect("~/CaixaAcoes.aspx", false);
      }

      protected void lbkAjudaUsuario_Click(object sender, EventArgs e)
      {
          Response.Redirect("ModuloUsuarioHelp.htm", false);
      }
   }
}