using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.APP.Entities
{
    public class Contato
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(11)]
        public string WhatsApp { get; set; }
        [Required, MaxLength(100), DataType(DataType.EmailAddress)]
        public string EmailPrimario { get; set; }
        public string EmailSecundario { get; set; }
    }
}
