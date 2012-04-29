using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    class SimulaTEF
    {
        public long numeroCartao { set; get; }
        public int codigoVerificador { set; get; }
        public bool statusAprovacao { set; get; }

        public bool verificarPasta()
        {
            return true;
        }

        protected bool avaliarCredito()
        {
            return true;
        }

        public void buscarDados()
        {

        }

        public void gravarStatus()
        {
        }

        public void transmitirResposta()
        {
        }

        protected bool validarDados()
        {
            return true;
        }
    }

}
