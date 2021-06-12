using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteEtec.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(60, ErrorMessage = "O Nome da Categoria deve possuir no máximo 60 caracteres")]
        public string Nome { get; set; }

        [StringLength(500, ErrorMessage = "A Descrição deve possuir no máximo 500 caracteres")]
        public string Descricao { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        [StringLength(200)]
        public string Foto { get; set; }

        public bool ExibirHome { get; set; }

        public bool Ativo { get; set; }
    }
}
