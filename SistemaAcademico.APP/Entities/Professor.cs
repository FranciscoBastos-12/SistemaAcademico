using SistemaAcademico.APP.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.APP.Entities
{
    public class Professor
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(11)]
        public string Cpf { get; set; }
        [Required]
        public DateTime DataCadastro { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }

        public Guid ContatoId { get; set; }
        public virtual Contato Contato { get; set; }
        public Guid RedesSociaisId { get; set; }
        public virtual RedesSociais RedesSociais { get; set; }
    }
}
