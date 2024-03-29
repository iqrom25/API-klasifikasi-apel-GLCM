﻿// <auto-generated />
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.DataLatih", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Blue")
                        .HasColumnType("double");

                    b.Property<double>("Energi")
                        .HasColumnType("double");

                    b.Property<double>("Green")
                        .HasColumnType("double");

                    b.Property<double>("Homogenitas")
                        .HasColumnType("double");

                    b.Property<string>("Kelas")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Kontras")
                        .HasColumnType("double");

                    b.Property<double>("Korelasi")
                        .HasColumnType("double");

                    b.Property<double>("Red")
                        .HasColumnType("double");

                    b.Property<int>("Sudut")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("m_data_latih");
                });
#pragma warning restore 612, 618
        }
    }
}
