using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TOOLS_CBA
{
    internal class Estanterias
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;


        public Estanterias()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Estanterias";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[2];
            dc[0] = tabla.Columns["producto"];
            dc[1] = tabla.Columns["estanteria"];
            tabla.PrimaryKey = dc;

        }

        public DataRow GetFila(int pk1, int pk2)
        {
            Object[] datos = new Object[2];
            datos[0] = pk1;
            datos[1] = pk2;
            return tabla.Rows.Find(datos);
        }

        public DataTable GetData()
        {
            return tabla;
        }
    }
}
