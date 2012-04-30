using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class FormaPagamento
   {
      public static const int PAGAMENTODINHEIRO = 1;
      public static const int PAGAMENTOCREDITO = 2;
      public static const int PAGAMENTODEBITO = 3;

      protected int id;
      public void setId(int id)
      { this.id = id; }
      public int getId()
      { return id; }

      protected double valor;
      protected void setValor(double valor)
      { this.valor = valor; }
      public double getValor()
      { return valor; }

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
