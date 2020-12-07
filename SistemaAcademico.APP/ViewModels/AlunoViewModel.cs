using SistemaAcademico.APP.Entities;
using System;

namespace SistemaAcademico.APP.ViewModels
{
    public class AlunoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }

        public string WhatsApp { get; set; }
        public string EmailPrimario { get; set; }
        public string EmailSecundario { get; set; }

        public string LinkedIn { get; set; }
        public string GitHub { get; set; }

        public Aluno ToModel(AlunoViewModel alunoView)
        {
            Aluno alunoModel = new Aluno
            {
                Id = alunoView.Id,
                NomeCompleto = alunoView.Nome,
                DataNascimento = alunoView.DataNascimento
            };

            alunoModel.Contato.WhatsApp = alunoView.WhatsApp;
            alunoModel.Contato.EmailPrimario = alunoView.EmailPrimario;
            alunoModel.Contato.EmailSecundario = alunoView.EmailSecundario;
            alunoModel.RedesSociais.LinkedIn = alunoView.LinkedIn;
            alunoModel.RedesSociais.GitHub = alunoView.GitHub;
            
            return alunoModel;
        }
    }
}
