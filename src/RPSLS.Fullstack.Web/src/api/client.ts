import type { Choice, PlayResponse, ScoreEntry } from '../types'

const baseUrl = import.meta.env.VITE_API_BASE_URL ?? ''

export class ApiError extends Error {
  public readonly status: number

  constructor(message: string, status: number) {
    super(message)
    this.name = 'ApiError'
    this.status = status
  }
}

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${baseUrl}${path}`, {
    headers: {
      'Content-Type': 'application/json',
      ...(init?.headers ?? {}),
    },
    ...init,
  })

  if (!response.ok) {
    const text = await response.text()
    throw new ApiError(text || response.statusText, response.status)
  }

  if (response.status === 204) {
    return undefined as T
  }

  return (await response.json()) as T
}

export const getChoices = () => request<Choice[]>('/choices')

export const getRandomChoice = () => request<Choice>('/choice')

export const playRound = (player: number) =>
  request<PlayResponse>('/play', {
    method: 'POST',
    body: JSON.stringify({ player }),
  })

export const getScoreboard = () => request<ScoreEntry[]>('/scoreboard')

export const recordSeriesWin = (playerName: string) =>
  request<void>('/scoreboard/win', {
    method: 'POST',
    body: JSON.stringify({ playerName }),
  })

export const resetScoreboard = () =>
  request<void>('/scoreboard', {
    method: 'DELETE',
  })
