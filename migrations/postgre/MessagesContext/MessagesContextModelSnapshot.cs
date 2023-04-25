// <auto-generated />
using System;
using ASC.MessagingSystem.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ASC.Migrations.PostgreSql.Migrations
{
    [DbContext(typeof(MessagesContext))]
    partial class MessagesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ASC.Core.Common.EF.Model.DbTenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("alias");

                    b.Property<bool>("Calls")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("calls")
                        .HasDefaultValueSql("true");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("creationdatetime");

                    b.Property<int>("Industry")
                        .HasColumnType("integer")
                        .HasColumnName("industry");

                    b.Property<string>("Language")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("character(10)")
                        .HasColumnName("language")
                        .HasDefaultValueSql("'en-US'")
                        .IsFixedLength();

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("MappedDomain")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("mappeddomain")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<Guid?>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(38)
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(38)
                        .HasColumnType("character varying(38)")
                        .HasColumnName("payment_id")
                        .HasDefaultValueSql("NULL");

                    b.Property<bool>("Spam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("spam")
                        .HasDefaultValueSql("true");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("StatusChanged")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("statuschanged");

                    b.Property<string>("TimeZone")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("timezone")
                        .HasDefaultValueSql("NULL");

                    b.Property<int>("TrustedDomainsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("trusteddomainsenabled")
                        .HasDefaultValueSql("1");

                    b.Property<string>("TrustedDomainsRaw")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("trusteddomains")
                        .HasDefaultValueSql("NULL");

                    b.Property<int>("Version")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("version")
                        .HasDefaultValueSql("2");

                    b.Property<DateTime?>("Version_Changed")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("version_changed");

                    b.HasKey("Id");

                    b.HasIndex("Alias")
                        .IsUnique()
                        .HasDatabaseName("alias");

                    b.HasIndex("LastModified")
                        .HasDatabaseName("last_modified_tenants_tenants");

                    b.HasIndex("MappedDomain")
                        .HasDatabaseName("mappeddomain");

                    b.HasIndex("Version")
                        .HasDatabaseName("version");

                    b.ToTable("tenants_tenants", "onlyoffice");

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
                            OwnerId = new Guid("66faa6e4-f133-11ea-b126-00ffeec8b4ef"),
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
                            OwnerId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Spam = false,
                            Status = 1,
                            TrustedDomainsEnabled = 0,
                            Version = 0
                        });
                });

            modelBuilder.Entity("ASC.Core.Common.EF.Model.DbWebstudioSettings", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("TenantID");

                    b.Property<Guid>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("uuid")
                        .HasColumnName("ID");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(64)
                        .HasColumnType("uuid")
                        .HasColumnName("UserID");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TenantId", "Id", "UserId")
                        .HasName("webstudio_settings_pkey");

                    b.HasIndex("Id")
                        .HasDatabaseName("ID");

                    b.ToTable("webstudio_settings", "onlyoffice", t =>
                        {
                            t.ExcludeFromMigrations();
                        });

                    b.HasData(
                        new
                        {
                            TenantId = 1,
                            Id = new Guid("9a925891-1f92-4ed7-b277-d6f649739f06"),
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Data = "{\"Completed\":false}"
                        });
                });

            modelBuilder.Entity("ASC.Core.Common.EF.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(38)
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("ActivationStatus")
                        .HasColumnType("integer")
                        .HasColumnName("activation_status");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("bithdate");

                    b.Property<string>("Contacts")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("contacts")
                        .HasDefaultValueSql("NULL");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("create_on")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("CultureName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("culture")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("firstname");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("lastname");

                    b.Property<string>("Location")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("location")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("MobilePhone")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("phone")
                        .HasDefaultValueSql("NULL");

                    b.Property<int>("MobilePhoneActivation")
                        .HasColumnType("integer")
                        .HasColumnName("phone_activation");

                    b.Property<string>("Notes")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("notes")
                        .HasDefaultValueSql("NULL");

                    b.Property<bool>("Removed")
                        .HasColumnType("boolean")
                        .HasColumnName("removed");

                    b.Property<bool?>("Sex")
                        .HasColumnType("boolean")
                        .HasColumnName("sex");

                    b.Property<string>("Sid")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("sid")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("SsoNameId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("sso_name_id")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("SsoSessionId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)")
                        .HasColumnName("sso_session_id")
                        .HasDefaultValueSql("NULL");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("status")
                        .HasDefaultValueSql("1");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant");

                    b.Property<DateTime?>("TerminatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("terminateddate");

                    b.Property<string>("Title")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("title")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("username");

                    b.Property<DateTime?>("WorkFromDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("workfromdate");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .HasDatabaseName("email");

                    b.HasIndex("LastModified")
                        .HasDatabaseName("last_modified_core_user");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserName", "TenantId")
                        .HasDatabaseName("username");

                    b.ToTable("core_user", "onlyoffice", t =>
                        {
                            t.ExcludeFromMigrations();
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("66faa6e4-f133-11ea-b126-00ffeec8b4ef"),
                            ActivationStatus = 0,
                            CreateDate = new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "",
                            FirstName = "Administrator",
                            LastModified = new DateTime(2021, 3, 9, 9, 52, 55, 765, DateTimeKind.Utc).AddTicks(1420),
                            LastName = "",
                            MobilePhoneActivation = 0,
                            Removed = false,
                            Status = 1,
                            TenantId = 1,
                            UserName = "administrator",
                            WorkFromDate = new DateTime(2021, 3, 9, 9, 52, 55, 764, DateTimeKind.Utc).AddTicks(9157)
                        });
                });

            modelBuilder.Entity("ASC.Core.Common.EF.UserGroup", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant");

                    b.Property<Guid>("Userid")
                        .HasMaxLength(38)
                        .HasColumnType("uuid")
                        .HasColumnName("userid");

                    b.Property<Guid>("UserGroupId")
                        .HasMaxLength(38)
                        .HasColumnType("uuid")
                        .HasColumnName("groupid");

                    b.Property<int>("RefType")
                        .HasColumnType("integer")
                        .HasColumnName("ref_type");

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_modified")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<bool>("Removed")
                        .HasColumnType("boolean")
                        .HasColumnName("removed");

                    b.HasKey("TenantId", "Userid", "UserGroupId", "RefType")
                        .HasName("core_usergroup_pkey");

                    b.HasIndex("LastModified")
                        .HasDatabaseName("last_modified_core_usergroup");

                    b.ToTable("core_usergroup", "onlyoffice");
                });

            modelBuilder.Entity("ASC.MessagingSystem.EF.Model.AuditEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Action")
                        .HasColumnType("integer")
                        .HasColumnName("action");

                    b.Property<string>("Browser")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("browser")
                        .HasDefaultValueSql("NULL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("DescriptionRaw")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20000)
                        .HasColumnType("character varying(20000)")
                        .HasColumnName("description")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Initiator")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("initiator")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Ip")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("ip")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Page")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("page")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Platform")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("platform")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Target")
                        .HasColumnType("text")
                        .HasColumnName("target");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.Property<Guid?>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(38)
                        .HasColumnType("uuid")
                        .HasColumnName("user_id")
                        .HasDefaultValueSql("NULL")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Date")
                        .HasDatabaseName("date");

                    b.ToTable("audit_events", "onlyoffice");
                });

            modelBuilder.Entity("ASC.MessagingSystem.EF.Model.LoginEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Action")
                        .HasColumnType("integer")
                        .HasColumnName("action");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("Browser")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("browser")
                        .HasDefaultValueSql("NULL::character varying");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<string>("DescriptionRaw")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Ip")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("ip")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Login")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("login")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Page")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)")
                        .HasColumnName("page")
                        .HasDefaultValueSql("NULL");

                    b.Property<string>("Platform")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("platform")
                        .HasDefaultValueSql("NULL");

                    b.Property<int>("TenantId")
                        .HasColumnType("integer")
                        .HasColumnName("tenant_id");

                    b.Property<Guid?>("UserId")
                        .IsRequired()
                        .HasMaxLength(38)
                        .HasColumnType("uuid")
                        .HasColumnName("user_id")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("Date")
                        .HasDatabaseName("date_login_events");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserId", "TenantId")
                        .HasDatabaseName("tenant_id_login_events");

                    b.ToTable("login_events", "onlyoffice");
                });

            modelBuilder.Entity("ASC.Core.Common.EF.Model.DbWebstudioSettings", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ASC.Core.Common.EF.User", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ASC.Core.Common.EF.UserGroup", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ASC.MessagingSystem.EF.Model.AuditEvent", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ASC.MessagingSystem.EF.Model.LoginEvent", b =>
                {
                    b.HasOne("ASC.Core.Common.EF.Model.DbTenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });
#pragma warning restore 612, 618
        }
    }
}
