﻿// <auto-generated />
using System;
using FormBuilder.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FormBuilder.SqliteMigrations.Migrations
{
    [DbContext(typeof(FormBuilderDbContext))]
    [Migration("20250612205943_AddTranslations")]
    partial class AddTranslations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("FormBuilder.Models.FormRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "id");

                    b.Property<bool>("ActAsStep")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("CorrelationId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Elements")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ErrorMessageTranslations")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Realm")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SuccessMessageTranslations")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("VersionNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("FormBuilder.Models.Template", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Elements")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Realm")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Styles")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Windows")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("FormBuilder.Models.TemplateStyle", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Language")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TemplateId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TemplateId1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TemplateId");

                    b.HasIndex("TemplateId1");

                    b.ToTable("TemplateStyle");
                });

            modelBuilder.Entity("FormBuilder.Models.WorkflowLink", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionParameter")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionType")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMainLink")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceStepId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TargetStepId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Targets")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowRecordId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowRecordId");

                    b.ToTable("WorkflowLink");
                });

            modelBuilder.Entity("FormBuilder.Models.WorkflowRecord", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Realm")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Workflows");
                });

            modelBuilder.Entity("FormBuilder.Models.WorkflowStep", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("FormRecordCorrelationId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkflowRecordId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("WorkflowRecordId");

                    b.ToTable("WorkflowStep");
                });

            modelBuilder.Entity("FormBuilder.Models.TemplateStyle", b =>
                {
                    b.HasOne("FormBuilder.Models.Template", null)
                        .WithMany("CssStyles")
                        .HasForeignKey("TemplateId");

                    b.HasOne("FormBuilder.Models.Template", null)
                        .WithMany("JsStyles")
                        .HasForeignKey("TemplateId1");
                });

            modelBuilder.Entity("FormBuilder.Models.WorkflowLink", b =>
                {
                    b.HasOne("FormBuilder.Models.WorkflowRecord", null)
                        .WithMany("Links")
                        .HasForeignKey("WorkflowRecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FormBuilder.Models.WorkflowStep", b =>
                {
                    b.HasOne("FormBuilder.Models.WorkflowRecord", null)
                        .WithMany("Steps")
                        .HasForeignKey("WorkflowRecordId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FormBuilder.Models.Template", b =>
                {
                    b.Navigation("CssStyles");

                    b.Navigation("JsStyles");
                });

            modelBuilder.Entity("FormBuilder.Models.WorkflowRecord", b =>
                {
                    b.Navigation("Links");

                    b.Navigation("Steps");
                });
#pragma warning restore 612, 618
        }
    }
}
