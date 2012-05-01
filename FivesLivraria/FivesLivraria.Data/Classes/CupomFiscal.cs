using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class CupomFiscal
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

      public CupomFiscal() { }

      public CupomFiscal(long cpf, ItemPedido item)
      {
         setCliente(cpf);
         setItem(item);
      }

      public void imprimeCupom()
      {
      }

      public void cancelaCupom()
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

      protected string cabecalho()
      {
         string cabec = "";

         cabec += "\n";
         cabec += " ";
         cabec += " ";
         cabec += " ";
         cabec += " ";
         cabec += " ";
         cabec += " ";
         cabec += " ";

            return cabec;
      }
   }
}
