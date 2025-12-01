using Asisya.Models;

namespace Asisya.Token;

public interface IJwtGenerador {
    string CrearToken(Usuario usuario);
}