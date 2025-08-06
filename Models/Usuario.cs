namespace tpNoNum.Models;
public class Usuario{
    public int id { get; set; }
    public string nombre { get; set; }
    public string apellido { get; set; }
    public string contra { get; set; }
    public string email { get; set; }
    public DateTime fechaNac { get; set; }
    public string telefono { get; set; }
    public string direccion { get; set; }
    public string rol { get; set; }
    public string foto { get; set; }
    public int idGrupo { get; set; }

    public Usuario() { }

    public Usuario(int id, string nombre, string contra, string email, DateTime fechaNac, string telefono, string direccion, string rol, string foto, int idGrupo)
    {
        this.id = id;
        this.nombre = nombre;
        this.apellido = nombre;
        this.contra = contra;
        this.email = email;
        this.fechaNac = fechaNac;
        this.telefono = telefono;
        this.direccion = direccion;
        this.rol = rol;
        this.foto = foto;
        this.idGrupo = idGrupo;
    }
}