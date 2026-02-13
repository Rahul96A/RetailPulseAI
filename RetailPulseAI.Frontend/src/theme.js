import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    mode: 'light',
    primary: {
      main: '#1565c0'
    },
    secondary: {
      main: '#00838f'
    },
    background: {
      default: '#f4f7fb',
      paper: '#ffffff'
    }
  },
  shape: {
    borderRadius: 12
  },
  typography: {
    h5: {
      fontWeight: 700
    },
    button: {
      textTransform: 'none',
      fontWeight: 600
    }
  },
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          transition: 'transform 0.2s ease, box-shadow 0.2s ease'
        }
      }
    },
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 10
        }
      }
    }
  }
});

export default theme;
