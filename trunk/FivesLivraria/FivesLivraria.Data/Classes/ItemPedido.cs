using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    public class ItemPedido
    {
        private List<Produto> produtos;
        private int qtde;
        private double valor;
        private double desconto;

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
            Produto prodtemp = new Produto()
            {
                codigo = codigo,
                nome = nome,
                valor = valor
            };
            this.produtos.Add((Produto)prodtemp);
            this.qtde++;
        }

        public string ToString(int n)
        {
            string result = string.Empty;
            for (int i = 0; i < n; i++)
                result += produtos[n].ToString();
            return result;
        }
    }
}
