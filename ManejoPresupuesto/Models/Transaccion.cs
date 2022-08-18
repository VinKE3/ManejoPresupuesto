﻿using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        [Display(Name = "Fecha Transaccion")]
        [DataType(DataType.Date)]
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;
        public decimal Monto { get; set; }

        [Range(1, maximum:int.MaxValue, ErrorMessage ="Debe seleccionr una categoría")]
        [Display(Name ="Categoria")]
        public int CategoriaId { get; set; }

        [StringLength(maximumLength:1000, ErrorMessage ="La nota no debe superar los {1} caracteres")]
        public string Nota { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionr una cuenta")]
        [Display(Name = "Cuenta")]
        public int CuentaId { get; set; }
    }
}