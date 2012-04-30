using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class ItemPedido
   {
      private List<Produto> produtos;
      private int[] qtde;
      private double valor;
      private double desconto;

      public ItemPedido(List<Produto> produtos, double valor, double desconto)
      {
         this.produtos = produtos;
         this.valor = valor;
         this.desconto = desconto;
      }

      public void AddProduto(int codigo, string nome, double valor)
      {
         this.produtos.Add(new Produto(codigo, nome, valor));
      }
   }
}
