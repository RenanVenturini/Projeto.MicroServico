using System.ComponentModel.DataAnnotations;

namespace Cliente.API.Models.Request
{
    public class ClienteRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public EnderecoRequest Endereco { get; set; }
    }
}
