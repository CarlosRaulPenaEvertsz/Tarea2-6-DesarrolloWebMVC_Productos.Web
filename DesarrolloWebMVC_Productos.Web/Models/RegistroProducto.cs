using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DesarrolloWebMVC_Productos.Web.Models
{
    public class RegistroProducto
    {
        private SqlConnection con;

        /* Conectarse a la DB*/

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(constr);
        }

        /* Grabar un Registro en la DB */
        public int GrabarProducto(Producto Prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Insert Into Productos (Descripcion, Tipo, Precio) " +
                                                "Values (@Descripcion, @Tipo, @Precio)", con);
            /* types */
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@Tipo", SqlDbType.VarChar);
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            /* data */
            comando.Parameters["@Descripcion"].Value = Prod.Descripcion;
            comando.Parameters["@Tipo"].Value = Prod.Tipo;
            comando.Parameters["@Precio"].Value = Prod.Precio;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }


        /* Mostrar todos los Registros de la DB */
        public List<Producto> RecuperarTodos()
        {
            Conectar();
            List<Producto> productos = new List<Producto>();

            SqlCommand com = new SqlCommand("Select Descripcion, Tipo, Precio From Productos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                Models.Producto Prod = new Models.Producto()
                {
                    Descripcion = registros["Descripcion"].ToString(),
                    Tipo = registros["Tipo"].ToString(),
                    Precio = double.Parse(registros["Precio"].ToString()),
                };
                productos.Add(Prod);
            }
            con.Close();
            return productos;
        }

        /* Mostrar un Registro especifico de la DB */
        public Models.Producto Recuperar(string Desc)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Select Descripcion, Tipo, Precio " +
                                               "From Productos where Descripcion = @Descripcion", con);
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters["@Descripcion"].Value = Desc;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Producto producto = new Producto();
            if (registros.Read())
            {
                producto.Descripcion = registros["Descripcion"].ToString();
                producto.Tipo = registros["Tipo"].ToString();
                producto.Precio = double.Parse(registros["Precio"].ToString());
 
            }
            con.Close();
            return producto;
        }

        /* Modificar un Registro especifico de la DB */
        public int Modificar(Producto Prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Update Productos set Descripcion=@Descripcion, Tipo=@Tipo, Precio=@Precio where Descripcion = @Descripcion", con);
            /* types */
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@Tipo", SqlDbType.VarChar);
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            /* data */
            comando.Parameters["@Descripcion"].Value = Prod.Descripcion;
            comando.Parameters["@Tipo"].Value = Prod.Tipo;
            comando.Parameters["@Precio"].Value = Prod.Precio;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

        // Borrar un Registro especifico de la DB
        public int Borrar(string desc)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Delete from Productos where Descripcion=@Descripcion", con);
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters["@Descripcion"].Value = desc;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }


}
