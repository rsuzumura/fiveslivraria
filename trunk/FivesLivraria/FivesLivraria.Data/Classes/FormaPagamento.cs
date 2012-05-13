using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   [Serializable]
   public class FormaPagamento
   {
      public const int PAGAMENTODINHEIRO = 1;
      public const int PAGAMENTOCREDITO = 2;
      public const int PAGAMENTODEBITO = 3;

      public int id { get; set; }
      public double valor { get; set; }

      public FormaPagamento() 
      { 
      }

      public bool gravar()
      {
         return true;
      }

      public bool check()
      {
         return true;
      }
   }
}
