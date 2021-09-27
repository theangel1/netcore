import {
  Avatar,
  Button,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemText,
  makeStyles,
  Toolbar,
  Typography,
} from "@material-ui/core";
import React, { useState } from "react";
import FotoUsuarioTemp from "../../../logo.svg";
//import { useStateValue } from "../../../contexto/store";
import { MenuIzquierda } from "./menuIzquierda";
import { MenuDerecho } from "./menuDerecho";
import { withRouter } from "react-router";

const useStyles = makeStyles((theme) => ({
  seccionDesktop: {
    display: "none",
    [theme.breakpoints.up("md")]: {
      display: "flex",
    },
  },
  seccionMobile: {
    display: "flex",
    [theme.breakpoints.up("md")]: {
      display: "none",
    },
  },
  grow: {
    flexGrow: 1,
  },
  avatarSize: {
    width: 40,
    height: 40,
  },
  list: {
    width: 250,
  },
  listItemText: {
    fontSize: "14px",
    fontWeight: 600,
    paddingLeft: "15px",
    color: "#212121",
  },
}));

const BarSesion = (props) => {
    const classes = useStyles();
    //const [{sesionUsuario }, dispatch] = useStateValue();
    const [abrirMenuIzquierdo, setAbrirMenuIzquierdo] = useState(false);
    const [abrirMenuDerecho, setAbrirMenuDerecho] = useState(false);

  const cerrarMenuIzquierdo = () => {
    setAbrirMenuIzquierdo(false);
  };

  const abrirMenuIzquierdoAction = () => {
    setAbrirMenuIzquierdo(true);
    };
    
    const cerrarMenuDerecho = () => {
        setAbrirMenuDerecho(false);
    }

    const salirSesionApp = () => {
        localStorage.removeItem('token_seguridad');
        props.history.push('/auth/login');
    }

    const abrirMenuDerechoAction = () => {
        setAbrirMenuDerecho(true);
    }

  return (
    <React.Fragment>
      <Drawer
        open={abrirMenuIzquierdo}
        onClose={cerrarMenuIzquierdo}
        anchor="left"
      >
        <div
          className={classes.list}
          onKeyDown={cerrarMenuIzquierdo}
          onClick={cerrarMenuIzquierdo}
        >
          <MenuIzquierda classes={classes} />
        </div>
      </Drawer>

      <Drawer
        open={abrirMenuDerecho}
        onClose={cerrarMenuDerecho}
        anchor="right"
      >
              <div role="button" onClick={cerrarMenuDerecho} onKeyDown={cerrarMenuDerecho}>
                  <MenuDerecho classes={classes} salirSesion={salirSesionApp}
                      //usuario={sesionUsuario ? sesionUsuario.usuario : null}
                  />
              </div>
      </Drawer>

      <Toolbar>
        <IconButton color="inherit" onClick={abrirMenuIzquierdoAction}>
          <i className="material-icons">menu</i>
        </IconButton>
        <Typography variant="h6">Maqueta</Typography>
        <div className={classes.grow}></div>

        <div className={classes.seccionDesktop}>
          <Button color="inherit">Salir</Button>
          <Button color="inherit">
            
          </Button>
          <Avatar ></Avatar>
        </div>
        <div className={classes.seccionMobile}>
                  <IconButton color="inherit" onClick={ abrirMenuDerechoAction}>
            <i className="material-icons">more_vert</i>
          </IconButton>
        </div>
      </Toolbar>
    </React.Fragment>
  );
};

export default withRouter(BarSesion);
