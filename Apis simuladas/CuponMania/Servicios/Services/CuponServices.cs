using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.DTOs;
using DataBase.Model;
using Servicios.Interfaces;


namespace Servicios.Services
{
    public class CuponServices : ICuponService

    {
        private static readonly List<Cupon> _cuponesBD= new List<Cupon>
        {
            new Cupon { Codigo = "DESC10", Descuento = 10, Vencimiento = new DateTime(2024, 12, 31), Utilizado = false },
            new Cupon { Codigo = "DESC20", Descuento = 20, Vencimiento = new DateTime(2024, 11, 30), Utilizado = false },
            new Cupon { Codigo = "DESC15", Descuento = 15, Vencimiento = new DateTime(2024, 10, 15), Utilizado = false },
            new Cupon { Codigo = "DESC05", Descuento = 5, Vencimiento = new DateTime(2023, 11, 30), Utilizado = false },
            new Cupon { Codigo = "DESC50", Descuento = 50, Vencimiento = new DateTime(2024, 12, 31), Utilizado = true },
            new Cupon { Codigo = "DESC30", Descuento = 30, Vencimiento = new DateTime(2024, 12, 07), Utilizado = false },
            new Cupon { Codigo = "BLACKFRIDAY", Descuento = 40, Vencimiento = new DateTime(2024, 11, 28), Utilizado = true },
            new Cupon { Codigo = "CYBERMONDAY", Descuento = 25, Vencimiento = new DateTime(2024, 12, 03), Utilizado = false },
            new Cupon { Codigo = "EXPIRED", Descuento = 10, Vencimiento = new DateTime(2023, 09, 30), Utilizado = false }
        };

        public RespuestaPrivada<Cupon> obtenerCupon(string codigo)
        {
            var respuestaPrivada = new RespuestaPrivada<Cupon>();
            respuestaPrivada.Datos = null;
            // no hay conexion a la base de datos => try catch
            respuestaPrivada.Exito = true;
            //en cuponesBD se deberia llamar a la base de datos con await

            var cuponesBD = _cuponesBD;
            var cupon = cuponesBD.FirstOrDefault(c => c.Codigo == codigo);
            if (cupon != null) {
              
                respuestaPrivada.Datos = cupon;
                respuestaPrivada.Mensaje = "Cupon encontrado";
                return respuestaPrivada;
            }
            else
            {
                respuestaPrivada.Mensaje = "Cupon no encontrado";
                return respuestaPrivada;

            }

        }

        public RespuestaPrivada<Cupon> PatchDesabilitar(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
