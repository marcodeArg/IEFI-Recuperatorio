using System;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                objEst = new Estanterias();
                objProd = new Productos();
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
            DataTable tablaEstanterias = new DataTable();
            DataTable tablaProductos = new DataTable();

            tablaEstanterias = objEst.GetData();
            tablaProductos = objProd.GetData();


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


                Dibujar();

            }
            else
            {
                MessageBox.Show("No existe ningun producto con esa id", "Advertencia", MessageBoxButtons.OK);
            }



            
        }

        public void Dibujar()
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

                    estanterias.DrawRectangle(Pens.Black, x, y, 60, 40);

                    estanterias.DrawString(numEstanteria.ToString(), fuente, Brushes.Black, x, y);

                    numEstanteria++;

                }
            }
        }
    }
}
