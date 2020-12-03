using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.APP.Contexto;
using SistemaAcademico.APP.Entities;
using SistemaAcademico.APP.ViewModels;

namespace SistemaAcademico.APP.Controllers
{
    public class CursoController : Controller
    {
        private DbContexto _contexto;

        public CursoController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaDeCursos()
        {
            var listaDeCursos = await _contexto.Cursos.AsNoTracking()
                                                      .Include(c => c.Disciplinas)
                                                      .ToListAsync();

            return View(listaDeCursos);
        }

        [HttpGet]
        public IActionResult NovoCurso()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoCurso(CursoViewModel model)
        {
            if (model != null)
            {
                Curso novoCurso = new Curso()
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Duracao = model.Duracao
                };

                await _contexto.Cursos.AddRangeAsync(novoCurso);
                await _contexto.SaveChangesAsync();
                await _contexto.DisposeAsync();
            }

            return RedirectToAction(nameof(ListaDeCursos));
        }
    }
}
