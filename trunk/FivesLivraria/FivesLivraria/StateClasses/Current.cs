using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FivesLivraria.Data;

namespace FivesLivraria
{
    public static class Current
    {
        public static int UserId
        {
            get
            {
                object o = HttpContext.Current.Session["userId"];
                if (o == null)
                    return 0;
                else
                    return (int)o;
            }
            set
            {
                HttpContext.Current.Session["userId"] = value;
            }
        }
        public static String UserName
        {
            get
            {
                object o = HttpContext.Current.Session["userName"];
                if (o == null)
                    return string.Empty;
                else
                    return (string)o;
            }
            set
            {
                HttpContext.Current.Session["userName"] = value;
            }
        }
        public static Acesso AcessoCaixa
        {
            get 
            {
                object o = HttpContext.Current.Session["Acesso"];
                if (o == null)
                    return new Acesso();
                else
                    return (Acesso)o;
            }
            set
            {
                HttpContext.Current.Session["Acesso"] = value;
            }
        }
    }
}