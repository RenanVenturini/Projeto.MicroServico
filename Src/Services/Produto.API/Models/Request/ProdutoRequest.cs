using System.ComponentModel.DataAnnotations;

namespace Produto.API.Models.Request
{
    public class ProdutoRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public decimal Preco { get; set; }
    }
}
