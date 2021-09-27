import {
  Paper,
  TableBody,
  TableContainer,
  TableHead,
  TableRow,
  TableCell,
  Table,
  TablePagination,
  Hidden,
  Grid,
  TextField,
  Button,
  ButtonGroup
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { paginacionCargo } from "../../actions/CargoAction";
import ControlTyping from "../Tool/ControlTyping";
import style from '../Tool/Style';
import { Link } from "react-router-dom";

const ListaCargo = () => {
  const [textoBusquedaCargo, setTextoBusquedaCargo] = useState("");
  const typingBuscadorTexto = ControlTyping(textoBusquedaCargo, 900);

  const [paginadorRequest, setPaginadorRequest] = useState({
    titulo: "",
    numeroPagina: 0,
    cantidadElementos: 5,
  });

  const [paginadorResponse, setPaginadorResponse] = useState({
    listaRecords: [],
    totalRecords: 0,
    numeroPaginas: 0,
  });

  useEffect(() => {
    const obtenerListaCargo = async () => {
      let tituloVariant = "";
      let paginaVariant = paginadorRequest.numeroPagina + 1;

      if (typingBuscadorTexto) {
        tituloVariant = typingBuscadorTexto;
        paginaVariant = 1;
      }

      const objetoPaginadorRequest = {
        titulo: tituloVariant,
        numeroPagina: paginaVariant,
        cantidadElementos: paginadorRequest.cantidadElementos,
      };
      const response = await paginacionCargo(objetoPaginadorRequest);
      setPaginadorResponse(response.data);
    };

    obtenerListaCargo();
  }, [paginadorRequest, typingBuscadorTexto]);

  const cambiarPagina = (event, nuevaPagina) => {
    setPaginadorRequest((anterior) => ({
      ...anterior,
      numeroPagina: parseInt(nuevaPagina),
    }));
  };

  const cambiarCantidadRecords = (event) => {
    setPaginadorRequest((anterior) => ({
      ...anterior,
      cantidadElementos: parseInt(event.target.value),
      numeroPagina: 0,
    }));
  };

  return (
    <div style={{ padding: "10px", width: "100%" }}>
      <Grid container style={{ paddingTop: "10px", paddingBottom: "10px" }}>
        <Grid item xs={12} sm={4} md={6}>
          <TextField
            fullWidth
            name="textoBusquedaCargo"
            variant="outlined"
            label="Busca tu cargo"
            onChange={(e) => setTextoBusquedaCargo(e.target.value)}
          />
        </Grid>
        <Grid item xs={12} sm={4} md={6}>
          <Button  variant="outlined" color="tercero" style={style.submit} component={Link} to="/cargo/crear">
             Nuevo Cargo
          </Button>
        </Grid>
      </Grid>     
      
      
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="left">Cargos</TableCell>
              <Hidden mdDown>
                <TableCell align="left">Descripcion</TableCell>
                <TableCell align="left">Fecha Creación</TableCell>
                <TableCell align="left">Fecha Modificación</TableCell>
              </Hidden>
              <TableCell align="left">Acción</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {paginadorResponse.listaRecords.map((cargo) => (
              <TableRow key={cargo.Id}>
                <TableCell align="left">{cargo.Nombre}</TableCell>
                <Hidden mdDown>
                  <TableCell align="left">{cargo.Descripcion}</TableCell>
                  <TableCell align="left">
                    {new Date(cargo.FechaCreacion).toLocaleString()}
                  </TableCell>
                  <TableCell align="left">
                    {new Date(cargo.FechaModificacion).toLocaleString()}
                  </TableCell>
                </Hidden>
                <TableCell align="left">
                  <ButtonGroup>                   
                    <Button  component={Link} to="/cargo/editar/4" fullWidth variant="outlined" color="primary" size="small" style={style.submit}>
                          Editar
                    </Button>
                    <Button  fullWidth variant="outlined" size="small" style={style.submit}>
                          Eliminar
                    </Button>
                  </ButtonGroup>
                     
                  </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[5, 10, 25]}
        count={paginadorResponse.totalRecords}
        rowsPerPage={paginadorRequest.cantidadElementos}
        page={paginadorRequest.numeroPagina}
        onPageChange={cambiarPagina}
        onRowsPerPageChange={cambiarCantidadRecords}
        labelRowsPerPage="Cargos por página"
      />
    </div>
  );
};

export default ListaCargo;
