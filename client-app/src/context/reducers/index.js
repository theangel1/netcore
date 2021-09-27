import openSnackbarReducer from "./openSnackbarReducer";

export const mainReducer = ({ openSnackbar }, action) => {
    return {
        openSnackbar : openSnackbarReducer(openSnackbar, action)
    }
}