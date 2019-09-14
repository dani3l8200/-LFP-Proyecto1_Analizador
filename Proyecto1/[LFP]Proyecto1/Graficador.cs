using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _LFP_Proyecto1.Modal;
namespace _LFP_Proyecto1
{
    class Graficador
    {
        String ruta;
        public String rpng;
      
        Grafica Grafica; 
        public Graficador()
        {
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }
        private void generatedot(String rdot, String rpng)
        {
            Grafica = new Grafica();
            System.IO.File.WriteAllText(rdot,Grafica.sbG.ToString());
            String comanDot = "dot.exe -Tpng " +rdot+ " -o " + rpng + " ";
            var comand = string.Format(comanDot);
            var procStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C"  + comand);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStart;
            proc.Start();
            proc.WaitForExit(); 
        }
        private void generatedot1(String rdot, String rpng)
        {
            Grafica = new Grafica();
            System.IO.File.WriteAllText(rdot, Grafica.sbG.ToString());
            String comanDot = "dot.exe -T pdf " + rdot + " -o " + rpng + " ";
            var comand = string.Format(comanDot);
            var procStart = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comand);
            var proc = new System.Diagnostics.Process();
            proc.StartInfo = procStart;
            proc.Start();
            proc.WaitForExit();
        }
        public void graficar1(Grafica data)
        {
            data.GetGraphString();
            String rdot = ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\ReporteGrafica.dot";
            String rpng = ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\ReporteGrafica.pdf";
           
            this.generatedot1(rdot, rpng);
        }

        public void graficar(Grafica data)
        {
            data.GetGraphString();
            String rdot = ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\imagen.dot";
            rpng = ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\imagen.png";
        
            this.generatedot(rdot, rpng);
            var intento = data.GetGraphString();
        }
        public void abrirgrafo(PictureBox mypicture)
        {
            Grafica data = new Grafica();
           String  myaux= @"\[LFP]Proyecto#1\Imagenes_Ricolinas\Proyecto1\imagen.png";
               
               try
                {
                
                    mypicture.Image = Image.FromFile(ruta + myaux);
                
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            
            
            

        }
        public void abrirpdf()
        {
            if (File.Exists(ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\ReporteGrafica.pdf"))
            {
                try
                {
                    System.Diagnostics.Process.Start(ruta + "\\[LFP]Proyecto#1\\Imagenes_Ricolinas\\Proyecto1\\ReporteGrafica.pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("error");
            }

        }
      
    }
}
