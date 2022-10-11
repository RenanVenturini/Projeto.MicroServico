using System.ComponentModel.DataAnnotations;

namespace Cliente.API.Models.Request
{
    public class AtualizarClienteRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public int ClienteId { get; set; }
      
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public AtualizarEnderecoRequest Endereco { get; set; }
    }
}
