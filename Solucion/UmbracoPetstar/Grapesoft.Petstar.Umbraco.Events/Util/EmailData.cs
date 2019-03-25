using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grapesoft.Petstar.Events.Util
{
    public class EmailData
    {
        public string nombre { get; set; }
        public string email { get; set; }
        public string ciudad { get; set; }
        public string rndTipoT { get; set; }
        public string telefono { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
        public string emailsSend { get; set; }
        public string asuntoStr { get; set; }

        

    }

    public class EmailDataSus
    {
        public string nombre { get; set; }
        public string email { get; set; }
        public string emailsSend { get; set; }
        public string asuntoStr { get; set; }



    }
}