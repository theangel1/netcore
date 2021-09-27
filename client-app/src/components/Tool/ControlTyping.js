import React, { useState, useEffect } from 'react';

//componente me permite controlar el tiempo en el que usuario dejó de escribir
export default function ControlTyping(texto, delay) {
    const [textoValor, setTextoValor] = useState();
    useEffect(() => {
        
        const handler = setTimeout(() => {
            setTextoValor(texto);
        }, delay);
        
        return () => {
            clearTimeout();
        }
    }, [texto]);

    return textoValor;
}