using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1.Modal
{


    /// <summary>
    /// Country
    /// </summary>
    class Pais
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Population
        /// </summary>
        public long Poblacion { get; set; }

        /// <summary>
        /// Saturation
        /// </summary>
        public string Saturacion { get; set; }

        public int SaturacionInt { get { return Convert.ToInt32(Saturacion); } }
        /// <summary>
        /// Flag
        /// </summary>
        public string Bandera { get; set; }
    }
}
