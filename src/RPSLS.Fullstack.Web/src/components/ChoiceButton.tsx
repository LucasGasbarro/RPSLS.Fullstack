import { memo } from 'react'
import type { Choice } from '../types'

type Props = {
  choice: Choice
  onSelect: (choiceId: number) => void
  disabled?: boolean
}

const iconByChoice: Record<string, string> = {
  rock: '🪨',
  paper: '📄',
  scissors: '✂️',
  lizard: '🦎',
  spock: '🖖',
}

export const ChoiceButton = memo(function ChoiceButton({ choice, onSelect, disabled }: Props) {
  return (
    <button
      type="button"
      disabled={disabled}
      onClick={() => onSelect(choice.id)}
      className="mx-auto flex min-h-24 w-32 flex-col items-center justify-center rounded-2xl border border-slate-200 bg-white/90 px-3 py-3 text-center text-slate-900 shadow-sm transition hover:-translate-y-0.5 hover:shadow-lg disabled:cursor-not-allowed disabled:opacity-50 dark:border-slate-800 dark:bg-slate-900 dark:text-slate-100"
    >
      <span className="text-2xl">{iconByChoice[choice.name] ?? '🎮'}</span>
      <span className="mt-2 text-xs font-semibold capitalize">{choice.name}</span>
    </button>
  )
})
