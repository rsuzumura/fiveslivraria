using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    [Serializable]
    public class ItemPedido
    {
       public List<Produto> produtos { get; set; }
       public int qtde { get; set; }
       public double valor { get; set; }
       public double desconto { get; set; }
       public int numeroPeiddo { get; set; }


        public ItemPedido()
        {
            produtos = new List<Produto>();
            qtde = 0;
            valor = 0.00;
            desconto = 0.00;
        }

        public ItemPedido(List<Produto> produtos, double valor, double desconto)
        {
            this.produtos = produtos;
            this.valor = valor;
            this.desconto = desconto;
            this.qtde = produtos.Count;
        }

        public void AddProduto(int codigo, string nome, double valor)
        {
            Produto prodtemp = new Produto 
            {
                idProduto  = codigo,
                nmTitulo    = nome, 
                vlPreco  = (decimal)valor
            };
           this.produtos.Add(prodtemp);
           this.qtde++;
        }

        public string ToString(int n)
        {
           return produtos[n].ToString();
        }

        public int Count()
        {
           return produtos.Count;
        }

        public double totalizarItens()
        {
           double valorTotal = 0.00;

           foreach (Produto k in produtos)
           {
              if (k.qtdProduto.IsNull)
                 k.qtdProduto = 1;
              valorTotal = (double)(k.qtdProduto.Value * k.vlPreco.Value);
           }
           return valorTotal;
        }
    }
}
