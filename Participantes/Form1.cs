using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Participantes
{
    public partial class Form1 : Form
    {
        List<Participante> participantes = new List<Participante>();

        public Form1()
        {
            InitializeComponent();
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtAltura.Text = string.Empty;
            txtPuesto.Text = string.Empty;
            dateFecha.Text = string.Empty;
        }
        private void Bloquear(bool Editando)
        {
            btnNuevo.Enabled = !Editando;
            btnGuardar.Enabled = Editando;
            btnEliminar.Enabled = !Editando;
            btnCancelar.Enabled = Editando;
        }
        private List<string> ValidarDatos()
        {
            List<string> errores = new List<string>();
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                errores.Add("Debe ingresar el Nombre");
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                errores.Add("Debe ingresar el apellido");
            }
            int a;
            if (!int.TryParse(txtAltura.Text, out a))
            {
                errores.Add("La altura no es valida");
            }
            if (!int.TryParse(txtPuesto.Text, out a))
            {
                errores.Add("El puesto no es valido");
            }
            if (dateFecha.Value > DateTime.Now.AddYears(-18))
            {
                errores.Add("Debe ser mayor de 18 años");
            }
            return errores;
        }
        private void ActualizarGrilla()
        {
            dgv.DataSource = null;
            dgv.DataSource = participantes;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Bloquear(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            List<string> errores = ValidarDatos();
            if (errores.Count != 0)
            {
                MessageBox.Show(errores.Aggregate("", (acc, it) => acc + it + "\r\n").Trim());
                return;
            }
            Participante p = new Participante(0, txtNombre.Text, txtApellido.Text, dateFecha.Value, int.Parse(txtAltura.Text), int.Parse(txtPuesto.Text));
            participantes.Add(p);
            ActualizarGrilla();
            Bloquear(false);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Bloquear(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count != 1)
            {
                MessageBox.Show("Debe seleccionar un elemento de la lista para eliminar");
            }
            else if (MessageBox.Show("Esta Seguro?","Eliminar",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                participantes.RemoveAt(dgv.SelectedRows[0].Index);
                ActualizarGrilla();
            }
        }
    }
}
