using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace tpNoNum.Models;

public class BD{
    private static string _connectionString = @"Server=localhost;DataBase=Integrantes;Integrated Security=True;TrustServerCertificate=True;";
    public BD(){}
    public List<Usuario> ConseguirUsuarios(){
        List<Usuario> usuarios = new List<Usuario>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Integrante";
            usuarios = connection.Query<Usuario>(query).ToList();
        }
        return usuarios;
    }
     public Usuario BuscarUsuarioPorId(int idBuscado){
        Usuario integranteBuscado = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Integrante WHERE id = @pIdBuscado";
            integranteBuscado = connection.QueryFirstOrDefault<Usuario>(query, new {pIdBuscado = idBuscado});
        }
        return integranteBuscado;
    }
    public Usuario BuscarUsuarioPorUsername(string userBuscado){
        Usuario usuarioBuscado = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Integrante WHERE nombre = @pnombreBuscado";
            usuarioBuscado = connection.QueryFirstOrDefault<Usuario>(query, new {pnombreBuscado = userBuscado});
        }
        return usuarioBuscado;
    }
    public void CambiarContra(string nombre, string nuevaContra)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Integrante SET contra = @pNuevaContra WHERE nombre = @pNombre";
            connection.Execute(query, new { pNuevaContra = nuevaContra, pNombre = nombre });
        }
    }
    public void AgregarUsuario(string nombre, string contra, string email, DateTime fechaNac, string telefono, string direccion, string rol, string foto, int idGrupo)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO Integrante 
                (nombre, contra, email, fechaNac, telefono, direccion, rol, foto, idGrupo)
                VALUES 
                (@pNombre, @pContra, @pEmail, @pFechaNac, @pTelefono, @pDireccion, @pRol, @pFoto, @pIdGrupo)";
                            
            connection.Execute(query, new 
            { pNombre = nombre, pContra = contra, pEmail = email, pFechaNac = fechaNac, pTelefono = telefono, pDireccion = direccion, pRol = rol, pFoto = foto, pIdGrupo = idGrupo});
        }
    }
    // public List<Usuario> ObtenerIntegrantesPorGrupo(int idGrupo)
    // {
    //     List<Usuario> integrantes = new List<Usuario>();

    //     using(SqlConnection connection = new SqlConnection(_connectionString))
    //     {
    //         string query = "SELECT * FROM Integrante WHERE idGrupo = @pIdGrupo";
    //         integrantes = connection.Query<Usuario>(query, new { pIdGrupo = idGrupo }).ToList();
    //     }

    //     return integrantes;
    // }
}