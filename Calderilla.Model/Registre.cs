using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calderilla.Model
{
    public class Registre
    {
        public DateTime Data { get; set; }
        public String Concepte { get; set; }
        public Decimal Import { get; set; }
        public String Categoria { get; set; }
    }
}
