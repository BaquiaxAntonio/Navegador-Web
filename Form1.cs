using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            String url=comboBox1.Text.ToString();

            if (!(url.Contains("https://")) && url.Contains("."))
            {
                url = "https://" + url;
                webView21.CoreWebView2.Navigate(url);

            }
            if(!(url.Contains("https://"))&& !(url.Contains(".")))
            {
                webView21.CoreWebView2.Navigate(("https://www.google.com/search?q="+url));
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
            comboBox1.SelectedIndex = 0;
            //webBrowser1.GoHome();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
