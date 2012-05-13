using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace FivesLivraria.Data.Classes
{
   public class SimulaTEF
   {
      public const string ARQUIVO = "c:\\fives\\files\\simulatef.dat";

      public long numeroCartao { get; set; }
      public int codigoVerificador {get; set; }
      public bool statusAprovacao {get; set;}
      protected ArrayList Dados;

      public SimulaTEF()
      {
      }

      private void loadDados()
      {
         Dados = new ArrayList();
         StreamReader inFile = new StreamReader("c:\\test.txt");
         string sLine = "";

         while (sLine != null)
         {
            sLine = inFile.ReadLine();
            if (sLine != null)
               Dados.Add(sLine);
         }
      }

      private bool confirmar()
      {
         string keyCard = "";
         Dados.Sort();
         // ----------------------------------------------------------
         // inserir iteração no objeto Dados 
         // procurando pela chave recebida nos atributos da classe
         // ----------------------------------------------------------
         return true;
      }
   }
}
