using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1.Modal
{
    /// <summary>
    /// Continent
    /// </summary>
    class Continente
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Nombre { get; set; }

        public List<Pais> Paiss { get; set; }

        public int Saturacion
        {
            get
            {
                int totalSaturacion = this.Paiss.Sum(x => int.Parse(x.Saturacion));
                return totalSaturacion / this.Paiss.Count;
            }
        }
        public Continente()
        {
            this.Paiss = new List<Modal.Pais>();
        }

        //internal string GetGraphNodeString()
        //{
        //    StringBuilder sbG = new StringBuilder(Nombre);

        //    foreach (var node in this.Paiss)
        //        sbG.Append(node.GetGraphNodeString());

        //    sbG.Append("}");
        //    return sbG.ToString();
        //}
    }

}
