namespace tp6_tudu.Models;
public class Tarea{
    public int id { get; set; }
    public string titulo { get; set; }
    public string descripcion { get; set; }
    public DateTime fecha { get; set; }
    public bool finalizada { get; set; }
    public int idUsuario { get; set; }

    public Tarea() { }

    public Tarea(int id, string nombre, string descripcion, DateTime fecha, bool finalizada, int idUsuario)
    {
        this.id = id;
        this.titulo = nombre;
        this.descripcion = descripcion;
        this.fecha = fecha;
        this.finalizada = finalizada;
        this.idUsuario = idUsuario;
    }
}