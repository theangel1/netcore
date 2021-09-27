import { Divider, List, ListItem, ListItemText } from "@material-ui/core";
import React from "react";
import { Link } from "react-router-dom";

export const MenuIzquierda = ({ classes }) => (
    <div className={classes.list}>
        <List>
            <ListItem component={Link} button to="/auth/perfil">
                <i className="material-icons">account_box</i>
                <ListItemText classes={{primary: classes.listItemText}} primary="Perfil"/>
            </ListItem>
        </List>
        <Divider />
        <List>           
            <ListItem component={Link} button to="/cargo/ListaCargo">
                <i className="material-icons">menu_book</i>
                <ListItemText classes={{primary: classes.listItemText}} primary="Lista Cargos"/>
            </ListItem>
        </List>
        <Divider />
        <List>          
            <ListItem component={Link} button to="/instructor/lista">
                <i className="material-icons">people</i>
                <ListItemText classes={{primary: classes.listItemText}} primary="Lista colaboradores"/>
            </ListItem>
        </List>           
        
    </div>
);