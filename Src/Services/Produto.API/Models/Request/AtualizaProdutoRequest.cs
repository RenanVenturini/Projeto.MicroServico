using System.ComponentModel.DataAnnotations;

namespace Produto.API.Models.Request
{
    public class AtualizaProdutoRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public decimal Preco { get; set; }
    }
}
