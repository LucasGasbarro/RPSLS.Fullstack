export type Choice = {
  id: number
  name: string
}

export type PlayRequest = {
  player: number
}

export type PlayResponse = {
  results: 'win' | 'lose' | 'tie'
  player: number
  computer: number
}

export type ScoreEntry = {
  playerName: string
  points: number
}
