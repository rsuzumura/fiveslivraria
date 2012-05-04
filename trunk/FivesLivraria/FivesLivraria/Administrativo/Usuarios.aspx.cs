using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;
using System.Web.Security;

namespace FivesLivraria.Administrativo
{
    public partial class Usuarios : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillControl(dropRoles, Roles.GetAllRoles());
                BindGrid(null, null, 0);
            }
        }

        protected void gridUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridUsuarios.EditIndex  = e.NewEditIndex;
            BindGrid((string)ViewState["role"], (string)ViewState["username"], gridUsuarios.PageIndex);
            DropDownList roles      = (DropDownList)gridUsuarios.Rows[e.NewEditIndex].FindControl("dropRoles");
            HiddenField hdnRole     = (HiddenField)gridUsuarios.Rows[e.NewEditIndex].FindControl("hdnRole");
            FillControlWithoutNull(roles, Roles.GetAllRoles());
            roles.SelectedValue     = hdnRole.Value;            
        }

        protected void gridUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
            {
                try
                {
                    string dsLogin = ((HiddenField)gridUsuarios.Rows[e.RowIndex].FindControl("hdnLogin")).Value;
                    MembershipUser mu = Membership.GetUser(dsLogin);

                    TextBox txtEmail = (TextBox)gridUsuarios.Rows[e.RowIndex].FindControl("txtEmail");
                    DropDownList dropRoles = (DropDownList)gridUsuarios.Rows[e.RowIndex].FindControl("dropRoles");
                    CheckBox ckbLocked = (CheckBox)gridUsuarios.Rows[e.RowIndex].FindControl("ckbLocked");

                    if (Usuario.VerificaEmail(dsLogin, txtEmail.Text))
                    {
                        Usuario u = Usuario.Get(dsLogin);
                        u.Email = txtEmail.Text;
                        u.RoleName = dropRoles.SelectedValue;
                        u.IsLocked = ckbLocked.Checked;

                        u.Update();
                        gridUsuarios.EditIndex = -1;
                        BindGrid((string)ViewState["role"], (string)ViewState["username"], gridUsuarios.PageIndex);
                    }
                    else
                        throw new Exception("Já existe usuário com este email cadastrado.");
                }
                catch (Exception ex)
                {
                    ShowMessage(MessageType.Error, ex.Message, "Erro na atualização do usuário");
                }
            }
            else
                return;
        }

        protected void gridUsuarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string dsLogin = ((HiddenField)gridUsuarios.Rows[e.RowIndex].FindControl("hdnLogin")).Value;
                if (dsLogin != Page.User.Identity.Name)
                {
                    if (!Usuario.Delete(dsLogin))
                        throw new Exception("Não foi possível remover este usuário.");
                }
                else
                    throw new Exception("Não é possível remover um usuário logado.");

                BindGrid((string)ViewState["role"], (string)ViewState["username"], gridUsuarios.PageIndex);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageType.Error, ex.Message, "Erro ao remover usuário");
            }
        }

        protected void gridUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridUsuarios.EditIndex = -1;
            BindGrid((string)ViewState["role"], (string)ViewState["username"], gridUsuarios.PageIndex);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroUsuario.aspx", false);
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["username"]   = !string.IsNullOrEmpty(txtName.Text.Trim()) ? txtName.Text.Trim() : null;
            ViewState["role"]       = !string.IsNullOrEmpty(dropRoles.SelectedValue) ? dropRoles.SelectedValue : null;
            BindGrid((string)ViewState["role"], (string)ViewState["username"], 0);
        }

        protected void gridUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsuarios.EditIndex = -1;
            BindGrid((string)ViewState["role"], (string)ViewState["username"], e.NewPageIndex);
        }

        protected void BindGrid(string role, string username, int pageIndex)
        {
            int t = 0;
            gridUsuarios.PageIndex      = pageIndex;
            gridUsuarios.DataSource     = ListaUsuario.List(username, role, gridUsuarios.PageIndex, gridUsuarios.PageSize, out t);
            gridUsuarios.VirtualCount   = t;
            gridUsuarios.DataBind();
        }
    }
}