using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class TEF : FormaPagamento
   {
      
      public long numeroCartao { get; set; }
      public int codigoCartao { get; set; }
      public double valorTransacao { get; set; }
      public bool statusConexao { get; set; }

      public TEF()
      { 
      }

      public bool confirmarTransacao()
      {
         SimulaTEF sTef = new SimulaTEF(){
            numeroCartao = numeroCartao,
            codigoVerificador = codigoCartao
         };

         return sTef.statusAprovacao;
      }

      public bool cancelarTransacao()
      {
         return true;
      }
   }
}
