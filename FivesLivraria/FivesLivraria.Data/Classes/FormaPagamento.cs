using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class FormaPagamento
   {
      public const int PAGAMENTODINHEIRO = 1;
      public const int PAGAMENTOCREDITO = 2;
      public const int PAGAMENTODEBITO = 3;

      public int id { get; set; }
      public double valor { get; set; }

      // construtor sem argumentos 
      // para inicialização posterior
      public FormaPagamento() { }

      public FormaPagamento(int id, double valor)
      {
         this.id = id;
         this.valor = valor;
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
