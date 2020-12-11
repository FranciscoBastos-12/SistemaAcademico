using SistemaAcademico.APP.Entities;
using System;

namespace SistemaAcademico.APP.ViewModels
{
    public class DisciplinaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Periodo { get; set; }

        public Disciplina ToModel(DisciplinaViewModel disciplinaView)
        {
            Disciplina disciplinaModel = new Disciplina()
            {
                Id = disciplinaView.Id,
                Nome = disciplinaView.Nome,
                Periodo = disciplinaView.Periodo,
            };

            return disciplinaModel;
        }
    }
}
