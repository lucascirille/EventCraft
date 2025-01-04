using DataBase.Data;
using CORE.DTOs;
using Servicios.Interfaces;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Mapster;
using BCrypt.Net;


namespace Servicios.Servicios 
{
    public class UsuarioService : IUsuarioService
    {
        private readonly EventCraftContext _context;
        private readonly IJWT _jwt; 
        public UsuarioService(EventCraftContext context, IJWT jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        public async Task<RespuestaPrivada<ICollection<UsuarioDTOConId>>> GetUsuarios()
        {
            var respuesta = new RespuestaPrivada<ICollection<UsuarioDTOConId>>();
            respuesta.Datos = null;

            try
            {
                var usuariosBD = await _context.Usuarios.ToListAsync();
                if (usuariosBD.Count() != 0)
                {
                    var usuariosDTO = new List<UsuarioDTOConId>();
                    foreach (var usuario in usuariosBD)
                    {
                        var usuarioDTO = usuario.Adapt<UsuarioDTOConId>();
                        //respuesta.Datos.Add(new UsuarioDTOConId()
                        //{
                        //    Id = usuario.Id,
                        //    NombreUsuario = usuario.NombreUsuario,
                        //    Nombre = usuario.Nombre,
                        //    Apellido = usuario.Apellido,
                        //    Correo = usuario.Correo,
                        //    Rol = usuario.Rol,
                        //    ClaveHasheada = usuario.ClaveHasheada
                        //});
                        usuariosDTO.Add(usuarioDTO);
                    }
                    respuesta.Datos = usuariosDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los usuarios";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron usuarios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<ICollection<UsuarioSinPassDTO>>> GetUsuariosSinPass()
        {
            var respuesta = new RespuestaPrivada<ICollection<UsuarioSinPassDTO>>();
            respuesta.Datos = null;

            try
            {
                var usuariosBD = await _context.Usuarios.ToListAsync();
                if (usuariosBD.Count() != 0)
                {
                    var usuariosDTO = new List<UsuarioSinPassDTO>();
                    foreach (var usuario in usuariosBD)
                    {
                        var usuarioDTO = usuario.Adapt<UsuarioSinPassDTO>();
                        //respuesta.Datos.Add(new UsuarioSinPassDTO()
                        //{
                        //    Id = usuario.Id,
                        //    NombreUsuario = usuario.NombreUsuario,
                        //    Nombre = usuario.Nombre,
                        //    Apellido = usuario.Apellido,
                        //    Correo = usuario.Correo,
                        //    Rol = usuario.Rol
                        //});
                        usuariosDTO.Add(usuarioDTO);
                    }
                    respuesta.Datos = usuariosDTO;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "Se recuperaron correctamente todos los usuarios";
                    return respuesta;
                }

                respuesta.Mensaje = "No se hallaron usuarios";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDTO)
        {
            var respuesta = new RespuestaPrivada<UsuarioDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == usuarioDTO.NombreUsuario);
                if (usuarioBD == null)
                {
                    var usuarioNuevo = usuarioDTO.Adapt<Usuario>();
                    /*var usuarioNuevo = new Usuario();
                    usuarioNuevo.NombreUsuario = usuarioDTO.NombreUsuario;
                    usuarioNuevo.Nombre = usuarioDTO.Nombre;
                    usuarioNuevo.Apellido = usuarioDTO.Apellido;
                    usuarioNuevo.Correo = usuarioDTO.Correo;
                    usuarioNuevo.Rol = usuarioDTO.Rol*/
                    usuarioNuevo.ClaveHasheada = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.ClaveHasheada);

                    await _context.Usuarios.AddAsync(usuarioNuevo);
                    await _context.SaveChangesAsync();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario se ha creado correctamente";
                    respuesta.Datos = usuarioDTO;
                    return (respuesta);
                }
                respuesta.Mensaje = "El usuario que intenta crear ya existe";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<Usuario>> DeleteUsuario(int id)
        {
            var respuesta = new RespuestaPrivada<Usuario>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios.FindAsync(id);
                if (usuarioBD != null)
                {
                    _context.Usuarios.Remove(usuarioBD);
                    await _context.SaveChangesAsync();
                    respuesta.Datos = usuarioBD;
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario se ha eliminado correctamente ";
                    return respuesta;
                }
                respuesta.Mensaje = "El usuario a eliminar no fue hallado ";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno" + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<UsuarioDTO>> PutUsuario(int id, UsuarioDTO usuarioDTO)
        {
            var respuesta = new RespuestaPrivada<UsuarioDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios.FindAsync(id);
                if (usuarioBD != null)
                {

                    var usuarioConMismoNombre = await _context.Usuarios
                        .AnyAsync(x => x.NombreUsuario.ToLower() == usuarioDTO.NombreUsuario.ToLower() && x.Id != id);

                    if (usuarioConMismoNombre)
                    {
                        respuesta.Mensaje = "Ya existe un usuario con el mismo nombre. Elija otro nombre.";
                        return respuesta;
                    }

                    usuarioBD.NombreUsuario = usuarioDTO.NombreUsuario;
                    usuarioBD.Nombre = usuarioDTO.Nombre;
                    usuarioBD.Apellido = usuarioDTO.Apellido;
                    usuarioBD.Correo = usuarioDTO.Correo;
                    usuarioBD.Rol = usuarioDTO.Rol;
                    usuarioBD.ClaveHasheada = BCrypt.Net.BCrypt.HashPassword(usuarioDTO.ClaveHasheada);

                    await _context.SaveChangesAsync();
                    respuesta.Datos = usuarioBD.Adapt<UsuarioDTO>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El usuario se ha modificado correctamente";
                    return respuesta;
                }
                respuesta.Mensaje = "El usuario a modificar no fue hallado";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<UsuarioDTOConId>> PatchUsuarioRol(int id, string nuevoRol)
        {
            var respuesta = new RespuestaPrivada<UsuarioDTOConId>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
                if (usuarioBD != null)
                {
                    usuarioBD.Rol = nuevoRol;
                    await _context.SaveChangesAsync();
                    respuesta.Datos = usuarioBD.Adapt<UsuarioDTOConId>();
                    respuesta.Exito = true;
                    respuesta.Mensaje = "El rol del usuario fue cambiado con exito";
                    return respuesta;
                }
                respuesta.Mensaje = "No se ha podido hallar al usuario";
                return (respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno : " + ex.Message;
                return (respuesta);
            }
        }

        public async Task<RespuestaPrivada<LoginUsuarioConRolDTO>> AutenticarUsuario(LoginUsuarioDTO loginUsuario)
        {
            var respuesta = new RespuestaPrivada<LoginUsuarioConRolDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioBD = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == loginUsuario.NombreUsuario);

                if (usuarioBD == null)
                {
                    respuesta.Mensaje = "Usuario no encontrado";
                    return respuesta;
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginUsuario.Clave, usuarioBD.ClaveHasheada);

                if (!isPasswordValid)
                {
                    respuesta.Mensaje = "Contraseña incorrecta";
                    return respuesta;
                }

                var data = new LoginUsuarioConRolDTO
                {
                    Token = _jwt.GenerarToken(usuarioBD),
                    Rol = usuarioBD.Rol,
                    Id = usuarioBD.Id,
                    LoginUsuario = loginUsuario

                };

                respuesta.Datos = data;
                respuesta.Exito = true;
                respuesta.Mensaje = "Autenticación exitosa";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno: " + ex.Message;
                return respuesta;
            }
        }

        public async Task<RespuestaPrivada<RegisterUsuarioDTO>> RegistrarUsuario(RegisterUsuarioDTO registerUsuario)
        {
            var respuesta = new RespuestaPrivada<RegisterUsuarioDTO>();
            respuesta.Datos = null;

            try
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == registerUsuario.NombreUsuario.ToLower());
                if (usuarioExistente != null)
                {
                    respuesta.Mensaje = "El nombre de usuario ya está en uso.";
                    return respuesta;
                }

                var usuarioNuevo = registerUsuario.Adapt<Usuario>();
                usuarioNuevo.Rol = "Cliente";
                usuarioNuevo.ClaveHasheada = BCrypt.Net.BCrypt.HashPassword(registerUsuario.Clave); 

                await _context.Usuarios.AddAsync(usuarioNuevo);
                await _context.SaveChangesAsync();

                respuesta.Exito = true;
                respuesta.Mensaje = "El usuario se ha registrado correctamente";
                respuesta.Datos = usuarioNuevo.Adapt<RegisterUsuarioDTO>(); 
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = "Error interno: " + ex.Message;
                return respuesta;
            }
        }

    }
}
