using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class TEF : FormaPagamento
   {
      public const string linhaDiv = "==================================================";
      public const string CNPJ = "11.222.333/0001-99";
      
      public long numeroCartao { get; set; }
      public int codigoCartao { get; set; }
      public double valorTransacao { get; set; }
      public bool statusConexao { get; set; }
      public long codigoCupom { get; set; }
      public long codigoAutorizacao { get; set; }

      public TEF()
      { 
      }

      public bool confirmarTransacao()
      {
         SimulaTEF sTef = new SimulaTEF {
            numeroCartao = numeroCartao,
            codigoVerificador = codigoCartao,
            valorTransacao = valorTransacao
         };

         return sTef.statusAprovacao;
      }

      public bool cancelarTransacao()
      {
         return true;
      }

      public string imprimir()
      {
         string operacao = (id == PAGAMENTOCREDITO ? "CREDITO" : "DEBITO");
         string txtImprimir = "";

            txtImprimir += Comprovante.cabecalho(true);
            txtImprimir += "            COMPROVANTE " + operacao + '\n';
            txtImprimir += "                1ª VIA" + '\n';
            txtImprimir += " " + '\n';
            txtImprimir += "COO DOCUMENTO VINCULADO:                " + codigoCupom.ToString() + '\n';
            txtImprimir += "VALOR DA COMPRA  R$                     " + valorTransacao.ToString() + '\n';
            txtImprimir += "VALOR DO PAGAMENTO  R$                  " + valorTransacao.ToString() + '\n';
            txtImprimir += Comprovante.linhaDIV + '\n';

            txtImprimir += "        comprovante de " + operacao + '\n';
            txtImprimir += "CRED FACIL" + '\n';
            txtImprimir += "        Av. Paulista, 534, São Paulo" + '\n';
            txtImprimir += "term: " + 0001 + "         doc: " + codigoCupom.ToString() + "     lote: " + 0000 + '\n';
            txtImprimir += DateTime.Today.ToString() + " " + DateTime.Now.ToString() + "     aut: " + codigoAutorizacao.ToString() + "     onl-X" + '\n';
            txtImprimir += "venda " + operacao + "  a vista" + '\n';
            txtImprimir += "valor venda:                " + valorTransacao.ToString() + '\n';
            txtImprimir += '\n';
            txtImprimir += Comprovante.linhaDIV + '\n';

            txtImprimir += "EMULADOR DE ECF                     " + /* usuariologado +*/ '\n';
            txtImprimir += "V. 001                              " + /* id maquina + */ '\n';
            txtImprimir += "                           " + DateTime.Today.ToString() + " " + DateTime.Now.ToString() + '\n';

            txtImprimir += linhaDiv + '\n';
            txtImprimir += "                2ª via - cliente" + '\n';
            txtImprimir += linhaDiv + '\n';

            txtImprimir += "        comprovante de " + operacao + '\n';
            txtImprimir += "CRED FACIL" + '\n';
            txtImprimir += "CNPJ: " + CNPJ + '\n';
            txtImprimir += "" + '\n';
            txtImprimir += Comprovante.cabecalho();
            txtImprimir += "" + '\n';

            txtImprimir += "term: " + 0001 + "         doc: " + codigoCupom.ToString() + "     lote: " + 0000 + '\n';
            txtImprimir += DateTime.Today.ToString() + " " + DateTime.Now.ToString() + "     aut: " + codigoAutorizacao.ToString() + "     onl-X" + '\n';
            txtImprimir += "venda " + operacao + "  a vista" + '\n';
            txtImprimir += "valor venda:                " + valorTransacao.ToString() + '\n';

         return txtImprimir;
      }

      protected void criarAutorizacao()
      {

      }
   }
}
