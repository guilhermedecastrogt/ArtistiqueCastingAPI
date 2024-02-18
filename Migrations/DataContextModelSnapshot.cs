﻿// <auto-generated />
using System;
using ArtistiqueCastingAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArtistiqueCastingAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            #pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.AuthenticationModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Authentication");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CastingModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsExclusive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubCategorySlug")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Casting");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CastingSubCategoryModel", b =>
                {
                    b.Property<Guid>("CastingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SubCategorySlug")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CastingId", "SubCategorySlug");

                    b.HasIndex("SubCategorySlug");

                    b.ToTable("CastingSubCategory");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CategoryModel", b =>
                {
                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IconFontAlwesome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Slug");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.SubCategoryCategoryModel", b =>
                {
                    b.Property<string>("CategorySlug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SubCategorySlug")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategorySlug", "SubCategorySlug");

                    b.HasIndex("SubCategorySlug");

                    b.ToTable("SubCategoryCategory");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.SubCategoryModel", b =>
                {
                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Slug");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.CastingSubCategoryModel", b =>
                {
                    b.HasOne("ArtistiqueCastingAPI.Models.CastingModel", null)
                        .WithMany()
                        .HasForeignKey("CastingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtistiqueCastingAPI.Models.SubCategoryModel", null)
                        .WithMany()
                        .HasForeignKey("SubCategorySlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArtistiqueCastingAPI.Models.SubCategoryCategoryModel", b =>
                {
                    b.HasOne("ArtistiqueCastingAPI.Models.CategoryModel", null)
                        .WithMany()
                        .HasForeignKey("CategorySlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArtistiqueCastingAPI.Models.SubCategoryModel", null)
                        .WithMany()
                        .HasForeignKey("SubCategorySlug")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
            #pragma warning restore 612, 618
        }
    }
}
