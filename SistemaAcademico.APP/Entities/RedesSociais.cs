using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.APP.Entities
{
    public class RedesSociais
    {
        [Key]
        public Guid Id { get; set; }
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
    }
}
