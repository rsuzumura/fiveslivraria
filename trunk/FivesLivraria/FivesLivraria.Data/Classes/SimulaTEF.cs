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
      public const string ARQUIVO = @"c:\fives\files\simulatef.txt";  // configurar conforme o servidor

      public long numeroCartao { get; set; }
      public int codigoVerificador {get; set; }
      public bool statusAprovacao {get; set;}
      public double valorTransacao { get; set; }
      protected List<String> Dados;

      public SimulaTEF()
      {
         this.loadDados();
         this.statusAprovacao = this.confirmar();
      }

      private void loadDados()
      {
         Dados = new List<string>();
         StreamReader inFile = new StreamReader(ARQUIVO);
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
         bool retorno = false;
         string keyCard = (numeroCartao.ToString()) + (codigoVerificador.ToString());
         int tamDados = Dados.Count;
         
         if ( tamDados > 0)
         {
              Dados.Sort();
            // ----------------------------------------------------------
            // inserir iteração no objeto Dados 
            // procurando pela chave recebida nos atributos da classe
            // ----------------------------------------------------------
            for (int k = 0; k < Dados.Count; ++k)
            {
               string[] stream = Dados[k].Split(';');
               if (stream[0]+stream[1] == keyCard && double.Parse(stream[2]) >= valorTransacao)
               {
                  retorno = true;
                  break;
               }
            }
         }
         return retorno;
      }
   }
}
