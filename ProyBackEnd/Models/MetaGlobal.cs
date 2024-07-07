namespace ProyBackEnd.Models
{
    public class MetaGlobal
    {
        public static string Cnx = "";
        public static void LoadConnetionString(string conexion)
        {
            Cnx = conexion;
        }
    }
}
