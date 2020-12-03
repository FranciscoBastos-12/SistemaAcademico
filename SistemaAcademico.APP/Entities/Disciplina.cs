using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.APP.Entities
{
    public class Disciplina
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }
        [Required, MaxLength(20)]
        public string Periodo { get; set; }
        
        public Guid ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual ICollection<Aluno> Alunos { get; set; }
    }
}
