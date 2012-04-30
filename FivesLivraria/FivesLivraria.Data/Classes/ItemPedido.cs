﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    public class ItemPedido
    {
        private List<Produto> produtos;
        private int[] qtde;
        private double valor;
        private double desconto;

        public ItemPedido()
        { }

        public ItemPedido(List<Produto> produtos, double valor, double desconto)
        {
            this.produtos = produtos;
            this.valor = valor;
            this.desconto = desconto;
        }

        public void AddProduto(int codigo, string nome, double valor)
        {
            this.produtos.Add(new Produto(codigo, nome, valor));
        }

        public string ToString(int n)
        {
            string result = string.Empty;
            for (int i = 0; i < n; i++)
                result +=  produtos[n].ToString();
            return result;
        }
    }
}