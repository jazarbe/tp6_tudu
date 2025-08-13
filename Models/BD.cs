using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace tp6_tudu.Models;

public class BD{
    private static string _connectionString = @"Server=localhost;DataBase=bd;Integrated Security=True;TrustServerCertificate=True;";
    public BD(){}

    public void ActualizarLogin(int idUsuario){
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "UPDATE Usuarios SET ultimoLogin = @pUltimoLogin WHERE id = @pIdUsuario";
            connection.QueryFirstOrDefault<Usuario>(query, new {pUltimoLogin = DateTime.Now, pIdUsuario = idUsuario});
        }
    }
    public List<Tarea> ConseguirTareasDeUsuario(int idBuscado){
        List<Tarea> tareas = new List<Tarea>();
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tareas WHERE idUsuario = @pIdBuscado";
            tareas = connection.Query<Tarea>(query, new {pIdBuscado = idBuscado}).ToList();
        }
        return tareas;
    }
    public Tarea BuscarTareaPorId(int idBuscado){
        Tarea tareaBuscada = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Tareas WHERE id = @pIdBuscado";
            tareaBuscada = connection.QueryFirstOrDefault<Tarea>(query, new {pIdBuscado = idBuscado});
        }
        return tareaBuscada;
    }
    public void UpdateTarea(Tarea tareaBuscada, string titulo, string descripcion, DateTime fecha, bool finalizada){
        int idBuscado = tareaBuscada.id;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "UPDATE Tareas SET titulo = @ptitulo, descripcion = @pdescripcion, fecha = @pfecha, finalizada = @pfinalizada WHERE id = @pIdBuscado";
            tareaBuscada = connection.QueryFirstOrDefault<Tarea>(query, new {pIdBuscado = idBuscado});
        }
    }
    public void FinalizarTarea(int idBuscado){
        Tarea tareaBuscada = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "UPDATE Tareas SET finalizada = true WHERE id = @pIdBuscado";
            tareaBuscada = connection.QueryFirstOrDefault<Tarea>(query, new {pIdBuscado = idBuscado});
        }
    }
    public void DeleteTarea(Tarea tareaBuscada){
        int idBuscado = tareaBuscada.id;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "DELETE FROM Tareas WHERE id = @pIdBuscado";
            tareaBuscada = connection.QueryFirstOrDefault<Tarea>(query, new {pIdBuscado = idBuscado});
        }
    }
    public Usuario BuscarUsuarioPorId(int idBuscado){
        Usuario usuarioBuscado = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Usuarios WHERE id = @pIdBuscado";
            usuarioBuscado = connection.QueryFirstOrDefault<Usuario>(query, new {pIdBuscado = idBuscado});
        }
        return usuarioBuscado;
    }
    public Usuario BuscarUsuarioPorUsername(string userBuscado){
        Usuario usuarioBuscado = null;
        using(SqlConnection connection = new SqlConnection(_connectionString)){
            string query = "SELECT * FROM Usuarios WHERE username = @puserBuscado";
            usuarioBuscado = connection.QueryFirstOrDefault<Usuario>(query, new {puserBuscado = userBuscado});
        }
        return usuarioBuscado;
    }
    public void CambiarPassword(string username, string nuevapassword)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Usuarios SET password = @pNuevapassword WHERE username = @pUsername";
            connection.Execute(query, new { pNuevapassword = nuevapassword, pUsername = username });
        }
    }
    public void AgregarUsuario(string nombre, string apellido, string password, string username, string foto)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO Usuarios 
                (nombre, apellido, password, username, foto)
                VALUES 
                (@pNombre, @pApellido, @pPassword, @pUsername, @pFoto)";
            
                connection.Execute(query, new 
            {pNombre = nombre, pApellido = apellido, pPassword = password, pUsername = username, pFoto = foto});
        }
    }

    public void AgregarTarea(string titulo, string descripcion, DateTime fecha, bool finalizada, int idUsuario)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = @"
                INSERT INTO Tareas 
                (titulo, descripcion, fecha, finalizada, idUsuario)
                VALUES 
                (@ptitulo, @pdescripcion, @pfecha, @pfinalizada, @pidUsuario)";
                            
            connection.Execute(query, new 
            {ptitulo = titulo, pdescripcion = descripcion, pfecha = fecha, pfinalizada = finalizada, pidUsuario = idUsuario});
        }
    }
    public List<Tarea> ObtenerTareasPorUsuario(int idUsuario)
    {
        List<Tarea> tareas = new List<Tarea>();

        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE idUsuario = @pidUsuario";
            tareas = connection.Query<Tarea>(query, new { pidUsuario = idUsuario }).ToList();
        }

        return tareas;
    }
}