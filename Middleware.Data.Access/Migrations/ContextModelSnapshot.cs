﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Middleware.Data.Access;

namespace Middleware.Data.Access.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Middleware.Data.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acknowledgment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Activable")
                        .HasColumnType("bit");

                    b.Property<double>("ActivationTime")
                        .HasColumnType("float");

                    b.Property<bool?>("AutomaticRenewal")
                        .HasColumnType("bit");

                    b.Property<int>("AutomaticRenewalStatus")
                        .HasColumnType("int");

                    b.Property<int>("ComState")
                        .HasColumnType("int");

                    b.Property<string>("ContractId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CreationTime")
                        .HasColumnType("float");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateReceived")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceTypeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LastCom")
                        .HasColumnType("float");

                    b.Property<string>("LastEditedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LastEditionTime")
                        .HasColumnType("float");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("LongPolling")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("Lqi")
                        .HasColumnType("int");

                    b.Property<string>("ModemCertificateId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pac")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Payload")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ProductCertificateId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Prototype")
                        .HasColumnType("bit");

                    b.Property<string>("RequestOrigin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("SatelliteCapable")
                        .HasColumnType("bit");

                    b.Property<double>("SeqNumber")
                        .HasColumnType("float");

                    b.Property<double>("SequenceNumber")
                        .HasColumnType("float");

                    b.Property<string>("SigfoxDeviceTypeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TokenDetailMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TokenEnd")
                        .HasColumnType("int");

                    b.Property<int>("TokenState")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
