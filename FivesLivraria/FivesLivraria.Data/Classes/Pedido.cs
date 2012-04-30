using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class Pedido
   {
      private FormaPagamento pagamento;
         public void setPagamento(int id, double valor)
            {
               if (id != FormaPagamento.PAGAMENTODINHEIRO)
                  this.pagamento = new TEF(id, valor);
               else
                  this.pagamento = new FormaPagamento(id, valor);
            }

         public FormaPagamento getPagamento()
            { return pagamento; }

      private int codigoPedido;
      private double totalPedido;

      public Pedido()
      {

      }

   }
}
