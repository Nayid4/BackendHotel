using Dominio.ObjetosDeValor;
using Dominio.Usuarios;

namespace Aplicacion.Utilidades
{
    public interface IGestionToken
    {
        string EncryptSHA256(string texto);
        string GenerateJWT(Usuario usuario, int opcion);
        DatosUsuarioDTO? DecodeJWT();
    }
}
