using PruebaUsuarios.connection;
using System;
using System.Data;
using System.Windows.Forms;

namespace PruebaUsuarios{
    public partial class Form1 : Form{

        private Conexion conexion;

        public Form1(){
            InitializeComponent();

            // Creamos una instacia de la clase de conexión en el constructor del formulario
            conexion = new Conexion();

            // Cargamos automaticamente los datos de nuestra tabla
            CargarDatos();

        }

        private void Form1_Load(object sender, EventArgs e){
            // Desactivamos los botones al iniciar el formulario
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;
        }

        private void CargarDatos(){
            // Construimos la consulta SQL para obtener datos de la tabla
            string consulta = "SELECT * FROM usuarios";

            // Ejecutamos la consulta y obtienemos los datos en un DataTable
            DataTable dataTable = conexion.EjecutarConsulta(consulta);

            // Configuramos el DataGridView para NO generar automáticamente las columnas
            dataGridView1.AutoGenerateColumns = true;

            // Asignamos el DataTable como origen de datos para el DataGridView
            dataGridView1.DataSource = dataTable;
        }

        private void LimpiarTextBox(){

            tb_buscar.Text = string.Empty;
            tb_id.Text = string.Empty;
            tb_nombre.Text = string.Empty;
            tb_correo.Text = string.Empty;
            tb_numero.Text = string.Empty;
            tb_direccion.Text = string.Empty;

            btn_agregar.Enabled = true;
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;

        }

        private void btn_limpiar_Click(object sender, EventArgs e){
            LimpiarTextBox();
        }

        private void btn_agregar_Click(object sender, EventArgs e){

            // Almacenamos nuestros Textbox en variables
            string nombre = tb_nombre.Text;
            string correo = tb_correo.Text;
            string numero = tb_numero.Text;
            string direccion = tb_direccion.Text;

            // Validamos que nuestros Textbox no esten vaciós
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes llenar todos los campos.");
            }else if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(numero)){
                MessageBox.Show("Debes ingresar tu nombre, correo y número.");
            }else if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes ingresar tu nombre, correo y dirección.");
            }else if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes ingresar correo, número y dirección.");
            }else if (string.IsNullOrEmpty(nombre)){
                MessageBox.Show("Debes ingresar tu nombre.");
            }else if (string.IsNullOrEmpty(correo)){
                MessageBox.Show("Debes ingresar tu correo.");
            }else if (string.IsNullOrEmpty(numero)){
                MessageBox.Show("Debes ingresar tu número.");
            }else if (string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes ingresar tu dirección.");
            }else{

                // Construimos nuestro Query de inserción con los valores de nuestros TextBox
                string insercion = $"INSERT INTO usuarios (usuario_nombre, usuario_correo, usuario_numero, usuario_direccion) VALUES ('{nombre}', '{correo}', '{numero}', '{direccion}')";
                
                // Ejecutamos la consulta 
                int filasAfectadas = conexion.EjecutarNonQuery(insercion);

                // Validamos si la consulta se hizo con exito
                if (filasAfectadas > 0){
                    MessageBox.Show("Usuario registrado correctamente.");
                    // Llamamos CargarDatos para actualizar la tabla
                    CargarDatos();
                    LimpiarTextBox();
                }
                else{
                    MessageBox.Show("Error al registrar al usuario.");
                }

            }
        }

        private void btn_modificar_Click(object sender, EventArgs e){
            // Almacenamos nuestros Textbox en variables
            string id = tb_id.Text;
            string nombre = tb_nombre.Text;
            string correo = tb_correo.Text;
            string numero = tb_numero.Text;
            string direccion = tb_direccion.Text;

            // Validamos que nuestros Textbox no esten vaciós
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes llenar todos los campos.");
            }else if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(numero)){
                MessageBox.Show("Debes ingresar tu nombre, correo y número.");
            }else if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes ingresar tu nombre, correo y dirección.");
            }else if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(numero) || string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes ingresar correo, número y dirección.");
            }else if (string.IsNullOrEmpty(nombre)){
                MessageBox.Show("Debes ingresar tu nombre.");
            }else if (string.IsNullOrEmpty(correo)){
                MessageBox.Show("Debes ingresar tu correo.");
            }else if (string.IsNullOrEmpty(numero)){
                MessageBox.Show("Debes ingresar tu número.");
            }else if (string.IsNullOrEmpty(direccion)){
                MessageBox.Show("Debes ingresar tu dirección.");
            }else{

                // Construimos nuestro Query de edicion con los valores de nuestros TextBox
                string edicion = $"UPDATE usuarios SET usuario_nombre = '{nombre}', usuario_correo = '{correo}', usuario_numero = '{numero}', usuario_direccion = '{direccion}' WHERE usuario_id = '{id}'";

                // Ejecutamos la consulta 
                int filasAfectadas = conexion.EjecutarNonQuery(edicion);

                // Validamos si la consulta se hizo con exito
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Usuario modificado correctamente.");
                    // Llamamos CargarDatos para actualizar la tabla
                    CargarDatos();
                    LimpiarTextBox();
                }
                else{
                    MessageBox.Show("Error al modificar al usuario.");
                }

            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e){
            
            // Almacenamos el Textbox de nuestro id en una variable
            string id = tb_id.Text;

            // Construimos nuestro Query para eliminar con el valor de nuestro TextBox
            string eliminacion = $"DELETE FROM usuarios WHERE usuario_id = '{id}'";

            DialogResult resultado = MessageBox.Show("Estas a punto de eliminar este usuario.", "Eliminar usuario", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

            if (resultado == DialogResult.OK){

                // Ejecutamos la consulta 
                int filasAfectadas = conexion.EjecutarNonQuery(eliminacion);

                // Validamos si la consulta se hizo con exito
                if (filasAfectadas > 0){
                    MessageBox.Show("Usuario eliminado correctamente.");
                    // Llamamos CargarDatos para actualizar la tabla
                    CargarDatos();
                    LimpiarTextBox();
                }
                else{
                    MessageBox.Show("Error al eliminar al usuario.");
                }

            }else if (resultado == DialogResult.Cancel){
                // Dejamos en blanco para regresar a la vista
            }

        }

        private void btn_buscar_Click(object sender, EventArgs e){

            // Obtenemos el valor de nuestro textbox busqueda
            string valorBusqueda = tb_buscar.Text;

            // Hablitamos los botones de editar y borrar, y deshabilitamos el boton de nuevo
            btn_agregar.Enabled = false;
            btn_modificar.Enabled = true;
            btn_eliminar.Enabled = true;

            // Construimos nuestra consulta SQL dinámica
            string consulta = "SELECT * FROM usuarios WHERE ";

            // Verificamos si el TextBox no está vacío
            if (!string.IsNullOrEmpty(valorBusqueda)){

                // Agregamos condiciones para cada campo
                consulta += $"usuario_nombre LIKE '%{valorBusqueda}%' OR ";
                consulta += $"usuario_correo LIKE '%{valorBusqueda}%' OR ";
                consulta += $"usuario_numero LIKE '%{valorBusqueda}%' OR ";
                consulta += $"usuario_direccion LIKE '%{valorBusqueda}%' OR ";

                // Eliminamos el último "OR" para que la consulta sea válida
                consulta = consulta.TrimEnd(' ', 'O', 'R');
            }else{
                MessageBox.Show("Ingresa un valor para realizar la búsqueda.");
            }

            // Ejecutamos la consulta SQL
            DataTable resultados = conexion.EjecutarConsulta(consulta);

            // Verificamos si se encontraron resultados
            if (resultados.Rows.Count > 0){
                // Obtenemos el primer registro 
                DataRow primerResultado = resultados.Rows[0];

                // Asignamos los valores a los TextBox
                tb_id.Text = primerResultado["usuario_id"].ToString();
                tb_nombre.Text = primerResultado["usuario_nombre"].ToString();
                tb_correo.Text = primerResultado["usuario_correo"].ToString();
                tb_numero.Text = primerResultado["usuario_numero"].ToString();
                tb_direccion.Text = primerResultado["usuario_direccion"].ToString();
            }else{
                MessageBox.Show("No se encontraron resultados para la búsqueda.");
                LimpiarTextBox();
            }

        }
    }
}
