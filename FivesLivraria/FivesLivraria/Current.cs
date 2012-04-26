using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}