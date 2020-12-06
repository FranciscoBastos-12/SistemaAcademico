using SistemaAcademico.APP.Entities;
using System;

namespace SistemaAcademico.APP.ViewModels
{
    public class CursoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Duracao { get; set; }

        public Curso ToModel(CursoViewModel cursoView)
        {
            Curso cursoModel = new Curso()
            {
                Id = cursoView.Id,
                Nome = cursoView.Nome,
                Duracao = cursoView.Duracao
            };

            return cursoModel;
        }
    }
}
