import React, { useEffect, useState, ChangeEvent } from 'react'
import useStyles from './styles'
import { useCurrentMove } from '../../context/MoveContext'
import axios from 'axios'
import Paper from '@material-ui/core/Paper'
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import Icon from '@material-ui/core/Icon'
import ReactMapGL, { Marker } from 'react-map-gl'
import HouseIcon from '@material-ui/icons/House'
import { PointOfInterest } from './PointOfInterest'
import { House } from './House'

export default () => {
  const Star = () => <Icon>star</Icon>
  const House = () => <HouseIcon />
  const classes = useStyles()
  const context = useCurrentMove()
  const move = context.currentMove
  const [pois, setPois] = useState<Array<PointOfInterest>>([])
  const [newPoi, setNewPoi] = useState({})

  const [houses, setHouses] = useState<Array<House>>([])
  const [newHouse, setNewHouse] = useState({})

  const [viewport, setViewport] = useState({
    width: '100vw',
    height: '50vh',
    latitude: 27.7676,
    longitude: -82.6403,
    zoom: 8,
  })
  const setNewPoiData = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target
    setNewPoi(prev => {
      return {
        ...prev,
        [name]: value,
      }
    })
  }

  const sendPoiToApi = async () => {
    const resp = await axios.post('/api/PointOfInterest', {
      ...newPoi,
      moveId: move.id,
    })
    console.log(resp.data)
    setPois(p => [...p, resp.data])
  }

  const setNewHouseData = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target
    setNewHouse(prev => {
      return {
        ...prev,
        [name]: value,
      }
    })
  }

  const sendHouseToApi = async () => {
    const resp = await axios.post('/api/House', {
      ...newHouse,
      moveId: move.id,
    })
    setHouses(p => [...p, resp.data])
  }

  useEffect(() => {
    const getData = async () => {
      const resp = await axios.get('/api/PointOfInterest')
      setPois(resp.data)
    }
    getData()
    const getHouses = async () => {
      const resp = await axios.get('/api/House')
      setHouses(resp.data)
    }
    getHouses()
  }, [])

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
        <li>- see on map</li>
      </ul>
      <Paper elevation={3} className={classes.main}>
        <h4>add a POI</h4>
        <form className={classes.form} noValidate autoComplete="off">
          <span className={classes.input}>
            <TextField
              id="outlined-basic"
              label="Name"
              variant="outlined"
              name="name"
              onChange={setNewPoiData}
            />
          </span>
          <span className={classes.input}>
            <TextField
              className={classes.input}
              id="outlined-basic"
              label="Full Address"
              variant="outlined"
              name="address"
              onChange={setNewPoiData}
            />
          </span>
          <Button
            variant="contained"
            color="primary"
            className={classes.button}
            endIcon={<Star />}
            onClick={sendPoiToApi}
          >
            Add
          </Button>
        </form>
      </Paper>
      <Paper elevation={3} className={classes.main}>
        <h4>add a House</h4>
        <form className={classes.form} noValidate autoComplete="off">
          <span className={classes.input}>
            <TextField
              id="outlined-basic"
              label="Name"
              variant="outlined"
              name="name"
              onChange={setNewHouseData}
            />
          </span>
          <span className={classes.input}>
            <TextField
              className={classes.input}
              id="outlined-basic"
              label="Full Address"
              variant="outlined"
              name="address"
              onChange={setNewHouseData}
            />
          </span>
          <span className={classes.input}>
            <TextField
              className={classes.input}
              id="outlined-basic"
              label="Url"
              variant="outlined"
              name="url"
              onChange={setNewHouseData}
            />
          </span>
          <span className={classes.input}>
            <TextField
              className={classes.input}
              id="outlined-basic"
              label="Notes"
              variant="outlined"
              name="notes"
              onChange={setNewHouseData}
            />
          </span>
          <Button
            variant="contained"
            color="primary"
            className={classes.button}
            endIcon={<House />}
            onClick={sendHouseToApi}
          >
            Add
          </Button>
        </form>
      </Paper>

      <ReactMapGL
        {...viewport}
        mapboxApiAccessToken={process.env.REACT_APP_MAPBOX_TOKEN}
        onViewportChange={(viewport: any) => setViewport({ ...viewport })}
      >
        {pois.map((place: PointOfInterest) => {
          return (
            <Marker
              key={place.id}
              latitude={place.latitude}
              longitude={place.longitude}
              offsetLeft={-20}
              offsetTop={-10}
            >
              <Star />
            </Marker>
          )
        })}
        {houses.map((place: House) => {
          return (
            <Marker
              key={place.id}
              latitude={place.latitude}
              longitude={place.longitude}
              offsetLeft={-20}
              offsetTop={-10}
            >
              <House />
            </Marker>
          )
        })}
      </ReactMapGL>
    </>
  )
}
