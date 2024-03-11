using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PROYETGUIA
{
    public partial class Form1 : Form
    {
        // Definición de variables
        private Dictionary<string, string> guiaTelefonica = new Dictionary<string, string>();
        private string filePath = "guia_telefonica.txt";

        // Constructor
        public Form1()
        {
            InitializeComponent();
            CargarGuiaTelefonica(); // Cargar la guía telefónica al iniciar la aplicación
        }

        // Método para agregar un nuevo contacto
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string telefono = txtTelefono.Text;

            // Verificar que se ingresaron datos válidos
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(telefono))
            {
                // Verificar si el contacto ya existe en la guía telefónica
                if (!guiaTelefonica.ContainsKey(nombre))
                {
                    // Agregar el nuevo contacto
                    guiaTelefonica.Add(nombre, telefono);
                    MessageBox.Show("Contacto agregado correctamente.");
                    LimpiarCampos(); // Limpiar los campos de entrada
                    MostrarContactos(); // Actualizar la lista de contactos en la interfaz
                }
                else
                {
                    MessageBox.Show("El contacto ya existe en la guía telefónica.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre y un número de teléfono.");
            }
        }

        // Método para borrar un contacto existente
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;

            if (!string.IsNullOrEmpty(nombre))
            {
                if (guiaTelefonica.ContainsKey(nombre))
                {
                    guiaTelefonica.Remove(nombre);
                    MessageBox.Show("Contacto borrado correctamente.");
                    LimpiarCampos();
                    MostrarContactos();
                }
                else
                {
                    MessageBox.Show("El contacto no existe en la guía telefónica.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre.");
            }
        }

        // Método para editar un contacto existente
        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;

            if (!string.IsNullOrEmpty(nombre))
            {
                if (guiaTelefonica.ContainsKey(nombre))
                {
                    string nuevoTelefono = txtTelefono.Text;
                    guiaTelefonica[nombre] = nuevoTelefono;
                    MessageBox.Show("Contacto editado correctamente.");
                    LimpiarCampos();
                    MostrarContactos();
                }
                else
                {
                    MessageBox.Show("El contacto no existe en la guía telefónica.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un nombre.");
            }
        }

        // Método para mostrar todos los contactos en la lista
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            MostrarContactos();
        }

        // Método para limpiar los campos de entrada
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
        }

        // Método para cargar la guía telefónica desde un archivo al iniciar la aplicación
        private void CargarGuiaTelefonica()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        guiaTelefonica.Add(parts[0], parts[1]);
                    }
                }
            }
        }

        // Método para guardar la guía telefónica en un archivo al cerrar la aplicación
        private void GuardarGuiaTelefonica()
        {
            List<string> lines = new List<string>();
            foreach (var contacto in guiaTelefonica)
            {
                lines.Add($"{contacto.Key},{contacto.Value}");
            }
            File.WriteAllLines(filePath, lines);
        }

        // Método para actualizar la lista de contactos en la interfaz
        private void MostrarContactos()
        {
            lstContactos.Items.Clear(); // Limpiar la lista de contactos antes de mostrarlos nuevamente
            foreach (var contacto in guiaTelefonica)
            {
                lstContactos.Items.Add($"Nombre: {contacto.Key}, Teléfono: {contacto.Value}");
            }
        }

        // Método ejecutado al cargar el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarContactos(); // Mostrar los contactos al cargar el formulario
        }
    }
}
