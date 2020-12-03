using SistemaAcademico.APP.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademico.APP.ViewModels
{
    public class ProfessorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public string WhatsApp { get; set; }
        public string EmailPrimario { get; set; }
        public string EmailSecundario { get; set; }

        public string LinkedIn { get; set; }
        public string GitHub { get; set; }

        public Professor ToModel(ProfessorViewModel professorView)
        {
            Professor professorModel = new Professor
            {
                Id = professorView.Id,
                Nome = professorView.Nome,
                DataNascimento = professorView.DataNascimento
            };
            professorModel.Contato.WhatsApp = professorView.WhatsApp;
            professorModel.Contato.EmailPrimario = professorView.EmailPrimario;
            professorModel.Contato.EmailSecundario = professorView.EmailSecundario;
            professorModel.RedesSociais.LinkedIn = professorView.LinkedIn;
            professorModel.RedesSociais.GitHub = professorView.GitHub;
            return professorModel;
        }
    }
}
