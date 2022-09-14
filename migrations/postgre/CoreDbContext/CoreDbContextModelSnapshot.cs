// <auto-generated />
using System;
using ASC.Core.Common.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASC.Migrations.PostgreSql.Migrations
{
    [DbContext(typeof(CoreDbContext))]
    partial class CoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ASC.Core.Common.EF.DbQuota", b =>
                {
                    b.Property<int>("Tenant")
                        .HasColumnType("integer")
                        .HasColumnName("tenant");

                    b.Property<string>("Description")
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<string>("Features")
                        .HasColumnType("text")
                        .HasColumnName("features");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("price")
                        .HasDefaultValueSql("0.00");

                    b.Property<string>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("product_id")
                        .HasDefaultValueSql("NULL");

                    b.Property<bool>("Visible")
                        .HasColumnType("boolean")
                        .HasColumnName("visible");

                    b.HasKey("Tenant")
                        .HasName("tenants_quota_pkey");

                    b.ToTable("tenants_quota", "onlyoffice");

                    b.HasData(
                        new
                        {
                            Tenant = -1,
                            Features = "trial,audit,ldap,sso,whitelabel,restore,total_size:10995116277760,file_size:100,manager:1",
                            Name = "trial",
                            Price = 0.00m,
                            Visible = false
                        },
                        new
                        {
                            Tenant = -2,
                            Features = "audit,ldap,sso,whitelabel,restore,total_size:10995116277760,file_size:1024,manager:1",
                            Name = "admin",
                            Price = 30.00m,
                            ProductId = "1002",
                            Visible = true
                        },
                        new
                        {
                            Tenant = -3,
                            Features = "free,audit,ldap,sso,restore,total_size:2147483648,file_size:100,manager:1,rooms:12,usersInRoom:3",
                            Name = "startup",
                            Price = 0.00m,
                            Visible = false
                        });
                });

            modelBuilder.Entity("ASC.Core.Common.EF.DbQuotaRow", b =>
                {
                    b.Property<int>("Tenant")
                        .HasColumnType("integer")
                        .HasColumnName("tenant");

                    b.Property<string>("Path")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("path");

                    b.Property<long>("Counter")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("counter")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Tag")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("tag")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("Tenant", "Path")
                        .HasName("tenants_quotarow_pkey");

                    b.HasIndex("LastModified")
                        .HasDatabaseName("last_modified_tenants_quotarow");

                    b.ToTable("tenants_quotarow", "onlyoffice");
                });

            modelBuilder.Entity("ASC.Core.Common.EF.DbTariff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Comment")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("comment")
                        .HasDefaultValueSql("NULL");

                    b.Property<DateTime>("CreateOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_on")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("customer_id")
                        .HasDefaultValueSql("NULL");

                    b.Property<DateTime>("Stamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("stamp");

                    b.Property<int>("Tenant")
                        .HasColumnType("integer")
                        .HasColumnName("tenant");

                    b.HasKey("Id");

                    b.HasIndex("Tenant")
                        .HasDatabaseName("tenant_tenants_tariff");

                    b.ToTable("tenants_tariff", "onlyoffice");
                });

            modelBuilder.Entity("ASC.Core.Common.EF.DbTariffRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Quota")
                        .HasColumnType("integer");

                    b.Property<int>("TariffId")
                        .HasColumnType("integer");

                    b.Property<int>("Tenant")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TariffRows");
                });
#pragma warning restore 612, 618
        }
    }
}
