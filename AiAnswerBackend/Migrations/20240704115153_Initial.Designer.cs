﻿// <auto-generated />
using System;
using AiAnswerBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AiAnswerBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240704115153_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AiAnswerBackend.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 7, 4, 19, 51, 53, 365, DateTimeKind.Local).AddTicks(5560));

                    b.Property<DateTime>("UpdateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)")
                        .HasDefaultValue(new DateTime(2024, 7, 4, 19, 51, 53, 365, DateTimeKind.Local).AddTicks(5880));

                    b.Property<string>("UserAccount")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserAvatar")
                        .HasColumnType("varchar(1024)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("varchar(512)");

                    b.Property<string>("UserProfile")
                        .HasColumnType("varchar(512)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}
