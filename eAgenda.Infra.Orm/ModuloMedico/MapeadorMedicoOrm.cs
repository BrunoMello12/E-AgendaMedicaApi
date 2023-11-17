using eAgenda.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infra.Orm.ModuloMedico
{
    public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.ToTable("TBMedico");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(300)").IsRequired();
            builder.Property(x => x.Telefone).HasColumnType("varchar(20)").IsRequired();
            builder.Property(x => x.Disponivel).HasDefaultValue(true).IsRequired();
            builder.Property(x => x.CRM).HasColumnType("varchar(20)").IsRequired();

            builder.HasMany(x => x.Cirurgias)
                 .WithMany(x => x.Medicos)
                 .UsingEntity(j => j.ToTable("TBMedicoCirurgia"));
        }
    }
}
