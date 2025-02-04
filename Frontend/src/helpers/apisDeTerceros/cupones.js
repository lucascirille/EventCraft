import axios from "axios";

// GET
export async function getDescuento(codigo) {
    try {
        const response = await axios.get(`https://localhost:7214/api/Cupon/obtenerCupon`, {
            params: { codigo }  // Pasar el código como query parameter
        });
        return response.data; // Devolver solo los datos de la respuesta
    } catch (error) {
        console.error(error.response);
        console.log("Error ===> ", error);
        throw new Error(error.response?.data?.mensaje);
    }
}

