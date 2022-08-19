using ManejoPresupuesto.Models;
using ManejoPresupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuesto.Controllers
{
    public class TransaccionesController : Controller
    {
        private readonly IServicioUsuarios servicioUsuarios;
        private readonly IRepositorioCuentas repositorioCuentas;
        private readonly IRepositorioCategorias repositorioCategorias;

        public TransaccionesController(IServicioUsuarios servicioUsuarios, IRepositorioCuentas repositorioCuentas, IRepositorioCategorias repositorioCategorias)
        {
            this.servicioUsuarios = servicioUsuarios;
            this.repositorioCuentas = repositorioCuentas;
            this.repositorioCategorias = repositorioCategorias;
        }

        public async Task<IActionResult> Crear()
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var modelo = new Models.TransaccionCreacionViewModel();
            modelo.Cuentas = await obtenerCuentas(usuarioId);
            modelo.Categorias = await ObtenerCategorias(usuarioId, modelo.TipoOperacionId);
            return View(modelo);
        }

        private async Task<IEnumerable<SelectListItem>> obtenerCuentas(int usuarioId)
        {
            var cuentas = await repositorioCuentas.Buscar(usuarioId);
            return cuentas.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        //    private async Task<IEnumerable<SelectListItem>> ObtenerCategorias(int usuarioId,
        //TipoOperacion tipoOperacion)
        //    {
        //        var categorias = await repositorioCategorias.Obtener(usuarioId, tipoOperacion);
        //        var opcionPorDefecto = new SelectListItem("--Seleccione una opción--", "0", true);
        //        var resultado = categorias.Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();
        //        resultado.Insert(0, opcionPorDefecto);
        //        return resultado;
        //    }
        private async Task<IEnumerable<SelectListItem>> ObtenerCategorias(int usuarioId,
           TipoOperacion tipoOperacion)
        {
            var categorias = await repositorioCategorias.Obtener(usuarioId, tipoOperacion);
            return categorias.Select(x => new SelectListItem(x.Nombre, x.Id.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCategorias([FromBody] TipoOperacion tipoOperacion)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var categorias = await ObtenerCategorias(usuarioId, tipoOperacion);
            return Ok(categorias);
        }


    }
}