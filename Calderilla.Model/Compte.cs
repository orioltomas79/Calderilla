using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calderilla.Model
{
    public class Compte
    {
        public String rutaExtractSabadell { get; set; }
        public String rutaInformesExcel { get; set; }
        public String rutaInformesPdf { get; set; }
        public List<Moviment> moviments { get; set; }
        public List<PatrimoniMes> patrimoniMes { get; set; }

        public Dictionary<String, Int32> DonaCategoriesConcepte(String Concepte)
        {
            Dictionary<String, Int32> diccionari = new Dictionary<String, Int32>();

            foreach (var line in this.moviments.Where(r => r.Concepte != null && r.Concepte.Equals(Concepte) && r.Categoria != null)
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

        public Dictionary<String, Int32> DonaCategories()
        {
            Dictionary<String, Int32> diccionari = new Dictionary<String, Int32>();

            foreach (var line in this.moviments.Where(r => r.Categoria != null)
            .GroupBy(r => r.Categoria)
            .Select(group => new
            {
                Categoria = group.Key,
                Count = group.Count()
            })
            .OrderByDescending(x => x.Count))
            {
                diccionari.Add(line.Categoria, line.Count);
            }

            return diccionari;

        }


    }

}

