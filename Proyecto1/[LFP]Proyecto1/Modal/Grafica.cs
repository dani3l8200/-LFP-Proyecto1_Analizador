using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1.Modal
{
    /// <summary>
    /// Graph
    /// </summary>
    class Grafica
    {
        String ruta;
        public static  StringBuilder sbG;

        static int counter = 0;
        static string getUniqueName()
        {
            return "node" + ++counter;
        }
        public string Nombre { get; set; }

        public List<Continente> Continente { get; set; }

        public Grafica()
        {
            this.Continente = new List<Modal.Continente>();
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        private void generatedot(String rdot, String rpng)
        {
            System.IO.File.WriteAllText(rdot, sbG.ToString());
            String comanDot = "dot.exe -Tpng" + rdot + "-o" + rpng + " ";
            var comand = string.Format(comanDot);
            var procStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comand);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStart;
            proc.Start();
            proc.WaitForExit();
        }
        
        public string GetGraphString()
        {

            sbG = new StringBuilder("digraph { G [shape=Mdiamond,];");
            String rdot = ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\imagen.dot";
            String rpng = ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\imagen.png";
            var rootNode = "G";
            sbG.Append($"{rootNode} [label =\"{Nombre.Replace("\"", "")}\"];");

            foreach (var cont in this.Continente)
            {
                var continenteNode = getUniqueName();
                string continenteNombre = cont.Nombre.Replace("\"", "");
                int totalSaturacion = cont.Paiss.Sum(x => int.Parse(x.Saturacion));
                int contSaturacion = totalSaturacion / cont.Paiss.Count;

                sbG.AppendFormat("{0} [shape=record, style=filled, fillcolor={5}, label = \"{3}{1} | {2}{4}\"]", continenteNode, continenteNombre, contSaturacion, "{", "}", GetSaturacionColor(contSaturacion));
                sbG.Append($"{rootNode} -> {continenteNode}; ");

                foreach (var pais in cont.Paiss)
                {
                    var paisNode = getUniqueName();
                    string paisNombre = pais.Nombre.Replace("\"", "");
                    sbG.AppendFormat("{0} [shape=record, style=filled, fillcolor={5}, label = \"{3}{1} | {2}{4}\"]", paisNode, paisNombre, pais.Saturacion, "{", "}", GetSaturacionColor(int.Parse(pais.Saturacion)));
                    sbG.Append($"{continenteNode} -> {paisNode}; ");
                }

            }
            //sbG.Append(node.GetGraphNodeString());

            sbG.Append("}");
            File.WriteAllText(rdot, sbG.ToString());
            this.generatedot(rdot, rpng);
            return sbG.ToString();
        }

        string GetSaturacionColor(int sat)
        {
            if (sat > 75)
                return "red";
            if (sat > 60)
                return "orange";
            if (sat > 45)
                return "yellow";
            if (sat > 30)
                return "green";
            if (sat > 15)
                return "blue";
            return "white";
        }
        internal Pais GetBestPais()
        {
            int minSat = int.MaxValue;
            int foundCount = 0;
            List<Continente> sameBestSat = new List<Modal.Continente>();
            Pais bestPais = null;
            foreach (var cont in this.Continente)
                foreach (var pais in cont.Paiss)
                {
                    if (pais.SaturacionInt <= minSat)
                    {
                        foundCount++;
                        bestPais = pais;
                        if (!sameBestSat.Contains(cont))
                            sameBestSat.Add(cont);
                    }
                }

            if (sameBestSat.Count == 1)
                return bestPais;

            int minSatCont = int.MaxValue;
            
            Continente bestCont = null;
            foreach (var cont in sameBestSat)

                if (cont.Saturacion <= minSatCont)

                    bestCont = cont; 
             
                return bestCont.Paiss.FirstOrDefault(x => x.SaturacionInt == bestCont.Paiss.Min(y => y.SaturacionInt));
        }
    }
}
