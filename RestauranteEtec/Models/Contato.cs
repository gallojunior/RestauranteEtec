using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteEtec.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
                
        [Required]
        public DateTime DataCadastro { get; set; }

        [Required]
        [StringLength(60)]
        public string NomePessoa { get; set; }

        [Required]
        [StringLength(100)]
        public string EmailPessoa { get; set; }

        [Required]
        [StringLength(100)]
        public string Assunto { get; set; }

        [Required]
        [StringLength(500)]
        public string Mensagem { get; set; }

        [Required]
        public byte Status { get; set; }

        [StringLength(500)]
        public string Retorno { get; set; }
    }
}
