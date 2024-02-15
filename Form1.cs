using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Navegador_Web
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form_Resize);
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            webView21.Size = this.ClientSize - new System.Drawing.Size(webView21.Location);
            buttonBuscar.Left = this.ClientSize.Width - buttonBuscar.Width;
            comboBox1.Width = buttonBuscar.Left - comboBox1.Left;
        }

        private void Guardar(string NombreArchivo, string texto)
        {
            FileStream flujo = new FileStream(NombreArchivo, FileMode.Append, FileAccess.Write);
            StreamWriter escritor = new StreamWriter(flujo);
            escritor.WriteLine(texto);
            escritor.Close();
        }

        private void leer()
        {
            string fileName = @"C:\Users\HP\source\repos\Navegador Web\Historial.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                string Texto = reader.ReadLine();
                comboBox1.Items.Add(Texto);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String urlIngresado = comboBox1.Text;

            if (!(urlIngresado.StartsWith("https://")))
            {
                if (!(urlIngresado.Contains(".")))
                {
                    urlIngresado = "https://www.google.com/search?q=" + Uri.EscapeDataString(urlIngresado);
                }
                else
                {
                    urlIngresado = "https://" + urlIngresado + "/";
                }
                comboBox1.Text = urlIngresado;
            }
            webView21.CoreWebView2.Navigate(urlIngresado);
            Guardar(@"C:\Users\HP\source\repos\Navegador Web\Historial.txt", comboBox1.Text);
            comboBox1.Items.Clear();
            leer();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void navegadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView21.CoreWebView2.Navigate("https://www.google.com.gt/?hl=es");
        }

        private void paginaSiguienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView21.GoForward();
        }

        private void atrasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webView21.GoBack();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            leer();
 
            //webBrowser1.GoHome();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
