﻿// <auto-generated />
using System;
using DBManageSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBManageSystem.Infrastructure.Data.Migrations.DbManageSysDbContext
{
    [DbContext(typeof(DBManageSystem.Infrastructure.Data.DbManageSysDbContext))]
    [Migration("20221011012945_add databaseserver entity")]
    partial class adddatabaseserverentity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DBManageSystem.Core.Entities.DatabaseServer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConnectUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("DatabaseType")
                        .HasColumnType("int");

                    b.Property<bool>("IsMonitored")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastUpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("databaseServers");
                });

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "数据库管理",
                            Order = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "账号管理",
                            Order = 2
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MainMenuId = 1,
                            Name = "数据库监控",
                            Order = 1,
                            Path = "/dbMonitor"
                        },
                        new
                        {
                            Id = 2,
                            MainMenuId = 1,
                            Name = "数据库报表",
                            Order = 2,
                            Path = "/dbReport"
                        },
                        new
                        {
                            Id = 3,
                            MainMenuId = 2,
                            Name = "用户管理",
                            Order = 3,
                            Path = "/userManage"
                        },
                        new
                        {
                            Id = 4,
                            MainMenuId = 2,
                            Name = "角色管理",
                            Order = 4,
                            Path = "/roleManage"
                        },
                        new
                        {
                            Id = 5,
                            MainMenuId = 2,
                            Name = "权限管理",
                            Order = 5,
                            Path = "/permissionManage"
                        });
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleId = 1,
                            SubMenuId = 1
                        },
                        new
                        {
                            Id = 2,
                            RoleId = 1,
                            SubMenuId = 2
                        },
                        new
                        {
                            Id = 3,
                            RoleId = 1,
                            SubMenuId = 3
                        },
                        new
                        {
                            Id = 4,
                            RoleId = 1,
                            SubMenuId = 4
                        },
                        new
                        {
                            Id = 5,
                            RoleId = 1,
                            SubMenuId = 5
                        });
                });

            modelBuilder.Entity("DBManageSystem.Core.Entities.SqlLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConnectUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("ExecuteTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SqlText")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("sqlLogs");
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