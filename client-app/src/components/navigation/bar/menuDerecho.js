import { Avatar, List, ListItem, ListItemText } from "@material-ui/core";
import React from "react";
import { Link } from "react-router-dom";
import FotoUsuarioTemp from "../../../logo.svg";

export const MenuDerecho = ({
    classes,
    usuario,
    salirSesion
}) => (
    <div className={ classes.list}>
        <List>
            <ListItem button component={Link} >
                <Avatar src={FotoUsuarioTemp} />
                <ListItemText classes={{ primary: classes.listItemText }} primary="usuario"/>
            </ListItem>
            <ListItem button onClick={salirSesion}>
                <ListItemText classes={{primary: classes.listItemText}} primary= "Salir"/>
            </ListItem>
        </List>
    </div>
);