import { memo } from 'react'
import type { Choice, PlayResponse } from '../types'

type Props = {
  result: PlayResponse
  playerName: string
  choices: Choice[]
}

function labelForChoice(id: number, choices: Choice[]) {
  return choices.find((choice) => choice.id === id)?.name ?? `#${id}`
}

function iconForChoice(name: string) {
  switch (name) {
    case 'rock':
      return '🪨'
    case 'paper':
      return '📄'
    case 'scissors':
      return '✂️'
    case 'lizard':
      return '🦎'
    case 'spock':
      return '🖖'
    default:
      return '🎮'
  }
}

function outcomeClass(result: PlayResponse['results']) {
  switch (result) {
    case 'win':
      return 'bg-green-50 text-green-700 dark:bg-green-950/40 dark:text-green-200'
    case 'lose':
      return 'bg-red-50 text-red-700 dark:bg-red-950/40 dark:text-red-200'
    default:
      return 'bg-slate-50 text-slate-700 dark:bg-slate-900 dark:text-slate-300'
  }
}

export const GameResult = memo(function GameResult({ result, playerName, choices }: Props) {
  return (
    <section className="p-6 text-center">
      <p className="text-sm uppercase tracking-[0.3em] text-slate-500 dark:text-slate-400">Round result</p>
      <h3 className="mt-2 text-2xl font-semibold text-slate-950 dark:text-white">
        {result.results === 'win' ? '🏆 Victory!' : result.results === 'lose' ? '💥 Defeat!' : '🤝 Tie!'}
      </h3>
      <p className="mt-2 text-base text-slate-600 dark:text-slate-300">
        {playerName ? `${playerName}, you ${result.results}!` : `You ${result.results}!`}
      </p>
      <dl className="mt-5 grid gap-3 text-sm sm:grid-cols-3">
        <div className="rounded-2xl bg-slate-50 p-4 text-center dark:bg-slate-900">
          <dt className="text-slate-500 dark:text-slate-400">Player</dt>
          <dd className="mt-1 font-semibold capitalize">
            {iconForChoice(labelForChoice(result.player, choices))} {labelForChoice(result.player, choices)}
          </dd>
        </div>
        <div className="rounded-2xl bg-slate-50 p-4 text-center dark:bg-slate-900">
          <dt className="text-slate-500 dark:text-slate-400">Computer</dt>
          <dd className="mt-1 font-semibold capitalize">
            {iconForChoice(labelForChoice(result.computer, choices))} {labelForChoice(result.computer, choices)}
          </dd>
        </div>
        <div className={`rounded-2xl p-4 text-center ${outcomeClass(result.results)}`}>
          <dt className="text-slate-500 dark:text-slate-400">Outcome</dt>
          <dd className="mt-1 font-semibold uppercase tracking-wide">
            {result.results === 'win' ? '🏆 win' : result.results === 'lose' ? '💀 lose' : '🤝 tie'}
          </dd>
        </div>
      </dl>
    </section>
  )
})
