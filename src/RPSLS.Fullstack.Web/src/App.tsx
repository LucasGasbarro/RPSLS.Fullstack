import { Suspense, lazy, useEffect, useState } from 'react'
import { Link, Navigate, Route, Routes } from 'react-router-dom'
import { ThemeToggle } from './components/ThemeToggle'

const HomePage = lazy(() => import('./pages/HomePage').then((module) => ({ default: module.HomePage })))
const GamePage = lazy(() => import('./pages/GamePage').then((module) => ({ default: module.GamePage })))
const LeaderboardPage = lazy(() => import('./pages/LeaderboardPage').then((module) => ({ default: module.LeaderboardPage })))

export function App() {
  const [theme, setTheme] = useState<'light' | 'dark'>(() => {
    const savedTheme = localStorage.getItem('theme')
    if (savedTheme === 'light' || savedTheme === 'dark') {
      return savedTheme
    }

    return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
  })

  useEffect(() => {
    document.documentElement.classList.toggle('dark', theme === 'dark')
    localStorage.setItem('theme', theme)
  }, [theme])

  return (
    <div className="min-h-screen bg-slate-100 text-slate-950 transition-colors dark:bg-slate-950 dark:text-slate-100">
      <header className="sticky top-0 z-20 border-b border-slate-200/80 bg-white/80 backdrop-blur dark:border-slate-800 dark:bg-slate-950/80">
        <div className="mx-auto flex max-w-6xl items-center justify-between gap-4 px-4 py-4 sm:px-6 lg:px-8">
          <Link to="/" className="text-lg font-black tracking-tight">
            RPSLS-Fullstack
          </Link>
          <nav className="flex items-center gap-3 text-sm font-medium">
            <Link to="/" className="hover:text-sky-600 dark:hover:text-sky-400">
              Home
            </Link>
            <Link to="/game" className="hover:text-sky-600 dark:hover:text-sky-400">
              Game
            </Link>
            <Link to="/leaderboard" className="hover:text-sky-600 dark:hover:text-sky-400">
              Leaderboard
            </Link>
            <ThemeToggle theme={theme} onToggle={() => setTheme(theme === 'dark' ? 'light' : 'dark')} />
          </nav>
        </div>
      </header>

      <Suspense
        fallback={
          <div className="mx-auto flex min-h-[calc(100vh-5rem)] max-w-6xl items-center justify-center px-4 py-10 text-slate-500 dark:text-slate-400">
            Loading...
          </div>
        }
      >
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/game" element={<GamePage />} />
          <Route path="/leaderboard" element={<LeaderboardPage />} />
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </Suspense>
    </div>
  )
}
