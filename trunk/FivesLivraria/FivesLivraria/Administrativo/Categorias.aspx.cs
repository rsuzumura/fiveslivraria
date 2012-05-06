using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;

namespace FivesLivraria.Administrativo
{
    public partial class Categorias : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindGrid(0);
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["dsCategoria"] = !string.IsNullOrEmpty(txtName.Text.Trim()) ? txtName.Text.Trim() : null;
            BindGrid(0);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            gridCategorias.EditIndex = -1;
            BindGrid(gridCategorias.EditIndex);
            txtCategoria.Text = string.Empty;
            txtName.Text      = string.Empty;
            divInsert.Visible = btnCancel.Visible = btnSave.Visible = true;
            divFilter.Visible = btnNew.Visible = ckbAtivo.Checked = gridCategorias.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria c = new Categoria() 
                {
                    dsCategoria = txtCategoria.Text,
                    stCategoria = ckbAtivo.Checked
                };
                c.Add();
                divInsert.Visible = btnCancel.Visible = btnSave.Visible = false;
                divFilter.Visible = btnNew.Visible = gridCategorias.Enabled = true;
                BindGrid(0);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageType.Error, string.Format("Erro na inclusão de categoria: {0}", ex.Message), "Erro de inclusão");
            }            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divInsert.Visible = btnCancel.Visible = btnSave.Visible = false;
            divFilter.Visible = btnNew.Visible = gridCategorias.Enabled = true;
        }

        protected void gridCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridCategorias.EditIndex = -1;
            BindGrid(e.NewPageIndex);
        }

        protected void gridCategorias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridCategorias.EditIndex = -1;
            BindGrid(gridCategorias.PageIndex);
        }

        protected void gridCategorias_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(((HiddenField)gridCategorias.Rows[e.RowIndex].FindControl("hdnIdCategoria")).Value);
                
                if (!Categoria.Delete(idCategoria))
                    throw new Exception("Não foi possível remover esta categoria.");

                BindGrid(gridCategorias.PageIndex);
            }
            catch (Exception ex)
            {
                this.ShowMessage(MessageType.Error, ex.Message, "Erro ao remover usuário");
            }
        }

        protected void gridCategorias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridCategorias.EditIndex = e.NewEditIndex;
            BindGrid(gridCategorias.PageIndex);
        }

        protected void gridCategorias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int idCategoria = Convert.ToInt32(((HiddenField)gridCategorias.Rows[e.RowIndex].FindControl("hdnIdCategoria")).Value);

                TextBox txtDescricao = (TextBox)gridCategorias.Rows[e.RowIndex].FindControl("txtDescricao");
                CheckBox ckbStatus   = (CheckBox)gridCategorias.Rows[e.RowIndex].FindControl("ckbStatus");

                Categoria c = Categoria.Get(idCategoria);
                c.dsCategoria = txtDescricao.Text;
                c.stCategoria = ckbStatus.Checked;

                c.Update();
                gridCategorias.EditIndex = -1;
                BindGrid(gridCategorias.PageIndex);
            }
            catch (Exception ex)
            {
                ShowMessage(MessageType.Error, ex.Message, "Erro na atualização da categoria");
            }
        }

        protected void BindGrid(int pageIndex)
        {
            int t = 0;
            gridCategorias.PageIndex = pageIndex;
            gridCategorias.DataSource = ListaCategoria.List((string)ViewState["dsCategoria"], false, gridCategorias.PageIndex, gridCategorias.PageSize, out t);
            gridCategorias.VirtualCount = t;
            gridCategorias.DataBind();
        }
    }
}