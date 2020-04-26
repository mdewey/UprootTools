import React, { useEffect } from 'react'
import useStyles from './styles'
import { useCurrentMove } from '../../context/MoveContext'
import axios from 'axios'

export default () => {
  const classes = useStyles()
  const context = useCurrentMove()
  const move = context.currentMove

  useEffect(() => {
    if (!move) {
      const getData = async () => {
        const resp = await axios.get('/api/move/current')
        context.setCurrentMove(resp.data)
      }
      getData()
    }
  }, [move])

  return (
    <>
      <ul>
        <li> - add house</li>
        <li> - add POI </li>
        <li>- see on map</li>
      </ul>
      <main></main>
    </>
  )
}
