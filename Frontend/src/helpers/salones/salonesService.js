import axios from "axios";

// GET
export async function getSalones() {
    try {
        const response = await axios.get("https://localhost:7164/api/Salon/obtenerSalones");
        return response;
    } catch (error) {
        console.log(error.response);
        throw new Error(error.response?.data?.mensaje);
    }
}

// POST
export async function createSalon(nuevoSalon, auth) {
    try {
        console.log("estoy en crear salon", nuevoSalon);
        const response = await axios.post("https://localhost:7164/api/Salon/crearSalon", nuevoSalon, {
            headers: {
                Authorization: `Bearer ${auth}`
            }
        });
        return response;
    } catch (error) {
        throw new Error(error.response?.data?.mensaje);
    }
};

//DELETE
export async function deleteSalon(salonId, auth) {
    try {
        const response = await axios.delete(`https://localhost:7164/api/Salon/eliminarSalon?id=${salonId}`, {
            headers: {
                Authorization: `Bearer ${auth}`
            }
        });
        return response;
    } catch (error) {
        throw new Error(error.response?.data?.mensaje);
    }
}

//EDIT
export async function updateSalon(salonId, salonData, auth) {
    try {
        const response = await axios.put(`https://localhost:7164/api/Salon/modificarSalon?id=${salonId}`, salonData, {
            headers: {
                Authorization: `Bearer ${auth}`
            }
        });
        return response;
    } catch (error) {
        throw new Error(error.response?.data?.mensaje);
    } 
};


// GET 
export async function getCaracteristicasBySalonId(salonId) {
    try {
        const response = await axios.get(`https://localhost:7164/api/SalonCaracteristica/obtenerCaracteristicaPorSalonId?id=${salonId}`);
        return response;
    } catch (error) {
        throw new Error(error.response?.data?.mensaje);
    } 
}


// GET
export async function getServiciosBySalonId(salonId) {
    try {
        const response = await axios.get(`https://localhost:7164/api/SalonServicio/obtenerServicioPorSalonId?id=${salonId}`);
        return response;
    } catch (error) {
        throw new Error(error.response?.data?.mensaje);

    }  
}

