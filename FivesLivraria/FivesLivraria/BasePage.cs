using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Web.Security;

namespace FivesLivraria
{
    public enum MessageType
    {
        Info,
        Warning,
        Error
    }
    public class BasePage : System.Web.UI.Page
    {
        public void ShowMessage(MessageType messageType, string message, string title)
        {
            message = Server.HtmlEncode(message).Replace("\r\n", "<br />");
            string type = "info";
            switch (messageType)
            {
                case MessageType.Info:
                    type = "info";
                    break;
                case MessageType.Error:
                    type = "error";
                    break;
                case MessageType.Warning:
                    type = "alert";
                    break;
            }

            StringBuilder script = new StringBuilder();
            script.Append("$(document).ready(function(){");
            script.Append("$.msgBox({");
            script.AppendFormat("title:\"{0}\",", title);
            script.AppendFormat("content:\"{0}\",", message);
            script.AppendFormat("type:\"{0}\"", type);
            script.Append("});");
            script.Append("});");
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "", script.ToString(), true);
        }

        protected void FillControl<T>(DropDownList dropDownList, List<T> list)
        {
            if (dropDownList != null)
            {
                PropertyInfo propertyInfoTextField = null;
                PropertyInfo propertyInfoValueField = null;

                dropDownList.Items.Clear();
                dropDownList.Items.Insert(0, new ListItem("Selecione...", ""));

                string dataTextField = null;
                string dataValueField = null;

                if (list != null)
                {
                    foreach (T item in list)
                    {
                        propertyInfoTextField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataTextField);
                        if (propertyInfoTextField != null) dataTextField = Convert.ToString(propertyInfoTextField.GetValue(item, null));

                        propertyInfoValueField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataValueField);
                        if (propertyInfoValueField != null) dataValueField = Convert.ToString(propertyInfoValueField.GetValue(item, null));

                        dropDownList.Items.Add(new ListItem(dataTextField, dataValueField));
                    }
                }
            }
        }

        protected void FillControl<T>(DropDownList dropDownList, List<T> list, string nullMessage)
        {
            if (dropDownList != null)
            {
                PropertyInfo propertyInfoTextField = null;
                PropertyInfo propertyInfoValueField = null;

                dropDownList.Items.Clear();
                dropDownList.Items.Insert(0, new ListItem(nullMessage, ""));

                string dataTextField = null;
                string dataValueField = null;

                if (list != null)
                {
                    foreach (T item in list)
                    {
                        propertyInfoTextField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataTextField);
                        if (propertyInfoTextField != null) dataTextField = Convert.ToString(propertyInfoTextField.GetValue(item, null));

                        propertyInfoValueField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataValueField);
                        if (propertyInfoValueField != null) dataValueField = Convert.ToString(propertyInfoValueField.GetValue(item, null));

                        dropDownList.Items.Add(new ListItem(dataTextField, dataValueField));
                    }
                }
            }
        }

        protected void FillControl<T>(DropDownList dropDownList, List<T> list, string nullMessage, string nullValue)
        {
            if (dropDownList != null)
            {
                PropertyInfo propertyInfoTextField = null;
                PropertyInfo propertyInfoValueField = null;

                dropDownList.Items.Clear();
                dropDownList.Items.Insert(0, new ListItem(nullMessage, nullValue));

                string dataTextField = null;
                string dataValueField = null;

                if (list != null)
                {
                    foreach (T item in list)
                    {
                        propertyInfoTextField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataTextField);
                        if (propertyInfoTextField != null) dataTextField = Convert.ToString(propertyInfoTextField.GetValue(item, null));

                        propertyInfoValueField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataValueField);
                        if (propertyInfoValueField != null) dataValueField = Convert.ToString(propertyInfoValueField.GetValue(item, null));

                        dropDownList.Items.Add(new ListItem(dataTextField, dataValueField));
                    }
                }
            }
        }

        protected void FillControlWithoutNull<T>(DropDownList dropDownList, List<T> list)
        {
            if (dropDownList != null)
            {
                PropertyInfo propertyInfoTextField = null;
                PropertyInfo propertyInfoValueField = null;

                dropDownList.Items.Clear();

                string dataTextField = null;
                string dataValueField = null;

                if (list != null)
                {
                    foreach (T item in list)
                    {
                        propertyInfoTextField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataTextField);
                        if (propertyInfoTextField != null) dataTextField = Convert.ToString(propertyInfoTextField.GetValue(item, null));

                        propertyInfoValueField = (PropertyInfo)item.GetType().GetProperty(dropDownList.DataValueField);
                        if (propertyInfoValueField != null) dataValueField = Convert.ToString(propertyInfoValueField.GetValue(item, null));

                        dropDownList.Items.Add(new ListItem(dataTextField, dataValueField));
                    }
                }
            }
        }

        protected void FillControl<T>(CheckBoxList checkBoxList, List<T> list, string nullMessage, string nullValue)
        {
            if (checkBoxList != null)
            {
                PropertyInfo propertyInfoTextField = null;
                PropertyInfo propertyInfoValueField = null;

                checkBoxList.Items.Clear();
                checkBoxList.Items.Insert(0, new ListItem(nullMessage, nullValue));

                string dataTextField = null;
                string dataValueField = null;

                if (list != null)
                {
                    foreach (T item in list)
                    {
                        propertyInfoTextField = (PropertyInfo)item.GetType().GetProperty(checkBoxList.DataTextField);
                        if (propertyInfoTextField != null) dataTextField = Convert.ToString(propertyInfoTextField.GetValue(item, null));

                        propertyInfoValueField = (PropertyInfo)item.GetType().GetProperty(checkBoxList.DataValueField);
                        if (propertyInfoValueField != null) dataValueField = Convert.ToString(propertyInfoValueField.GetValue(item, null));

                        checkBoxList.Items.Add(new ListItem(dataTextField, dataValueField));
                    }
                }
            }
        }

        protected void FillControl<T>(ListBox listBox, List<T> list)
        {
            if (listBox != null)
            {
                PropertyInfo propertyInfoTextField = null;
                PropertyInfo propertyInfoValueField = null;

                listBox.Items.Clear();

                string dataTextField = null;
                string dataValueField = null;

                if (list != null)
                {
                    foreach (T item in list)
                    {
                        propertyInfoTextField = (PropertyInfo)item.GetType().GetProperty(listBox.DataTextField);
                        if (propertyInfoTextField != null) dataTextField = Convert.ToString(propertyInfoTextField.GetValue(item, null));

                        propertyInfoValueField = (PropertyInfo)item.GetType().GetProperty(listBox.DataValueField);
                        if (propertyInfoValueField != null) dataValueField = Convert.ToString(propertyInfoValueField.GetValue(item, null));

                        listBox.Items.Add(new ListItem(dataTextField, dataValueField));
                    }
                }
            }
        }

        protected void FillControl(DropDownList dropDownList, string[] strings)
        {            
            if (dropDownList != null)
            {
                dropDownList.Items.Clear();

                dropDownList.Items.Insert(0, new ListItem("TODOS", ""));

                if (strings != null)
                {
                    foreach (string item in strings)                        
                        dropDownList.Items.Add(new ListItem(item, item));
                }
            }
        }

        protected void FillControlWithoutNull(DropDownList dropDownList, string[] strings)
        {
            if (dropDownList != null)
            {
                dropDownList.Items.Clear();

                if (strings != null)
                {
                    foreach (string item in strings)
                        dropDownList.Items.Add(new ListItem(item, item));
                }
            }
        }

        protected string TraduzMensagem(MembershipCreateStatus ms)
        {
            try
            {
                string s = string.Empty;
                switch (ms)
                {
                    case MembershipCreateStatus.DuplicateEmail: s = "Já existe cliente cadastrado com esse email."; break;
                    case MembershipCreateStatus.DuplicateUserName: s = "Já existe usuário cadastrado com esse login."; break;
                    case MembershipCreateStatus.InvalidAnswer: s = "Verifique a resposta a ser cadastrada"; break;
                    case MembershipCreateStatus.InvalidEmail: s = "O formato do email está inválido."; break;
                    case MembershipCreateStatus.InvalidQuestion: s = "Verifique a pergunta a ser cadastrada."; break;
                    case MembershipCreateStatus.InvalidUserName: s = "Verifique o login do usuário."; break;
                    default: s = string.Concat(s, "Erro no cadastro do usuário: ", ms.ToString()); break;
                }
                return s;
            }
            catch (Exception err)
            {
                return string.Concat("Erro desconhecido: ", err.Message);
            }
        }
    }
}