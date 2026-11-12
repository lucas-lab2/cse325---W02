# Lucas Miranda Movies — CSE325 W02 ASP.NET Core MVC

ASP.NET Core MVC (.NET 10) movie app built by following Microsoft's
[Get started with ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-10.0&tabs=visual-studio-code)
tutorial (Visual Studio Code path), plus the BYU-Idaho W02 additional requirements.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio Code](https://code.visualstudio.com/) with the **C# Dev Kit** extension
- Internet access the first time, so `dotnet restore` can download the NuGet packages
  (`Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`)

## Run it

```bash
cd MvcMovie            # the folder that contains MvcMovie.csproj
dotnet restore
dotnet dev-certs https --trust    # one-time, trusts the local HTTPS dev certificate
dotnet run                        # or: dotnet watch
```

Open the `https://localhost:<port>` URL printed in the terminal, then click **Movies**.

## Database

- SQLite, via Entity Framework Core. Connection string: `appsettings.json` → `MvcMovieContext`
  (`Data Source=MvcMovie.Data.db`).
- On startup `Models/SeedData.cs` creates `MvcMovie.Data.db` if it does not exist and inserts the
  favorite movies **only when the Movie table is empty**, so restarting never creates duplicates.
- Seeded movies: *Interstellar* (2014), *The Dark Knight* (2008),
  *The Lord of the Rings: The Fellowship of the Ring* (2001).
- To reset the data, stop the app and delete `MvcMovie.Data.db`.
- The `.db` file is git-ignored; it is regenerated automatically.

### Optional: use EF Core migrations instead of `EnsureCreated`

The tutorial's `dotnet ef migrations add` step needs the `dotnet-ef` tool. This project creates the
schema with `Database.EnsureCreated()` so it runs with zero extra steps. If you prefer migrations:

```bash
dotnet tool install --global dotnet-ef
rm MvcMovie.Data.db                        # (Windows: del MvcMovie.Data.db)
dotnet ef migrations add InitialCreate
```

then in `Models/SeedData.cs` replace `context.Database.EnsureCreated();` with `context.Database.Migrate();`.

## What was implemented

Tutorial modules: Get Started · Add a controller (`HelloWorldController`, `/HelloWorld`,
`/HelloWorld/Welcome?name=Lucas&numTimes=3`) · Add a view · Add a model (`Movie`) ·
Work with a database (EF Core + SQLite + seed data) · Controller actions and views (Movies CRUD) ·
Add search (title + genre) · Add a new field (`Rating`) · Add validation (Required, StringLength,
RegularExpression, Range, client + server side) · Details and Delete.

Additional assignment requirements:

| Requirement | Where |
|---|---|
| App title "Lucas Miranda Movies" | `Views/Shared/_Layout.cshtml` (browser title, navbar brand, footer), `Views/Home/Index.cshtml` |
| At least 3 favorite movies in the data | `Models/SeedData.cs` |
| Listing heading "My Favorite Movies" (not "Index") | `Views/Movies/Index.cshtml` (`<h1>` and page title) |
| Search by year ("selected year or newer") | `MoviesController.Index` (`movieYear`) + year drop-down in `Views/Movies/Index.cshtml`; combines with title and genre filters |
| Padding on input / select / textarea | `wwwroot/css/site.css` (bottom of file); used by Create, Edit and the search form |

## Project layout

```
Controllers/   HomeController, HelloWorldController, MoviesController
Data/          MvcMovieContext (EF Core DbContext)
Models/        Movie, MovieGenreViewModel, SeedData, ErrorViewModel
Views/         Home, HelloWorld, Movies (Index/Create/Edit/Details/Delete), Shared
wwwroot/       Bootstrap, jQuery, jQuery Validation, site.css
```
