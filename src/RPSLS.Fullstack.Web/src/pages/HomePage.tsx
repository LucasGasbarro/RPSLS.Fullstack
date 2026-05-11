import { Link } from 'react-router-dom'

export function HomePage() {
  return (
    <main className="mx-auto flex min-h-[calc(100vh-5rem)] max-w-6xl flex-col justify-center px-4 py-10 sm:px-6 lg:px-8">
      <section className="grid gap-10 lg:grid-cols-[1.3fr_0.7fr] lg:items-center">
        <div className="space-y-6">
          <span className="inline-flex rounded-full border border-sky-200 bg-sky-50 px-4 py-1 text-sm font-medium text-sky-700 dark:border-sky-900 dark:bg-sky-950 dark:text-sky-300">
            .NET 8 · React · SQLite
          </span>
          <div className="space-y-4">
            <h1 className="max-w-3xl text-5xl font-black tracking-tight text-slate-950 dark:text-white sm:text-6xl">
              Rock, Paper, Scissors, Lizard, Spock
            </h1>
            <p className="max-w-2xl text-lg leading-8 text-slate-600 dark:text-slate-300">
              A modern full-stack implementation of the extended game, built with a clean ASP.NET Core API, a
              responsive React interface, SQLite persistence, and a simple bonus scoreboard.
            </p>
          </div>
          <div className="flex flex-wrap gap-4">
            <Link
              to="/game"
              className="rounded-full bg-slate-950 px-8 py-4 text-base font-semibold text-white shadow-lg transition hover:-translate-y-0.5 hover:bg-slate-800 dark:bg-white dark:text-slate-950 dark:hover:bg-slate-200"
            >
              Start playing
            </Link>
            <Link
              to="/leaderboard"
              className="rounded-full border border-slate-300 px-8 py-4 text-base font-semibold text-slate-900 transition hover:bg-slate-100 dark:border-slate-700 dark:text-slate-100 dark:hover:bg-slate-900"
            >
              View leaderboard
            </Link>
          </div>
        </div>

        <div className="grid gap-4 rounded-3xl border border-slate-200 bg-white p-6 shadow-sm dark:border-slate-800 dark:bg-slate-950">
          <h2 className="text-xl font-semibold text-slate-950 dark:text-white">How it works</h2>
          <div className="space-y-3 text-sm leading-6 text-slate-600 dark:text-slate-300">
            <p>The API exposes the required choices and play endpoints from the specification.</p>
            <p>The computer move is generated through the provided random service endpoint.</p>
            <p>Round results are stored in SQLite and shown on the leaderboard page.</p>
            <p>React Query powers the data flow, while the UI keeps a responsive light/dark layout.</p>
          </div>
          <div className="rounded-2xl bg-slate-50 p-4 text-sm text-slate-600 dark:bg-slate-900 dark:text-slate-300">
            <p className="font-semibold text-slate-950 dark:text-white">Stack</p>
            <p>ASP.NET Core 8, EF Core SQLite, React 19, TypeScript, Vite, React Router, React Query, Tailwind CSS.</p>
          </div>
        </div>
      </section>
    </main>
  )
}
