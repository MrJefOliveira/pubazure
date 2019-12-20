using Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace VideoLocadora.Contexto
{
    public class EFContext : DbContext
    {
        public EFContext() : base("VideoLocadora")
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Locacao> Locacao { get; set; }
    }
}