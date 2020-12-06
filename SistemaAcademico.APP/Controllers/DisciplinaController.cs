using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.APP.Contexto;
using SistemaAcademico.APP.Entities;
using SistemaAcademico.APP.ViewModels;

namespace SistemaAcademico.APP.Controllers
{
    public class DisciplinaController : Controller
    {
        private DbContexto _contexto;

        public DisciplinaController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaDeDisciplinas()
        {
            var listaDeAlunos = await _contexto.Disciplinas.AsNoTracking().ToListAsync();

            return View(listaDeAlunos);
        }

        [HttpGet]
        public IActionResult NovaDisciplina()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaDisciplina(DisciplinaViewModel model)
        {
            if (model != null)
            {
                Disciplina novaDisciplina = new Disciplina()
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Periodo = model.Periodo,
                    ProfessorId = model.ProfessorId
                };

                await _contexto.Disciplinas.AddRangeAsync(novaDisciplina);
                await _contexto.SaveChangesAsync();
                await _contexto.DisposeAsync();
            }

            return RedirectToAction(nameof(ListaDeDisciplinas));
        }
    }
}
