using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Honoured.Migrations
{
    public partial class Addedunitsofmeasurement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCountries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCountries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDimensions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxHeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxWidth = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDimensions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppDisciplines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtType = table.Column<int>(type: "int", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDisciplines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppPersons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<long>(type: "bigint", nullable: false),
                    ParentType = table.Column<int>(type: "int", nullable: false),
                    First = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPersons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSubscriptionTiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumbetOfPieces = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubscriptionTiers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMarkets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Points = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CountryIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMarkets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMarkets_AppCountries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "AppCountries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsBilling = table.Column<bool>(type: "bit", nullable: false),
                    IsDeliveryPoint = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppAddresses_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppArtLovers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivePlacements = table.Column<int>(type: "int", nullable: false),
                    NextPlacementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtLovers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArtLovers_AppPersons_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppContactPoints",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppContactPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppContactPoints_AppPersons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppArtistsSubscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtitstId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TierId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtistsSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArtistsSubscriptions_AppSubscriptionTiers_TierId",
                        column: x => x.TierId,
                        principalTable: "AppSubscriptionTiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppDeliveries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewArtWorkId = table.Column<long>(type: "bigint", nullable: false),
                    NewArtworArtistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewArtworkTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldArtWorkId = table.Column<long>(type: "bigint", nullable: false),
                    OldArtworArtistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldArtworkTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtLoverId = table.Column<long>(type: "bigint", nullable: false),
                    ArtLoverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDeliveries_AppAddresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AppAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppSubsCatgories",
                columns: table => new
                {
                    ArtLoverId = table.Column<long>(type: "bigint", nullable: false),
                    ArtDisciplineId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubsCatgories", x => new { x.ArtDisciplineId, x.ArtLoverId });
                    table.ForeignKey(
                        name: "FK_AppSubsCatgories_AppArtLovers_ArtLoverId",
                        column: x => x.ArtLoverId,
                        principalTable: "AppArtLovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppSubscriptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtLoverId = table.Column<long>(type: "bigint", nullable: false),
                    NumberOfPieces = table.Column<int>(type: "int", nullable: false),
                    DimensionId = table.Column<long>(type: "bigint", nullable: true),
                    DeliveryAddressId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOneArtistPerPlacement = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubscriptions_AppAddresses_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "AppAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppSubscriptions_AppArtLovers_ArtLoverId",
                        column: x => x.ArtLoverId,
                        principalTable: "AppArtLovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppSubscriptions_AppDimensions_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "AppDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppArtists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalDetailsId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DefaultContactPointId = table.Column<long>(type: "bigint", nullable: true),
                    ShortDesciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubscriptionId = table.Column<long>(type: "bigint", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArtists_AppArtistsSubscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "AppArtistsSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppArtists_AppContactPoints_DefaultContactPointId",
                        column: x => x.DefaultContactPointId,
                        principalTable: "AppContactPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppArtists_AppPersons_PersonalDetailsId",
                        column: x => x.PersonalDetailsId,
                        principalTable: "AppPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppPlacements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtWorkId = table.Column<long>(type: "bigint", nullable: false),
                    ArtLoverId = table.Column<long>(type: "bigint", nullable: false),
                    DimensionId = table.Column<long>(type: "bigint", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPlacements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppPlacements_AppArtLovers_ArtLoverId",
                        column: x => x.ArtLoverId,
                        principalTable: "AppArtLovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppPlacements_AppSubscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "AppSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppArtistDisciplines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtistDisciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArtistDisciplines_AppArtists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "AppArtists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppArtistDisciplines_AppDisciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "AppDisciplines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppArtistMarkets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artistid = table.Column<long>(type: "bigint", nullable: false),
                    MarketId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtistMarkets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArtistMarkets_AppArtists_Artistid",
                        column: x => x.Artistid,
                        principalTable: "AppArtists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppArtistMarkets_AppMarkets_MarketId",
                        column: x => x.MarketId,
                        principalTable: "AppMarkets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppArtPieces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<long>(type: "bigint", nullable: false),
                    IsFileUploaded = table.Column<bool>(type: "bit", nullable: false),
                    IsHasAdultContent = table.Column<bool>(type: "bit", nullable: false),
                    IsInsured = table.Column<bool>(type: "bit", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateNextAvailable = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Depth = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    MeasurementUnit = table.Column<int>(type: "int", nullable: false),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Story = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MainDisciplineId = table.Column<long>(type: "bigint", nullable: true),
                    DimensionId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtPieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppArtPieces_AppArtists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "AppArtists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppArtPieces_AppDimensions_DimensionId",
                        column: x => x.DimensionId,
                        principalTable: "AppDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppArtPieces_AppDisciplines_MainDisciplineId",
                        column: x => x.MainDisciplineId,
                        principalTable: "AppDisciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppArtworkDisciplines",
                columns: table => new
                {
                    ArtWorkId = table.Column<long>(type: "bigint", nullable: false),
                    DisciplineId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppArtworkDisciplines", x => new { x.ArtWorkId, x.DisciplineId });
                    table.ForeignKey(
                        name: "FK_AppArtworkDisciplines_AppArtPieces_ArtWorkId",
                        column: x => x.ArtWorkId,
                        principalTable: "AppArtPieces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppArtworkDisciplines_AppDisciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "AppDisciplines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppCareInstructions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtWorkId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCareInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCareInstructions_AppArtPieces_ArtWorkId",
                        column: x => x.ArtWorkId,
                        principalTable: "AppArtPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtWorkId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTags_AppArtPieces_ArtWorkId",
                        column: x => x.ArtWorkId,
                        principalTable: "AppArtPieces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAddresses_PersonId",
                table: "AppAddresses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtistDisciplines_ArtistId",
                table: "AppArtistDisciplines",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtistDisciplines_DisciplineId",
                table: "AppArtistDisciplines",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtistMarkets_Artistid",
                table: "AppArtistMarkets",
                column: "Artistid");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtistMarkets_MarketId",
                table: "AppArtistMarkets",
                column: "MarketId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtists_DefaultContactPointId",
                table: "AppArtists",
                column: "DefaultContactPointId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtists_PersonalDetailsId",
                table: "AppArtists",
                column: "PersonalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtists_SubscriptionId",
                table: "AppArtists",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtistsSubscriptions_TierId",
                table: "AppArtistsSubscriptions",
                column: "TierId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtLovers_ProfileId",
                table: "AppArtLovers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtPieces_ArtistId",
                table: "AppArtPieces",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtPieces_DimensionId",
                table: "AppArtPieces",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtPieces_MainDisciplineId",
                table: "AppArtPieces",
                column: "MainDisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_AppArtworkDisciplines_DisciplineId",
                table: "AppArtworkDisciplines",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCareInstructions_ArtWorkId",
                table: "AppCareInstructions",
                column: "ArtWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_AppContactPoints_PersonId",
                table: "AppContactPoints",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDeliveries_AddressId",
                table: "AppDeliveries",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMarkets_CountryId",
                table: "AppMarkets",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPlacements_ArtLoverId",
                table: "AppPlacements",
                column: "ArtLoverId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPlacements_SubscriptionId",
                table: "AppPlacements",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubsCatgories_ArtLoverId",
                table: "AppSubsCatgories",
                column: "ArtLoverId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptions_ArtLoverId",
                table: "AppSubscriptions",
                column: "ArtLoverId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptions_DeliveryAddressId",
                table: "AppSubscriptions",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptions_DimensionId",
                table: "AppSubscriptions",
                column: "DimensionId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTags_ArtWorkId",
                table: "AppTags",
                column: "ArtWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppArtistDisciplines");

            migrationBuilder.DropTable(
                name: "AppArtistMarkets");

            migrationBuilder.DropTable(
                name: "AppArtworkDisciplines");

            migrationBuilder.DropTable(
                name: "AppCareInstructions");

            migrationBuilder.DropTable(
                name: "AppDeliveries");

            migrationBuilder.DropTable(
                name: "AppPlacements");

            migrationBuilder.DropTable(
                name: "AppSubsCatgories");

            migrationBuilder.DropTable(
                name: "AppTags");

            migrationBuilder.DropTable(
                name: "AppMarkets");

            migrationBuilder.DropTable(
                name: "AppSubscriptions");

            migrationBuilder.DropTable(
                name: "AppArtPieces");

            migrationBuilder.DropTable(
                name: "AppCountries");

            migrationBuilder.DropTable(
                name: "AppAddresses");

            migrationBuilder.DropTable(
                name: "AppArtLovers");

            migrationBuilder.DropTable(
                name: "AppArtists");

            migrationBuilder.DropTable(
                name: "AppDimensions");

            migrationBuilder.DropTable(
                name: "AppDisciplines");

            migrationBuilder.DropTable(
                name: "AppArtistsSubscriptions");

            migrationBuilder.DropTable(
                name: "AppContactPoints");

            migrationBuilder.DropTable(
                name: "AppSubscriptionTiers");

            migrationBuilder.DropTable(
                name: "AppPersons");
        }
    }
}
