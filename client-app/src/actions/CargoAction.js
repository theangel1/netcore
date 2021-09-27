import HttpClient from "../services/HttpClient";

export const crearCargo = (cargo) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("cargo", cargo)
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        console.log("error: " + error.response);
        resolve(error.response);
      });
  });
};

export const actualizarCargo = (cargo) => {
  return new Promise((resolve, eject) => {
    HttpClient.put("/cargo", cargo)
      .then((response) => {
        resolve(response);
      })
      .catch((error) => {
        resolve(error.response);
      });
  });
};

export const obtenerCargo = (dispatch) => {
  return new Promise((resolve, eject) => {    
    HttpClient.get("/cargo").then((response) => {
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

//debo obtener cargo por id
export const eliminarCargo = (dispatch) => {
 
};


export const paginacionCargo = (paginador) => {
  return new Promise((resolve, eject) => {
    HttpClient.post("cargo/report", paginador).then((response) => {
      resolve(response);
    });
  });
};
