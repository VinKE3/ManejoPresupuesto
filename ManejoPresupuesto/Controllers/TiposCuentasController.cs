//CREAMOS TIPOS CUENTAS CONTROLLER

using ManejoPresupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController: Controller
    {
        public IActionResult Crear()
        {
            return View();
        }
        //CREAMOS LA ACCION QUE RESPONDA HACIE EL HTTP POST
        [HttpPost]
        public IActionResult Crear(TipoCuenta tipoCuenta)
        {
            //USAMOS MODELSTATE PARA SABER SI EL MODELO ES VALIDO O NO
            if (!ModelState.IsValid) //EN ESTE CASO ESTAMOS NEGANDO, SI EL MODEL NO ES VALIDO ENTONCES
            { 
                return View(tipoCuenta); //ESTO VA A SER IMPORTANTE PARA ASI PODER LLENAR EL FORMULARIO CON LA INFORMACION QUE EL USARIO YA TENIA Y NO TENER QUE ESTAR BORRANDO LA INFORMACION QUE HABIA RELLENADO
            }
            return View(tipoCuenta);
        }
        //1.YA CON ESTO TENEMOS UN FORMULARIO TIPOCUENTA QUE ENVIA LOS DATOS DEL FORMULARIO HACIA EL CONTROLADOR 
    }
}
