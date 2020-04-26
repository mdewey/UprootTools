import React from 'react'
import { Link } from 'react-router-dom'
import './style.scss'

export default function Home() {
  return (
    <main className="hero">
      <h1>Organize your Journey</h1>
      <Link to="/dashboard">Get started</Link>
    </main>
  )
}
