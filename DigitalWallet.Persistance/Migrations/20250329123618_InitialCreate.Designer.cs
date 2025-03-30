﻿// <auto-generated />
using System;
using DigitalWallet.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigitalWallet.Persistance.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250329123618_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DigitalWallet.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DigitalWallet.Domain.Entities.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("DigitalWallet.Domain.Entities.WalletTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReceiverWalletId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SenderWalletId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SenderWalletId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverWalletId");

                    b.HasIndex("SenderWalletId");

                    b.HasIndex("SenderWalletId1");

                    b.ToTable("WalletTransactions", (string)null);
                });

            modelBuilder.Entity("DigitalWallet.Domain.Entities.Wallet", b =>
                {
                    b.HasOne("DigitalWallet.Domain.Entities.User", null)
                        .WithOne("Wallet")
                        .HasForeignKey("DigitalWallet.Domain.Entities.Wallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalWallet.Domain.Entities.WalletTransaction", b =>
                {
                    b.HasOne("DigitalWallet.Domain.Entities.Wallet", "ReceiverWallet")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("ReceiverWalletId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DigitalWallet.Domain.Entities.Wallet", null)
                        .WithMany("SentTransactions")
                        .HasForeignKey("SenderWalletId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DigitalWallet.Domain.Entities.Wallet", "SenderWallet")
                        .WithMany()
                        .HasForeignKey("SenderWalletId1");

                    b.Navigation("ReceiverWallet");

                    b.Navigation("SenderWallet");
                });

            modelBuilder.Entity("DigitalWallet.Domain.Entities.User", b =>
                {
                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("DigitalWallet.Domain.Entities.Wallet", b =>
                {
                    b.Navigation("ReceivedTransactions");

                    b.Navigation("SentTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
