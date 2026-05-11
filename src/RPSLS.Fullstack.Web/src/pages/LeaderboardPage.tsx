import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { Link } from 'react-router-dom'
import { getScoreboard, resetScoreboard } from '../api/client'
import { Scoreboard } from '../components/Scoreboard'

export function LeaderboardPage() {
  const queryClient = useQueryClient()
  const scoreboardQuery = useQuery({ queryKey: ['scoreboard'], queryFn: getScoreboard })

  const resetMutation = useMutation({
    mutationFn: resetScoreboard,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['scoreboard'] })
    },
  })

  return (
    <main className="mx-auto max-w-6xl px-4 py-10 sm:px-6 lg:px-8">
      <section className="rounded-3xl border border-slate-200 bg-white p-6 shadow-sm dark:border-slate-800 dark:bg-slate-950">
        <div className="flex flex-wrap items-center justify-between gap-4">
          <div>
            <p className="text-sm uppercase tracking-[0.3em] text-slate-500 dark:text-slate-400">Leaderboard</p>
            <h1 className="mt-2 text-3xl font-bold text-slate-950 dark:text-white">Player ranking</h1>
          </div>
          <div className="flex flex-wrap gap-3">
            <button
              type="button"
              onClick={() => resetMutation.mutate()}
              className="rounded-full border border-slate-300 px-5 py-2.5 text-sm font-semibold text-slate-900 dark:border-slate-700 dark:text-slate-100"
            >
              Reset
            </button>
            <Link
              to="/game"
              className="rounded-full bg-slate-950 px-5 py-2.5 text-sm font-semibold text-white dark:bg-white dark:text-slate-950"
            >
              Play
            </Link>
          </div>
        </div>

        <div className="mt-6">
          {scoreboardQuery.isLoading ? (
            <div className="text-slate-500 dark:text-slate-400">Loading leaderboard...</div>
          ) : scoreboardQuery.isError ? (
            <div className="rounded-2xl bg-red-50 p-4 text-red-700 dark:bg-red-950/40 dark:text-red-200">
              Could not load the leaderboard.
            </div>
          ) : (
            <Scoreboard entries={scoreboardQuery.data ?? []} />
          )}
        </div>
      </section>
    </main>
  )
}
