using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using FivesLivraria.Security.SqlScripts;

namespace FivesLivraria.Security
{
    public sealed class FivesMembershipProvider : MembershipProvider
    {
        private string eventSource      = "FivesMembershipProvider";
        private string eventLog         = "Application";
        private string exceptionMessage = MessageResources.GeneralExceptionMessage;
        private string connectionString;
        private MachineKeySection machineKey;

        string _applicationName;
        int _maxInvalidPasswordAttempts;
        int _passwordAttemptWindow;
        int _minRequiredNonAlphanumericCharacters;
        int _minRequiredPasswordLength;
        string _passwordStrengthRegularExpression;
        bool _enablePasswordReset;
        bool _enablePasswordRetrieval;
        bool _requiresQuestionAndAnswer;
        bool _requiresUniqueEmail;
        bool _writeExceptionsToEventLog;
        MembershipPasswordFormat _passwordFormat;

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;
            return configValue;
        }
        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source   = eventSource;
            log.Log      = eventLog;

            string message = string.Format(MessageResources.ServerCommunicationError, action, e.ToString());

            log.WriteEntry(message);
        }
        private string EncodePassword(string password)
        {
            if (password == null)
                password = string.Empty;
            string encodedPassword = password;
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException(MessageResources.PasswordFormatError);
            }
            return encodedPassword;
        }
        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException(MessageResources.HashCodeError);
                default:
                    throw new ProviderException(MessageResources.PasswordFormatError);
            }
            return password;
        }
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        private MembershipUser GetUserFromReader(SqlDataReader reader)
        {
            string username                 = reader.GetString(reader.GetOrdinal("lgi_Username"));
            string email                    = reader.GetString(reader.GetOrdinal("lgi_Email"));
            string passwordQuestion         = !reader.IsDBNull(reader.GetOrdinal("lgi_PasswordQuestion")) ? reader.GetString(reader.GetOrdinal("lgi_PasswordQuestion")) : string.Empty;
            string notes                    = !reader.IsDBNull(reader.GetOrdinal("lgi_Notes")) ? reader.GetString(reader.GetOrdinal("lgi_Notes")) : string.Empty;
            bool isApproved                 = reader.GetBoolean(reader.GetOrdinal("lgi_IsApproved"));
            bool isLockedOut                = reader.GetBoolean(reader.GetOrdinal("lgi_IsLockedOut"));
            DateTime creationDate           = reader.GetDateTime(reader.GetOrdinal("lgi_CreationDate"));
            DateTime lastLoginDate          = !reader.IsDBNull(reader.GetOrdinal("lgi_LastLoginDate")) ? reader.GetDateTime(reader.GetOrdinal("lgi_LastLoginDate")) : new DateTime();
            DateTime lastActivityDate       = reader.GetDateTime(reader.GetOrdinal("lgi_LastActivityDate"));
            DateTime lastPasswordChangeDate = reader.GetDateTime(reader.GetOrdinal("lgi_LastPasswordChangeDate"));
            DateTime lastLockedOutDate      = !reader.IsDBNull(reader.GetOrdinal("lgi_LastLockedOutDate")) ? reader.GetDateTime(reader.GetOrdinal("lgi_LastLockedOutDate")) : new DateTime();
            MembershipUser u = new MembershipUser(this.Name,
                                                  username,
                                                  null,
                                                  email,
                                                  passwordQuestion,
                                                  notes,
                                                  isApproved,
                                                  isLockedOut,
                                                  creationDate,
                                                  lastLoginDate,
                                                  lastActivityDate,
                                                  lastPasswordChangeDate,
                                                  lastLockedOutDate);

            return u;
        }
        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;
            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            return (pass1 == pass2);
        }
        private void UpdateFailureCount(string username, string failureType)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.LoadFailureCounters, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            SqlDataReader reader = null;
            DateTime windowStart = new DateTime();
            int failureCount     = 0;
            
            try
            {
                cn.Open();
                reader = cm.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    if (failureType == "password")
                    {
                        failureCount = reader.GetInt32(0);
                        windowStart  = reader.GetDateTime(1);
                    }
                    if (failureType == "passwordAnswer")
                    {
                        failureCount = reader.GetInt32(2);
                        windowStart  = reader.GetDateTime(3);
                    }
                }
                reader.Close();
                DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);
                
                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    // primeiro erro se senha ou fora da janela 
                    // começa uma nova contagem em 1 e marca o começo da janela
                    if (failureType == "password")
                        cm.CommandText = Scripts.PasswordErrorCounter;
                    if (failureType == "passwordAnswer")
                        cm.CommandText = Scripts.PasswordAnswerErrorCounter;
                    cm.Parameters.Clear();
                    cm.Parameters.AddWithValue("count", 1);
                    cm.Parameters.AddWithValue("windowStart", DateTime.Now);
                    cm.Parameters.AddWithValue("username", username);
                    cm.Parameters.AddWithValue("applicationName", _applicationName);
                    if (cm.ExecuteNonQuery() < 0)
                        throw new ProviderException(MessageResources.ErrorCounterFailUpdate);
                }
                else
                {
                    if (failureCount++ >= MaxInvalidPasswordAttempts)
                    {
                        // tentativas passaram do limite. trava o login
                        cm.CommandText = Scripts.BlockLogin;
                        cm.Parameters.Clear();
                        cm.Parameters.AddWithValue("isLockedOut", 1);
                        cm.Parameters.AddWithValue("lastLockedOutDate", DateTime.Now);
                        cm.Parameters.AddWithValue("username", username);
                        cm.Parameters.AddWithValue("applicationName", _applicationName);
                        if (cm.ExecuteNonQuery() < 0)
                            throw new ProviderException(MessageResources.ErrorBlockAccount);
                    }
                    else
                    {
                        // tentativas não passaram do limite. incrementa
                        if (failureType == "password")
                            cm.CommandText = Scripts.UpdateErrorPassword;
                        if (failureType == "passwordAnswer")
                            cm.CommandText = Scripts.UpdateErrorPasswordAnswer;
                        cm.Parameters.Clear();
                        cm.Parameters.AddWithValue("count", failureCount);
                        cm.Parameters.AddWithValue("username", username);
                        cm.Parameters.AddWithValue("applicationName", _applicationName);
                        if (cm.ExecuteNonQuery() < 0)
                            throw new ProviderException(MessageResources.ErrorCounterFailUpdate);
                    }
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateFailureCount");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                cn.Dispose();
            }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            //verifica os parâmetros de configuração foram passados
            if (config == null)
                throw new ArgumentNullException("config");

            //assume o nome padrão, se não foi passado
            if (name == null || name.Length == 0)
                name = "FivesMembershipProvider";

            //assume a descrição padrão, se não foi passada
            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Five'S MS SQL Server Membership Provider");
            }

            //inicializa a base class
            base.Initialize(name, config);

            //captura os parâmetros passados nas propriedades
            _applicationName                        = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _maxInvalidPasswordAttempts             = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _passwordAttemptWindow                  = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            _minRequiredNonAlphanumericCharacters   = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            _minRequiredPasswordLength              = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            _passwordStrengthRegularExpression      = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            _enablePasswordReset                    = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            _enablePasswordRetrieval                = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            _requiresQuestionAndAnswer              = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            _requiresUniqueEmail                    = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            _writeExceptionsToEventLog              = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            //assume o formato padrão para senhas
            string pf = config["passwordFormat"];
            if (pf == null)
                pf = "Hashed";
            switch (pf)
            {
                case "Hashed":
                    _passwordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    _passwordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    _passwordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException(MessageResources.PasswordFormatError);
            }

            //captura a string de conexão ao banco
            ConnectionStringSettings ConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];
            if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
                throw new ProviderException(MessageResources.ConnectionStringError);
            connectionString = ConnectionStringSettings.ConnectionString;

            //carrega a chave de criptografia da máquina. se for um farm, os servidores devem ter o mesmo machineKey
            //System.IO.FileInfo configFile = new System.IO.FileInfo(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //VirtualDirectoryMapping vdm = new VirtualDirectoryMapping(configFile.DirectoryName, true, configFile.Name);
            //WebConfigurationFileMap wcfm = new WebConfigurationFileMap();
            //wcfm.VirtualDirectories.Add("/", vdm);
            //Configuration cfg = WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");
            //machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

            machineKey = (MachineKeySection)ConfigurationManager.GetSection("system.web/machineKey");
        }

        #region OVERRIDE PROPERTIES
        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }
        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _minRequiredNonAlphanumericCharacters; }
        }
        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }
        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _passwordFormat; }
        }
        public override string PasswordStrengthRegularExpression
        {
            get { return _passwordStrengthRegularExpression; }
        }
        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }
        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }
        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }
        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }
        #endregion

        public override string ResetPassword(string username, string answer)
        {
            //verifica se pode resetar senha
            if (!EnablePasswordReset)
                throw new NotSupportedException(MessageResources.PasswordResetDisabled);

            ////nova senha
            string newPassword = "password";
            
            SqlConnection cn     = new SqlConnection(connectionString);
            int rowsAffected     = 0;
            SqlDataReader reader = null;
            try
            {
                cn.Open();
                SqlCommand updateCmd = new SqlCommand(Scripts.ResetPassword, cn);
                updateCmd.Parameters.AddWithValue("password", EncodePassword(newPassword));
                updateCmd.Parameters.AddWithValue("question", "pergunta");
                updateCmd.Parameters.AddWithValue("answer", EncodePassword("resposta"));
                updateCmd.Parameters.AddWithValue("isLockedOut", 0);
                updateCmd.Parameters.AddWithValue("lastPasswordChangeDate", DateTime.Now);
                updateCmd.Parameters.AddWithValue("username", username);
                updateCmd.Parameters.AddWithValue("applicationName", _applicationName);
                rowsAffected = updateCmd.ExecuteNonQuery();
                updateCmd.Dispose();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetPassword");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                //cm.Dispose();
                cn.Dispose();
            }
            return newPassword;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword))
                return false;

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, true);
            OnValidatingPassword(args);
            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException(MessageResources.CancelChangePassword);
                        
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.ChangePassword, cn);
            cm.Parameters.AddWithValue("password", EncodePassword(newPassword));
            cm.Parameters.AddWithValue("lastPasswordChangeDate", DateTime.Now);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            int rowsAffected = 0;
            try
            {
                cn.Open();
                rowsAffected = cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            if (!ValidateUser(username, password))
                return false;

            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.ChangeQuestionAndAnswer, cn);
            cm.Parameters.AddWithValue("question", newPasswordQuestion);
            cm.Parameters.AddWithValue("answer", EncodePassword(newPasswordAnswer));
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            int rowsAffected = 0;
            try
            {
                cn.Open();
                rowsAffected = cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePasswordQuestionAndAnswer");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            //lança o evento de validação de senha e verifica se foi cancelado
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            //verifica se o email deve ser único e se há outro usuário com este email
            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            //verifica se já existe o login e cria o novo usuário
            MembershipUser u = GetUser(username, false);
            if (u == null)
            {
                DateTime createDate = DateTime.Now;
                if (providerUserKey == null)
                    providerUserKey = Guid.NewGuid();
                else
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }

                if (password.Length < _minRequiredPasswordLength)
                    throw new Exception(string.Format("Tamanho de senha inválido, tamanho mínimo da senha => {0} caracteres", MinRequiredPasswordLength));

                SqlConnection cn = new SqlConnection(connectionString);
                SqlCommand cm    = new SqlCommand(Scripts.Create, cn);
                cm.Parameters.AddWithValue("login", (Guid)providerUserKey);
                cm.Parameters.AddWithValue("username", username);
                cm.Parameters.AddWithValue("password", EncodePassword(password));
                cm.Parameters.AddWithValue("email", email);
                cm.Parameters.AddWithValue("passwordQuestion", passwordQuestion);
                cm.Parameters.AddWithValue("passwordAnswer", EncodePassword(passwordAnswer));
                cm.Parameters.AddWithValue("isApproved", (isApproved ? 1 : 0));
                cm.Parameters.AddWithValue("notes", "");
                cm.Parameters.AddWithValue("creationDate", createDate);
                cm.Parameters.AddWithValue("lastPasswordChangeDate", createDate);
                cm.Parameters.AddWithValue("lastActivityDate", createDate);
                cm.Parameters.AddWithValue("applicationName", _applicationName);
                cm.Parameters.AddWithValue("isLockedOut", 0);
                cm.Parameters.AddWithValue("lastLockedOutDate", createDate);
                cm.Parameters.AddWithValue("failedAttemptCount", 0);
                cm.Parameters.AddWithValue("failedAttemptWindow", createDate);
                cm.Parameters.AddWithValue("failedAnswerAttemptCount", 0);
                cm.Parameters.AddWithValue("failedAnswerAttemptWindow", createDate);
                try
                {
                    cn.Open();
                    int recAdded = cm.ExecuteNonQuery();
                    if (recAdded > 0)
                        status = MembershipCreateStatus.Success;
                    else
                        status = MembershipCreateStatus.UserRejected;
                }
                catch (SqlException e)
                {
                    if (_writeExceptionsToEventLog)
                        WriteToEventLog(e, "CreateUser");
                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    cm.Dispose();
                    cn.Dispose();
                }
                return GetUser(username, false);
            }
            else
                status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.Delete, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            int rowsAffected = 0;
            try
            {
                cn.Open();
                rowsAffected = cm.ExecuteNonQuery();
                if (deleteAllRelatedData)
                {
                    cm.CommandText = Scripts.DeleteRoles;
                    cm.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteUser");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.CountUsersByEmail, cn);
            cm.Parameters.AddWithValue("emailSearch", emailToMatch);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            MembershipUserCollection users = new MembershipUserCollection();
            SqlDataReader reader = null;
            totalRecords = 0;
            try
            {
                cn.Open();
                totalRecords = Convert.ToInt32(cm.ExecuteScalar());
                if (totalRecords <= 0)
                    return users;
                cm.Parameters.AddWithValue("pageIndex", pageIndex);
                cm.Parameters.AddWithValue("pageSize", pageSize);
                cm.CommandText = Scripts.ListByEmail;
                reader = cm.ExecuteReader();
                
                while (reader.Read())
                {                    
                    MembershipUser u = GetUserFromReader(reader);
                    users.Add(u);
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByEmail");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                cn.Dispose();
            }
            return users;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.CountUsersByName, cn);
            cm.Parameters.AddWithValue("usernameSearch", usernameToMatch);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            
            MembershipUserCollection users = new MembershipUserCollection();
            SqlDataReader reader = null;
            try
            {
                cn.Open();
                totalRecords = Convert.ToInt32(cm.ExecuteScalar());
                if (totalRecords <= 0) { return users; }
                cm.CommandText = Scripts.ListByName;
                cm.Parameters.AddWithValue("pageIndex", pageIndex);
                cm.Parameters.AddWithValue("pageSize", pageSize);
                reader = cm.ExecuteReader();
                
                while (reader.Read())
                {   
                    MembershipUser u = GetUserFromReader(reader);
                    users.Add(u);
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByName");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                cn.Dispose();
            }
            return users;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {            
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.CountAll, cn);
            cm.Parameters.AddWithValue("applicationName", ApplicationName);
            MembershipUserCollection users = new MembershipUserCollection();
            SqlDataReader reader = null;
            totalRecords = 0;
            try
            {
                cn.Open();
                totalRecords = Convert.ToInt32(cm.ExecuteScalar());
                if (totalRecords <= 0)
                    return users;
                cm.CommandText = Scripts.ListAll;
                reader = cm.ExecuteReader();
                int counter     = 0;
                int startIndex  = pageSize * pageIndex;
                int endIndex    = startIndex + pageSize - 1;
                while (reader.Read())
                {
                    if ((counter >= startIndex) && (counter < endIndex + 1))
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }
                    if (counter >= endIndex)
                        cm.Cancel();
                    counter++;
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetAllUsers");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                cn.Dispose();
            }
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            TimeSpan onlineSpan  = new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
            SqlConnection cn     = new SqlConnection(connectionString);
            SqlCommand cmd       = new SqlCommand(Scripts.GetUsersOnline, cn);
            cmd.Parameters.AddWithValue("compareDate", compareTime);
            cmd.Parameters.AddWithValue("applicationName", _applicationName);
            
            int numOnline = 0;
            try
            {
                cn.Open();
                numOnline = (int)cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetNumberOfUsersOnline");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cn.Dispose();
            }
            return numOnline;

        }

        public override string GetPassword(string username, string answer)
        {
            //verifica se a recuperação de senha está ligada
            if (!EnablePasswordRetrieval)
                throw new ProviderException(MessageResources.PasswordRecoveryDisabled);

            //verifica o formato hash
            if (PasswordFormat == MembershipPasswordFormat.Hashed)
                throw new ProviderException(MessageResources.HashCodeError);

            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.GetPassword, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            string password         = string.Empty;
            string passwordAnswer   = string.Empty;
            SqlDataReader reader    = null;
            try
            {
                cn.Open();
                reader = cm.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    if (reader.GetBoolean(2))
                        throw new MembershipPasswordException(MessageResources.BlockedAccount);
                    password        = reader.GetString(0);
                    passwordAnswer  = reader.GetString(1);
                }
                else
                    throw new MembershipPasswordException(MessageResources.LoginNotFound);
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetPassword");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                cm.Dispose();
                cn.Dispose();
            }

            //verifica se é necessária a verificação de pergunta secreta
            if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            {
                UpdateFailureCount(username, "passwordAnswer");
                throw new MembershipPasswordException(MessageResources.IncorrectPasswordAnswer);
            }

            //retorna a senha
            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
                password = UnEncodePassword(password);

            return password;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.GetByUsername, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            
            MembershipUser u     = null;
            SqlDataReader reader = null;
            try
            {
                cn.Open();
                reader = cm.ExecuteReader();
                if (reader.Read())
                    u = GetUserFromReader(reader);
                reader.Close();
                if (u != null && userIsOnline)
                {
                    SqlCommand updateCmd = new SqlCommand(Scripts.UpdateLastActivity, cn);
                    updateCmd.Parameters.AddWithValue("lastActivityDate", DateTime.Now);
                    updateCmd.Parameters.AddWithValue("username", username);
                    updateCmd.Parameters.AddWithValue("applicationName", _applicationName);
                    updateCmd.ExecuteNonQuery();
                    updateCmd.Dispose();
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUser(String, Boolean)");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                cm.Dispose();
                cn.Close();
            }

            return u;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.GetUsernameByEmail, cn);
            cm.Parameters.AddWithValue("email", email);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            string username = string.Empty;
            try
            {
                cn.Open();
                username = (string)cm.ExecuteScalar();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUserNameByEmail");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cm.Dispose();
                cn.Dispose();
            }

            if (username == null)
                username = string.Empty;

            return username;
        }

        public override bool UnlockUser(string userName)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.UnlockUser, cn);
            cm.Parameters.AddWithValue("lastLockedOutDate", DateTime.Now);
            cm.Parameters.AddWithValue("username", userName);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            int rowsAffected = 0;
            try
            {
                cn.Open();
                rowsAffected = cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UnlockUser");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cn.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override void UpdateUser(MembershipUser user)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.Update, cn);
            cm.Parameters.AddWithValue("email", user.Email);
            cm.Parameters.AddWithValue("notes", user.Comment);
            cm.Parameters.AddWithValue("isApproved", user.IsApproved);
            cm.Parameters.AddWithValue("username", user.UserName);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            try
            {
                cn.Open();
                cm.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateUser");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                cn.Close();
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand cm    = new SqlCommand(Scripts.ValidateUser, cn);
            cm.Parameters.AddWithValue("username", username);
            cm.Parameters.AddWithValue("applicationName", _applicationName);
            SqlDataReader reader = null;
            bool isApproved      = false;
            string pwd = string.Empty;
            try
            {
                cn.Open();
                reader = cm.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    pwd         = reader.GetString(0);
                    isApproved  = reader.GetBoolean(1);
                }
                else
                    return false;
                reader.Close();
                if (CheckPassword(password, pwd))
                {
                    if (isApproved)
                    {
                        isValid = true;
                        SqlCommand updateCmd = new SqlCommand(Scripts.UpdateLastLoginDate, cn);
                        updateCmd.Parameters.AddWithValue("lastLoginDate", DateTime.Now);
                        updateCmd.Parameters.AddWithValue("username", username);
                        updateCmd.Parameters.AddWithValue("applicationName", _applicationName);
                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    cn.Dispose();
                    UpdateFailureCount(username, "password");
                }
            }
            catch (SqlException e)
            {
                if (_writeExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ValidateUser");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                cn.Close();
            }

            return isValid;
        }
    }
}
