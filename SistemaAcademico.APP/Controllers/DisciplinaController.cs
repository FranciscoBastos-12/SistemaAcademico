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
    public class DisciplinaController : Controller
    {
        private DbContexto _contexto;

        public DisciplinaController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaDeDisciplinas = await _contexto.Disciplinas.AsNoTracking().ToListAsync();

            return View(listaDeDisciplinas);
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
                };

                await _contexto.Disciplinas.AddRangeAsync(novaDisciplina);
                await _contexto.SaveChangesAsync();
                await _contexto.DisposeAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditarDisciplina(Guid id)
        {
            Disciplina disciplinaModel = await _contexto.Disciplinas.Where(c => c.Id == id).FirstOrDefaultAsync();

            DisciplinaViewModel disciplinaEditavel = new DisciplinaViewModel()
            {
                Id = disciplinaModel.Id,
                Nome = disciplinaModel.Nome,
                Periodo = disciplinaModel.Periodo,
            };

            return View(disciplinaEditavel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarDisciplina(DisciplinaViewModel model)
        {
            Disciplina disciplinaEditada = await _contexto.Disciplinas.Where(d => d.Id == model.Id).FirstOrDefaultAsync();
            disciplinaEditada.Nome = model.Nome;
            disciplinaEditada.Periodo = model.Periodo;

            _contexto.Disciplinas.UpdateRange(disciplinaEditada);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoverDisciplina(Guid id)
        {
            Disciplina disciplinaModel = await _contexto.Disciplinas.Where(d => d.Id == id).FirstOrDefaultAsync();
            _contexto.Disciplinas.RemoveRange(disciplinaModel);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
