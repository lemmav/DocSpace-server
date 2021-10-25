﻿// <auto-generated />
using ASC.Core.Common.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASC.Core.Common.Migrations.MySql.FilesDbContextMySql
{
    [DbContext(typeof(MySqlFilesDbContext))]
    partial class MySqlFilesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.8");

            modelBuilder.Entity("ASC.Core.Common.EF.Model.FilesConverts", b =>
                {
                    b.Property<string>("Input")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("input")
                        .UseCollation("utf8_general_ci")
                        .HasCharSet("utf8");

                    b.Property<string>("Output")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("output")
                        .UseCollation("utf8_general_ci")
                        .HasCharSet("utf8");

                    b.HasKey("Input", "Output")
                        .HasName("PRIMARY");

                    b.ToTable("files_converts");

                    b.HasData(
                        new
                        {
                            Input = ".csv",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".csv",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".csv",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".doc",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".doc",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".doc",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".doc",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".doc",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".docm",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".docm",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".docm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".docm",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".docm",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".doct",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".docx",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".docx",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".docx",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".docx",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".dot",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".dot",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".dot",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".dot",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".dot",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".dotm",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".dotm",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".dotm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".dotm",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".dotm",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".dotx",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".dotx",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".dotx",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".dotx",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".dotx",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".epub",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".epub",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".epub",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".epub",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".epub",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".fb2",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".fb2",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".fb2",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".fb2",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".fb2",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".fodp",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".fodp",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".fodp",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".fods",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".fods",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".fods",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".fods",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".fodt",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".fodt",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".fodt",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".fodt",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".fodt",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".html",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".html",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".html",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".html",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".html",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".mht",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".mht",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".mht",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".mht",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".mht",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".odp",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".odp",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".otp",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".otp",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".otp",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".ods",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".ods",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".ods",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".ots",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".ots",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".ots",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".ots",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".odt",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".odt",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".odt",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".odt",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".ott",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".ott",
                            Output = ".odt"
                        },
                        new
                        {
                            Input = ".ott",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".ott",
                            Output = ".rtf"
                        },
                        new
                        {
                            Input = ".ott",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".pot",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".pot",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".pot",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".potm",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".potm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".potm",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".potx",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".potx",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".potx",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".pps",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".pps",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".pps",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".ppsm",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".ppsm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".ppsm",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".ppsx",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".ppsx",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".ppsx",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".ppt",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".ppt",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".ppt",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".pptm",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".pptm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".pptm",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".pptt",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".pptt",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".pptt",
                            Output = ".pptx"
                        },
                        new
                        {
                            Input = ".pptx",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".pptx",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".rtf",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".rtf",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".rtf",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".rtf",
                            Output = ".txt"
                        },
                        new
                        {
                            Input = ".txt",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".txt",
                            Output = ".docx"
                        },
                        new
                        {
                            Input = ".txt",
                            Output = ".odp"
                        },
                        new
                        {
                            Input = ".txt",
                            Output = ".rtx"
                        },
                        new
                        {
                            Input = ".xls",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".xls",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".xls",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".xls",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".xlsm",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".xlsm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".xlsm",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".xlsm",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".xlst",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".xlst",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".xlst",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".xlst",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".xlt",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".xlt",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".xlt",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".xlt",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".xltm",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".xltm",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".xltm",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".xltm",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".xltx",
                            Output = ".pdf"
                        },
                        new
                        {
                            Input = ".xltx",
                            Output = ".csv"
                        },
                        new
                        {
                            Input = ".xltx",
                            Output = ".ods"
                        },
                        new
                        {
                            Input = ".xltx",
                            Output = ".xlsx"
                        },
                        new
                        {
                            Input = ".xps",
                            Output = ".pdf"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
