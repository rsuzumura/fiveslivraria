using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace FivesLivraria.Data
{
    public class Cliente
    {
        public SqlInt32 idCliente { get; set; }
        public SqlString nmCliente { get; set; }
        public SqlInt32 idUsuario { get; set; }
    }

    public class Pessoa : Cliente
    {
        public SqlInt32 idPessoa { get; set; }
        public SqlString cpf { get; set; }
        public SqlString rg { get; set; }
        public SqlDateTime dtNascimento { get; set; }
        public SqlString nmMae { get; set; }
    }

    public class Empresa : Cliente
    {
        public SqlInt32 idEmpresa { get; set; }
        public SqlString nmRazaoSocial { get; set; }
        public SqlString cnpj { get; set; }
        public SqlString inscricaoEstadual { get; set; }
        public SqlString inscricaoMunicipal { get; set; }
    }
}
