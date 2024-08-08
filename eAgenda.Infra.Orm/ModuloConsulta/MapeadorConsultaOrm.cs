using eAgenda.Dominio.ModuloConsulta;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eAgenda.Infra.Orm.ModuloConsulta
{
    public class MapeadorConsultaOrm : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("TBConsulta");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Titulo).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HoraInicio).IsRequired();
            builder.Property(x => x.HoraTermino).IsRequired();


            builder.HasOne(x => x.Medico)
                .WithMany(x => x.Consultas)
                .HasForeignKey(x => x.MedicoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
