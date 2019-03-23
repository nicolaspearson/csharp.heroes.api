# C# Heroes API

A simple REST API that provides CRUD operations on a `hero` object, it was built using C# and PostgreSQL.

## Getting Started

**1. Clone the application**

```bash
git clone https://github.com/nicolaspearson/csharp.heroes.api.git
```

**2. Start the database**

```bash
docker-compose up
```

**3. Migrations**

Create the initial migrations, if they do not exist:

```bash
dotnet ef migrations add initial
```

Run the migrations:

```bash
dotnet ef database update
```

**4. Build and run the app**

#### Run the app in development mode:

```bash
dotnet run -c Debug --project ./src/HeroApi/HeroApi.csproj
```

The app will start running at <http://localhost:8000>

#### Run the app in release mode:

```bash
dotnet run -c Release --project ./src/HeroApi/HeroApi.csproj
```

The app will start running at <http://localhost:8000>

## How I Created The Project

Create `src` and `test` directories:

```bash
mkdir src
mkdir test
```

Initialize the Web Api Project:

```bash
cd src
dotnet new webapi -o HeroApi
```

Add PostgreSQL packages:

```bash
cd ./src/HeroApi
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package NpgSql.EntityFrameworkCore.PostgreSQL.Design
```

Added connection string to `appsettings.json`:

```json
{
  ...
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=master;Password=masterkey;Database=hero;"
  }
}
```

Connect to the database in the `Startup` class:

```csharp
        ...

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HeroContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            ...
        }

        ...
```

## Endpoints

The following endpoints are available:

```
GET /hero/{heroId}
```

```
GET /hero
```

```
POST /hero
```

```
PUT /hero/{heroId}
```

```
DELETE /hero/{heroId}
```

## Benchmarking

Run this command to benchmark request performance:

```
wrk -d1m http://localhost:8000/hero
```

![benchmark](/img/benchmark.png)
