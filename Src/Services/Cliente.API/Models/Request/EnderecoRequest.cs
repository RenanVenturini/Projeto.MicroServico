using System.ComponentModel.DataAnnotations;

namespace Cliente.API.Models.Request
{
    public class EnderecoRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Logradouro { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Cidade { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Bairro { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string UF { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string CEP { get; set; }
    }
}
