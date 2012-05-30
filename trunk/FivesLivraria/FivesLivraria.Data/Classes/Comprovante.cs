using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FivesLivraria.Data.Classes
{
   [Serializable]
   public class Comprovante
   {
      // ------------------------------------------------------------------
      //  A referência das outras classes é para esta constante, alterando 
      // aqui a gravação em todos os demais será alterada também
      // ------------------------------------------------------------------
      public const string PATH = @"c:\fives\files\";
      public const string CNPJ = "12.345.678/0001-90";
      public const string IncE = "123.345.578.890";
      public const string IncM = "1.234.567-0";
      public const string ENDERECO = "Rua Baker Street, 110";
      public const string CEP = "CEP 01157-989";
      public const string linhaDIV = "--------------------------------------------------";

      public String texto { get; set; }
      public String nomeArquivo { get; set; }

      public Comprovante()
      {
      }

      public bool saveFile()
      {
         bool retorno = false;
         try
         {
            StreamWriter outArquivo = new StreamWriter(this.nomeArquivo);
            String[] strArray = this.texto.Split('\n');
            foreach ( String str in strArray )
               outArquivo.WriteLine( str);

            outArquivo.Close();
         }
         catch
         {
            retorno = false;
         }
         return retorno;
      }

      public bool openFile()
      {
         bool retorno = false;
         try
         {
            StreamReader inArquivo = new StreamReader(this.nomeArquivo);
            string line = "";

            while ((line = inArquivo.ReadLine()) != null)
            {
               this.texto += line;
            }

            inArquivo.Close();
         }
         catch
         {
            retorno = false;
         }
         return retorno;
      }

      public static String cabecalho( bool endereco = false )
      {
         String txtRetorno = "";
            txtRetorno += "             FIVE's LIVRARIA ME." + '\n';

               if (endereco)
               {
                  txtRetorno += "         " + ENDERECO + '\n';
                  txtRetorno += "             " + CEP + '\n';
               }

            txtRetorno += "CNPJ:  " + CNPJ + '\n';
            txtRetorno += "IE:    " + IncE + '\n';
            txtRetorno += "IM:    " + IncM + '\n';
            txtRetorno += linhaDIV + '\n';

         return txtRetorno;
      }
   }
}
