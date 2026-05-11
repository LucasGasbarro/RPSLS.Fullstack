import { memo } from 'react'
import type { ScoreEntry } from '../types'

type Props = {
  entries: ScoreEntry[]
}

export const Scoreboard = memo(function Scoreboard({ entries }: Props) {
  if (entries.length === 0) {
    return (
      <div className="rounded-3xl border border-dashed border-slate-300 p-6 text-slate-500 dark:border-slate-700 dark:text-slate-400">
        No ranked players yet.
      </div>
    )
  }

  return (
    <div className="grid gap-3">
      {entries.map((entry, index) => (
        <article
          key={entry.playerName}
          className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm dark:border-slate-800 dark:bg-slate-950"
        >
          <div className="flex flex-wrap items-center justify-between gap-3">
            <div>
              <p className="font-semibold">#{index + 1} {entry.playerName}</p>
              <p className="text-sm text-slate-500 dark:text-slate-400">Series wins: {entry.points}</p>
            </div>
            <p className="rounded-full bg-sky-50 px-3 py-1 text-sm font-semibold text-sky-700 dark:bg-sky-950/40 dark:text-sky-300">
              {entry.points} pts
            </p>
          </div>
        </article>
      ))}
    </div>
  )
})
