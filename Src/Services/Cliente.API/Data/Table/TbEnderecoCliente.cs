﻿namespace Cliente.API.Data.Table
{
    public class TbEnderecoCliente
    {
        public int EnderecoId { get; set; }
        public int ClienteId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public TbCliente Cliente { get; set; }
    }
}
