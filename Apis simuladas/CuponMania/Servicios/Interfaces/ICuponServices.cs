using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.DTOs;
using DataBase.Model;

namespace Servicios.Interfaces
{
    public interface ICuponService
    {
        RespuestaPrivada<Cupon> obtenerCupon(string codigo);
        RespuestaPrivada<Cupon> PatchDesabilitar(string codigo);        

    }
}
