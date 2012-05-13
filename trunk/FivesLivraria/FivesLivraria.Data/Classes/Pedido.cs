using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   [Serializable]
   public class Pedido
   {
      public int codigoPedido { get; set; }
      public FormaPagamento pagamento { get; set; }
      public double valorTotalPedido { get; set; }
      public ItemPedido itens { get; set; }

      public Pedido()
      {
         this.codigoPedido = 1000;
      }

      public double getValorTotalPedido()
      {
         this.valorTotalPedido = this.itens.totalizarItens();
         return valorTotalPedido;
      }
   }
}
