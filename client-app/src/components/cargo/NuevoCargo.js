import React, {useState} from 'react';
import style from '../Tool/Style';
import { Button, Container, Grid, TextField, Typography } from '@material-ui/core';
import { crearCargo } from '../../actions/CargoAction';
import { useStateValue } from '../../context/store';

const NuevoCargo = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();

    const [cargo, setCargo] = useState({
        nombre: '',
        descripcion: '',        
    });

    const ingresarValoresMemoria = e => {
        const {name, value} = e.target;
        setCargo(anterior => ({
            ...anterior,
            [name] : value
        }))
    }

    const botonCrear = e => {
        e.preventDefault();
        crearCargo(cargo).then(response => {            
            if (response.status === 201) {
                dispatch({
                    type: "OPEN_SNACKBAR",
                    openMensaje: {
                        open: true,
                        mensaje: "Cargo creado correctamente"
                    }
                })                
            }
            else {
                dispatch({
                    type: "OPEN_SNACKBAR",
                    openMensaje: {
                        open: true,
                        mensaje: "Errores al intentar crear en: " + Object.keys(response.data.errors)
                    }
                })
            }          
            
        });
    }

return (
    <Container component="main" maxWidth="md" justify="center">
        <div style={style.paper}>            
            <Typography component="h1" variant="h5">
                Crear cargo
            </Typography>            
            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={12}>
                        <TextField name="nombre" value={cargo.nombre} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese nombre cargo"/>
                    </Grid>
                   
                    <Grid item xs={12} md={6}>
                        <TextField name="descripcion" value={cargo.descripcion} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese descripcion"/>
                    </Grid>
                  
                   
                </Grid>
                <Grid container justifyContent="center">
                    <Grid item xs={12} md={6}>
                        <Button type="submit" onClick={botonCrear} fullWidth variant="contained" color="primary" size="large" style={style.submit}>
                            Crear
                        </Button>
                    </Grid>
                </Grid>
            </form>                                                            
        </div>
    </Container>
);
}

export default NuevoCargo;