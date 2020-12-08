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
    public class ProfessorController : Controller
    {
        private DbContexto _contexto;

        public ProfessorController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaDeProfessores = await _contexto.Professores.AsNoTracking()
                                                                .Include(p => p.Contato)
                                                                .Include(p => p.RedesSociais)
                                                                .ToListAsync();
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
                    EmailPrimario = model.EmailPrimario.ToString(),
                    EmailSecundario = model.EmailSecundario.ToString()
                };

                Professor novoProfessor = new Professor()
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Cpf = model.Cpf,
                    DataCadastro = DateTime.Now,
                    DataNascimento = model.DataNascimento,
                    Contato = contato,
                    RedesSociais = redesSociais
                };

                await _contexto.Professores.AddRangeAsync(novoProfessor);
                await _contexto.SaveChangesAsync();
                await _contexto.DisposeAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditarProfessor(string cpf)
        {
            Professor professorModel = await _contexto.Professores.Where(p => p.Cpf == cpf).FirstOrDefaultAsync();
            Contato contatoModel = await _contexto.Contatos.Where(c => c.Id == professorModel.ContatoId).FirstOrDefaultAsync();
            RedesSociais redesSociaisModel = await _contexto.RedesSociais.Where(r => r.Id == professorModel.RedesSociaisId).FirstOrDefaultAsync();

            ProfessorViewModel professorEditavel = new ProfessorViewModel()
            {
                Id = professorModel.Id,
                Nome = professorModel.Nome,
                Cpf = professorModel.Cpf,
                DataNascimento = professorModel.DataNascimento,
                WhatsApp = contatoModel.WhatsApp,
                EmailPrimario = contatoModel.EmailPrimario,
                EmailSecundario = contatoModel.EmailSecundario,
                LinkedIn = redesSociaisModel.LinkedIn,
                GitHub = redesSociaisModel.GitHub
            };

            return View(professorEditavel);
        }

        [HttpPost]
        public async Task<IActionResult> EditarProfessor(ProfessorViewModel model)
        {
            Professor professorEditado = await _contexto.Professores.Where(p => p.Id == model.Id).FirstOrDefaultAsync();
            Contato contatoEditado = await _contexto.Contatos.Where(c => c.Id == professorEditado.ContatoId).FirstOrDefaultAsync();
            RedesSociais redesSociaisEditado = await _contexto.RedesSociais.Where(r => r.Id == professorEditado.RedesSociaisId).FirstOrDefaultAsync();

            contatoEditado.WhatsApp = model.WhatsApp;
            contatoEditado.EmailPrimario = model.EmailPrimario;
            contatoEditado.EmailSecundario = model.EmailSecundario;
            _contexto.Contatos.Update(contatoEditado);

            redesSociaisEditado.LinkedIn = model.LinkedIn;
            redesSociaisEditado.GitHub = model.GitHub;
            _contexto.RedesSociais.Update(redesSociaisEditado);

            professorEditado.Nome = model.Nome;
            professorEditado.Cpf = model.Cpf;
            professorEditado.DataNascimento = model.DataNascimento;
            professorEditado.Contato = contatoEditado;
            professorEditado.RedesSociais = redesSociaisEditado;
            _contexto.Professores.UpdateRange(professorEditado);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> RemoverProfessor(string cpf)
        {
            Professor professorModel = await _contexto.Professores.Where(p => p.Cpf == cpf).FirstOrDefaultAsync();
            _contexto.Professores.RemoveRange(professorModel);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
