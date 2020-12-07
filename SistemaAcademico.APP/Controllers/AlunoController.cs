using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.APP.Contexto;
using SistemaAcademico.APP.Entities;
using SistemaAcademico.APP.ViewModels;
using System;
using System.Threading.Tasks;

namespace SistemaAcademico.APP.Controllers
{
    public class AlunoController : Controller
    {
        private DbContexto _contexto;

        public AlunoController(DbContexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListaDeAlunos()
        {
            var listaDeAlunos = await _contexto.Alunos.AsNoTracking()
                                                      .Include(a => a.Contato)
                                                      .Include(a => a.RedesSociais)
                                                      .ToListAsync();

            return View(listaDeAlunos);
        }

        [HttpGet]
        public IActionResult NovoAluno()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovoAluno(AlunoViewModel model)
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

                Aluno novoAluno = new Aluno()
                {
                    Id = Guid.NewGuid(),
                    NomeCompleto = model.Nome,
                    Cpf = model.Cpf,
                    DataCadastro = DateTime.Now,
                    DataNascimento = model.DataNascimento,
                    Contato = contato,
                    RedesSociais = redesSociais
                };

                await _contexto.Alunos.AddRangeAsync(novoAluno);
                await _contexto.SaveChangesAsync();
                await _contexto.DisposeAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
