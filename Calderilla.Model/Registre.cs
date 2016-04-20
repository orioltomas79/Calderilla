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
        public Boolean Deshabilita { get; set; }
        public Boolean Revisat { get; set; }
        public String Comentari { get; set; }

        public string GetString()
        {

            String str;
            str = "DATA\n" + Data + "\n\n";
            str += "CONCEPTE\n" + Concepte + "\n\n";
            str += "IMPORT\n" + Import + "\n\n";
            str += "CATEGORIA\n" + Categoria + "\n\n";
            str += "DESHABILITAT\n" + Deshabilita + "\n\n";
            str += "REVISAT\n" + Revisat + "\n\n";
            str += "COMENTARI\n" + Comentari + "\n\n";

            return str;

        }

    }

    



}
