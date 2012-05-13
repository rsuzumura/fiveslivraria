using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FivesLivraria.Data.Classes
{
   public class Comprovante
   {

      public const string CNPJ = "12.345.678/0001-90";
      public const string IncE = "123.345.578.890";
      public const string IncM = "1.234.567-0";
      public const string ENDERECO = "Rua Baker Street, 110";
      public const string CEP = "CEP 01157-989";
      public const string linhaDIV = "--------------------------------------------------";

      public string texto { get; set; }
      public string nomeArquivo { get; set; }

      public Comprovante()
      {
      }

      public bool gravar()
      {
         bool retorno = false;
         try
         {
            StreamWriter outArquivo = new StreamWriter(nomeArquivo);
            outArquivo.WriteLine(texto);
         }
         catch
         {
            retorno = false;
         }
         return retorno;
      }

      public bool buscar()
      {
         bool retorno = false;
         try
         {
            StreamReader inArquivo = new StreamReader(nomeArquivo);
            string line = "";

            while ((line = inArquivo.ReadLine()) != null)
            {
               this.texto += line;
            }
         }
         catch
         {
            retorno = false;
         }
         return retorno;
      }

      public static string cabecalho( bool endereco = false )
      {
         string txtRetorno = "";
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
