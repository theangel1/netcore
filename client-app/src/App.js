import "./App.css";
import { ThemeProvider as MuiThemeProvider } from "@material-ui/core/styles";
import theme from "./theme/Theme";
import AppNavBar from "./components/navigation/AppNavBar";
import CrearCargo from "./components/cargo/NuevoCargo";
import Login from "./components/security/Login";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { Grid, Snackbar } from "@material-ui/core";
import { useStateValue } from "./context/store";
import React from "react";
import ListaCargo from "./components/cargo/ListaCargo";
import EditarCargo from "./components/cargo/EditarCargo";

function App() {
  const [{ openSnackbar }, dispatch] = useStateValue();

  return (
    <React.Fragment>
      <Snackbar
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
        open={openSnackbar ? openSnackbar.open : false}
        autoHideDuration={3000}
        ContentProps={{ "aria-describedby": "message-id" }}
        message={
          <span id="message-id">
            {openSnackbar ? openSnackbar.mensaje : ""}
          </span>
        }
        onClose={() =>
          dispatch({
            type: "OPEN_SNACKBAR",
            openMensaje: {
              open: false,
              mensaje: "",
            },
          })
        }
      ></Snackbar>
      <Router>
        <MuiThemeProvider theme={theme}>
          <AppNavBar />
          <Grid container>
            <Switch>
              <Route exact path="/auth/login" component={Login} />              
              <Route exact path="/cargo/crear" component={CrearCargo} />
              <Route exact path="/cargo/listaCargo" component={ListaCargo} />
              <Route exact path="/cargo/editar" component={EditarCargo} />
            </Switch>
          </Grid>
        </MuiThemeProvider>
      </Router>
    </React.Fragment>
  );
}

export default App;
