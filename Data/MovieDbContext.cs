using MediaStoreEF_CF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MediaStoreEF_CF.Data
{
    public class MovieDbContext : DbContext
    {
        //Constructor; with DI you make the DbContext available to all controllers which need it.
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {             
        }

        public DbSet<Character> characters { get; set; }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Franchise> franchises { get; set; }

        #region Seeded data
        // Creating dummy data about franchises, movies and characters.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Creating dummy data about Franchises
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise
                {
                    Id = 1,
                    FranchiseTitle = "Lord of the Rings",
                    Description = "Fantasy adventure film"
                });
            modelBuilder.Entity<Franchise>()
                .HasData(new Franchise
                {
                    Id = 2,
                    FranchiseTitle = "The Marvel Cinematic Universe",
                    Description = "An American media franchise and shared universe centered on a series of superhero films produced by Marvel Studios."
                });

            // Creating dummy data about Movies
            modelBuilder.Entity<Movie>()
                .HasData(new Movie
                {
                    Id = 1,
                    MovieTitle = "The Fellowship of the Ring",
                    Genre = "Fantasy",
                    ReleaseYear = 2001,
                    Director = "Peter Jackson",
                    LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/8/8a/The_Lord_of_the_Rings_The_Fellowship_of_the_Ring_%282001%29.jpg",
                    Trailer = "https://www.youtube.com/watch?v=_e8QGuG50ro",
                    FranchiseId = 1
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie
                {
                    Id = 2,
                    MovieTitle = "The Two Towers",
                    Genre = "Fantasy",
                    ReleaseYear = 2002,
                    Director = "Peter Jackson",
                    LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/d/d0/Lord_of_the_Rings_-_The_Two_Towers_%282002%29.jpg",
                    Trailer = "https://www.youtube.com/watch?v=hYcw5ksV8YQ",
                    FranchiseId = 1
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie
                {
                    Id = 3,
                    MovieTitle = "The Return of the King",
                    Genre = "Fantasy",
                    ReleaseYear = 2003,
                    Director = "Peter Jackson",
                    LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/b/be/The_Lord_of_the_Rings_-_The_Return_of_the_King_%282003%29.jpg",
                    Trailer = "https://www.youtube.com/watch?v=r5X-hFf6Bwo",
                    FranchiseId = 1
                });
            modelBuilder.Entity<Movie>()
                .HasData(new Movie
                {
                    Id = 4,
                    MovieTitle = "The Hobbit: An Unexpected Journey",
                    Genre = "Fantasy",
                    ReleaseYear = 2012,
                    Director = "Peter Jackson",
                    LinkToMoviePicture = "https://upload.wikimedia.org/wikipedia/en/thumb/a/a9/The_Hobbit_trilogy_dvd_cover.jpg/220px-The_Hobbit_trilogy_dvd_cover.jpg",
                    Trailer = "https://www.youtube.com/watch?v=SDnYMbYB-nU",
                    FranchiseId = 1
                });

            // Creating dummy data about Characters
            modelBuilder.Entity<Character>()
                .HasData(new Character
                {
                    Id = 1,
                    CharacterName = "Liv Taylor",
                    Alias = "N/A",
                    Gender = 'F',
                    LinkToPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2f/Liv_Tyler_%2829566238128%29_%28cropped%29.jpg/220px-Liv_Tyler_%2829566238128%29_%28cropped%29.jpg",
                });
            modelBuilder.Entity<Character>()
                .HasData(new Character
                {
                    Id = 2,
                    CharacterName = "Viggo Mortensen",
                    Alias = "N/A",
                    Gender = 'M',
                    LinkToPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/64/Viggo_Mortensen_B_%282020%29.jpg/220px-Viggo_Mortensen_B_%282020%29.jpg"
                });
            modelBuilder.Entity<Character>()
                .HasData(new Character
                {
                    Id = 3,
                    CharacterName = "Martin John Christopher Freeman",
                    Alias = "N/A",
                    Gender = 'M',
                    LinkToPicture = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7f/Martin_Freeman-5341.jpg/220px-Martin_Freeman-5341.jpg"
                });

            // Filling up id's to ensure the connection between Character and Movie which has a many-to-many relationship.
            modelBuilder.Entity<Character>()
                .HasMany(p => p.Movies)
                .WithMany(t => t.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterMovie",
                    r => r.HasOne<Movie>().WithMany().HasForeignKey("MoviesId"),
                    l => l.HasOne<Character>().WithMany().HasForeignKey("CharactersId"),
                    je =>
                    {
                        je.HasKey("CharactersId", "MoviesId");
                        je.HasData(
                            new { CharactersId = 1, MoviesId = 1 },
                            new { CharactersId = 1, MoviesId = 2 },
                            new { CharactersId = 1, MoviesId = 3 },
                            new { CharactersId = 2, MoviesId = 1 },
                            new { CharactersId = 2, MoviesId = 2 },
                            new { CharactersId = 2, MoviesId = 3 },
                            new { CharactersId = 3, MoviesId = 4 });
                    });
        }
        #endregion
    }
}

