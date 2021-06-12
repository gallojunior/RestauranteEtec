using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteEtec.Models
{
    [Table("Cargo")]
    public class Cargo
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o Nome do Cargo")]
        [StringLength(30, ErrorMessage = "O Nome não deve possuir mais que 30 caracteres")]
        public string Nome { get; set; }
    }
}
