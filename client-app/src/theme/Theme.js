import { createTheme } from "@material-ui/core/styles";

const theme = createTheme({
  palette: {
    primary: {
      light: "#63a4fff",
      main: "#1976d2",
      dark: "#004ba0",
      contrastText: "#ecfad8",
    },
    tercero: {
        light: '#fc03db',
        main: '#fc03db',
        dark: '#fc03db',
        contrastText: '#fc03db',
      },
  },
});

export default theme;
