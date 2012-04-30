using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class Produto
   {
      private int codigo;
      private string nome;
      private double valor;

      public Produto(int codigo, string nome, double valor)
      {
         this.codigo = codigo;
         this.nome = nome;
         this.valor = valor;
      }

      override
      public string ToString()
      {
         return "codigo: " + codigo + "\n nome: " + nome + "\n valor: " + valor;
      }
   }
}
