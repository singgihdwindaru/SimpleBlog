using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace simpleBlog.Api.data.migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    role_name = table.Column<string>(type: "character varying", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    role_description = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    updated_by = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    nama = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_status", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    username = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "artikel",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'6', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    pub_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    author_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    excerpt = table.Column<string>(type: "text", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_artikel", x => x.id);
                    table.ForeignKey(
                        name: "fk_artikel_status",
                        column: x => x.author_id,
                        principalTable: "status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_artikel_user",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "userrole_fkroleid",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "userrole_fkUserid",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "fki_fk_artikel_user",
                table: "artikel",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "fki_userrole_fkroleid",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "fki_userrole_fkUserid",
                table: "user_roles",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "artikel");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "status");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
