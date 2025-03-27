using System;

namespace Fluxus.Common.Security.Interfaces
{
    /// <summary>
    /// Contrato mínimo para representar um usuário autenticado.
    /// </summary>
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        string Role { get; }
    }
}
