using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.APP.Contexto;
using SistemaAcademico.APP.Entities;
using SistemaAcademico.APP.InterfacesRepositories;
using SistemaAcademico.APP.ViewModels;

namespace SistemaAcademico.APP.Controllers
{
    public class ProfessorController : Controller
    {
        private DbContexto _contexto;

        public ProfessorController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaDeProfessores()
        {
            var listaDeProfessores = await _contexto.Professores.ToListAsync();
            return View(listaDeProfessores);
        }

        [HttpGet]
        public IActionResult NovoProfessor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoProfessor(ProfessorViewModel model)
        {
            if (model != null)
            {
                RedesSociais redesSociais = new RedesSociais()
                {
                    Id = Guid.NewGuid(),
                    LinkedIn = model.LinkedIn,
                    GitHub = model.GitHub
                };

                Contato contato = new Contato()
                {
                    Id = Guid.NewGuid(),
                    WhatsApp = model.WhatsApp,
                    EmailPrimario = model.EmailPrimario,
                    EmailSecundario = model.EmailSecundario
                };

                Professor novoProfessor = new Professor()
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    DataCadastro = DateTime.Now,
                    DataNascimento = model.DataNascimento,
                    Contato = contato,
                    RedesSociais = redesSociais
                };

                await _contexto.Professores.AddRangeAsync(novoProfessor);
                await _contexto.SaveChangesAsync();
                await _contexto.DisposeAsync();
            }
            return RedirectToAction(nameof(ListaDeProfessores));
        }
    }
}
