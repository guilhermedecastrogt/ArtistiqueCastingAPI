﻿// <auto-generated />
using System;
using ArtistiqueCastingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240113022513_NullableCastingModel")]
    partial class NullableCastingModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CastingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategorySlug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategorySlug");

                    b.ToTable("Casting");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CategoryModel", b =>
                {
                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Slug");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.SubCategoryModel", b =>
                {
                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategorySlug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Slug");

                    b.HasIndex("CategorySlug");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CastingModel", b =>
                {
                    b.HasOne("ArtistiqueCastingAPI.Models.CategoryModel", "Category")
                        .WithMany("Casting")
                        .HasForeignKey("CategorySlug");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.SubCategoryModel", b =>
                {
                    b.HasOne("ArtistiqueCastingAPI.Models.CategoryModel", "Category")
                        .WithMany("SubCategorysGroup")
                        .HasForeignKey("CategorySlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CategoryModel", b =>
                {
                    b.Navigation("Casting");

                    b.Navigation("SubCategorysGroup");
                });
#pragma warning restore 612, 618
        }
    }
}
