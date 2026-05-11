# RPSSL Fullstack

This is a small Rock Paper Scissors Lizard Spock app built with a .NET 8 API, React + TypeScript, SQLite, and React Query.

## What's inside

The backend exposes the game endpoints and stores the scoreboard in SQLite. The frontend is a React app with routing, light/dark mode, and a simple flow for playing, checking the leaderboard, and starting over.

## Stack

- Backend: ASP.NET Core Web API
- Frontend: React 19 + TypeScript + Vite
- Data: SQLite + EF Core
- Styling: Tailwind CSS

## Run it locally

Start the API:

```bash
dotnet run --project src/RPSLS.Fullstack.Api/RPSLS.Fullstack.Api.csproj
```

Start the frontend:

```bash
cd src/RPSLS.Fullstack.Web
npm install
npm run dev
```

## Docker

```bash
docker compose up --build
```

That runs everything on port `8080` and keeps the SQLite file in a volume.

## API routes

- `GET /choices`
- `GET /choice`
- `POST /play`
- `GET /scoreboard`
- `DELETE /scoreboard`

## Note

I used GitHub Copilot to help build the project structure and handle frontend layout work.
