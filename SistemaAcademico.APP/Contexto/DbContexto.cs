using Microsoft.EntityFrameworkCore;
using SistemaAcademico.APP.Entities;

namespace SistemaAcademico.APP.Contexto
{
    public class DbContexto : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Questionario> Questionarios { get; set; }
        public DbSet<RedesSociais> RedesSociais { get; set; }
        
        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}
    }
}
