import makeStyles from '@material-ui/core/styles/makeStyles'

export default makeStyles(theme => ({
  main: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    flexDirection: 'column',
    margin: theme.spacing(3),
    padding: theme.spacing(2),
    '& h4': {
      color: theme.palette.secondary.main,
      fontSize: '0.9rem',
      textTransform: 'uppercase',
    },
  },
}))
