using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademico.APP.Entities
{
    public class Questionario
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string QuaisLinguagensConhece { get; set; }
        [Required]
        public string OquePensaParaOFuturo { get; set; }
        [Required]
        public string QualConfiguracaoDoComputador { get; set; }
        [Required]
        public string IdeiasDeProjetos { get; set; }
        [Required]
        public string SugestoesDoAluno { get; set; }
        [Required]
        public string ObservacoesDoProfessor { get; set; }

        public Guid AlunoId { get; set; }
        public Guid ProfessorId { get; set; }
    }
}
