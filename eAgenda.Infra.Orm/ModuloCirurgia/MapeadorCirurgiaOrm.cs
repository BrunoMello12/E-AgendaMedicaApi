using eAgenda.Dominio.ModuloCirurgia;
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
            builder.Property(x => x.Titulo).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HoraInicio).IsRequired();
            builder.Property(x => x.HoraTermino).IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
