﻿// <auto-generated />
using System;
using DeckIQ.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DeckIQ.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241017115044_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DeckIQ.Core.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("NVARCHAR");

                    b.HasKey("Id");

                    b.ToTable("category", (string)null);
                });

            modelBuilder.Entity("DeckIQ.Core.Models.FleshCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("CardImage")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IncorrectAnswerA")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("IncorrectAnswerB")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("IncorrectAnswerC")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("IncorrectAnswerD")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("FlashCard", (string)null);
                });

            modelBuilder.Entity("DeckIQ.Core.Models.FleshCard", b =>
                {
                    b.HasOne("DeckIQ.Core.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
