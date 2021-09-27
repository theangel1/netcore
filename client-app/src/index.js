import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { initialState } from './context/initialState';
import { StateProvider } from './context/store';
import { mainReducer } from './context/reducers';

ReactDOM.render(
  <React.StrictMode>
    <StateProvider initialState={initialState} reducer={ mainReducer}>
      <App />
    </StateProvider>
    
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
