import React, { Component, useState } from 'react'
import { Route, Switch } from 'react-router'
import { Layout } from './components/Layout'
import Home from './pages/Home'
import NotFound from './pages/NotFound'
import Dashboard from './pages/Dashboard'
import Locations from './pages/Locations'

import './custom.scss'
import { CurrentMoveContext } from './context/MoveContext'

const App = () => {
  const [currentMove, setCurrentMove] = useState({})

  return (
    <CurrentMoveContext.Provider value={{ currentMove, setCurrentMove }}>
      <Layout>
        <Switch>
          <Route exact path="/" component={Home} />
          <Route exact path="/dashboard" component={Dashboard} />
          <Route exact path="/locations/:id" component={Locations} />
          <Route exact path="*" component={NotFound} />
        </Switch>
      </Layout>
    </CurrentMoveContext.Provider>
  )
}

export default App
