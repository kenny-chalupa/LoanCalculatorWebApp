﻿// <auto-generated />
using System;
using LoanCalculatorWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LoanCalculatorWebApp.Migrations
{
    [DbContext(typeof(LoanDataContext))]
    partial class LoanDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LoanCalculatorWebApp.Models.LoanData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("InterestRates");

                    b.Property<double>("LoanAmount")
                        .HasMaxLength(64);

                    b.Property<int>("LoanTerm")
                        .HasMaxLength(64);

                    b.Property<double>("MonthlyBill");

                    b.Property<string>("NameOfLoan");

                    b.Property<double>("TotalCost");

                    b.HasKey("Id");

                    b.ToTable("LoanData");
                });
#pragma warning restore 612, 618
        }
    }
}
