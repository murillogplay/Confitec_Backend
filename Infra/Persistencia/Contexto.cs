using Dominio.Entidades.Usuario;
using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using System;
using System.Linq;

namespace Infra.Persistencia
{
    public class Contexto : DbContext
    {
        public Contexto() : base() { }
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<Usuario> Usuario; 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) entityType.SetTableName(entityType.DisplayName());
            var cascadeFKs = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Restrict);
            foreach (var fk in cascadeFKs) fk.DeleteBehavior = DeleteBehavior.Restrict;
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(" + (property.GetMaxLength() > 0 ? property.GetMaxLength().ToString() : "100") + ")");

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        { 
            return base.SaveChanges();
        }

         
    }
}
