using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class SimulaTEF
   {
      private long numeroCartao;
         public void setNumeroCartao( long numeroCartao )
            { this.numeroCartao = numeroCartao; }
         public long getNumeroCartao()
            { return numeroCartao;  }

      private int codigoVerificador;
         public void setCodigoVerificador( int codigo )
            { this.codigoVerificador = codigo; }
         public int getCodigoVerificador()
            { return codigoVerificador; }
      
      private bool statusAprovacao;
         public void setStatus( bool status)
            { this.statusAprovacao = status; }
         public bool isAprovado()
            { return statusAprovacao; }

      public SimulaTEF(long numeroCartao, int codigoVerificador, bool statusAprovacao)
      {

      }

      public bool verificarPasta()
      {
         return true;
      }

      protected bool avaliarCredito()
      {
         return true;
      }

      public void buscarDados()
      {

      }

      public void gravarStatus()
      {
      }

      public void transmitirResposta()
      {
      }

      protected bool validarDados()
      {
         return true;
      }
   }
}
