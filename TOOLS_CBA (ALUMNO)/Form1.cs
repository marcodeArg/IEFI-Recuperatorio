﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace TOOLS_CBA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Estanterias objEst;
        Productos objProd;
        Graphics estanterias;


        private DataTable tablaEstanterias;
        private DataTable tablaProductos;


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                objEst = new Estanterias();
                objProd = new Productos();
                tablaEstanterias = objEst.GetData();
                tablaProductos = objProd.GetData();


                estanterias = pictureBox1.CreateGraphics();
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo cargar la base de datos", "Error", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int codProducto = Convert.ToInt32(txtCodigo.Text);
            int stockTotal = 0;


            DataRow filaProducto;

            filaProducto = objProd.GetFila(codProducto);
            if (filaProducto != null)
            {
                lblNombre.Text = filaProducto["nombre"].ToString();

                foreach (DataRow filaEstanterias in tablaEstanterias.Rows)
                {
                    if ((int)filaEstanterias["producto"] == codProducto)
                    {
                        stockTotal += Convert.ToInt32(filaEstanterias["stock"]);
                    }
                }

                lblStock.Text = stockTotal.ToString();


                Dibujar(codProducto);

            }
            else
            {
                MessageBox.Show("No existe ningun producto con esa id", "Advertencia", MessageBoxButtons.OK);
            }
        }

        public void Dibujar(int codigo)
        {
            int x;
            int y;
            int numEstanteria = 1;

            Font fuente = new Font("Verdana", 8, FontStyle.Bold);


            // Filas del picture box
            for (y = 0; y < pictureBox1.Height - 42; y += 42)
            {
                // Columnas del picture box
                for (x = 0; x < pictureBox1.Width - 30; x += 30)
                {
                    // BORREN ALGUNA DE LAS DOS OPCIONES

                    //OPCION 1

                    //estanterias.DrawRectangle(Pens.Black, x, y, 32, 4);
                    //foreach (DataRow filaEstanteria in tablaEstanterias.Rows)
                    //{

                    //    if ((int)filaEstanteria["producto"] == codigo && (int)filaEstanteria["estanteria"] == numEstanteria)
                    //    {
                    //        estanterias.FillRectangle(Brushes.Green, x, y, 30, 42);
                    //    }
                    //}


                    // OPCION 2

                    DataRow fila = objEst.GetFila(codigo, numEstanteria);
                    if (fila != null)
                    {
                        estanterias.FillRectangle(Brushes.Green, x, y, 30, 42);
                    }
                    else
                    {
                        estanterias.DrawRectangle(Pens.Black, x, y, 32, 42);
                    }

                    estanterias.DrawString(numEstanteria.ToString(), fuente, Brushes.Black, x, y);
                    numEstanteria++;

                }
            }
        }
    }
}
