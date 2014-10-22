﻿using SIMEDVirtual.Entity;
using SIMEDVirtual.IT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIMEDVirtual
{
    public partial class frm_empresa : Form
    {
        public frm_empresa()
        {
            InitializeComponent();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_registraEmpresa splash = new frm_registraEmpresa();
            splash.ShowDialog();
        }

        //cargamos las empresas
        private void cargarDataGrid()
        {
            //cargamos todos la info de drs en el datagrid
            var pts = new BindingList<EmpresaEntity>(EmpresaIT.getAllEmpresas());
            dgEmpresas.AutoGenerateColumns = false;
            dgEmpresas.DataSource = pts;
            //agregar el evento 
            //dgEmpresas.CellContentDoubleClick += dgClientes_CellClick;

            //se asignan datos al datagrid
            for (int j = 0; j < pts.Count; j++)
            {
                dgEmpresas.Rows[j].Cells[0].Value = pts.ElementAt(j).cedula.ToString();
                dgEmpresas.Rows[j].Cells[1].Value = pts.ElementAt(j).nombre.ToString();
                dgEmpresas.Rows[j].Cells[2].Value = pts.ElementAt(j).direccion.ToString();

                List<EmpresaEntity> empresa = EmpresaIT.getTelefono(pts.ElementAt(j).cedula.ToString());
                if (empresa.Count != 0)
                {
                    dgEmpresas.Rows[j].Cells[3].Value = empresa[0].telefono1;
                }
                else
                {
                    dgEmpresas.Rows[j].Cells[3].Value = "";
                }
            }
        }

        private void frm_empresa_Load(object sender, EventArgs e)
        {
            this.cargarDataGrid();
            dgEmpresas.Columns[0].Width = 250;
            dgEmpresas.Columns[1].Width = 350;
            dgEmpresas.Columns[2].Width = 150;
        }

        private void dgEmpresas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //cargamos todos la info de drs en el datagrid
            string cedula_juridica = dgEmpresas.Rows[e.RowIndex].Cells[0].Value.ToString();
            //selecciona el medico dependiento de la cedula
            var pts = new BindingList<EmpresaEntity>(EmpresaIT.getEmpresaByID(cedula_juridica));
            dgEmpresas.AutoGenerateColumns = false;
            dgEmpresas.DataSource = pts;

            string nombre = pts.ElementAt(0).nombre.ToString();
            string cedula = pts.ElementAt(0).cedula.ToString();
            string descripcion = pts.ElementAt(0).descripcion.ToString();
            string direccion = pts.ElementAt(0).direccion.ToString();

            int telefono1 = 0;
            int telefono2 = 0;
            string encargado1 = "";
            string encargado2 = "";

            List<EmpresaEntity> empresa = EmpresaIT.getTelefono(cedula_juridica);


            if (empresa.Count != 0)
            {
                telefono1 = Convert.ToInt32(empresa[0].telefono1);
                encargado1 = empresa[0].encargado1;

                if (empresa.Count == 2)
                {
                    telefono2 = empresa[1].telefono1;
                    encargado2 = empresa[1].encargado1;
                }
            }

            this.Hide();
            frm_registraEmpresa frm = new frm_registraEmpresa(cedula_juridica, nombre, descripcion, direccion, telefono1,
                encargado1, telefono2, encargado2, 1);
            frm.ShowDialog();
        }
    }
}
