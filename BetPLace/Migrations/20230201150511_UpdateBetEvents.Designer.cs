﻿// <auto-generated />
using System;
using BetPlace.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BetPlace.Migrations
{
    [DbContext(typeof(BetPlaceContext))]
    [Migration("20230201150511_UpdateBetEvents")]
    partial class UpdateBetEvents
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BetPlace.Models.BetEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EventEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EventStartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Team1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team1Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Team2Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("coef0")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("coef1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("coef2")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("BetEvent");
                });
#pragma warning restore 612, 618
        }
    }
}
