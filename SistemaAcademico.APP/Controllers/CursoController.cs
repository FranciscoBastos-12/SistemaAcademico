using System;
using System.Linq;
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
        public async Task<IActionResult> Index()
        {
            var listaDeCursos = await _contexto.Cursos.AsNoTracking().ToListAsync();

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

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditarCurso(Guid id)
        {
            Curso cursoModel = await _contexto.Cursos.Where(c => c.Id == id).FirstOrDefaultAsync();

            CursoViewModel cursoEditavel = new CursoViewModel()
            {
                Id = cursoModel.Id,
                Nome = cursoModel.Nome,
                Duracao = cursoModel.Duracao,
            };

            return View(cursoEditavel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarCurso(CursoViewModel model)
        {
            Curso cursoEditado = await _contexto.Cursos.Where(c => c.Id == model.Id).FirstOrDefaultAsync();
            cursoEditado.Nome = model.Nome;
            cursoEditado.Duracao = model.Duracao;

            _contexto.Cursos.UpdateRange(cursoEditado);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoverCurso(Guid id)
        {
            Curso cursoModel = await _contexto.Cursos.Where(c => c.Id == id).FirstOrDefaultAsync();
            _contexto.Cursos.RemoveRange(cursoModel);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
