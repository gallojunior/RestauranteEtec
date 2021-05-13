using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteEtec.Models
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(8000)]
        public string Descricao { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [StringLength(100)]
        public string Imagem { get; set; }

    }
}
