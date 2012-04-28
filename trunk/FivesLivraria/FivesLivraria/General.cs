using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace FivesLivraria
{
    public class General
    {
        public static Dictionary<string, string> emailSettings 
        {
            get 
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                foreach (string item in ConfigurationManager.AppSettings["sendEmailSettings"].Split(';'))
                {
                    string[] values = item.Split('=');
                    dictionary.Add(values[0], values[1]);
                }
                return dictionary;
            }
        }
    }
}