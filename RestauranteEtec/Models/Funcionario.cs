using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RestauranteEtec.Models
{
    [Table("Funcionario")]
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o Nome do Funcionário")]
        [StringLength(100, ErrorMessage = "O Nome não deve possuir mais que 100 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(500)]
        public string Descricao { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "Informe o Nome do Funcionário")]
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        [Display(Name = "Foto")]
        [StringLength(200)]
        public string Foto { get; set; }

        [Display(Name = "Exibir Home")]
        [Required]
        public bool ExibirHome { get; set; }

        [Display(Name = "Ordem de Exibição")]
        [Required]
        public byte OrdemExibicao { get; set; }

        public bool Ativo { get; set; }

    }
}
