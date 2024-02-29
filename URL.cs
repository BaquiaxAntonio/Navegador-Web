using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navegador_Web
{
    internal class URL
    {
        string url;
        int veces;
        DateTime fecha;

        public string Url { get => url; set => url = value; }
        public int Veces { get => veces; set => veces = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
    }
}
