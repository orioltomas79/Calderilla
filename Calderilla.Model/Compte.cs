using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calderilla.Model
{
    public class Compte
    {
        public String nom { get; set; }
        public List<Registre> registres { get; set; }

        public Dictionary<String, Int32> DonaCategoriesConcepte(String Concepte)
        {
            Dictionary<String, Int32> diccionari = new Dictionary<String, Int32>();

            foreach (var line in this.registres.Where(r => r.Concepte.Equals(Concepte) && r.Categoria != null)
            .GroupBy(r => r.Categoria)
            .Select(group => new
            {
                Categoria = group.Key,
                Count = group.Count()
            })
            .OrderBy(x => x.Categoria))
            {
                diccionari.Add(line.Categoria, line.Count);
            }

            return diccionari;

        }
    }

}

