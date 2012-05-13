using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class SimulaTEF
   {
      public const string ARQUIVO = "c:\\fives\\files\\simulatef.ini";
      public long numeroCartao { get; set; }
      public int codigoVerificador {get; set; }
      public bool statusAprovacao {get; set;}
      protected int handle { get; set; }
      protected static string[,,] Dados;

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
