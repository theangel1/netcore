import HttpClient from "../services/HttpClient";
import axios from "axios";
const instancia = axios.create();
instancia.CancelToken = axios.CancelToken;
instancia.isCancel = axios.isCancel;

//estos son metodos o funciones!!!
export const registrarUsuario = (usuario) => {
  return new Promise((resolve, eject) => {
    instancia.post("/usuario/registrar", usuario).then((response) => {
      resolve(response);
    });
  });
};

export const obtenerUsuarioActual = (dispatch) => {
  return new Promise((resolve, eject) => {    
    HttpClient.get("/usuario").then((response) => {
      if (typeof dispatch == "function") {
        dispatch({
          type: "INICIAR_SESION",
          sesion: response.data,
          autenticado: true,
        });
      }
      resolve(response);
    });
  });
};

export const actualizarUsuario = (usuario) => {
  return new Promise((resolve, eject) => {
    HttpClient.put("/usuario", usuario).then((response) => {
      resolve(response);
    })
        .catch(error => {
            resolve(error.response);
    })
  });
};

export const loginUsuario = (usuario) => {
  return new Promise((resolve, eject) => {
    instancia.post("/usuario/login", usuario).then((response) => {
      resolve(response);
    });
  });
};
