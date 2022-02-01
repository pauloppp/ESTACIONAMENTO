using ESTACIONAMENTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESTACIONAMENTO.Dados
{
    public class EstacionamentoContext : DbContext
    {
        public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options) : base(options) 
        {
        }

        public DbSet<Manobrista> Manobristas { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Manobra> Manobras { get; set; }
        public DbSet<Classificacao> Classificacoes { get; set; }


        // Resolve preenchimento automático da DataManobra sem percepção do Usuário.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataManobra") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataManobra").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataManobra").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
