﻿// <auto-generated />
using System;
using MediaStoreEF_CF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediaStoreEF_CF.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    partial class MovieDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CharacterMovie", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("int");

                    b.Property<int>("MoviesId")
                        .HasColumnType("int");

                    b.HasKey("CharactersId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("CharacterMovie");

                    b.HasData(
                        new
                        {
                            CharactersId = 1,
                            MoviesId = 1
                        },
                        new
                        {
                            CharactersId = 1,
                            MoviesId = 2
                        },
                        new
                        {
                            CharactersId = 1,
                            MoviesId = 3
                        },
                        new
                        {
                            CharactersId = 2,
                            MoviesId = 1
                        },
                        new
                        {
                            CharactersId = 2,
                            MoviesId = 2
                        },
                        new
                        {
                            CharactersId = 2,
                            MoviesId = 3
                        },
                        new
                        {
                            CharactersId = 3,
                            MoviesId = 4
                        });
                });

            modelBuilder.Entity("MediaStoreEF_CF.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CharacterName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LinkToPicture")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Alias = "N/A",
                            CharacterName = "Liv Taylor",
                            Gender = "F",
                            LinkToPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Liv_Tyler_%2829566238128%29_%28cropped%29.jpg/220px-Liv_Tyler_%2829566238128%29_%28cropped%29.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Alias = "N/A",
                            CharacterName = "Viggo Mortensen",
                            Gender = "M",
                            LinkToPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Viggo_Mortensen_B_%282020%29.jpg/220px-Viggo_Mortensen_B_%282020%29.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Alias = "N/A",
                            CharacterName = "Martin John Christopher Freeman",
                            Gender = "M",
                            LinkToPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Martin_Freeman-5341.jpg/220px-Martin_Freeman-5341.jpg"
                        });
                });

            modelBuilder.Entity("MediaStoreEF_CF.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("FranchiseTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("franchises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Fantasy adventure film",
                            FranchiseTitle = "Lord of the Rings"
                        },
                        new
                        {
                            Id = 2,
                            Description = "An American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios.",
                            FranchiseTitle = "The Marvel Cinematic Universe"
                        });
                });

            modelBuilder.Entity("MediaStoreEF_CF.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Director")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Genre")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LinkToMoviePicture")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<string>("Trailer")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Director = "Peter Jackson",
                            FranchiseId = 1,
                            Genre = "Fantasy",
                            LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_%282001%29.jpg",
                            MovieTitle = "The Fellowship of the Ring",
                            ReleaseYear = 2001,
                            Trailer = "https://www.youtube.com/watch?v=_e8QGuG50ro"
                        },
                        new
                        {
                            Id = 2,
                            Director = "Peter Jackson",
                            FranchiseId = 1,
                            Genre = "Fantasy",
                            LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/d/d0/Lord_of_the_Rings_-_The_Two_Towers_%282002%29.jpg",
                            MovieTitle = "The Two Towers",
                            ReleaseYear = 2002,
                            Trailer = "https://www.youtube.com/watch?v=hYcw5ksV8YQ"
                        },
                        new
                        {
                            Id = 3,
                            Director = "Peter Jackson",
                            FranchiseId = 1,
                            Genre = "Fantasy",
                            LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/b/be/The_Lord_of_the_Rings_-_The_Return_of_the_King_%282003%29.jpg",
                            MovieTitle = "The Return of the King",
                            ReleaseYear = 2003,
                            Trailer = "https://www.youtube.com/watch?v=r5X-hFf6Bwo"
                        },
                        new
                        {
                            Id = 4,
                            Director = "Peter Jackson",
                            FranchiseId = 1,
                            Genre = "Fantasy",
                            LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a9/The_Hobbit_trilogy_dvd_cover.jpg/220px-The_Hobbit_trilogy_dvd_cover.jpg",
                            MovieTitle = "The Hobbit: An Unexpected Journey",
                            ReleaseYear = 2012,
                            Trailer = "https://www.youtube.com/watch?v=SDnYMbYB-nU"
                        });
                });

            modelBuilder.Entity("CharacterMovie", b =>
                {
                    b.HasOne("MediaStoreEF_CF.Models.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaStoreEF_CF.Models.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaStoreEF_CF.Models.Movie", b =>
                {
                    b.HasOne("MediaStoreEF_CF.Models.Franchise", "Franchise")
                        .WithMany("Movies")
                        .HasForeignKey("FranchiseId");

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("MediaStoreEF_CF.Models.Franchise", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
