﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;

namespace FivesLivraria
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario u = Usuario.Get(User.Identity.Name);
                Current.UserId   = u.idUsuario.Value;
                Current.UserName = u.nmUsuario.Value;
 
            }
        }
    }
}