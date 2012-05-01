using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FivesLivraria.Data.Classes;
using FivesLivraria.Data;
using System.Data;

namespace FivesLivraria
{
    public partial class Principal : System.Web.UI.Page
    {
        private const int pagesize = 20;

        int page {
            get
            {
                if (string.IsNullOrEmpty(Request.QueryString["page"]))
                    return 1;
                else
                    return int.Parse(Request.QueryString["page"]); 
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CarregaDropDown();
                CarregaPagina(0,"");
            }
        }

        protected void CarregaDropDown()
        {
            ddlCategoria.DataTextField = "dsCategoria";
            ddlCategoria.DataValueField = "idCategoria";
            ddlCategoria.DataSource = ListaCategoria.List();
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0,"<< Não Selecionado >>");
            ddlCategoria.SelectedIndex = 0;
        }

        protected void CarregaPagina(int categoria, string produto)
        {
            int x;
            DataSet ds = ListaProdutos.List(categoria, "%" + produto + "%", page - 1, pagesize, out x);
            this.Show(page, x, "Principal.aspx", "Principal.aspx?page={0}", true);
            DataList1.DataSource = ds;
            DataList1.DataBind();
        }
        // simple struct that represents a (page number, url) association
        public struct PageUrl
        {
            private string page;
            private string url;

            // Page and Url property definitions
            public string Page
            {
                get
                {
                    return page;
                }
            }
            public string Url
            {
                get
                {
                    return url;
                }
            }
            // constructor
            public PageUrl(string page, string url)
            {
                this.page = page;
                this.url = url;
            }
        }

        // show the pager
        public void Show(int currentPage, int howManyPages, string firstPageUrl,
        string pageUrlFormat, bool showPages)
        {
            // display paging controls
            if (howManyPages >= 1)
            {
                // make the pager visible
                this.Visible = true;
                // display the current page
                currentPageLabel.Text = currentPage.ToString();
                howManyPagesLabel.Text = howManyPages.ToString();
                // create the Previous link
                if (currentPage == 1)
                {
                    previousLink.Enabled = false;
                }

                else
                {
                    previousLink.NavigateUrl = (currentPage == 2) ?
                    firstPageUrl : String.Format(pageUrlFormat, currentPage - 1);
                }
                // create the Next link
                if (currentPage == howManyPages)
                {
                    nextLink.Enabled = false;
                }
                else
                {
                    nextLink.NavigateUrl = String.Format(pageUrlFormat, currentPage + 1);
                }
                // create the page links
                if (showPages)
                {
                    // the list of pages and their URLs as an array
                    PageUrl[] pages = new PageUrl[howManyPages];
                    // generate (page, url) elements
                    pages[0] = new PageUrl("1", firstPageUrl);
                    for (int i = 2; i <= howManyPages; i++)
                    {
                        pages[i - 1] =
                        new PageUrl(i.ToString(), String.Format(pageUrlFormat, i));
                    }
                    // do not generate a link for the current page
                    pages[currentPage - 1] = new PageUrl((currentPage).ToString(), "");
                    // feed the pages to the repeater
                    pagesRepeater.DataSource = pages;
                    pagesRepeater.DataBind();
                }
            }
        }

        protected void btnBusca_Click(object sender, EventArgs e)
        {
            int x = 0;
            if (ddlCategoria.SelectedIndex > 0)
            {
                x = int.Parse(ddlCategoria.SelectedValue);
            }
            CarregaPagina(x,txtBusca.Text);
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            Label id = (Label)e.Item.FindControl("idProduto");
            Response.Redirect("Carrindo.aspx?id=" + id.Text);
        }
    }



}