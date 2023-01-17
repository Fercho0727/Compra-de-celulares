using Examen_Final.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examen_Final
{
    public partial class Form1 : Form
    {
        private List<Celular> ListaCelular = new List<Celular>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarNuevoCelular();
        }

        private void AgregarNuevoCelular() 
        {
            Celular nuevoCelular = new Celular
            {
                Modelo = comboModelo.SelectedItem.ToString(),
                Lugar = comboLugar.SelectedItem.ToString(),
                Color = comboColor.SelectedItem.ToString(),
                Fecha = dateTimePicker1.Text,
            };
            AgregarCelular(nuevoCelular);
        }

        public void AgregarCelular(Celular celular) 
        {
            ListaCelular.Add(celular);
            BindingSource bs = new BindingSource
            {
                DataSource = ListaCelular
            };
            dgCelulares.DataSource = bs;
            dgCelulares.Refresh();
            AgregarCelularDB(celular);
        }

        private void AgregarCelularDB(Celular celular) 
        {
            try
            {
                //Connection String, (servidor, la base de datos, usuario, contrasenia)
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = @"Server=DESKTOP-BDRJS3S\SQLEXPRESS;Database = IpearSegundoAnioDB;Trusted_Connection=true";
                    //Abrir la conexion
                    conn.Open();
                    //QUERY DE INSERTAR INSERT INTO TABLA (CAMPOS) VALUES (VALORES), INSERT INTO TABLA VALUES(VALORES)
                    SqlCommand insertCommand = new SqlCommand("INSERT INTO Celulares VALUES(@Modelo,@Lugar," +
                        "@Color, @Fecha)", conn);
                    //Agregar Paramatros
                    insertCommand.Parameters.Add(new SqlParameter("Modelo", celular.Modelo));
                    insertCommand.Parameters.Add(new SqlParameter("Lugar", celular.Lugar));
                    insertCommand.Parameters.Add(new SqlParameter("Color", celular.Color));
                    insertCommand.Parameters.Add(new SqlParameter("Fecha", celular.Fecha));
                    //Ejecutar el comando
                    //insertCommand.ExecuteScalar(); //ejercutar. retornar la primera fila y la primera columna
                    insertCommand.ExecuteNonQuery(); //ejecuta la instruccion y la retorna el numero de filas afectadas
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
