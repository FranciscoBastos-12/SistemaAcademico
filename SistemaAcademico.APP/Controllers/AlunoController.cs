using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.APP.Contexto;
using SistemaAcademico.APP.Entities;
using SistemaAcademico.APP.ViewModels;
using System;
using System.Linq;
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
        public async Task<IActionResult> Index()
        {
            var listaDeAlunos = await _contexto.Alunos.AsNoTracking()
                                                      .Include(a => a.Contato)
                                                      .Include(a => a.RedesSociais)
                                                      .ToListAsync();
            return View();
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

        [HttpPost]
        public async Task<IActionResult> EditarAluno(string cpf)
        {
            Aluno alunoModel = await _contexto.Alunos.Where(a => a.Cpf == cpf).FirstOrDefaultAsync();
            Contato contatoModel = await _contexto.Contatos.Where(c => c.Id == alunoModel.ContatoId).FirstOrDefaultAsync();
            RedesSociais redesSociaisModel = await _contexto.RedesSociais.Where(r => r.Id == alunoModel.RedesSociaisId).FirstOrDefaultAsync();

            AlunoViewModel alunoEditavel = new AlunoViewModel()
            {
                Id = alunoModel.Id,
                Nome = alunoModel.NomeCompleto,
                Cpf = alunoModel.Cpf,
                DataNascimento = alunoModel.DataNascimento,
                WhatsApp = contatoModel.WhatsApp,
                EmailPrimario = contatoModel.EmailPrimario,
                EmailSecundario = contatoModel.EmailSecundario,
                LinkedIn = redesSociaisModel.LinkedIn,
                GitHub = redesSociaisModel.GitHub
            };

            return View(alunoEditavel);
        }

        [HttpGet]
        public async Task<IActionResult> RemoverAluno(string cpf)
        {
            Aluno alunoModel = await _contexto.Alunos.Where(a => a.Cpf == cpf).FirstOrDefaultAsync();
            _contexto.Alunos.RemoveRange(alunoModel);
            await _contexto.SaveChangesAsync();
            await _contexto.DisposeAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
