namespace Inmobiliaria.Domain.Entities;

public class Admin : User
{
    public Admin()
    {
        Role = "Administrador";
    }
}