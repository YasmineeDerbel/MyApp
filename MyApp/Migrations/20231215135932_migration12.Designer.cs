﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApp.Models;

#nullable disable

namespace MyApp.Migrations
{
    [DbContext(typeof(MyappContext))]
    [Migration("20231215135932_migration12")]
    partial class migration12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyApp.Models.Post", b =>
                {
                    b.Property<string>("id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("auteurid")
                        .HasColumnType("int");

                    b.Property<string>("reponse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sujet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("auteurid");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("MyApp.Models.Utilisateur", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("valide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Utilisateurs");
                });

            modelBuilder.Entity("MyApp.Models.Post", b =>
                {
                    b.HasOne("MyApp.Models.Utilisateur", "auteur")
                        .WithMany()
                        .HasForeignKey("auteurid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("auteur");
                });
#pragma warning restore 612, 618
        }
    }
}
