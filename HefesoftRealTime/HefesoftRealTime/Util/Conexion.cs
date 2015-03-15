using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HefesoftRealTime.Util
{
    public class Conexion : IEntidadBase
    {

        public bool? Activo { get; set; }
        public Hefesoft.Standard.Enumeradores.Estado_Entidad Estado_Entidad { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string nombreTabla { get; set; }

        public bool generarIdentificador { get; set; }


        public string idConexion { get; set; }
    }
}
