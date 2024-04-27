using System;
using System.Collections.Generic;

namespace ProyectMVC.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? NombreCliente { get; set; }

    public string? NumeroDeCuenta { get; set; }

    public string? CorreoElectronico { get; set; }

    public decimal? Saldo { get; set; }

    public int? IdInversion { get; set; }

    public virtual Inversione? IdInversionNavigation { get; set; }
}
