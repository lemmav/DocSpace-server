﻿// <auto-generated />
using System;
using ASC.Files.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASC.Files.Core.Migrations.MSSql.FilesDbContextMSSql
{
    [DbContext(typeof(MSSqlFilesDbContext))]
    partial class MSSqlFilesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASC.Files.Core.EF.DbFile", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("Version")
                        .HasColumnType("int")
                        .HasColumnName("version");

                    b.Property<int>("Category")
                        .HasColumnType("int")
                        .HasColumnName("category");

                    b.Property<string>("Changes")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("changes")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("Comment")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("comment")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<long>("ContentLength")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(0L)
                        .HasColumnName("content_length");

                    b.Property<string>("ConvertedType")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("converted_type")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<Guid>("CreateBy")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("create_by")
                        .IsFixedLength(true);

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_on");

                    b.Property<bool>("CurrentVersion")
                        .HasColumnType("bit")
                        .HasColumnName("current_version");

                    b.Property<bool>("Encrypted")
                        .HasColumnType("bit")
                        .HasColumnName("encrypted");

                    b.Property<int>("FileStatus")
                        .HasColumnType("int")
                        .HasColumnName("file_status");

                    b.Property<int>("FolderId")
                        .HasColumnType("int")
                        .HasColumnName("folder_id");

                    b.Property<int>("Forcesave")
                        .HasColumnType("int")
                        .HasColumnName("forcesave");

                    b.Property<Guid>("ModifiedBy")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("modified_by")
                        .IsFixedLength(true);

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on");

                    b.Property<int>("Thumb")
                        .HasColumnType("int")
                        .HasColumnName("thumb");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("title")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<int>("VersionGroup")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1)
                        .HasColumnName("version_group");

                    b.HasKey("TenantId", "Id", "Version")
                        .HasName("files_file_pkey");

                    b.HasIndex("FolderId")
                        .HasDatabaseName("folder_id");

                    b.HasIndex("Id")
                        .HasDatabaseName("id");

                    b.HasIndex("ModifiedOn")
                        .HasDatabaseName("modified_on_files_file");

                    b.ToTable("files_file");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesBunchObjects", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("RightNode")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("right_node")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("LeftNode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("left_node")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.HasKey("TenantId", "RightNode")
                        .HasName("files_bunch_objects_pkey");

                    b.HasIndex("LeftNode")
                        .HasDatabaseName("left_node");

                    b.ToTable("files_bunch_objects");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesSecurity", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("EntryId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("entry_id")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<int>("EntryType")
                        .HasColumnType("int")
                        .HasColumnName("entry_type");

                    b.Property<Guid>("Subject")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("subject")
                        .IsFixedLength(true);

                    b.Property<Guid>("Owner")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("owner")
                        .IsFixedLength(true);

                    b.Property<int>("Security")
                        .HasColumnType("int")
                        .HasColumnName("security");

                    b.Property<DateTime>("TimeStamp")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("TenantId", "EntryId", "EntryType", "Subject")
                        .HasName("files_security_pkey");

                    b.HasIndex("Owner")
                        .HasDatabaseName("owner");

                    b.HasIndex("TenantId", "EntryType", "EntryId", "Owner")
                        .HasDatabaseName("tenant_id_files_security");

                    b.ToTable("files_security");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Flag")
                        .HasColumnType("int")
                        .HasColumnName("flag");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("name")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<Guid>("Owner")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("owner");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.HasKey("Id");

                    b.HasIndex("TenantId", "Owner", "Name", "Flag")
                        .HasDatabaseName("name_files_tag");

                    b.ToTable("files_tag");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesTagLink", b =>
                {
                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<int>("TagId")
                        .HasColumnType("int")
                        .HasColumnName("tag_id");

                    b.Property<string>("EntryId")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)")
                        .HasColumnName("entry_id")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<int>("EntryType")
                        .HasColumnType("int")
                        .HasColumnName("entry_type");

                    b.Property<Guid?>("CreateBy")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("create_by")
                        .IsFixedLength(true);

                    b.Property<DateTime?>("CreateOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_on");

                    b.Property<int>("TagCount")
                        .HasColumnType("int")
                        .HasColumnName("tag_count");

                    b.HasKey("TenantId", "TagId", "EntryId", "EntryType")
                        .HasName("files_tag_link_pkey");

                    b.HasIndex("CreateOn")
                        .HasDatabaseName("create_on_files_tag_link");

                    b.HasIndex("TenantId", "EntryId", "EntryType")
                        .HasDatabaseName("entry_id");

                    b.ToTable("files_tag_link");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesThirdpartyAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_on");

                    b.Property<int>("FolderType")
                        .HasColumnType("int")
                        .HasColumnName("folder_type");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("password")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("provider")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("customer_title")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("user_name")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.HasKey("Id");

                    b.ToTable("files_thirdparty_account");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesThirdpartyApp", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("user_id");

                    b.Property<string>("App")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("app")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<DateTime>("ModifiedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.HasKey("UserId", "App")
                        .HasName("files_thirdparty_app_pkey");

                    b.ToTable("files_thirdparty_app");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFilesThirdpartyIdMapping", b =>
                {
                    b.Property<string>("HashId")
                        .HasMaxLength(32)
                        .HasColumnType("nchar(32)")
                        .HasColumnName("hash_id")
                        .IsFixedLength(true)
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("id")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.HasKey("HashId")
                        .HasName("files_thirdparty_id_mapping_pkey");

                    b.HasIndex("TenantId", "HashId")
                        .HasDatabaseName("index_1");

                    b.ToTable("files_thirdparty_id_mapping");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("CreateBy")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("create_by")
                        .IsFixedLength(true);

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("create_on");

                    b.Property<int>("FilesCount")
                        .HasColumnType("int")
                        .HasColumnName("filesCount");

                    b.Property<int>("FolderType")
                        .HasColumnType("int")
                        .HasColumnName("folder_type");

                    b.Property<int>("FoldersCount")
                        .HasColumnType("int")
                        .HasColumnName("foldersCount");

                    b.Property<Guid>("ModifiedBy")
                        .HasMaxLength(38)
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("modified_by")
                        .IsFixedLength(true);

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2")
                        .HasColumnName("modified_on");

                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<int>("TenantId")
                        .HasColumnType("int")
                        .HasColumnName("tenant_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)")
                        .HasColumnName("title")
                        .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8");

                    b.HasKey("Id");

                    b.HasIndex("ModifiedOn")
                        .HasDatabaseName("modified_on_files_folder");

                    b.HasIndex("TenantId", "ParentId")
                        .HasDatabaseName("parent_id");

                    b.ToTable("files_folder");
                });

            modelBuilder.Entity("ASC.Files.Core.EF.DbFolderTree", b =>
                {
                    b.Property<int>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.Property<int>("FolderId")
                        .HasColumnType("int")
                        .HasColumnName("folder_id");

                    b.Property<int>("Level")
                        .HasColumnType("int")
                        .HasColumnName("level");

                    b.HasKey("ParentId", "FolderId")
                        .HasName("files_folder_tree_pkey");

                    b.HasIndex("FolderId")
                        .HasDatabaseName("folder_id_files_folder_tree");

                    b.ToTable("files_folder_tree");
                });
#pragma warning restore 612, 618
        }
    }
}
