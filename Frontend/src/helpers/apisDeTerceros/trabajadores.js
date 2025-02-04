import axios from "axios";

// GET
export async function getTrabajadoresByProfesion(profesion) {
    try {
        const response = await axios.get("http://localhost:5055/api/Trabajador/obtenerTrabajadoresPorProfesion", {
            params: {
                profesion: profesion
            }
        });
        
        
        return response;
    } catch (error) {
        console.log(error.response);
        throw new Error(error.response?.data?.mensaje);
    }
}
