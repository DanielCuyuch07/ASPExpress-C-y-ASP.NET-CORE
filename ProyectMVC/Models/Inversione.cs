using System;
using System.Collections.Generic;

namespace ProyectMVC.Models;

public partial class Inversione
{
    public int IdInversion { get; set; }

    public string? NombreInversion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
