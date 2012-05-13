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

      public void setItem(ItemPedido item)
      {
         this.item = item;
      }

      public void setCliente(long cliente)
      {
         this.cliente = cliente;
      }

      public void imprimeCupom()
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
