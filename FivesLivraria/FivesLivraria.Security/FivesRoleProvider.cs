using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration.Provider;
using System.Web.Security;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using FivesLivraria.Security.SqlScripts;
using System.Data;

namespace FivesLivraria.Security
{
    public sealed class FivesRoleProvider : RoleProvider
    {
        private string eventSource = "FivesRoleProvider";
        private string eventLog = "Application";
        private string exceptionMessage = MessageResources.GeneralExceptionMessage;
        private string connectionString;

        /// <summary>
        /// Carrega um valor de configuração ou seu default.
        /// </summary>
        /// <param name="configValue">Valor.</param>
        /// <param name="defaultValue">Default.</param>
        /// <returns>O valor passado ou seu default, se nulo.</returns>
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;
            return configValue;
        }
        /// <summary>
        /// Abre uma conexão ao banco de dados.
        /// </summary>
        /// <returns>Conexão Sql</returns>
        private SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection(connectionString);
            try
            {
                cn.Open();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "OpenConnection");
                    return null;
                }
                else
                    throw e;
            }
            return cn;
        }
        private void WriteToEventLog(SqlException e, string action)
        {
            EventLog log = new EventLog();
            log.Source  = eventSource;
            log.Log     = eventLog;
            string message = exceptionMessage + "\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();
            log.WriteEntry(message);
        }
        #region Propriedades

        private bool writeExceptionsToEventLog = false;
        public bool WriteExceptionsToEventLog
        {
            get { return writeExceptionsToEventLog; }
            set { writeExceptionsToEventLog = value; }
        }

        private string applicationName;
        public override string ApplicationName
        {
            get { return applicationName; }
            set { applicationName = value; }
        }

        #endregion

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            //verifica os parâmetros de configuração foram passados
            if (config == null)
                throw new ArgumentNullException("config");

            //assume o nome padrão, se não foi passado
            if (name == null || name.Length == 0)
                name = "SqlRoleProvider";

            //assume a descrição padrão, se não foi passada
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Five'S MS SQL Server Role provider");
            }

            //inicializa a base class
            base.Initialize(name, config);

            //captura os parâmetros passados nas propriedades
            applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            writeExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            //captura a string de conexão ao banco
            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];
            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == string.Empty)
                throw new ProviderException(MessageResources.ConnectionStringError);
            connectionString = ConnectionStringSettings.ConnectionString;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            // verifica se as roles existem
            foreach (string rolename in roleNames)
                if (!RoleExists(rolename))
                    throw new ProviderException(String.Format(MessageResources.RoleNotFound, rolename));

            // verifica os nomes e se já estão nas roles
            foreach (string username in usernames)
            {
                if (username.IndexOf(',') > 0)
                    throw new ArgumentException(MessageResources.UsernameError);
                foreach (string rolename in roleNames)
                    if (IsUserInRole(username, rolename))
                        throw new ProviderException(String.Format(MessageResources.RoleAlreadyExists, username, rolename));
            }

            SqlConnection cn = OpenConnection();
            if (cn == null)
                return;
            SqlTransaction tr = cn.BeginTransaction();
            SqlCommand cm = new SqlCommand(Scripts.AddRoles, cn, tr);
            cm.Parameters.Add("username", SqlDbType.VarChar);
            cm.Parameters.Add("rolename", SqlDbType.VarChar);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            try
            {
                foreach (string username in usernames)
                    foreach (string rolename in roleNames)
                    {
                        cm.Parameters["username"].Value = username;
                        cm.Parameters["rolename"].Value = rolename;
                        cm.ExecuteNonQuery();
                    }
                tr.Commit();
            }
            catch (SqlException e)
            {
                tr.Rollback();
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "AddUsersToRoles");
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }
        }

        public override void CreateRole(string roleName)
        {
            //verifica o nome
            if (roleName.IndexOf(',') > 0)
                throw new ArgumentException(MessageResources.RoleNameError);

            //verifica se já existe
            if (RoleExists(roleName))
                throw new ProviderException(MessageResources.RoleExists);

            SqlConnection cn = OpenConnection();
            if (cn == null)
                return;
            SqlCommand cm = new SqlCommand(Scripts.CreateRole, cn);
            cm.Parameters.AddWithValue("rolename", roleName);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            try
            {
                cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "CreateRole");
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            //verifica se existe
            if (!RoleExists(roleName))
                throw new ProviderException(MessageResources.RoleNotExists);

            // verifica se existem usuários na role
            if (throwOnPopulatedRole && GetUsersInRole(roleName).Length > 0)
                throw new ProviderException(MessageResources.RoleConstraint);

            SqlConnection cn = OpenConnection();
            if (cn == null)
                return false;
            SqlTransaction tr   = cn.BeginTransaction();
            SqlCommand cm       = null;
            try
            {
                cm = new SqlCommand(Scripts.RemoveLoginRoles, cn, tr);
                cm.Parameters.AddWithValue("rolename", roleName);
                cm.Parameters.AddWithValue("applicationName", ApplicationName);
                cm.ExecuteNonQuery();
                cm = new SqlCommand(Scripts.RemoveRoles, cn, tr);
                cm.Parameters.AddWithValue("rolename", roleName);
                cm.Parameters.AddWithValue("applicationName", applicationName);
                cm.ExecuteNonQuery();
                tr.Commit();
            }
            catch (SqlException e)
            {
                tr.Rollback();
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteRole");
                    return false;
                }
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }

            return true;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            SqlConnection cn = OpenConnection();
            if (cn == null)
                return new string[0];
            SqlCommand cm = new SqlCommand(Scripts.FindUsersInRole, cn);
            cm.Parameters.AddWithValue("usernameSearch", usernameToMatch);
            cm.Parameters.AddWithValue("roleName", roleName);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            string r = string.Empty;
            SqlDataReader dr = null;
            try
            {
                dr = cm.ExecuteReader();
                while (dr.Read())
                    r = String.Concat(r, dr.GetString(0), ",");
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "FindUsersInRole");
                else
                    throw e;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
                cm.Dispose();
                cn.Dispose();
            }

            if (r.Length > 0)
            {
                r = r.Substring(0, r.Length - 1);
                return r.Split(',');
            }
            return new string[0];
        }

        public override string[] GetAllRoles()
        {
            string r = string.Empty;
            SqlConnection cn = OpenConnection();
            if (cn == null)
                return new string[0];
            SqlCommand cm = new SqlCommand(Scripts.GetAllRoles, cn);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            SqlDataReader dr = null;
            try
            {
                dr = cm.ExecuteReader();
                while (dr.Read())
                    r = String.Concat(r, dr.GetString(0), ",");
                dr.Close();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "GetAllRoles");
                else
                    throw e;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
                cm.Dispose();
                cn.Dispose();
            }

            if (r.Length > 0)
            {
                r = r.Substring(0, r.Length - 1);
                return r.Split(',');
            }

            return new string[0];
        }

        public override string[] GetRolesForUser(string username)
        {
            string r = string.Empty;
            SqlConnection cn = OpenConnection();
            if (cn == null)
                return new string[0];
            SqlCommand cm = new SqlCommand(Scripts.GetRolesByUser, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            SqlDataReader dr = null;
            try
            {
                dr = cm.ExecuteReader();
                while (dr.Read())
                    r = String.Concat(r, dr.GetString(0), ",");
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "GetRolesForUser");
                else
                    throw e;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
                cm.Dispose();
                cn.Dispose();
            }

            if (r.Length > 0)
            {
                r = r.Substring(0, r.Length - 1);
                return r.Split(',');
            }
            return new string[0];
        }

        public override string[] GetUsersInRole(string roleName)
        {
            string r = string.Empty;
            SqlConnection cn = OpenConnection();
            if (cn == null)
                return new string[0];
            SqlCommand cm = new SqlCommand(Scripts.GetUsersInRole, cn);
            cm.Parameters.AddWithValue("rolename", roleName);
            cm.Parameters.AddWithValue("applicationName", ApplicationName);
            SqlDataReader dr = null;
            try
            {
                dr = cm.ExecuteReader();
                while (dr.Read())
                    r = String.Concat(r, dr.GetString(0), ",");
                dr.Close();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "GetUsersInRole");
                else
                    throw e;
            }
            finally
            {
                if (dr != null)
                    dr.Dispose();
                cm.Dispose();
                cn.Dispose();
            }

            if (r.Length > 0)
            {
                r = r.Substring(0, r.Length - 1);
                return r.Split(',');
            }
            return new string[0];
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            bool userIsInRole = false;
            SqlConnection cn  = OpenConnection();
            if (cn == null)
                return false;
            SqlCommand cm = new SqlCommand(Scripts.CountRolesByUser, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("rolename", roleName);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            try
            {
                int cnt = Convert.ToInt32(cm.ExecuteScalar());
                if (cnt > 0)
                    userIsInRole = true;
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "IsUserInRole");
                else
                    throw e;
            }
            finally
            {
                cn.Dispose();
            }
            return userIsInRole;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            // verifica os nomes
            foreach (string rolename in roleNames)
                if (!RoleExists(rolename))
                    throw new ProviderException(String.Format(MessageResources.RoleNotFound, rolename));

            // verifica se os usuários estão nas roles
            foreach (string username in usernames)
                foreach (string rolename in roleNames)
                    if (!IsUserInRole(username, rolename))
                        throw new ProviderException(String.Format(MessageResources.UserNotFoundInRole, username, rolename));

            SqlConnection cn = OpenConnection();
            if (cn == null)
                return;
            SqlTransaction tr = cn.BeginTransaction();
            SqlCommand cm = new SqlCommand(Scripts.RemoveUsersFromRoles, cn, tr);
            cm.Parameters.Add("username", SqlDbType.VarChar);
            cm.Parameters.Add("rolename", SqlDbType.VarChar);
            cm.Parameters.Add("applicationName", SqlDbType.VarChar).Value = applicationName;
            try
            {
                foreach (string username in usernames)
                {
                    foreach (string rolename in roleNames)
                    {
                        cm.Parameters["username"].Value = username;
                        cm.Parameters["rolename"].Value = rolename;
                        cm.ExecuteNonQuery();
                    }
                }
                tr.Commit();
            }
            catch (SqlException e)
            {
                tr.Rollback();
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "RemoveUsersFromRoles");
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }
        }

        public override bool RoleExists(string roleName)
        {
            bool exists = false;

            SqlConnection cn = OpenConnection();
            if (cn == null)
                return false;
            SqlCommand cm = new SqlCommand(Scripts.RoleExists, cn);
            cm.Parameters.AddWithValue("rolename", roleName);
            cm.Parameters.AddWithValue("applicationName", applicationName);
            try
            {
                int cnt = Convert.ToInt32(cm.ExecuteScalar());
                if (cnt > 0)
                    exists = true;
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                    WriteToEventLog(e, "RoleExists");
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }
            return exists;
        }
    }
}
