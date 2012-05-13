using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class Pedido
   {
      public int codigoPedido { get; set; }
      public FormaPagamento pagamento { get; set; }
      public double totalPedido { get; set; }
      public ItemPedido itens { get; set; }

      public Pedido()
      {
         this.codigoPedido = 1000;
      }
      

   }
}
