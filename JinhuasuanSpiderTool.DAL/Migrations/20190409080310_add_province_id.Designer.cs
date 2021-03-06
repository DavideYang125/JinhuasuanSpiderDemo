﻿// <auto-generated />
using System;
using JinhuasuanSpiderTool.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JinhuasuanSpiderTool.DAL.Migrations
{
    [DbContext(typeof(JinhuasuanStoreContext))]
    [Migration("20190409080310_add_province_id")]
    partial class add_province_id
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("JinhuasuanSpiderTool.DAL.Model.Citys", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullSpell")
                        .HasMaxLength(200);

                    b.Property<string>("IdentificationCode")
                        .HasMaxLength(10);

                    b.Property<string>("Initial")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("KeyWordJson");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("ProvinceId");

                    b.Property<string>("SimpleSpell")
                        .HasMaxLength(50);

                    b.Property<int>("Sort");

                    b.Property<string>("Unique")
                        .HasMaxLength(6);

                    b.Property<string>("ZipCode")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("deyouyun_address_city");
                });

            modelBuilder.Entity("JinhuasuanSpiderTool.DAL.Model.Districts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("FullSpell")
                        .HasMaxLength(200);

                    b.Property<string>("IdentificationCode")
                        .HasMaxLength(10);

                    b.Property<string>("Initial")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("KeyWordJson");

                    b.Property<string>("Name");

                    b.Property<string>("SimpleSpell")
                        .HasMaxLength(50);

                    b.Property<int>("Sort");

                    b.Property<string>("Unique")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("deyouyun_address_districts");
                });

            modelBuilder.Entity("JinhuasuanSpiderTool.DAL.Model.JinhuasuanStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("CityId");

                    b.Property<string>("Content")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("create_time");

                    b.Property<int?>("DistrictId");

                    b.Property<int>("ErrorCount")
                        .HasColumnName("error_count");

                    b.Property<int?>("OwnStoreId")
                        .HasColumnName("own_store_id");

                    b.Property<int?>("OwnUserId")
                        .HasColumnName("own_user_id");

                    b.Property<int?>("ProvinceId");

                    b.Property<int>("Status")
                        .HasColumnName("status");

                    b.Property<int>("StoreId")
                        .HasColumnName("store_id");

                    b.Property<DateTime?>("SyncTime")
                        .HasColumnName("sync_time");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnName("update_time");

                    b.HasKey("Id");

                    b.ToTable("jinhuasuan_store");
                });

            modelBuilder.Entity("JinhuasuanSpiderTool.DAL.Model.Provinces", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullSpell")
                        .HasMaxLength(200);

                    b.Property<string>("IdentificationCode")
                        .HasMaxLength(10);

                    b.Property<string>("Initial")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("KeyWordJson");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<string>("SimpleSpell")
                        .HasMaxLength(50);

                    b.Property<int>("Sort");

                    b.Property<string>("Unique")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("deyouyun_address_province");
                });
#pragma warning restore 612, 618
        }
    }
}
