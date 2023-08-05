﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using recipies_ms.Db;

namespace recipies_ms.Migrations
{
    [DbContext(typeof(RecipeContext))]
    partial class RecipeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.5.21301.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("recipies_ms.Db.Models.Ingredient", b =>
                {
                    b.Property<Guid>("IngredientKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("ingredient_key");

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ingredient_name");

                    b.Property<string>("Note")
                        .HasColumnType("text")
                        .HasColumnName("ingredient_note");

                    b.HasKey("IngredientKey");

                    b.ToTable("ingredient");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.IngredientNutrition", b =>
                {
                    b.Property<Guid>("NutritionKey")
                        .HasColumnType("uuid")
                        .HasColumnName("nutrition_key");

                    b.Property<float>("Amount")
                        .HasColumnType("real")
                        .HasColumnName("amount");

                    b.Property<float>("FatContentPercentageAmount")
                        .HasColumnType("real")
                        .HasColumnName("fat_content_percentage");

                    b.Property<float>("FibrePercentageContent")
                        .HasColumnType("real")
                        .HasColumnName("fibre_content_percentage");

                    b.Property<float>("KjEnergyContent")
                        .HasColumnType("real")
                        .HasColumnName("kj_energy_content");

                    b.Property<float>("ProteinPercentageContent")
                        .HasColumnType("real")
                        .HasColumnName("protein_content_percentage");

                    b.Property<float>("SaccharidesPercentageContent")
                        .HasColumnType("real")
                        .HasColumnName("saccharides_content_percentage");

                    b.Property<float>("SaltPercentageContent")
                        .HasColumnType("real")
                        .HasColumnName("salt_content_percentage");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("unit");

                    b.HasKey("NutritionKey");

                    b.ToTable("ingredient_nutrition");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.RecipeIngredientItem", b =>
                {
                    b.Property<Guid>("RecipeItemId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipe_key");

                    b.Property<Guid>("IngredientId")
                        .HasColumnType("uuid")
                        .HasColumnName("ingredient_key");

                    b.Property<float>("Amount")
                        .HasColumnType("real")
                        .HasColumnName("amount");

                    b.Property<string>("IngredientRecipeNote")
                        .HasColumnType("text")
                        .HasColumnName("ingredient_recipe_note");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("unit");

                    b.HasKey("RecipeItemId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("recipe_ingredient_rel");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.RecipeItem", b =>
                {
                    b.Property<Guid>("RecipeKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("recipe_key");

                    b.Property<string>("RecipeDescription")
                        .HasColumnType("text")
                        .HasColumnName("recipe_desc");

                    b.Property<string>("RecipeName")
                        .HasColumnType("text")
                        .HasColumnName("recipe_name");

                    b.HasKey("RecipeKey");

                    b.ToTable("recipe");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.Unit", b =>
                {
                    b.Property<string>("SiUnit")
                        .HasColumnType("text")
                        .HasColumnName("si_unit");

                    b.HasKey("SiUnit");

                    b.ToTable("unit");

                    b.HasData(
                        new
                        {
                            SiUnit = "Milliliter"
                        },
                        new
                        {
                            SiUnit = "Cup"
                        },
                        new
                        {
                            SiUnit = "Piece"
                        },
                        new
                        {
                            SiUnit = "TeaSpoon"
                        },
                        new
                        {
                            SiUnit = "TableSpoon"
                        },
                        new
                        {
                            SiUnit = "Gram"
                        });
                });

            modelBuilder.Entity("recipies_ms.Db.Models.UnitConversion", b =>
                {
                    b.Property<string>("SiUnitSource")
                        .HasColumnType("text")
                        .HasColumnName("si_unit_source");

                    b.Property<string>("SiUnitTarget")
                        .HasColumnType("text")
                        .HasColumnName("si_unit_target");

                    b.Property<float>("AmountSource")
                        .HasColumnType("real")
                        .HasColumnName("amount_source");

                    b.Property<float>("AmountTarget")
                        .HasColumnType("real")
                        .HasColumnName("amount_target");

                    b.HasKey("SiUnitSource", "SiUnitTarget");

                    b.ToTable("unit_conversion_rel");
                });

            modelBuilder.Entity("sina.messaging.contracts.RecipeScheduleCreated", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("schedule_id");

                    b.Property<float>("PlannedPortions")
                        .HasColumnType("real")
                        .HasColumnName("planned_recipe_multiplier");

                    b.Property<Guid>("RecipeId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipe_id");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("recipe_name");

                    b.Property<DateTime>("ScheduleDatetime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("schedule_date");

                    b.HasKey("ScheduleId");

                    b.ToTable("schedule");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.IngredientNutrition", b =>
                {
                    b.HasOne("recipies_ms.Db.Models.Ingredient", "Ingredient")
                        .WithOne("ingredientNutritionalValue")
                        .HasForeignKey("recipies_ms.Db.Models.IngredientNutrition", "NutritionKey");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.RecipeIngredientItem", b =>
                {
                    b.HasOne("recipies_ms.Db.Models.Ingredient", null)
                        .WithMany("recipeIngredientItem")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("recipies_ms.Db.Models.RecipeItem", null)
                        .WithMany("Ingredient")
                        .HasForeignKey("RecipeItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("recipies_ms.Db.Models.Ingredient", b =>
                {
                    b.Navigation("ingredientNutritionalValue");

                    b.Navigation("recipeIngredientItem");
                });

            modelBuilder.Entity("recipies_ms.Db.Models.RecipeItem", b =>
                {
                    b.Navigation("Ingredient");
                });
#pragma warning restore 612, 618
        }
    }
}
