using System;
using System.Collections.Generic;

namespace ProyectMVC.Models;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? AddressPerson { get; set; }

    public string? Email { get; set; }

    public decimal? Dpi { get; set; }

    public string? AccountNumber { get; set; }
}
