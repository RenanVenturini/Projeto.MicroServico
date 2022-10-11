namespace Cliente.API.Models.Response
{
    public class ClienteResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public EnderecoResponse Endereco { get; set; }
    }
}
