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

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(30)]
        public string NomeResumido { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        [StringLength(200)]
        public string Foto { get; set; }

        public bool ExibirHome { get; set; }

        public byte OrdemExibicao { get; set; }

        public bool Ativo { get; set; }

    }
}
