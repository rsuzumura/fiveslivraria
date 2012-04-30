using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class Pedido
   {
      private FormaPagamento pagamento;
      private NotaFiscal notaFiscal;

      public Pedido()
      {
         //
         // TODO: Add constructor logic here
         //
      }

      public void setPagamento(int id, double valor)
      {
         if (id != FormaPagamento.PAGAMENTODINHEIRO)
            this.pagamento = new TEF(id, valor);

      }
   }
}
