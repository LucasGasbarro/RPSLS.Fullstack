# Decisions

I kept the API controller-based because it is easier to follow and fits the structure of the task well. The game rules live in services, while EF Core only handles persistence.

For data, I used SQLite for the scoreboard so results survive restarts without adding much complexity. The database stays inside the API process instead of being exposed directly.

On the frontend, I stayed with React + TypeScript and used routed pages for the main flow: home, gameplay, and leaderboard. React Query handles server state, while local state stays where it makes sense.

I also kept the app responsive and easy to use by adding light/dark mode, structured logging, correlation IDs, async/await across I/O, and standard `ProblemDetails` errors.

I changed the gameplay to a best-of-5 format so matches feel more challenging than a single-round game.

I thought about adding extra auth and session layers for security, but that would have changed the contract, so I left them out to stay within scope.
