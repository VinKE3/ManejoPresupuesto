using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados);
    }
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string conecctionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            conecctionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(conecctionString);
            var id = await connection.QuerySingleAsync<int>("TiposCuentas_Insertar",
                new {usuarioId = tipoCuenta.UsuarioId,
                nombre = tipoCuenta.Nombre},
                commandType: System.Data.CommandType.StoredProcedure);
            tipoCuenta.Id = id;
        }

        //CREAMOS UN MODELO PARA QUE UN USUARIO NO PUEDA REPETIR DOS VECES EL MISMO NOMBRE PARA SU TIPO DE CUENTA
        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var connection = new SqlConnection(conecctionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1
                FROM TiposCuentas 
                WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId;",
                new {nombre, usuarioId});
            return existe == 1;
        }

        //CREAMOS UN NUEVO METODO PARA VISUALIZAR REGISTROS EN NUESTRA APLICACIONES, PARA ESO VAMOS A HACER UN SELECT A LA BASE DE DATOS, TRAER DICHOS REGISTROS Y MOSTRARSELOS A LOS USUARIOS

        public async Task<IEnumerable<TipoCuenta>> obtener(int usuarioId)
        {
            using var connection = new SqlConnection(conecctionString);
            return await connection.QueryAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                                            FROM TiposCuentas
                                                            WHERE UsuarioId = @UsuarioId
                                                            ORDER BY Orden",
                                                            new { usuarioId });
        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(conecctionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas
                                            SET Nombre= @Nombre
                                            WHERE id = @Id", tipoCuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(conecctionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                                                            FROM TiposCuentas
                                                                            WHERE Id = @Id AND UsuarioId = @UsuarioId",
                                                                             new { id, usuarioId });
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(conecctionString);
            await connection.ExecuteAsync(@"DELETE TiposCuentas
                                                WHERE Id = @Id", new { id });
        }

        public async Task Ordenar(IEnumerable<TipoCuenta> tipoCuentasOrdenados)
        {
            var query = @"UPDATE TiposCuentas SET Orden = @Orden WHERE Id=@Id";
            using var connection = new SqlConnection(conecctionString);
            await connection.ExecuteAsync(query, tipoCuentasOrdenados);
        }
    }
}
