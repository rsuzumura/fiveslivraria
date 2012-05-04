using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.ComponentModel;

namespace FivesLivraria.Controls
{
    [DefaultEvent("SelectedIndexChanged")]
    [SupportsEventValidation]
    [ControlValueProperty("SelectedValue")]
    [ToolboxData("<{0}:PagedGridView runat=server></{0}:PagedGridView>")]
    public class PagedGridView : GridView 
    {
        #region Public Properties

        public Int32 VirtualCount
        {
            get { return (Int32)(ViewState["VirtualCount"] ?? 0); }
            set { ViewState["VirtualCount"] = value; }
        }
        public Int32 CustomPageIndex
        {
            get { return (Int32)(ViewState["CustomPageIndex"] ?? 0); }
            set { ViewState["CustomPageIndex"] = value; }
        }

        public Int32 Index
        {
            get { return (Int32)(ViewState["Index"] ?? 0); }
            set { ViewState["Index"] = value; }
        }

        public Object SelectedItem
        {
            get { return (Object)(ViewState["SelectedItem"] ?? new object()); }
            private set { ViewState["SelectedItem"] = value; }
        }

        #endregion

        #region Protected Overrides Methods

        protected override void OnPageIndexChanging(GridViewPageEventArgs e)
        {
            this.CustomPageIndex = e.NewPageIndex;
            base.OnPageIndexChanging(e);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            Object collection = this.DataSource;

            if (collection != null)
            {
                String indexerName = ((DefaultMemberAttribute)collection.GetType()
                    .GetCustomAttributes(typeof(DefaultMemberAttribute),
                     true)[0]).MemberName;
                PropertyInfo propertyInfo = collection.GetType().GetProperty(indexerName);

                try
                {
                    this.SelectedItem = Activator.CreateInstance(propertyInfo.PropertyType, null);
                }
                catch { };                
            }

            base.OnDataBinding(e);
        }

        protected override void OnSelectedIndexChanging(GridViewSelectEventArgs e)
        {
            this.Index = e.NewSelectedIndex;
            base.OnSelectedIndexChanging(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (DataKeys.Count > 0)
            {
                DataKey dataKey = DataKeys[this.Index];
                this.SelectedItem = GetObject(DataKeyNames, dataKey);
            }

            base.OnSelectedIndexChanged(e);
        }

        protected override void OnRowDeleting(GridViewDeleteEventArgs e)
        {
            this.Index = e.RowIndex;
            base.OnRowDeleting(e);
        }

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            pagedDataSource.AllowPaging         = true;
            pagedDataSource.AllowCustomPaging   = true;
            pagedDataSource.VirtualCount        = VirtualCount;
            pagedDataSource.CurrentPageIndex    = CustomPageIndex;

            base.InitializePager(row, columnSpan, pagedDataSource);

            this.PageIndex = pagedDataSource.CurrentPageIndex;
        }

        #endregion

        #region Private Methods

        private object GetObject(string[] p, DataKey dataKey)
        {
            object o = SelectedItem;

            for (int i = 0; i < p.Length; i++)
            {
                string value = p[i];
                PropertyInfo propertyInfo = SelectedItem.GetType().GetProperty(value);
                propertyInfo.SetValue(o, dataKey[value], null);
            }

            return o;
        }

        #endregion
    }
}
