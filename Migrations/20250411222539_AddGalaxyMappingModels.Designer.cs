﻿// <auto-generated />
using System;
using JediArchives.DataStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JediArchives.Migrations
{
    [DbContext(typeof(DataContextWrite))]
    [Migration("20250411222539_AddGalaxyMappingModels")]
    partial class AddGalaxyMappingModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.CoordinatePoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PolygonId")
                        .HasColumnType("int");

                    b.Property<float>("X")
                        .HasColumnType("real");

                    b.Property<float>("Y")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("PolygonId", "Order");

                    b.ToTable("CoordinatePoints");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Planet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Allegiance")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("OrbitX")
                        .HasColumnType("real");

                    b.Property<float?>("OrbitY")
                        .HasColumnType("real");

                    b.Property<int>("SystemEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemEntityId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Polygon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Polygons");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PolygonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PolygonId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PolygonId")
                        .HasColumnType("int");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PolygonId");

                    b.HasIndex("RegionId");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.SystemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectorId")
                        .HasColumnType("int");

                    b.Property<float>("X")
                        .HasColumnType("real");

                    b.Property<float>("Y")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("SectorId");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.CoordinatePoint", b =>
                {
                    b.HasOne("JediArchives.DataStorage.EfModels.Polygon", "Polygon")
                        .WithMany("Points")
                        .HasForeignKey("PolygonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Polygon");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Planet", b =>
                {
                    b.HasOne("JediArchives.DataStorage.EfModels.SystemEntity", "System")
                        .WithMany("Planets")
                        .HasForeignKey("SystemEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Region", b =>
                {
                    b.HasOne("JediArchives.DataStorage.EfModels.Polygon", "Polygon")
                        .WithMany()
                        .HasForeignKey("PolygonId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Polygon");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Sector", b =>
                {
                    b.HasOne("JediArchives.DataStorage.EfModels.Polygon", "Polygon")
                        .WithMany()
                        .HasForeignKey("PolygonId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("JediArchives.DataStorage.EfModels.Region", "Region")
                        .WithMany("Sectors")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Polygon");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.SystemEntity", b =>
                {
                    b.HasOne("JediArchives.DataStorage.EfModels.Sector", "Sector")
                        .WithMany("Systems")
                        .HasForeignKey("SectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sector");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Polygon", b =>
                {
                    b.Navigation("Points");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Region", b =>
                {
                    b.Navigation("Sectors");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.Sector", b =>
                {
                    b.Navigation("Systems");
                });

            modelBuilder.Entity("JediArchives.DataStorage.EfModels.SystemEntity", b =>
                {
                    b.Navigation("Planets");
                });
#pragma warning restore 612, 618
        }
    }
}
