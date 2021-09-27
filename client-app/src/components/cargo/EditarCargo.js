import React, { useState, useEffect } from 'react';
import {Button, Container, Grid, Typography, TextField} from '@material-ui/core';
import style from '../Tool/Style';
import { actualizarCargo, obtenerCargo } from '../../actions/CargoAction';
import { useStateValue } from '../../context/store';
import { useParams } from 'react-router-dom';


const EditarCargo = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();
    const params = useParams();
    console.log('alooo',params);

    
    const [cargo, setCargo] = useState({
        id: 4,
        nombre: '',
        descripcion: ''
    });

    const ingresarValoresMemoria = e => {
        const {name, value} = e.target;
        setCargo(anterior => ({
            ...anterior, 
            [name] : value
        }));
    }

    useEffect(() => { ///esto solo se ejecuta una vez, ya que le indico q no evalue ninguna variable
        //aca necesito invocar al ACTIOn q permite obtener el cargo
        obtenerCargo().then(response => { //deberia obtener cargo por id
            console.log("esta es la data del objeto response del cargo actual:", response);
            setCargo(response.data);
        });
    }, [])

    const guardarCargo = e => { //la e contiene el objecto boton
        e.preventDefault();
        actualizarCargo(cargo).then(response => {
            if (response.status === 204) {
                dispatch({
                    type: "OPEN_SNACKBAR",
                    openMensaje: {
                        open: true,
                        mensaje: "Se guardaron exitosamente los cambios en cargo"
                    }
                })
                //window.localStorage.setItem("token_seguridad", response.data.token);
            }
            else {
                dispatch({
                    type: "OPEN_SNACKBAR",
                    openMensaje: {
                        open: true,
                        mensaje: "Errores al intentar guardar en: " + Object.keys(response.data.errors)
                    }
                })
            }                     
            
        })
    }    


    return (
        <Container component="main" maxWidth="md" justify="center">
            <div style={style.paper}>
                <Typography component="h1" variant="h5">
                    Actualizar Cargo
                </Typography>
            </div>
            <form style={style.form}>
                <Grid container spacing={2}>
                    <Grid item xs={12} md={6}>
                        <TextField name="nombre" onChange={ingresarValoresMemoria} value={cargo.nombre } variant="outlined" fullWidth label="Ingrese nombre del cargo"/>                        
                    </Grid>

                    <Grid item xs={12} md={12}>
                        <TextField name="descripcion" onChange={ingresarValoresMemoria} value={cargo.descripcion} variant="outlined" fullWidth label="Descripción"/>                        
                    </Grid> 
                   
                </Grid>
                <Grid container justifyContent="center">
                    <Grid item xs={12} md={6}>
                        <Button type="submit" 
                        onClick={guardarCargo}
                        fullWidth variant="contained" size="large" color="primary" style={style.submit}>
                            Guardar datos
                        </Button>
                    </Grid>
                </Grid>
            </form>

        </Container>
    );
};

export default EditarCargo;