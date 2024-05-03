using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectMVC.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    public string? NombreUsuario { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El correo electrónico es obligatorio.")]
    public string? Correo { get; set; }

    [Required(ErrorMessage = " La contraseña es obligatoria ")]
    [RegularExpression(@"^(?=.*[A-Z]).+$", ErrorMessage = "La clave debe contener al menos una letra mayúscula.")]
    public string? Clave { get; set; }
}
