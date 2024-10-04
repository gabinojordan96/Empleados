using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using EmpleadosAPI.Modelo;
using System.Data.SqlClient;

namespace EmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly string cadenaSQL;

        public EmpleadoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("cadenaSQL");
        }

        [HttpGet]
        [Route("lista")]
        public ActionResult Lista()
        {
            List<Empleado> lista = new List<Empleado>();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_empleados", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Empleado()
                            {
                                NumeroEmp = Convert.ToInt32(rd["NumeroEmp"]),
                                Nombre = rd["Nombre"].ToString(),
                                Apellidos = rd["Apellidos"].ToString()
                            });
                        }
                    }
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", Response = lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, Response = lista });
            }
        }

        [HttpGet]
        [Route("Obtener/{NumeroEmp:int}")]
        public ActionResult Obtener(int NumeroEmp)
        {
            List<Empleado> lista = new List<Empleado>();
            Empleado empleado = new Empleado();

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_lista_empleados", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            lista.Add(new Empleado()
                            {
                                NumeroEmp = Convert.ToInt32(rd["NumeroEmp"]),
                                Nombre = rd["Nombre"].ToString(),
                                Apellidos = rd["Apellidos"].ToString()
                            });
                        }
                    }
                }
                empleado = lista.Where(item => item.NumeroEmp == NumeroEmp).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", Response = empleado });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, Response = empleado });
            }
        }
        [HttpPost]
        [Route("Guardar")]
        public ActionResult Guardar([FromBody] Empleado objeto)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_guardar_empleado", conexion);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", objeto.Apellidos);
                    
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpPut]
        [Route("Editar")]
        public ActionResult Editar([FromBody] Empleado objeto)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_editar_empleado", conexion);
                    cmd.Parameters.AddWithValue("NumeroEmp", objeto.NumeroEmp == 0 ? DBNull.Value : objeto.NumeroEmp);
                    cmd.Parameters.AddWithValue("Nombre", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                    cmd.Parameters.AddWithValue("Apellidos", objeto.Apellidos is null ? DBNull.Value : objeto.Apellidos);
                    
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "editado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }

        [HttpDelete]
        [Route("Eliminar/{NumeroEmp:int}")]
        public ActionResult Eliminar(int NumeroEmp)
        {

            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_elimnar_empleado", conexion);
                    cmd.Parameters.AddWithValue("NumeroEmp", NumeroEmp);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "eliminado" });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }
        }
    }
}
