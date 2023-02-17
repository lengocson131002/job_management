using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class V2UpdateManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resume_JobDescription_JobDescriptionId",
                table: "Resume");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_JobDescription_JobDescriptionId",
                table: "Skill");

            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Resume_ResumeId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_JobDescriptionId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_ResumeId",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Resume_JobDescriptionId",
                table: "Resume");

            migrationBuilder.DropColumn(
                name: "JobDescriptionId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "ResumeId",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "JobDescriptionId",
                table: "Resume");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Application",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Application",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Application");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Application");

            migrationBuilder.AddColumn<long>(
                name: "JobDescriptionId",
                table: "Skill",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResumeId",
                table: "Skill",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "JobDescriptionId",
                table: "Resume",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_JobDescriptionId",
                table: "Skill",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_ResumeId",
                table: "Skill",
                column: "ResumeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resume_JobDescriptionId",
                table: "Resume",
                column: "JobDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resume_JobDescription_JobDescriptionId",
                table: "Resume",
                column: "JobDescriptionId",
                principalTable: "JobDescription",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_JobDescription_JobDescriptionId",
                table: "Skill",
                column: "JobDescriptionId",
                principalTable: "JobDescription",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Resume_ResumeId",
                table: "Skill",
                column: "ResumeId",
                principalTable: "Resume",
                principalColumn: "Id");
        }
    }
}
