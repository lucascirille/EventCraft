using CORE.DTOs;
using DataBase.Models;

namespace Servicios.Interfaces
{
    public interface IUsuarioService
    {
        Task<RespuestaPrivada<ICollection<UsuarioDTOConId>>> GetUsuarios();
        Task<RespuestaPrivada<ICollection<UsuarioSinPassDTO>>> GetUsuariosSinPass();
        Task<RespuestaPrivada<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDTO);
        Task<RespuestaPrivada<Usuario>> DeleteUsuario(int id);
        Task<RespuestaPrivada<UsuarioDTO>> PutUsuario(int id, UsuarioDTO usuarioDTO);
        Task<RespuestaPrivada<UsuarioDTOConId>> PatchUsuarioRol(int id, string nuevoRol);
        Task<RespuestaPrivada<LoginUsuarioConRolDTO>> AutenticarUsuario(LoginUsuarioDTO loginUsuario);
        Task<RespuestaPrivada<RegisterUsuarioDTO>> RegistrarUsuario(RegisterUsuarioDTO registerUsuario);
    }
}