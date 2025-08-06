using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace tp6_tudu.Models;

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
    public void Cambiarpassword(string nombre, string nuevapassword)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Integrante SET password = @pNuevapassword WHERE nombre = @pNombre";
            connection.Execute(query, new { pNuevapassword = nuevapassword, pNombre = nombre });
        }
    }
    public void AgregarUsuario(string nombre, string password, string username, DateTime fecha, string foto, int idUsuario)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO Integrante 
                (nombre, password, username, fecha, telefono, direccion, rol, foto, idUsuario)
                VALUES 
                (@pNombre, @pPassword, @pusername, @pfecha, @pFoto, @pidUsuario)";
                            
            connection.Execute(query, new 
            { pNombre = nombre, ppassword = password, pusername = username, pfecha = fecha, pFoto = foto, pidUsuario = idUsuario});
        }
    }
    public List<Usuario> ObtenerTareasPorUsuario(int idUsuario)
    {
        List<Usuario> integrantes = new List<Usuario>();

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Integrante WHERE idUsuario = @pidUsuario";
            integrantes = connection.Query<Usuario>(query, new { pidUsuario = idUsuario }).ToList();
        }

        return integrantes;
    }
}