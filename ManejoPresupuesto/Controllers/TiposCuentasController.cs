//CREAMOS TIPOS CUENTAS CONTROLLER

using Dapper;
using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Controllers
{
    public class TiposCuentasController : Controller
    {

        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

        public TiposCuentasController(IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }

        public IActionResult Crear()
        {


            return View();
        }
        //CREAMOS LA ACCION QUE RESPONDA HACIE EL HTTP POST
        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta)
        {
            //USAMOS MODELSTATE PARA SABER SI EL MODELO ES VALIDO O NO
            if (!ModelState.IsValid) //EN ESTE CASO ESTAMOS NEGANDO, SI EL MODEL NO ES VALIDO ENTONCES
            {
                return View(tipoCuenta); //ESTO VA A SER IMPORTANTE PARA ASI PODER LLENAR EL FORMULARIO CON LA INFORMACION QUE EL USARIO YA TENIA Y NO TENER QUE ESTAR BORRANDO LA INFORMACION QUE HABIA RELLENADO

            }
            tipoCuenta.UsuarioId = 1;

            var yaExisteTipoCuenta =
            await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);
            if(yaExisteTipoCuenta)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre),
                    $"El nombre {tipoCuenta.Nombre} ya existe");
                return View(tipoCuenta);
            }
            await repositorioTiposCuentas.Crear(tipoCuenta);
            return View();
        }
        //1.YA CON ESTO TENEMOS UN FORMULARIO TIPOCUENTA QUE ENVIA LOS DATOS DEL FORMULARIO HACIA EL CONTROLADOR 

        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre, usuarioId);
            if(yaExisteTipoCuenta)
            {
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);
        }
    }
}
