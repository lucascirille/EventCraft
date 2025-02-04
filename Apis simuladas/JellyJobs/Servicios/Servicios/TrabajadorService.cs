using CORE.DTOs;
using DataBase.Model;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Servicios
{

    public class TrabajadorService : ITrabajadorService
    {

        private static readonly List<Trabajador> _trabajadoresBaseDeDatos = new List<Trabajador>
    {
        // Maquillaje
        new Trabajador { Nombre = "Juan", Apellido = "Pérez", Email = "juan.perez@mail.com", Estrellas = 4.5, Profesion = "Maquillaje" },
        new Trabajador { Nombre = "Leo", Apellido = "Nuñez", Email = "leo.nuñez@mail.com", Estrellas = 4.8, Profesion = "Maquillaje" },
        new Trabajador { Nombre = "María", Apellido = "Fernández", Email = "maria.fernandez@mail.com", Estrellas = 4.7, Profesion = "Maquillaje" },
        new Trabajador { Nombre = "Laura", Apellido = "Martínez", Email = "laura.martinez@mail.com", Estrellas = 5.0, Profesion = "Maquillaje" },

        // Payaso
        new Trabajador { Nombre = "Ana", Apellido = "García", Email = "ana.garcia@mail.com", Estrellas = 5.0, Profesion = "Payaso" },
        new Trabajador { Nombre = "Roberto", Apellido = "Hernández", Email = "roberto.hernandez@mail.com", Estrellas = 4.3, Profesion = "Payaso" },
        new Trabajador { Nombre = "Carmen", Apellido = "López", Email = "carmen.lopez@mail.com", Estrellas = 4.9, Profesion = "Payaso" },
        new Trabajador { Nombre = "Jorge", Apellido = "Ramírez", Email = "jorge.ramirez@mail.com", Estrellas = 4.6, Profesion = "Payaso" },

        // Mago
        new Trabajador { Nombre = "Carlos", Apellido = "López", Email = "carlos.lopez@mail.com", Estrellas = 4.2, Profesion = "Mago" },
        new Trabajador { Nombre = "Luis", Apellido = "Pineda", Email = "luis.pineda@mail.com", Estrellas = 4.8, Profesion = "Mago" },
        new Trabajador { Nombre = "Valeria", Apellido = "Sánchez", Email = "valeria.sanchez@mail.com", Estrellas = 4.7, Profesion = "Mago" },
        new Trabajador { Nombre = "Esteban", Apellido = "Castro", Email = "esteban.castro@mail.com", Estrellas = 5.0, Profesion = "Mago" },

        // Barman
        new Trabajador { Nombre = "David", Apellido = "Martínez", Email = "david.martinez@mail.com", Estrellas = 4.6, Profesion = "Barman" },
        new Trabajador { Nombre = "Jessica", Apellido = "Gómez", Email = "jessica.gomez@mail.com", Estrellas = 4.4, Profesion = "Barman" },
        new Trabajador { Nombre = "Pedro", Apellido = "Vázquez", Email = "pedro.vazquez@mail.com", Estrellas = 4.8, Profesion = "Barman" },
        new Trabajador { Nombre = "Sofía", Apellido = "Reyes", Email = "sofia.reyes@mail.com", Estrellas = 4.7, Profesion = "Barman" },

        // Fotógrafo
        new Trabajador { Nombre = "Alfredo", Apellido = "Serrano", Email = "alfredo.serrano@mail.com", Estrellas = 5.0, Profesion = "Fotógrafo" },
        new Trabajador { Nombre = "Gabriela", Apellido = "Pérez", Email = "gabriela.perez@mail.com", Estrellas = 4.9, Profesion = "Fotógrafo" },
        new Trabajador { Nombre = "Andrés", Apellido = "Ruiz", Email = "andres.ruiz@mail.com", Estrellas = 4.6, Profesion = "Fotógrafo" },
        new Trabajador { Nombre = "Clara", Apellido = "López", Email = "clara.lopez@mail.com", Estrellas = 4.8, Profesion = "Fotógrafo" },

        // Animador
        new Trabajador { Nombre = "Ricardo", Apellido = "González", Email = "ricardo.gonzalez@mail.com", Estrellas = 4.7, Profesion = "Animador" },
        new Trabajador { Nombre = "Marta", Apellido = "Jiménez", Email = "marta.jimenez@mail.com", Estrellas = 5.0, Profesion = "Animador" },
        new Trabajador { Nombre = "Luis", Apellido = "Rodríguez", Email = "luis.rodriguez@mail.com", Estrellas = 4.5, Profesion = "Animador" },
        new Trabajador { Nombre = "Silvia", Apellido = "Morales", Email = "silvia.morales@mail.com", Estrellas = 4.6, Profesion = "Animador" },

        // Limpieza
        new Trabajador { Nombre = "Carlos", Apellido = "Vega", Email = "carlos.vega@mail.com", Estrellas = 4.3, Profesion = "Limpieza" },
        new Trabajador { Nombre = "Raquel", Apellido = "Sánchez", Email = "raquel.sanchez@mail.com", Estrellas = 4.8, Profesion = "Limpieza" },
        new Trabajador { Nombre = "Fernando", Apellido = "Hernández", Email = "fernando.hernandez@mail.com", Estrellas = 4.4, Profesion = "Limpieza" },
        new Trabajador { Nombre = "Verónica", Apellido = "Torres", Email = "veronica.torres@mail.com", Estrellas = 4.6, Profesion = "Limpieza" },

        // Seguridad
        new Trabajador { Nombre = "José", Apellido = "Mendoza", Email = "jose.mendoza@mail.com", Estrellas = 4.5, Profesion = "Seguridad" },
        new Trabajador { Nombre = "Antonio", Apellido = "Gómez", Email = "antonio.gomez@mail.com", Estrellas = 4.7, Profesion = "Seguridad" },
        new Trabajador { Nombre = "Beatriz", Apellido = "López", Email = "beatriz.lopez@mail.com", Estrellas = 4.8, Profesion = "Seguridad" },
        new Trabajador { Nombre = "Luis", Apellido = "Jiménez", Email = "luis.jimenez@mail.com", Estrellas = 4.6, Profesion = "Seguridad" }
    };

        public RespuestaPrivada<ICollection<Trabajador>> GetTrabajadores()
        {

            var respuesta = new RespuestaPrivada<ICollection<Trabajador>>();
            respuesta.Datos = null;
            // no hay conexion a la base de datos => try catch
            respuesta.Exito = true;
            //en trabajdoresBD se deberia llamar a la base de datos con await
            var TrabajadoresBD = _trabajadoresBaseDeDatos;

            if (TrabajadoresBD.Count() != 0)
            {
                respuesta.Datos = TrabajadoresBD;
                respuesta.Mensaje = "Trabajadores obtenidos correctamente";
                return respuesta;
            }
            else {
                respuesta.Mensaje = "No se encontraron trabajadores";
                return respuesta;
            }
        }

        public RespuestaPrivada<ICollection<Trabajador>> GetTrabajadoresPorProfesion(string profecion)
        {
            var respuesta = new RespuestaPrivada<ICollection<Trabajador>>();
            respuesta.Datos = null;
            respuesta.Exito = true;
            var TrabajadoresBD = _trabajadoresBaseDeDatos.Where(x => x.Profesion == profecion).ToList();
            if (TrabajadoresBD.Count() != 0)
            {
                respuesta.Datos = TrabajadoresBD;
                respuesta.Mensaje = "Trabajadores obtenidos correctamente";
                return respuesta;
            }
            else
            {
                respuesta.Mensaje = "No se encontraron trabajadores";
                return respuesta;
            }

        }
    }
}
