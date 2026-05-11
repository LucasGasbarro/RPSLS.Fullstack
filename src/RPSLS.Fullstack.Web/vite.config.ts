import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import tailwindcss from '@tailwindcss/vite'

export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: {
    proxy: {
      '/choice': 'http://localhost:5000',
      '/choices': 'http://localhost:5000',
      '/play': 'http://localhost:5000',
      '/scoreboard': 'http://localhost:5000',
    },
  },
})
