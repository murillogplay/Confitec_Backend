using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using _Usuario = Dominio.Entidades.Usuario.Usuario;

namespace Infra.Persistencia.Map.Endereco
{
    public class MapUsuario : IEntityTypeConfiguration<_Usuario>
    {
        public void Configure(EntityTypeBuilder<_Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();            
            builder.Property(p => p.Nome).HasColumnName("Nome").IsRequired();
            builder.Property(p => p.SobreNome).HasColumnName("SobreNome").IsRequired(); 
            builder.Property(p => p.Email).HasColumnName("Email").IsRequired();
            builder.Property(p => p.DataNascimento).HasColumnName("DataNascimento").IsRequired();
            builder.Property(p => p.Escolaridade).HasColumnName("Escolaridade").IsRequired();
        }
    }
}
