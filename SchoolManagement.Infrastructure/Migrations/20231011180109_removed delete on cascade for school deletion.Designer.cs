﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolManagement.Infrastructure.Persistance;

#nullable disable

namespace SchoolManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231011180109_removed delete on cascade for school deletion")]
    partial class removeddeleteoncascadeforschooldeletion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SchoolManagement.Domain.Common.JoinedEntities.StudentLessons", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId", "LessonId");

                    b.HasIndex("LessonId");

                    b.ToTable("StudentLessons");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Lessons.Lesson", b =>
                {
                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LessonId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Schools.School", b =>
                {
                    b.Property<Guid>("SchoolId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SchoolType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SchoolId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Students.Student", b =>
                {
                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SchoolId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StudentId");

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Common.JoinedEntities.StudentLessons", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Lessons.Lesson", "Lesson")
                        .WithMany("StudentLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolManagement.Domain.Students.Student", "Student")
                        .WithMany("StudentLessons")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Students.Student", b =>
                {
                    b.HasOne("SchoolManagement.Domain.Schools.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Lessons.Lesson", b =>
                {
                    b.Navigation("StudentLessons");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Schools.School", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("SchoolManagement.Domain.Students.Student", b =>
                {
                    b.Navigation("StudentLessons");
                });
#pragma warning restore 612, 618
        }
    }
}
