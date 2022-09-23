﻿// <auto-generated />
using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBManageSystem.Infrastructure.Data.Migrations.DbManageSysDbContext
{
    [DbContext(typeof(DbManageSysDbContext))]
    partial class DbManageSysDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DBManageSystem.Core.Entities.MenuAggregate.MainMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("mainMenus");
                });

            modelBuilder.Entity("DBManageSystem.Core.Entities.MenuAggregate.SubMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MainMenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MainMenuId");

                    b.ToTable("subMenus");
                });

            modelBuilder.Entity("DBManageSystem.Core.Entities.RoleMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("SubMenuId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("roleMenus");
                });

            modelBuilder.Entity("DBManageSystem.Core.Entities.MenuAggregate.SubMenu", b =>
                {
                    b.HasOne("DBManageSystem.Core.Entities.MenuAggregate.MainMenu", "MainMenu")
                        .WithMany("SubMenus")
                        .HasForeignKey("MainMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainMenu");
                });

            modelBuilder.Entity("DBManageSystem.Core.Entities.MenuAggregate.MainMenu", b =>
                {
                    b.Navigation("SubMenus");
                });
#pragma warning restore 612, 618
        }
    }
}