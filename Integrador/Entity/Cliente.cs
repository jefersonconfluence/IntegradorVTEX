using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrador.Entity
{
    class Cliente
    {
        //ID do registro
        public int id { get; set; }

        //ID do cliente
        public int userId { get; set; }

        //nome
        public string firstName { get; set; }

        //sobrenome
        public string lastName { get; set; }

        //e-mail do cliente
        public string email { get; set; }

        //Tipo do documento
        public string documentType { get; set; } //Brasil = "cpf"

        //Documento (CPF)
        public string document { get; set; }

        //Telefone
        public string homePhone { get; set; }

        
        // TODO Se o cliente for pessoa jurídica, além dos dados acima, preencher:

        //define se o cliente é pessoa jurídica
        public Boolean isCorporate { get; set; } //"true"

        //razão social
        public string corporateName { get; set; }

        //nome fantasia
        public string tradeName { get; set; }

        //inscrição estadual
        public string stateRegistration { get; set; }

        //CNPJ
        public string corporateDocument { get; set; }

        public Endereco endereco { get; set; }
    }
}
