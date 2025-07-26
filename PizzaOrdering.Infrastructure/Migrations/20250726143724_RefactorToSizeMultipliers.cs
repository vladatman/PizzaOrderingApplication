using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaOrdering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorToSizeMultipliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Pizzas");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "OrderItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Pizzas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
