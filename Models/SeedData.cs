using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MvcMovieContext>();

        // Creates the SQLite file and the Movie table on first run (no-op afterwards).
        context.Database.Migrate();

        // Seed only when the table is empty, so restarting never duplicates records.
        if (context.Movie.Any())
        {
            return;
        }

        context.Movie.AddRange(
            new Movie
            {
                Title = "Interstellar",
                ReleaseDate = new DateTime(2014, 11, 7),
                Genre = "Science Fiction",
                Rating = "PG-13",
                Price = 9.99M
            },
            new Movie
            {
                Title = "The Dark Knight",
                ReleaseDate = new DateTime(2008, 7, 18),
                Genre = "Action",
                Rating = "PG-13",
                Price = 8.99M
            },
            new Movie
            {
                Title = "The Lord of the Rings: The Fellowship of the Ring",
                ReleaseDate = new DateTime(2001, 12, 19),
                Genre = "Fantasy",
                Rating = "PG-13",
                Price = 7.99M
            });

        context.SaveChanges();
    }
}
