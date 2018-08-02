﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Universite.Models;

namespace Universite.Migrations
{
    [DbContext(typeof(UniversiteContext))]
    [Migration("20180731154031_initialise")]
    partial class initialise
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Universite.Models.Enseignant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Enseignant");
                });

            modelBuilder.Entity("Universite.Models.Enseigne", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnseignantID");

                    b.Property<int>("UEID");

                    b.HasKey("ID");

                    b.HasIndex("EnseignantID");

                    b.HasIndex("UEID");

                    b.ToTable("Enseigne");
                });

            modelBuilder.Entity("Universite.Models.Etudiant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Genre");

                    b.Property<DateTime>("Naissance");

                    b.Property<string>("Nom")
                        .IsRequired();

                    b.Property<string>("Prenom")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Etudiant");
                });

            modelBuilder.Entity("Universite.Models.Formation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnneeDiplome");

                    b.Property<string>("IntituleDiplome")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Formation");
                });

            modelBuilder.Entity("Universite.Models.Note", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EtudiantID");

                    b.Property<int>("UEID");

                    b.Property<float>("Valeur");

                    b.HasKey("ID");

                    b.HasIndex("EtudiantID");

                    b.HasIndex("UEID");

                    b.ToTable("Note");
                });

            modelBuilder.Entity("Universite.Models.UE", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FormationID");

                    b.Property<string>("Intitule")
                        .IsRequired();

                    b.Property<string>("Numero")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("FormationID");

                    b.ToTable("UE");
                });

            modelBuilder.Entity("Universite.Models.Enseigne", b =>
                {
                    b.HasOne("Universite.Models.Enseignant", "LEnseignant")
                        .WithMany("LesEnseigne")
                        .HasForeignKey("EnseignantID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Universite.Models.UE", "LUE")
                        .WithMany("LesEnseigne")
                        .HasForeignKey("UEID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Universite.Models.Note", b =>
                {
                    b.HasOne("Universite.Models.Etudiant", "LEtudiant")
                        .WithMany("LesNotes")
                        .HasForeignKey("EtudiantID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Universite.Models.UE", "LUe")
                        .WithMany("LesNotes")
                        .HasForeignKey("UEID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Universite.Models.UE", b =>
                {
                    b.HasOne("Universite.Models.Formation", "LaFormation")
                        .WithMany("LesUE")
                        .HasForeignKey("FormationID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}