namespace tp6_tudu.Models;
public class Usuario{
    public int id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string password { get; set; }
    public string username { get; set; }
    public DateTime ultimoLogin { get; set; }
    public string foto { get; set; }

    public Usuario() { }

    public Usuario(int id, string nombre, string apellido, string username, string password, DateTime fecha, string foto)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = apellido;
        this.username = username;
        this.password = password;
        this.ultimoLogin = fecha;
        this.foto = foto;
    }
}