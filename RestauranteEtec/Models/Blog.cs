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

        [Required(ErrorMessage = "Informe o Título do Blog")]
        [StringLength(100, ErrorMessage = "O Título não deve possuir mais que 100 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Informe o Texto do Blog")]
        [StringLength(8000)]
        public string Texto { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DataCadastro { get; set; }

        [StringLength(100)]
        public string Imagem { get; set; }

    }
}
