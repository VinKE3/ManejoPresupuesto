using ManejoPresupuesto.Validacion;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta //: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        [Remote(action:"VerificarExisteTipoCuenta", controller:"TiposCuentas")]
        //[StringLength(maximumLength:50, MinimumLength =3, ErrorMessage ="La longitud del campo {0} debe estar entre {2} y {1}")]
        //[Display(Name ="Nombre del tipo cuenta")]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }



        //VALIDACIONES PERSONALIZADAS POR MODELO EJEMPLO
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nombre != null && Nombre.Length > 0)
        //    {
        //        var PrimeraLetra = Nombre[0].ToString();

        //        if (PrimeraLetra != PrimeraLetra.ToUpper())
        //        {
        //            yield return new ValidationResult("La pimera letra debe ser mayuscula",
        //                new[] { nameof(Nombre) });
        //        }
        //    }
        //}
    }
}
