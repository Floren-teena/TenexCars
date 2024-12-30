﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TenexCars.DataAccess;

#nullable disable

namespace TenexCars.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Co_SubscriberInvitee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SubscriberId")
                        .HasColumnType("text");

                    b.Property<string>("VehicleId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Co_SubscriberInvitees");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Operator", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("BusinessName")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("CompanyAddress")
                        .HasColumnType("text");

                    b.Property<string>("CompanyLogo")
                        .HasColumnType("text");

                    b.Property<string>("CompanyName")
                        .HasColumnType("text");

                    b.Property<string>("CompanyRegistrationDocument")
                        .HasColumnType("text");

                    b.Property<DateTime>("ContactDOB")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ContactLink")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FAQLink")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HeroGraphics")
                        .HasColumnType("text");

                    b.Property<string>("IdentificationDocument")
                        .HasColumnType("text");

                    b.Property<int>("InsuranceOffering")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MultipleSubscribers")
                        .HasColumnType("integer");

                    b.Property<string>("OperatorSubscriptionDuration")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("SupportContact1")
                        .HasColumnType("text");

                    b.Property<string>("SupportContact2")
                        .HasColumnType("text");

                    b.Property<string>("Zip")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Operators");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.OperatorMember", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("OperatorId");

                    b.ToTable("OperatorMembers");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Subscriber", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AppUserId")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HomeAddress")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.ToTable("Subscribers");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Subscription", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("DLUrlBack")
                        .HasColumnType("text");

                    b.Property<string>("DLUrlFront")
                        .HasColumnType("text");

                    b.Property<string>("Duration")
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PickUpDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PickUpLocation")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RequestDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("SoftCredit")
                        .HasColumnType("boolean");

                    b.Property<string>("SubscriberId")
                        .HasColumnType("text");

                    b.Property<string>("SubscriptionStatus")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TermEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("TermStart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VehicleId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .HasColumnType("text");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<string>("SubscriberId")
                        .HasColumnType("text");

                    b.Property<string>("TrxRef")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("VehicleId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Vehicle", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<double?>("ActivationFee")
                        .HasColumnType("double precision");

                    b.Property<bool?>("AndroidAuto_AppleCarPlay")
                        .HasColumnType("boolean");

                    b.Property<bool?>("BluetoothSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("CarDealerName")
                        .HasColumnType("text");

                    b.Property<string>("CarDescription")
                        .HasColumnType("text");

                    b.Property<string>("CarWarrantyOverview")
                        .HasColumnType("text");

                    b.Property<string>("CargoSpace")
                        .HasColumnType("text");

                    b.Property<int?>("Cartype")
                        .HasColumnType("integer");

                    b.Property<string>("ChasisNumber")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("CurbWeight")
                        .HasColumnType("text");

                    b.Property<int?>("DcFastCharging")
                        .HasColumnType("integer");

                    b.Property<double?>("DecrementMarketValue")
                        .HasColumnType("double precision");

                    b.Property<string>("DiscBrakes")
                        .HasColumnType("text");

                    b.Property<string>("DriveAxleRatio")
                        .HasColumnType("text");

                    b.Property<bool?>("DriverLumbarSupport")
                        .HasColumnType("boolean");

                    b.Property<int?>("EngineType")
                        .HasColumnType("integer");

                    b.Property<string>("FinacialEndDate")
                        .HasColumnType("text");

                    b.Property<string>("FinacialStartDate")
                        .HasColumnType("text");

                    b.Property<int?>("HomeCharger")
                        .HasColumnType("integer");

                    b.Property<string>("Horsepower")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<string>("InsuranceDocument")
                        .HasColumnType("text");

                    b.Property<string>("Make")
                        .HasColumnType("text");

                    b.Property<double?>("MarketValue")
                        .HasColumnType("double precision");

                    b.Property<string>("Mileage")
                        .HasColumnType("text");

                    b.Property<string>("Model")
                        .HasColumnType("text");

                    b.Property<double?>("MonthlyCost")
                        .HasColumnType("double precision");

                    b.Property<string>("NumberOfSpeakers")
                        .HasColumnType("text");

                    b.Property<string>("OperatorId")
                        .HasColumnType("text");

                    b.Property<string>("PickupAddress")
                        .HasColumnType("text");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("text");

                    b.Property<bool?>("ProjectorBeamLedHeadlight")
                        .HasColumnType("boolean");

                    b.Property<string>("PublicId")
                        .HasColumnType("text");

                    b.Property<bool?>("Radio")
                        .HasColumnType("boolean");

                    b.Property<string>("RangeOfFullCharge")
                        .HasColumnType("text");

                    b.Property<bool?>("RemoteKeylessEntry")
                        .HasColumnType("boolean");

                    b.Property<double?>("ReservationFee")
                        .HasColumnType("double precision");

                    b.Property<int?>("SeatHeater")
                        .HasColumnType("integer");

                    b.Property<string>("SeatNumbers")
                        .HasColumnType("text");

                    b.Property<string>("StabilityControl")
                        .HasColumnType("text");

                    b.Property<bool?>("StandardLowTirePressureWarning")
                        .HasColumnType("boolean");

                    b.Property<int?>("State")
                        .HasColumnType("integer");

                    b.Property<string>("Torque")
                        .HasColumnType("text");

                    b.Property<double?>("TotalCostOfCar")
                        .HasColumnType("double precision");

                    b.Property<bool?>("TouchScreenDisplay")
                        .HasColumnType("boolean");

                    b.Property<bool?>("TouchScreenNavSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("TransmissionSpeed")
                        .HasColumnType("text");

                    b.Property<string>("TransmissionType")
                        .HasColumnType("text");

                    b.Property<string>("TrunkSize")
                        .HasColumnType("text");

                    b.Property<string>("TurningDiameter")
                        .HasColumnType("text");

                    b.Property<string>("VIN")
                        .HasColumnType("text");

                    b.Property<string>("VehicleStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("WheelDrive")
                        .HasColumnType("integer");

                    b.Property<string>("ZIP")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.VehicleRequest", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("SubscriberId")
                        .HasColumnType("text");

                    b.Property<string>("VehicleId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("VehicleId");

                    b.ToTable("VehicleRequests");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TenexCars.DataAccess.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Co_SubscriberInvitee", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("TenexCars.DataAccess.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId");

                    b.HasOne("TenexCars.DataAccess.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("AppUser");

                    b.Navigation("Subscriber");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Operator", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.OperatorMember", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.HasOne("TenexCars.DataAccess.Models.Operator", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId");

                    b.Navigation("AppUser");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Subscriber", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Subscription", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.Operator", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId");

                    b.HasOne("TenexCars.DataAccess.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId");

                    b.HasOne("TenexCars.DataAccess.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Operator");

                    b.Navigation("Subscriber");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Transaction", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.Operator", "Operator")
                        .WithMany("Transactions")
                        .HasForeignKey("OperatorId");

                    b.HasOne("TenexCars.DataAccess.Models.Subscriber", "Subscriber")
                        .WithMany("Transactions")
                        .HasForeignKey("SubscriberId");

                    b.HasOne("TenexCars.DataAccess.Models.Vehicle", "Vehicle")
                        .WithMany("Transactions")
                        .HasForeignKey("VehicleId");

                    b.Navigation("Operator");

                    b.Navigation("Subscriber");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Vehicle", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.Operator", "Operator")
                        .WithMany("Vehicles")
                        .HasForeignKey("OperatorId");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.VehicleRequest", b =>
                {
                    b.HasOne("TenexCars.DataAccess.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId");

                    b.HasOne("TenexCars.DataAccess.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId");

                    b.Navigation("Subscriber");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Operator", b =>
                {
                    b.Navigation("Transactions");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Subscriber", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("TenexCars.DataAccess.Models.Vehicle", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
