namespace Cliente.API.Data.Table
{
    public class TbCliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public TbEnderecoCliente Endereco { get; set; }
    }
}
