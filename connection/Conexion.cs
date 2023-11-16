using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace PruebaUsuarios.connection
{
    class Conexion {
        private MySqlConnection conexion;
        private static string servidor = "localhost";  // Nombre o ip del servidor de MySQL
        private static string db = "prueba_usuarios";            // Nombre de la base de datos
        private static string usuario = "root";        // Usuario de acceso a MySQL
        private static string contra = "";         // Contraseña de usuario de acceso a MySQL
        private string stringConexion = "Database=" + db +
                                          "; Data Source=" + servidor +
                                          "; User Id= " + usuario +
                                          "; Password=" + contra + "";   // Cadena de conexión con MySQL

        // Constructor
        public Conexion() {
            conexion = new MySqlConnection(stringConexion); // Agregamos la cadena de conexión
        }

        // Método para abrir la conexión con MySQL
        private void AbrirConexion() {
            if (conexion.State == System.Data.ConnectionState.Closed) {
                conexion.Open();
            }
        }

        // Método para cerrar la conexion con MySQL
        private void CerrarConexion() {
            if (conexion.State == System.Data.ConnectionState.Open) {
                conexion.Close();
            }
        }

        // Método para ejecutar el SELECT a la tabla
        public DataTable EjecutarConsulta(string consulta) {
            DataTable resultado = new DataTable();

            try{
                AbrirConexion();
                using (MySqlCommand comando = new MySqlCommand(consulta, conexion)){
                    using(MySqlDataAdapter adaptador = new MySqlDataAdapter(comando)){
                        adaptador.Fill(resultado);
                    }
                }
            }catch(Exception ex){
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message);
            }finally{
                CerrarConexion();
            }

            return resultado;
        }

        // Método para ejecutar QUERYS
        public int EjecutarNonQuery(string consulta) {
            int filasAfectadas = 0;

            try{
                AbrirConexion();

                using(MySqlCommand comando = new MySqlCommand(consulta, conexion)){
                    filasAfectadas = comando.ExecuteNonQuery();
                }

            }catch(Exception ex){
                MessageBox.Show("Error al ejecutar el query: " + ex.Message);
            }finally{
                CerrarConexion();
            }

            return filasAfectadas;
        }

    } 
}
