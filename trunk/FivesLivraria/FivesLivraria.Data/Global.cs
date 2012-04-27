using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FivesLivraria.Data
{
    public class Global
    {
        public static string ConnectionString
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["FivesConnectionString"].ConnectionString;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
