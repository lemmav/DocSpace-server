// <auto-generated />
using System;
using ASC.Feed.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASC.Migrations.MySql.Migrations.FeedDb
{
    [DbContext(typeof(FeedDbContext))]
    [Migration("20230425202926_FeedDbContext_Upgrade1")]
    partial class FeedDbContextUpgrade1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ASC.Core.Common.EF.Model.DbTenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("alias")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<bool>("Calls")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("calls")
                        .HasDefaultValueSql("'1'");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime")
                        .HasColumnName("creationdatetime");

                    b.Property<int>("Industry")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("industry")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Language")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(10)")
                        .HasColumnName("language")
                        .HasDefaultValueSql("'en-US'")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp")
                        .HasColumnName("last_modified");

                    b.Property<string>("MappedDomain")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("mappeddomain")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("OwnerId")
                        .HasColumnType("varchar(38)")
                        .HasColumnName("owner_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("PaymentId")
                        .HasColumnType("varchar(38)")
                        .HasColumnName("payment_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<bool>("Spam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("spam")
                        .HasDefaultValueSql("'1'");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("status")
                        .HasDefaultValueSql("'0'");

                    b.Property<DateTime?>("StatusChanged")
                        .HasColumnType("datetime")
                        .HasColumnName("statuschanged");

                    b.Property<string>("TimeZone")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("timezone")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<int>("TrustedDomainsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("trusteddomainsenabled")
                        .HasDefaultValueSql("'1'");

                    b.Property<string>("TrustedDomainsRaw")
                        .HasColumnType("varchar(1024)")
                        .HasColumnName("trusteddomains")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<int>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("version")
                        .HasDefaultValueSql("'2'");

                    b.Property<DateTime?>("Version_Changed")
                        .HasColumnType("datetime")
                        .HasColumnName("version_changed");

                    b.HasKey("Id");

                    b.HasIndex("Alias")
                        .IsUnique()
                        .HasDatabaseName("alias");

                    b.HasIndex("LastModified")
                        .HasDatabaseName("last_modified");

                    b.HasIndex("MappedDomain")
                        .HasDatabaseName("mappeddomain");

                    b.HasIndex("Version")
                        .HasDatabaseName("version");

                    b.ToTable("tenants_tenants", (string)null);

                    b.HasAnnotation("MySql:CharSet", "utf8");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "localhost",
                            Calls = false,
                            CreationDateTime = new DateTime(2021, 3, 9, 17, 46, 59, 97, DateTimeKind.Utc).AddTicks(4317),
                            Industry = 0,
                            LastModified = new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Web Office",
                            OwnerId = "66faa6e4-f133-11ea-b126-00ffeec8b4ef",
                            Spam = false,
                            Status = 0,
                            TrustedDomainsEnabled = 0,
                            Version = 0
                        },
                        new
                        {
                            Id = -1,
                            Alias = "settings",
                            Calls = false,
                            CreationDateTime = new DateTime(2021, 3, 9, 17, 46, 59, 97, DateTimeKind.Utc).AddTicks(4317),
                            Industry = 0,
                            LastModified = new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Web Office",
                            OwnerId = "00000000-0000-0000-0000-000000000000",
                            Spam = false,
                            Status = 1,
                            TrustedDomainsEnabled = 0,
                            Version = 0
                        });
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedAggregate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(88)")
                        .HasColumnName("id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<DateTime>("AggregateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("aggregated_date");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("char(38)")
                        .HasColumnName("author")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("ContextId")
                        .HasColumnType("text")
                        .HasColumnName("context_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("created_date");

                    b.Property<string>("GroupId")
                        .HasColumnType("varchar(70)")
                        .HasColumnName("group_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("Json")
                        .IsRequired()
                        .HasColumnType("mediumtext")
                        .HasColumnName("json")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("Keywords")
                        .HasColumnType("text")
                        .HasColumnName("keywords")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("char(38)")
                        .HasColumnName("modified_by")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("modified_date");

                    b.Property<string>("Module")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("module")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("Product")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("product")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "AggregateDate")
                        .HasDatabaseName("aggregated_date");

                    b.HasIndex("TenantId", "ModifiedDate")
                        .HasDatabaseName("modified_date");

                    b.HasIndex("TenantId", "Product")
                        .HasDatabaseName("product");

                    b.ToTable("feed_aggregate", (string)null);

                    b.HasAnnotation("MySql:CharSet", "utf8");
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedLast", b =>
                {
                    b.Property<string>("LastKey")
                        .HasColumnType("varchar(128)")
                        .HasColumnName("last_key")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<DateTime>("LastDate")
                        .HasColumnType("datetime")
                        .HasColumnName("last_date");

                    b.HasKey("LastKey")
                        .HasName("PRIMARY");

                    b.ToTable("feed_last", (string)null);

                    b.HasAnnotation("MySql:CharSet", "utf8");
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedReaded", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(38)")
                        .HasColumnName("user_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("Module")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("module")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime")
                        .HasColumnName("timestamp");

                    b.HasKey("TenantId", "UserId", "Module")
                        .HasName("PRIMARY");

                    b.ToTable("feed_readed", (string)null);

                    b.HasAnnotation("MySql:CharSet", "utf8");
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedUsers", b =>
                {
                    b.Property<string>("FeedId")
                        .HasColumnType("varchar(88)")
                        .HasColumnName("feed_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.Property<string>("UserId")
                        .HasColumnType("char(38)")
                        .HasColumnName("user_id")
                        .UseCollation("utf8_general_ci")
                        .HasAnnotation("MySql:CharSet", "utf8");

                    b.HasKey("FeedId", "UserId")
                        .HasName("PRIMARY");

                    b.HasIndex("UserId")
                        .HasDatabaseName("user_id");

                    b.ToTable("feed_users", (string)null);

                    b.HasAnnotation("MySql:CharSet", "utf8");
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedAggregate", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedReaded", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ASC.Feed.Model.FeedUsers", b =>
                {
                    b.HasOne("ASC.Feed.Model.FeedAggregate", "Feed")
                        .WithMany()
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feed");
                });
#pragma warning restore 612, 618
        }
    }
}
