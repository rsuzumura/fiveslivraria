﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace FivesLivraria.Data.Classes
{
    [Serializable]
    public class SimulaTEF
    {
        public const string ARQUIVO = "simulatef.txt";  // configurar conforme o servidor
        // -------------------------------------------------
        // Utiliza como base para validação um arquivo com 
        // lista, simulando numeros e códigos de cartão
        // o formato é:
        // numero_cartao;digito_verificador;valor_limite
        // -------------------------------------------------

        public string numeroCartao { get; set; }
        public string codigoVerificador { get; set; }
        public bool statusAprovacao { get; set; }
        public double valorTransacao { get; set; }
        protected List<String> Dados;

        public SimulaTEF()
        {
            this.loadDados();
        }

        private void loadDados()
        {
            Dados = new List<string>();
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!path.EndsWith("\\"))
                path += "\\Files\\";
            else
                path += "Files\\";

            StreamReader inFile = new StreamReader(path + ARQUIVO);
            string sLine = "";

            while (sLine != null)
            {
                sLine = inFile.ReadLine();
                if (sLine != null)
                    Dados.Add(sLine);
            }
        }

        public bool confirmar()
        {
            bool retorno = false;
            string keyCard = (numeroCartao + codigoVerificador);
            int tamDados = Dados.Count;

            if (tamDados > 0)
            {
                Dados.Sort();
                // ----------------------------------------------------------
                // inserir iteração no objeto Dados 
                // procurando pela chave recebida nos atributos da classe
                // ----------------------------------------------------------
                for (int k = 0; k < Dados.Count; ++k)
                {
                    string[] stream = Dados[k].Split(';');
                    if (stream[0] + stream[1] == keyCard && double.Parse(stream[2]) >= valorTransacao)
                    {
                        retorno = true;
                        break;
                    }
                }
            }
            this.statusAprovacao = retorno;
            return retorno;
        }
    }
}