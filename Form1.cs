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
        List<URL> urls = new List<URL>();
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
            FileStream flujo = new FileStream(NombreArchivo, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter escritor = new StreamWriter(flujo);
            foreach (URL url in urls)
            {
                escritor.WriteLine(url.Url);
                escritor.WriteLine(url.Veces);
                escritor.WriteLine(url.Fecha);
            }
            escritor.Close();
        }

        private void leer()
        {
            string fileName = @"C:\Users\HP\source\repos\Navegador Web\Historial.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                URL url = new URL();
                url.Url = reader.ReadLine();
                url.Veces= Convert.ToInt32(reader.ReadLine());
                url.Fecha= Convert.ToDateTime(reader.ReadLine());
                urls.Add(url);
            }
            reader.Close();
            comboBox1.DisplayMember = "url";
            comboBox1.DataSource = urls;
            comboBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                string url = comboBox1.Text.ToString();

                if (url.Contains(".") || url.Contains("/") || url.Contains(":"))
                {
                    if (url.Contains("https"))
                        webView21.CoreWebView2.Navigate(url);
                    else
                    {
                        url = "https://" + url;
                        webView21.CoreWebView2.Navigate(url);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        url = "https://www.google.com/search?q=" + url;
                        webView21.CoreWebView2.Navigate(url);
                    }
                }

                URL urlExiste = urls.Find(u => u.Url == url);
                if (urlExiste == null)
                {
                    URL urlNueva = new URL();
                    urlNueva.Url = url;
                    urlNueva.Veces = 1;
                    urlNueva.Fecha = DateTime.Now;
                    urls.Add(urlNueva);

                    // Guardar el historial en el archivo
                    Guardar(@"C:\Users\HP\source\repos\Navegador Web\Historial.txt", comboBox1.Text);

                    webView21.CoreWebView2.Navigate(url);
                }
                else
                {
                    urlExiste.Veces++;
                    urlExiste.Fecha = DateTime.Now;

                    // Guardar el historial en el archivo
                    Guardar(@"C:\Users\HP\source\repos\Navegador Web\Historial.txt",comboBox1.Text);

                    webView21.CoreWebView2.Navigate(urlExiste.Url);
                }

            }
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
