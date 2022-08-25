namespace ManejoPresupuesto.Models
{
    public class ObtenerTransaccionesPorCuenta
    {
        public int UsuarioId { get; set; }

        public int CuentaiD { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
    }
}
