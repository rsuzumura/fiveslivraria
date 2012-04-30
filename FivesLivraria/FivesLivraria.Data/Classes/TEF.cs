using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class TEF : FormaPagamento
   {
      
      private long numeroCartao;
         public void setNumeroCartao(long numeroCartao)
            { this.numeroCartao = numeroCartao; }
         protected long geNumeroCartao()
            { return numeroCartao; }

      private int codigoCartao;
         public void setCodigoCartao(int codigoCartao)
            { this.codigoCartao = codigoCartao; }
         protected long geCodigoCartao()
            { return codigoCartao; }

      public TEF()
      { }

      public TEF(int id, double valor, long numeroCartao, int codigoCartao)
      {
         setId(id); // método da classe superior
         setValor(valor); // método da classe superior
         setNumeroCartao(numeroCartao);
         setCodigoCartao(codigoCartao);
      }
   }
}
