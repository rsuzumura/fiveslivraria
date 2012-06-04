using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    [Serializable]
    public class TEF : FormaPagamento
    {
        public const string linhaDiv = "==================================================";
        public const string CNPJ = "11.222.333/0001-99";

        public string numeroCartao { get; set; }
        public string codigoCartao { get; set; }
        public double valorTransacao { get; set; }
        public bool statusConexao { get; set; }
        public long codigoCupom { get; set; }
        public long codigoAutorizacao { get; set; }

        public TEF()
        {
        }

        public bool confirmarTransacao()
        {
            SimulaTEF sTef = new SimulaTEF
            {
                numeroCartao = numeroCartao,
                codigoVerificador = codigoCartao,
                valorTransacao = valorTransacao
            };

            if (sTef.confirmar())
                this.criarAutorizacao();

            return sTef.statusAprovacao;
        }

        public bool cancelarTransacao()
        {
            return true;
        }

        public String imprimir()
        {
            string operacao = (id == PAGAMENTOCREDITO ? "CREDITO" : "DEBITO");
            String txtImprimir = "";

            txtImprimir += Comprovante.cabecalho(true);
            txtImprimir += "            COMPROVANTE " + operacao + '\n';
            txtImprimir += "                1ª VIA" + '\n';
            txtImprimir += " " + '\n';
            txtImprimir += "COO DOCUMENTO VINCULADO:                " + codigoCupom.ToString() + '\n';
            txtImprimir += "VALOR DA COMPRA  R$                     " + valorTransacao.ToString("#0.00") + '\n';
            txtImprimir += "VALOR DO PAGAMENTO  R$                  " + valorTransacao.ToString("#0.00") + '\n';
            txtImprimir += Comprovante.linhaDIV + '\n';

            txtImprimir += "        comprovante de " + operacao + '\n';
            txtImprimir += "CRED FACIL" + '\n';
            txtImprimir += "        Av. Paulista, 534, São Paulo" + '\n';
            txtImprimir += "term: 0001         doc: " + codigoCupom.ToString() + "     lote: 0000" + '\n';
            txtImprimir += DateTime.Now.ToString() + "     aut: " + codigoAutorizacao.ToString("#000000") + "     onl-X" + '\n';
            txtImprimir += "venda " + operacao + "  a vista" + '\n';
            txtImprimir += "valor venda:                " + valorTransacao.ToString() + '\n';
            txtImprimir += '\n';
            txtImprimir += Comprovante.linhaDIV + '\n';

            txtImprimir += "EMULADOR DE ECF                     " + /* usuariologado +*/ '\n';
            txtImprimir += "V. 001                              " + /* id maquina + */ '\n';
            txtImprimir += "                           " + DateTime.Now.ToString() + '\n';

            txtImprimir += linhaDiv + '\n';
            txtImprimir += "                2ª via - cliente" + '\n';
            txtImprimir += linhaDiv + '\n';

            txtImprimir += "        comprovante de " + operacao + '\n';
            txtImprimir += "CRED FACIL" + '\n';
            txtImprimir += "CNPJ: " + CNPJ + '\n';
            txtImprimir += "" + '\n';
            txtImprimir += Comprovante.cabecalho();
            txtImprimir += "" + '\n';

            txtImprimir += "term: 0001         doc: " + codigoCupom.ToString("#000000") + "     lote: 0000" + '\n';
            txtImprimir += DateTime.Now.ToString() + "     aut: " + codigoAutorizacao.ToString() + "     onl-X" + '\n';
            txtImprimir += "venda " + operacao + "  a vista" + '\n';
            txtImprimir += "valor venda:                " + valorTransacao.ToString("#0.00") + '\n';

            return txtImprimir;
        }

        protected void criarAutorizacao()
        {
            Random rdn = new Random();
            long cod = rdn.Next(10000, 99999);
            if (FormaPagamento.PAGAMENTODEBITO == id)
                cod += 200000;
            else
                cod += 100000;

            this.codigoAutorizacao = cod;
        }

        public void saveFile()
        {
            String dados = this.imprimir();
            string fileName = AppDomain.CurrentDomain.BaseDirectory;
            if (!fileName.EndsWith("\\"))
                fileName += "\\Files\\";
            else
                fileName += "Files\\";
            fileName += codigoAutorizacao.ToString();

            Comprovante comp = new Comprovante
            {
                texto = dados,
                nomeArquivo = fileName
            };
            comp.saveFile();
        }
    }
}
