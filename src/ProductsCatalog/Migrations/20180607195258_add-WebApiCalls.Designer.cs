﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ProductsCatalog.Contexts;
using System;

namespace ProductsCatalog.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180607195258_add-WebApiCalls")]
    partial class addWebApiCalls
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductsCatalog.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BriefDescription")
                        .IsRequired()
                        .HasMaxLength(2000);

                    b.Property<decimal>("Cost");

                    b.Property<bool>("IsArchive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantiy");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductsCatalog.Models.WebApiCall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CallRequest");

                    b.Property<string>("CallResponse");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("WebApiCalls");
                });
#pragma warning restore 612, 618
        }
    }
}
