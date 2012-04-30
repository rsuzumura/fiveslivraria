using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesLivraria.Data.Classes
{
    public class SimulaTEF
    {
        public long numeroCartao { get; set; }
        public int codigoVerificador { get; set; }
        public bool statusAprovacao { get; set; }

        public SimulaTEF(long numeroCartao, int codigoVerificador, bool statusAprovacao)
        {

        }

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
