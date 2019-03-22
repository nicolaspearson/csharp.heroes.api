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

**3. Build and run the app**

#### Run the app in development mode:

```bash
dotnet run -c Debug --project ./src/HeroApi/HeroApi.csproj
```

The app will start running at <http://localhost:5000>

#### Run the app in release mode:

```bash
dotnet run -c Release --project ./src/HeroApi/HeroApi.csproj
```

The app will start running at <http://localhost:5000>

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
wrk -d1m http://localhost:5000/hero
```

![benchmark](/img/benchmark.png)
