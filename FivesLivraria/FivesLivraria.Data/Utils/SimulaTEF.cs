using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class SimulaTEF
   {
      private long numeroCartao { public set; public get; }
      private int codigoVerificador { public set; public get; }
      private bool statusAprovacao { public set; public get; }

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
