using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class NotaFiscal
   {
      private long cliente;
      public void setCliente(long cpf)
      { this.cliente = cpf; }
      public long getCliente()
      { return cliente; }

      private ItemPedido item;
      public void setItem(ItemPedido item)
      { this.item = item; }
      public ItemPedido getItem()
      { return item; }

      private string linhaImpressao;
      public void setLinhaImpressao(string linhaImpressao)
      { this.linhaImpressao = linhaImpressao; }
      public string getLinhaImpressao()
      { return linhaImpressao; }

      public NotaFiscal() { }

      public NotaFiscal(long cpf, ItemPedido item)
      {
         setCliente(cpf);
         setItem(item);
      }

      public void imprimeNota()
      {
      }

      public void cancelaNota()
      {
      }

      public void leituraX()
      {
      }

      public void reducaoZ()
      {
      }

      public void imprimirLinha()
      {
      }
   }
}
