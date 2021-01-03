﻿// <auto-generated />
using System;
using History.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace History.Api.Migrations
{
    [DbContext(typeof(HistoryDbContext))]
    partial class HistoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("History.Shared.Models.Birth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Html")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Birth");
                });

            modelBuilder.Entity("History.Shared.Models.Death", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Html")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Death");
                });

            modelBuilder.Entity("History.Shared.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Html")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("History.Shared.Models.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BirthId")
                        .HasColumnType("int");

                    b.Property<int?>("DeathId")
                        .HasColumnType("int");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BirthId");

                    b.HasIndex("DeathId");

                    b.HasIndex("EventId");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("History.Shared.Models.Link", b =>
                {
                    b.HasOne("History.Shared.Models.Birth", null)
                        .WithMany("Link")
                        .HasForeignKey("BirthId");

                    b.HasOne("History.Shared.Models.Death", null)
                        .WithMany("Link")
                        .HasForeignKey("DeathId");

                    b.HasOne("History.Shared.Models.Event", null)
                        .WithMany("Link")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("History.Shared.Models.Birth", b =>
                {
                    b.Navigation("Link");
                });

            modelBuilder.Entity("History.Shared.Models.Death", b =>
                {
                    b.Navigation("Link");
                });

            modelBuilder.Entity("History.Shared.Models.Event", b =>
                {
                    b.Navigation("Link");
                });
#pragma warning restore 612, 618
        }
    }
}
