﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eAgenda.Infra.Orm.Compartilhado;

#nullable disable

namespace eAgenda.Infra.Orm.Migrations
{
    [DbContext(typeof(eAgendaDbContext))]
    partial class eAgendaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CirurgiaMedico", b =>
                {
                    b.Property<Guid>("CirurgiasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CirurgiasId", "MedicosId");

                    b.HasIndex("MedicosId");

                    b.ToTable("TBMedicoCirurgia", (string)null);
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloCirurgia.Cirurgia", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraTermino")
                        .HasColumnType("time");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.ToTable("TBCirurgia", (string)null);
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloConsulta.Consulta", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HoraTermino")
                        .HasColumnType("time");

                    b.Property<Guid>("MedicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("MedicoId");

                    b.ToTable("TBConsulta", (string)null);
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloMedico.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("TBMedico", (string)null);
                });

            modelBuilder.Entity("CirurgiaMedico", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloCirurgia.Cirurgia", null)
                        .WithMany()
                        .HasForeignKey("CirurgiasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eAgenda.Dominio.ModuloMedico.Medico", null)
                        .WithMany()
                        .HasForeignKey("MedicosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloConsulta.Consulta", b =>
                {
                    b.HasOne("eAgenda.Dominio.ModuloMedico.Medico", "Medico")
                        .WithMany("Consultas")
                        .HasForeignKey("MedicoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Medico");
                });

            modelBuilder.Entity("eAgenda.Dominio.ModuloMedico.Medico", b =>
                {
                    b.Navigation("Consultas");
                });
#pragma warning restore 612, 618
        }
    }
}
