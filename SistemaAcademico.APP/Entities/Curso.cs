using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.APP.Entities
{
    public class Curso
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(20)]
        public string Duracao { get; set; }

        public virtual ICollection<Disciplina> Disciplinas { get; set; }
    }
}
