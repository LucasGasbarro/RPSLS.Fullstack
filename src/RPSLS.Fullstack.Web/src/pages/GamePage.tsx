import { useCallback, useEffect, useMemo, useRef, useState } from 'react'
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query'
import { Link } from 'react-router-dom'
import { ChoiceButton } from '../components/ChoiceButton'
import { GameResult } from '../components/GameResult'
import { getChoices, playRound, recordSeriesWin } from '../api/client'
import type { Choice, PlayResponse } from '../types'

const MINIMUM_THINKING_TIME_MS = 1500

function sleep(milliseconds: number) {
  return new Promise<void>((resolve) => {
    window.setTimeout(resolve, milliseconds)
  })
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

function scoreToneClass(sideWins: number, otherWins: number) {
  if (sideWins === otherWins) {
    return 'bg-slate-100 text-slate-900 dark:bg-slate-900 dark:text-slate-100'
  }

  const leaderWins = Math.max(sideWins, otherWins)
  const intensity = leaderWins >= 3 ? 3 : leaderWins

  if (sideWins > otherWins) {
    if (intensity === 1) {
      return 'bg-green-100 text-green-800 dark:bg-green-900/40 dark:text-green-200'
    }
    if (intensity === 2) {
      return 'bg-green-200 text-green-900 dark:bg-green-800/50 dark:text-green-100'
    }
    return 'bg-green-300 text-green-950 dark:bg-green-700/60 dark:text-green-50'
  }

  if (intensity === 1) {
    return 'bg-red-100 text-red-800 dark:bg-red-900/40 dark:text-red-200'
  }
  if (intensity === 2) {
    return 'bg-red-200 text-red-900 dark:bg-red-800/50 dark:text-red-100'
  }
  return 'bg-red-300 text-red-950 dark:bg-red-700/60 dark:text-red-50'
}

function NameModal({
  open,
  defaultValue,
  onSubmit,
}: {
  open: boolean
  defaultValue: string
  onSubmit: (name: string) => void
}) {
  const [value, setValue] = useState(defaultValue)
  const inputRef = useRef<HTMLInputElement>(null)

  useEffect(() => {
    if (!open) {
      return
    }

    setValue(defaultValue)
    const timeout = window.setTimeout(() => {
      inputRef.current?.focus()
    }, 0)

    return () => window.clearTimeout(timeout)
  }, [defaultValue, open])

  if (!open) {
    return null
  }

  return (
    <div className="fixed inset-0 z-30 flex items-center justify-center bg-slate-950/70 p-4 backdrop-blur-sm">
      <form
        onSubmit={(event) => {
          event.preventDefault()
          const nextValue = value.trim()
          if (!nextValue) {
            return
          }

          onSubmit(nextValue)
        }}
        className="w-full max-w-md rounded-3xl border border-white/10 bg-white p-6 shadow-2xl dark:bg-slate-950"
      >
        <p className="text-sm uppercase tracking-[0.3em] text-slate-500 dark:text-slate-400">Before we start</p>
        <h2 className="mt-2 text-2xl font-semibold text-slate-950 dark:text-white">Enter your player name</h2>
        <p className="mt-3 text-sm leading-6 text-slate-600 dark:text-slate-300">
          This name will be shown on the game board while you play.
        </p>
        <label className="mt-5 block">
          <span className="mb-2 block text-sm font-medium text-slate-700 dark:text-slate-300">Player name</span>
          <input
            ref={inputRef}
            value={value}
            onChange={(event) => setValue(event.target.value)}
            placeholder="e.g. Lucas"
            className="w-full rounded-2xl border border-slate-300 bg-white px-4 py-3 text-slate-950 outline-none transition placeholder:text-slate-400 focus:border-sky-500 dark:border-slate-700 dark:bg-slate-900 dark:text-white"
          />
        </label>
        <button
          type="submit"
          disabled={!value.trim()}
          className="mt-5 w-full rounded-full bg-slate-950 px-6 py-3 font-semibold text-white transition hover:bg-slate-800 disabled:cursor-not-allowed disabled:opacity-50 dark:bg-white dark:text-slate-950 dark:hover:bg-slate-200"
        >
          Start game
        </button>
      </form>
    </div>
  )
}

function EndGameActions({
  isGameOver,
  winner,
  onPlayAgain,
  onChangeName,
}: {
  isGameOver: boolean
  winner: 'player' | 'computer' | null
  onPlayAgain: () => void
  onChangeName: () => void
}) {
  if (!isGameOver) {
    return null
  }

  return (
    <section className="rounded-3xl border border-slate-200 bg-white p-6 shadow-sm dark:border-slate-800 dark:bg-slate-950">
      <p className="text-sm uppercase tracking-[0.3em] text-slate-500 dark:text-slate-400">Game complete</p>
      <h2 className="mt-2 text-2xl font-semibold text-slate-950 dark:text-white">
        {winner === 'player' ? '🏆 You won this best-of-5!' : '💥 Computer won this best-of-5!'}
      </h2>
      <p className="mt-2 text-sm text-slate-600 dark:text-slate-300">What would you like to do next?</p>
      <div className="mt-6 grid gap-3 sm:grid-cols-3">
        <button
          type="button"
          onClick={onPlayAgain}
          className="play-again-pulse rounded-full bg-slate-950 px-6 py-3 font-semibold text-white transition hover:bg-slate-800 dark:bg-white dark:text-slate-950 dark:hover:bg-slate-200"
        >
          Play again
        </button>
        <button
          type="button"
          onClick={onChangeName}
          className="rounded-full border border-slate-300 px-6 py-3 font-semibold text-slate-900 transition hover:bg-slate-100 dark:border-slate-700 dark:text-slate-100 dark:hover:bg-slate-900"
        >
          Change name and play again
        </button>
        <Link
          to="/leaderboard"
          className="rounded-full border border-slate-300 px-6 py-3 text-center font-semibold text-slate-900 transition hover:bg-slate-100 dark:border-slate-700 dark:text-slate-100 dark:hover:bg-slate-900"
        >
          Leaderboard
        </Link>
      </div>
    </section>
  )
}

export function GamePage() {
  const queryClient = useQueryClient()
  const choicesQuery = useQuery({ queryKey: ['choices'], queryFn: getChoices })
  const [playerName, setPlayerName] = useState('')
  const [nameDraft, setNameDraft] = useState('')
  const [isNameModalOpen, setIsNameModalOpen] = useState(true)
  const [round, setRound] = useState<PlayResponse | null>(null)
  const [rounds, setRounds] = useState<PlayResponse[]>([])
  const hasRecordedSeriesWinRef = useRef(false)

  const playMutation = useMutation({
    mutationFn: async (choiceId: number) => {
      const [result] = await Promise.all([playRound(choiceId), sleep(MINIMUM_THINKING_TIME_MS)])
      return result
    },
    onSuccess: async (result) => {
      setRound(result)
      setRounds((currentRounds) => [...currentRounds, result])
      await queryClient.invalidateQueries({ queryKey: ['scoreboard'] })
    },
  })

  const recordSeriesWinMutation = useMutation({
    mutationFn: recordSeriesWin,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['scoreboard'] })
    },
  })

  const choices = useMemo(() => choicesQuery.data ?? [], [choicesQuery.data])
  const isThinking = playMutation.isPending
  const score = useMemo(
    () =>
      rounds.reduce(
        (currentScore, currentRound) => {
          if (currentRound.results === 'win') {
            return { ...currentScore, playerWins: currentScore.playerWins + 1 }
          }
          if (currentRound.results === 'lose') {
            return { ...currentScore, computerWins: currentScore.computerWins + 1 }
          }
          return { ...currentScore, ties: currentScore.ties + 1 }
        },
        { playerWins: 0, computerWins: 0, ties: 0 },
      ),
    [rounds],
  )
  const isGameOver = score.playerWins >= 3 || score.computerWins >= 3
  const winner: 'player' | 'computer' | null = score.playerWins >= 3 ? 'player' : score.computerWins >= 3 ? 'computer' : null
  const isGameplayLocked = isThinking || isNameModalOpen || isGameOver
  const isPlayerTurn = !isGameplayLocked && !!playerName && !choicesQuery.isLoading && !choicesQuery.isError

  useEffect(() => {
    if (!isGameOver || winner !== 'player' || !playerName.trim() || hasRecordedSeriesWinRef.current) {
      return
    }

    hasRecordedSeriesWinRef.current = true
    recordSeriesWinMutation.mutate(playerName)
  }, [isGameOver, winner, playerName, recordSeriesWinMutation])

  const handleSelect = useCallback(
    (choiceId: number) => {
      if (isGameplayLocked) {
        return
      }

      playMutation.mutate(choiceId)
    },
    [isGameplayLocked, playMutation],
  )

  const openNameModal = useCallback(() => {
    setNameDraft(playerName)
    setRound(null)
    setRounds([])
    hasRecordedSeriesWinRef.current = false
    setIsNameModalOpen(true)
  }, [playerName])

  const closeNameModal = useCallback((nextName: string) => {
    setPlayerName(nextName)
    setRound(null)
    setRounds([])
    hasRecordedSeriesWinRef.current = false
    setIsNameModalOpen(false)
  }, [])

  useEffect(() => {
    setIsNameModalOpen(true)
  }, [])

  const cheatsheet = useMemo(
    () => [
      { choiceId: 1, beats: [3, 4] },
      { choiceId: 2, beats: [1, 5] },
      { choiceId: 3, beats: [2, 4] },
      { choiceId: 4, beats: [2, 5] },
      { choiceId: 5, beats: [1, 3] },
    ],
    [],
  )

  return (
    <main className="mx-auto max-w-6xl px-4 pb-10 pt-10 sm:px-6 lg:px-8">
      <NameModal open={isNameModalOpen} defaultValue={nameDraft} onSubmit={closeNameModal} />

      <section className="space-y-6">
        <section className="rounded-3xl border border-slate-200 bg-white p-4 shadow-sm dark:border-slate-800 dark:bg-slate-950 sm:p-5">
          <div className="flex flex-col gap-3">
            <span className="w-fit rounded-full bg-sky-50 px-3 py-1 text-sm font-semibold text-sky-700 dark:bg-sky-950/40 dark:text-sky-300">
              Best of 5 (first to 3 wins)
            </span>
            <div className="rounded-2xl bg-slate-50 px-3 py-4 dark:bg-slate-900">
              <div className="flex items-center justify-center gap-3">
                <div className={`min-w-20 rounded-xl px-4 py-2 text-center ${scoreToneClass(score.playerWins, score.computerWins)}`}>
                  <p className="text-xs uppercase tracking-wide">👤 {playerName || 'Player'}</p>
                  <p className="text-3xl font-bold">{score.playerWins}</p>
                </div>
                <p className="text-3xl font-extrabold text-slate-700 dark:text-slate-200">x</p>
                <div className={`min-w-20 rounded-xl px-4 py-2 text-center ${scoreToneClass(score.computerWins, score.playerWins)}`}>
                  <p className="text-xs uppercase tracking-wide">🤖 Computer</p>
                  <p className="text-3xl font-bold">{score.computerWins}</p>
                </div>
              </div>
              <div className="mt-3 text-center">
                <p className="text-sm font-semibold text-slate-600 dark:text-slate-300">Ties: {score.ties}</p>
              </div>
            </div>
          </div>
        </section>
        <div className="grid gap-6 xl:grid-cols-2">
          <section className="rounded-3xl border border-slate-200 bg-white p-6 shadow-sm dark:border-slate-800 dark:bg-slate-950">
            <p className="text-sm uppercase tracking-[0.3em] text-slate-500 dark:text-slate-400">Result</p>
            <div
              className={`relative mt-4 overflow-hidden rounded-3xl border border-slate-200 dark:border-slate-800 ${
                round ? 'bg-white dark:bg-slate-950' : 'min-h-[24rem] bg-slate-50 p-5 dark:bg-slate-900'
              }`}
            >
              <div className={isThinking ? 'blur-sm' : ''}>
                {round && choicesQuery.data ? (
                  <GameResult result={round} playerName={playerName} choices={choicesQuery.data} />
                ) : (
                  <div className="flex min-h-[20rem] items-center justify-center text-center text-slate-500 dark:text-slate-400">
                    <div>
                      <div className="text-4xl">🎮</div>
                      <p className="mt-3 text-sm">Your round result will appear here.</p>
                    </div>
                  </div>
                )}
              </div>

              {isThinking ? (
                <div className="absolute inset-0 flex items-center justify-center bg-slate-950/25 backdrop-blur-[1px]">
                  <div className="rounded-3xl border border-white/10 bg-slate-950/90 px-6 py-5 text-center text-white shadow-2xl">
                    <div className="text-4xl">🤔</div>
                    <p className="mt-2 animate-pulse text-sm font-semibold tracking-wide">Thinking...</p>
                  </div>
                </div>
              ) : null}
            </div>
          </section>

          <section
            className={`rounded-3xl border bg-white p-6 shadow-sm dark:bg-slate-950 ${
              isPlayerTurn ? 'turn-border-pulse border-amber-300 dark:border-amber-500' : 'border-slate-200 dark:border-slate-800'
            }`}
          >
            <div className="flex items-start justify-between gap-4">
              <div className="space-y-2">
                <p className="text-sm uppercase tracking-[0.3em] text-slate-500 dark:text-slate-400">Gameplay</p>
                <h2 className="text-3xl font-bold text-slate-950 dark:text-white">Game board</h2>
              </div>
            </div>

            {!playerName ? (
              <div className="mt-6 rounded-2xl border border-dashed border-slate-300 p-6 text-slate-500 dark:border-slate-700 dark:text-slate-400">
                Enter your name to unlock the board.
              </div>
            ) : choicesQuery.isLoading ? (
              <div className="mt-6 text-slate-500 dark:text-slate-400">Loading choices...</div>
            ) : choicesQuery.isError ? (
              <div className="mt-6 rounded-2xl bg-red-50 p-4 text-red-700 dark:bg-red-950/40 dark:text-red-200">
                Could not load the choices.
              </div>
            ) : (
              <div className={`mt-6 ${isGameplayLocked ? 'blur-sm' : ''}`}>
                <div className="flex justify-center">
                  <div className="flex w-full max-w-5xl flex-wrap items-center justify-center gap-3">
                    {choices.map((choice) => (
                      <ChoiceButton
                        key={choice.id}
                        choice={choice}
                        onSelect={handleSelect}
                        disabled={isGameplayLocked}
                      />
                    ))}
                  </div>
                </div>
              </div>
            )}
          </section>
        </div>

        <EndGameActions
          isGameOver={isGameOver}
          winner={winner}
          onPlayAgain={() => {
            setRound(null)
            setRounds([])
            hasRecordedSeriesWinRef.current = false
          }}
          onChangeName={openNameModal}
        />

        <section className="rounded-3xl border border-slate-200 bg-white p-6 shadow-sm dark:border-slate-800 dark:bg-slate-950">
          <h3 className="text-lg font-semibold text-slate-950 dark:text-white">Quick cheatsheet</h3>
          <div className="mt-4 flex flex-wrap items-center justify-center gap-3">
            {cheatsheet.map((rule) => (
              <div
                key={rule.choiceId}
                className="flex w-full max-w-52 items-center gap-3 rounded-2xl bg-slate-50 px-4 py-3 dark:bg-slate-900 sm:w-auto"
              >
                <div className="flex h-11 w-11 shrink-0 items-center justify-center rounded-full bg-white text-xl shadow-sm dark:bg-slate-950">
                  {iconForChoice(labelForChoice(rule.choiceId, choices))}
                </div>
                <div>
                  <p className="font-semibold capitalize text-slate-950 dark:text-white">
                    {labelForChoice(rule.choiceId, choices)}
                  </p>
                  <p className="mt-1 text-sm text-slate-600 dark:text-slate-300">
                    Beats {labelForChoice(rule.beats[0], choices)} and {labelForChoice(rule.beats[1], choices)}.
                  </p>
                </div>
              </div>
            ))}
          </div>
        </section>
      </section>
    </main>
  )
}
