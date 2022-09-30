using CRUDtransporte.Datos;
using CRUDtransporte.Dominio;
using CRUDtransporte.Servicios.Implementacion;
using CRUDtransporte.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDtransporte
{
    public partial class FmrAlta : Form
    {
        private IServicio gestor;
        private Camion nuevo;
        public FmrAlta()
        {
            InitializeComponent();
            gestor = new Servicio();
            nuevo = new Camion();
        }

        private void FmrAlta_Load(object sender, EventArgs e)
        {
            ObtenerProximo();
            ObtenerTipos();

        }

        private void ObtenerTipos()
        {
            cboTipo.ValueMember = "IdTipo";
            cboTipo.DisplayMember = "NombreTipo";
            cboTipo.DataSource = gestor.ObtenerTipos();
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList; 
        }

        private void ObtenerProximo()
        {
            int next = gestor.ObtenerProximo();
            if(next > 0)
            {
                lblNext.Text = "Camion N°: " + next.ToString();
            }
            else
            {
                MessageBox.Show("No se pudo obtener el numero de camion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un tipo de carga", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            if(txtPesoCarga.Text == "")
            {
                MessageBox.Show("Debe insertar un peso de carga", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            foreach(DataGridViewRow row in dgvCamiones.Rows)
            {
                if (row.Cells["ColTipo"].Value.ToString().Equals(cboTipo.Text))
                {
                    MessageBox.Show("El tipo de carga " + cboTipo.Text + " ya se encuentra en la lista", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }

            TipoCarga t = (TipoCarga)cboTipo.SelectedItem;
            double peso = int.Parse(txtPesoCarga.Text);
            Carga c = new Carga(peso, t);
            nuevo.AgregarCarga(c);
            dgvCamiones.Rows.Add(c.Tipo.IdTipo, c.Tipo.NombreTipo, c.Peso);

            txtCantidadTotal.Text = nuevo.CantidadTotal().ToString();
            txtPesoTotal.Text = nuevo.PesoTotal().ToString();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtPatente.Text == "")
            {
                MessageBox.Show("Debe insertar una patente", "Control", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            GuardarCamion();
        }

        private void GuardarCamion()
        {
            nuevo.Patente = txtPatente.Text;
            nuevo.Estado = txtEstado.Text;
            nuevo.PesoMax = double.Parse(txtPesoMax.Text);

            if(Helper.ObtenerInstancia().ConfirmarCamion(nuevo))
            {
                MessageBox.Show("Se registro el camion con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                MessageBox.Show("No se pudo insertar el camion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvCamiones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvCamiones.CurrentCell.ColumnIndex == 3)
            {
                nuevo.EliminarCarga(dgvCamiones.CurrentRow.Index);
                dgvCamiones.Rows.Remove(dgvCamiones.CurrentRow);
                txtCantidadTotal.Text = nuevo.CantidadTotal().ToString();
                txtPesoTotal.Text = nuevo.PesoTotal().ToString();
            }
        }
    }
}
