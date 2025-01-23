﻿// <auto-generated />
using System;
using JogoBasqueteTarefa.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JogoBasqueteTarefa.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250123211216_jogos")]
    partial class jogos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JogoBasqueteTarefa.Models.Jogo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("Pontos")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Jogos");
                });
#pragma warning restore 612, 618
        }
    }
}
