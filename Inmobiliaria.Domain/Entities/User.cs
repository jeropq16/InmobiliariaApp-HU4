using System;

namespace Inmobiliaria.Domain.Entities;

public abstract class User
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Email { get; protected set; } = string.Empty;
    public string Password { get; protected set; } = string.Empty;
    public string Role { get; protected set; } = string.Empty;

    protected User()
    {
        Id = Guid.NewGuid();
    }
}