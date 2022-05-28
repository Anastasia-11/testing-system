﻿// <auto-generated />
using System;
using CourseWork.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseWork.Migrations.Application
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220419155233_UpdateAnswer")]
    partial class UpdateAnswer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseWork.Models.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CorrectResponse")
                        .HasColumnType("text");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("boolean");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Response")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("CourseWork.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("TestId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("CourseWork.Models.Test", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CloseTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OpenTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("QuestionsCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("CourseWork.Models.Answer", b =>
                {
                    b.HasOne("CourseWork.Models.Question", "Question")
                        .WithOne("Answer")
                        .HasForeignKey("CourseWork.Models.Answer", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("CourseWork.Models.Question", b =>
                {
                    b.HasOne("CourseWork.Models.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("CourseWork.Models.Question", b =>
                {
                    b.Navigation("Answer");
                });

            modelBuilder.Entity("CourseWork.Models.Test", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
