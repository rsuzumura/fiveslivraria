using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    [Serializable]
    public class CupomFiscal
    {
        public ItemPedido item { get; set; }
        public long cliente { get; set; }
        public double valorPagamento { get; set; }
        public int idFormaPgto { get; set; }
        public int numContadorFiscal { get; set; }
        public int numComprovanteTEF { get; set; }
        public double sumVendasGerais { get; set; }
        public double sumPgtoDinheiro { get; set; }
        public double sumPgtoCredito { get; set; }
        public double sumPgtoDebito { get; set; }
        public long codigoCupom { get; set; }


        public CupomFiscal()
        {
        }

        public void setItem(ItemPedido item)
        {
            this.item = item;
        }

        public void setCliente(long cliente)
        {
            this.cliente = cliente;
        }

        public void leituraX()
        {
            string fileName = "leitura_" + DateTime.Now.ToString();
            string result = modelo(false);
            Comprovante oper = new Comprovante
            {
                texto = result,
                nomeArquivo = fileName
            };
            oper.saveFile();
        }

        public void reducaoZ()
        {
            string fileName = "reducao_" + DateTime.Now.ToString();
            string result = modelo(true);
            Comprovante oper = new Comprovante
            {
                texto = result,
                nomeArquivo = fileName
            };
            oper.saveFile();
        }

        public String modelo(bool reducao)
        {
            String txtModelo = "";
            String operacao = (reducao ? "REDUÇÃO Z" : "LEITURA X");

            txtModelo += Comprovante.cabecalho() + '\n';
            txtModelo += DateTime.Now.ToString() + "                COD: " + codigoCupom.ToString() + '\n';
            txtModelo += "                " + operacao + '\n';
            txtModelo += "MOVIMENTO DIA: " + DateTime.Today.ToString() + '\n';

            txtModelo += "            CONTADORES" + '\n';
            txtModelo += "Geral de operações não fiscal:          000000" + '\n';
            txtModelo += "Contador de reduções Z:                 000000" + '\n';
            txtModelo += "Contador de cupom fiscal:               " + numContadorFiscal.ToString() + '\n';
            txtModelo += "Comprovante de Credito ou Debito:       " + numComprovanteTEF.ToString() + '\n';
            txtModelo += "Cumpom Fiscal Cancelado:                000000" + '\n';

            txtModelo += "            TOTALIZADORES" + '\n';
            txtModelo += "Venda Bruta Diária:              " + sumVendasGerais.ToString("#0.00") + '\n';
            txtModelo += "Total de ICMS:                   0000,00" + '\n';
            txtModelo += "Venda Líquida:                   " + sumVendasGerais.ToString("#0.00") + '\n';

            txtModelo += "            MEIOS DE PAGAMENTO" + '\n';
            txtModelo += "Nº   MEIO PAGAMENTO         VALOR ACUMULADO (R$)" + /*  + */'\n';
            txtModelo += "01    DINHEIRO                  " + sumPgtoDinheiro.ToString("#0.00") + '\n';
            txtModelo += "02    CARTÃO DE CRÉDITO         " + sumPgtoCredito.ToString("#0.00") + '\n';
            txtModelo += "03    CARTÃO DE DÉBITO          " + sumPgtoDebito.ToString("#0.00") + '\n';

            txtModelo += Comprovante.linhaDIV + '\n';
            txtModelo += "   CAIXA: " + /* usuariologado + */ '\n';

            return txtModelo;
        }

        public String imprimeCupom()
        {
            String txtVenda = "";

            txtVenda += Comprovante.cabecalho();
            txtVenda += DateTime.Now.ToString() + "                  COO: " + codigoCupom.ToString() + '\n';
            txtVenda += "            CUPOM FISCAL" + '\n';
            txtVenda += "Cliente: " + cliente.ToString(@"000\.000\.000-00") + '\n';
            txtVenda += "item codprod        descricao" + '\n';
            txtVenda += "qtd   um     vlr unit       TIPOTX      vlr item  " + '\n';
            txtVenda += "" + '\n';
            for (int pos = 0; pos < item.Count(); ++pos)
            {
                Produto prodTmp = item.produtos[pos];
                prodTmp.qtdProduto = 1;
                double vlrUnit = (double)prodTmp.vlPreco.Value;
                double vlrTotal = (double)(vlrUnit * prodTmp.qtdProduto.Value);
                txtVenda += (pos + 1).ToString() + "  " + prodTmp.idProduto.ToString() + "  "
                         + prodTmp.dsProduto.ToString() + '\n';
                txtVenda += ">> " + prodTmp.qtdProduto.ToString() + "  UN      " + vlrUnit.ToString("#0.00") + "                  "
                         + "       " + vlrTotal.ToString("#0.00") + '\n';
            }
            txtVenda += "                          ------------------------" + '\n';
            txtVenda += "  TOTAL  R$                           " + item.totalizarItens().ToString("#0.00") + '\n';
            txtVenda += "FORMAPGTO                             " + /* valorPagamento + */'\n'; // identificar via get na interface?
            txtVenda += "TROCO                                 " + /* diferenca */ '\n'; // calcular diferenca dinheiro superior ao total
            txtVenda += Comprovante.linhaDIV + '\n';
            txtVenda += "EMULADOR DE ECF                     " + /* usuariologado +*/ '\n';
            txtVenda += "V. 001                              " + /* id maquina + */ '\n';
            txtVenda += "                            " + DateTime.Now.ToString() + " " + '\n';

            // ----------------------------------------------------------
            //  Executa atualização das variáveis de totalizadores
            // a serem usados na 
            // ----------------------------------------------------------
            this.atualizaDados();
            return txtVenda;
        }
        protected void atualizaDados()
        {
            double valorTransacao = item.totalizarItens();
            numContadorFiscal++;
            sumVendasGerais += valorTransacao;
            switch (idFormaPgto)
            {
                case FormaPagamento.PAGAMENTODINHEIRO:
                    sumPgtoDinheiro += valorTransacao;
                    break;
                case FormaPagamento.PAGAMENTOCREDITO:
                    sumPgtoCredito += valorTransacao;
                    numComprovanteTEF++;
                    break;
                case FormaPagamento.PAGAMENTODEBITO:
                    sumPgtoDebito += valorTransacao;
                    numComprovanteTEF++;
                    break;
            }
        }

        public void gerarCodigo()
        {
            Random rdn = new Random();
            long cod = rdn.Next(100000, 999999);
            this.codigoCupom = cod;
        }

        public void saveFile()
        {
            String dados = this.imprimeCupom();
            string fileName = AppDomain.CurrentDomain.BaseDirectory;
            if (!fileName.EndsWith("\\"))
                fileName += "\\Files\\";
            else
                fileName += "Files\\";
            fileName += this.codigoCupom;
            Comprovante comp = new Comprovante
            {
                texto = dados,
                nomeArquivo = fileName
            };
            comp.saveFile();
        }
    }
}
