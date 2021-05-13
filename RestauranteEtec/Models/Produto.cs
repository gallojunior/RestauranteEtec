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

        [Required]
        [StringLength(60)]
        public string Nome { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        [StringLength(200)]
        public string Foto { get; set; }

        public bool ExibirHome { get; set; }

        public bool Ativo { get; set; }
    }
}
