using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data;
using System.IO;
using System.Configuration;

namespace FivesLivraria.Administrativo
{
    public partial class CadastroProduto : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.IsInRole("gestor"))
                Response.Redirect("~/Login.aspx", false);

            if (!IsPostBack)
            {
                ViewState["idProduto"] = Request.QueryString["id"];
                int t = 0;
                FillControl<Categoria>(dropCategorias, ListaCategoria.List(string.Empty, true, 0, int.MaxValue - 1, out t));
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int idProduto = Convert.ToInt32(Request.QueryString["id"]);
                    Produto p = Produto.Get(idProduto);
                    txtNmTitulo.Text        = p.nmTitulo.Value;
                    txtOriginalTitle.Text   = !p.nmTituloOriginal.IsNull ? p.nmTituloOriginal.Value : string.Empty;
                    txtDescription.Text     = !p.dsProduto.IsNull ? p.dsProduto.Value : string.Empty;
                    txtISBN.Text            = !p.ISBN.IsNull ? p.ISBN.Value : string.Empty;
                    txtAutor.Text           = !p.dsAutores.IsNull ? p.dsAutores.Value : string.Empty;
                    txtEditora.Text         = !p.nmEditora.IsNull ? p.nmEditora.Value : string.Empty;
                    if (!p.nmImagem.IsNull)
                    {
                        string imagePath = string.Format("~/Images/{0}", p.nmImagem.Value);
                        if (File.Exists(Server.MapPath(imagePath)))
                            imgPhoto.ImageUrl = !p.nmImagem.IsNull ? string.Format("~/Images/{0}", p.nmImagem.Value) : string.Empty;
                        else
                            imgPhoto.ImageUrl = "~/Images/imagem_nao_disponivel.jpg";
                    }
                    else
                        imgPhoto.ImageUrl = "~/Images/imagem_nao_disponivel.jpg";
                    txtValue.Text           = p.vlPreco.Value.ToString();
                    dropCategorias.SelectedValue = !p.idCategoria.IsNull ? p.idCategoria.Value.ToString() : string.Empty;
                    txtQuantity.Text        = !p.qtdProduto.IsNull ? p.qtdProduto.Value.ToString() : string.Empty;
                    txtYear.Text            = !p.nrAno.IsNull ? p.nrAno.Value.ToString() : string.Empty;
                    txtEdition.Text         = !p.dsEdicao.IsNull ? p.dsEdicao.Value : string.Empty;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Page.Validate();

            if (Page.IsValid)
                try
                {
                    if (FileUploadImage.HasFile)
                    {
                        int maxWidth = Convert.ToInt32(ConfigurationManager.AppSettings["imageMaxWidth"]);
                        int maxHeight = Convert.ToInt32(ConfigurationManager.AppSettings["imageMaxHeight"]);
                        System.Drawing.Image img = System.Drawing.Image.FromStream(FileUploadImage.FileContent, true, true);
                        if (img.Width > maxHeight || img.Height > maxHeight)
                            ShowMessage(MessageType.Warning, string.Format("A mensagem deve ter tamanho máximo de {0}px por {1}px", maxWidth, maxHeight), "Atenção");
                        else
                            FileUploadImage.SaveAs(Server.MapPath("~/Images/") + Path.GetFileName(FileUploadImage.FileName));
                    }
                    int idProduto;
                    int.TryParse((string)ViewState["idProduto"], out idProduto);
                    Produto p = null;

                    if (idProduto != 0)
                    {
                        p = Produto.Get(idProduto);
                        
                        p.nmTitulo          = txtNmTitulo.Text;
                        p.nmTituloOriginal  = txtOriginalTitle.Text;
                        p.dsProduto         = txtDescription.Text;
                        p.ISBN              = txtISBN.Text;
                        p.dsAutores         = txtAutor.Text;
                        p.nmEditora         = txtEditora.Text;
                        p.nrAno             = Convert.ToInt32(txtYear.Text);
                        p.dsEdicao          = txtEdition.Text;
                        p.qtdProduto        = Convert.ToInt32(txtQuantity.Text);
                        if (imgPhoto.ImageUrl != "~/Images/imagem_nao_disponivel.jpg")
                            p.nmImagem          = Server.MapPath(imgPhoto.ImageUrl).Replace(Server.MapPath("~/Images/"), string.Empty);
                        p.vlPreco           = Convert.ToDecimal(txtValue.Text);
                        if (!string.IsNullOrEmpty(dropCategorias.SelectedValue)) p.idCategoria = Convert.ToInt32(dropCategorias.SelectedValue);

                        p.Update();
                    }
                    else
                    {
                        p = new Produto()
                        {
                            nmTitulo         = txtNmTitulo.Text,
                            nmTituloOriginal = txtOriginalTitle.Text,
                            dsProduto        = txtDescription.Text,
                            ISBN             = txtISBN.Text,
                            dsAutores        = txtAutor.Text,
                            nmEditora        = txtEditora.Text,
                            nrAno            = Convert.ToInt32(txtYear.Text),
                            dsEdicao         = txtEdition.Text,
                            qtdProduto       = Convert.ToInt32(txtQuantity.Text),                            
                            vlPreco          = Convert.ToDecimal(txtValue.Text)
                        };
                        if (imgPhoto.ImageUrl != "~/Images/imagem_nao_disponivel.jpg")
                            p.nmImagem = Server.MapPath(imgPhoto.ImageUrl).Replace(Server.MapPath("~/Images/"), string.Empty);
                        if (!string.IsNullOrEmpty(dropCategorias.SelectedValue)) p.idCategoria = Convert.ToInt32(dropCategorias.SelectedValue);
                        
                        p.Insert();
                    }
                    Response.Redirect("Produtos.aspx", false);
                }
                catch(Exception ex)
                {
                    ShowMessage(MessageType.Error, ex.Message, "Erro");
                }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Produtos.aspx", false);
        }
        
        protected void btnUpload_Click(object sender, ImageClickEventArgs e)
        {
            FileUploadImage.SaveAs(Server.MapPath("~/Images/") + Path.GetFileName(FileUploadImage.FileName));
            imgPhoto.ImageUrl = string.Format("~/Images/{0}", FileUploadImage.FileName);
        }

        protected void cvImage_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (FileUploadImage.HasFile)
            {
                int maxWidth = Convert.ToInt32(ConfigurationManager.AppSettings["imageMaxWidth"]);
                int maxHeight = Convert.ToInt32(ConfigurationManager.AppSettings["imageMaxHeight"]);
                System.Drawing.Image img = System.Drawing.Image.FromStream(FileUploadImage.FileContent, true, true);
                if (img.Width > maxHeight || img.Height > maxHeight)
                {
                    cvImage.ErrorMessage = string.Format("A mensagem deve ter tamanho máximo de {0}px por {1}px", maxWidth, maxHeight);
                    args.IsValid = false;
                }
                else
                    args.IsValid = true;
            }
            else
                args.IsValid = true;
        }
    }
}