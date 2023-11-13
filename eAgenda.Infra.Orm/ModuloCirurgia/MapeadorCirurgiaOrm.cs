using eAgenda.Dominio.ModuloCirurgia;
using eAgenda.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eAgenda.Infra.Orm.ModuloCirurgia
{
    internal class MapeadorCirurgiaOrm : IEntityTypeConfiguration<Cirurgia>
    {
        public void Configure(EntityTypeBuilder<Cirurgia> builder)
        {
            builder.ToTable("TBCirurgia");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Titulo).HasColumnType("varchar(300)").IsRequired();
            builder.Property(x => x.HoraInicio).IsRequired();
            builder.Property(x => x.HoraTermino).IsRequired();

            builder.HasMany(x => x.Medicos)
                 .WithMany(x => x.Cirurgias)
                 .UsingEntity(j => j.ToTable("TBMedicoCirurgia"));
        }
    }
}
