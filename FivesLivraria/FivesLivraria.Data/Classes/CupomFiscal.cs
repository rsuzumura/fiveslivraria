using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
   public class CupomFiscal
   {
      public ItemPedido item { get; set; }
      public long cliente { get; set; }

      public CupomFiscal()
      {
      }

      public CupomFiscal(ItemPedido item ) 
      {
         this.item = item;
      }

      public CupomFiscal(long cpf, ItemPedido item)
      {
         this.cliente = cpf;
         this.item = item;
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

      public string cabecalho()
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
