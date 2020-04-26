import React, { useEffect } from 'react'
import axios from 'axios'

import { useCurrentMove } from '../../context/MoveContext'
import Paper from '@material-ui/core/Paper'
import moment from 'moment'
import CircularProgress from '@material-ui/core/CircularProgress'

import useStyles from './styles'
import { Link } from 'react-router-dom'

export default () => {
  const classes = useStyles()
  const context = useCurrentMove()
  const move = context.currentMove
  useEffect(() => {
    const getData = async () => {
      const resp = await axios.get('/api/move/current')
      context.setCurrentMove(resp.data)
    }
    getData()
  }, [])

  if (!move) {
    return (
      <>
        <CircularProgress color="secondary" />
      </>
    )
  }

  return (
    <div>
      <Paper elevation={3} className={classes.main}>
        <h4>destination</h4>
        <h1>{move.destination}</h1>
        <h2>{moment(new Date(move.targetDate)).format('MMM Do YYYY')}</h2>
        <h3>{moment(new Date(move.targetDate)).fromNow()}</h3>
      </Paper>

      <Paper elevation={3} className={classes.main}>
        <h4>locations</h4>
        <Link to={`/locations/${move.id}`}>Look!</Link>
      </Paper>
    </div>
  )
}
