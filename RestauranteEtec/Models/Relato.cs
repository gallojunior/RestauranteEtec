using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteEtec.Models
{
    [Table("Relato")]
    public class Relato
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Texto { get; set; }

        [StringLength(60)]
        public string NomePessoa { get; set; }

        [StringLength(200)]
        public string FotoPessoa { get; set; }

        [StringLength(30)]
        public string TipoPessoa { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        public bool ExibirHome { get; set; }

        public byte OrdemExibicao { get; set; }

        public bool Ativo { get; set; }
    }
}
