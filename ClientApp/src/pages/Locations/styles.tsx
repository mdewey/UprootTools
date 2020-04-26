import makeStyles from '@material-ui/core/styles/makeStyles'

export default makeStyles(theme => ({
  main: {
    display: 'flex',
    justifyContent: 'spa',
    alignItems: 'center',
    flexDirection: 'column',
    margin: theme.spacing(3),
    padding: theme.spacing(2),
  },
  form: {
    display: 'flex',
    flexDirection: 'column',
    width: '25vw',
    justifyContent: 'space-between',
  },
  input: {
    margin: theme.spacing(1),
  },
  button: {
    margin: theme.spacing(1),
  },
}))
