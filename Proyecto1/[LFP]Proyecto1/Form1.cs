using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * References that used for creating graph
 * https://graphviz.gitlab.io/_pages/pdf/dotguide.pdf
 * http://www.graphviz.org/doc/info/attrs.html#d:_background
 * https://www.graphviz.org/doc/info/lang.html
 * https://www.graphviz.org/doc/info/shapes.html
 */
namespace _LFP_Proyecto1
{

    public partial class Form1 : Form
    {
        private RichTextBox[] richTextBoxes = new RichTextBox[100];
        private int contador = 1;
        private int contadorPes = 0;
        private string ruta;
        private bool textChange = false;
        private int idTk = 0;
        private String fileDirectoryFocus;
        private TabPage tabPage;
        private OpenFileDialog fileDialog = new OpenFileDialog();
        private SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
        Graficador graficador = new Graficador();
        private int generarIdTk()
        {
            return idTk += 1;
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void htmlTTk(LinkedList<Tokens> tokens)
        {


            using (System.IO.FileStream fs = new FileStream("Tokens.html", FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<body bgcolor=#1DF1F9>");
                    w.WriteLine("<Center><h1>ANALIZADOR LEXICO</h1></Center>");
                    w.WriteLine("<Center><TABLE border = 3.5 bordercolor = black bgcolor = #B4F91D></Center>");
                    w.WriteLine("<TR>");
                    w.WriteLine("<Center><TH COLSPAN = 4 > Tabla de Tokens Validos </TH></Center>");
                    w.WriteLine("</TR>");
                    w.WriteLine("<TR>");
                    w.WriteLine("<TH> ID </TH>");
                    w.WriteLine("<TH> Token </TH>");
                    w.WriteLine("<TH> Lexema</TH>");
                    w.WriteLine("<TH> Fila</TH>");
                    w.WriteLine("<TH> Columna </TH>");
                    w.WriteLine("</TR>");
                    foreach (Tokens item in tokens)
                    {
                        w.WriteLine("<TR>");
                        w.WriteLine("<TH>" + generarIdTk() + "</TH>");
                        w.WriteLine("<TH>" + item.GetTipo().ToString() + "</TH>");
                        w.WriteLine("<TH>" + item.GetVal().ToString() + "</TH>");
                        w.WriteLine("<TH>" + item.GetFila().ToString() + "</TH>");
                        w.WriteLine("<TH>" + item.GetColumna() + "</TH>");
                        w.WriteLine("</TR>");
                    }
                    
                }
            }
            Process.Start("Tokens.html");   

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                String path;
                String entradaanalisis = richTextBoxes[tabControl1.SelectedIndex].Text;
                //String entradaanalisis = File.ReadAllText(@"C:\Users\User\Desktop\[LFP]Proyecto#1\[LFP]Proyecto1\ArchivoEntrada.org");
                AnalizadorLexico analizadorLexico = new AnalizadorLexico();
                LinkedList<Tokens> tokens = analizadorLexico.analisisLex(entradaanalisis, richTextBoxes[tabControl1.SelectedIndex]);
                analizadorLexico.imprimirToken(tokens);
                //htmlTTk(tokens);
                var data = new sintactico_analisis().Process(tokens);
                graficador = new Graficador();
                graficador.graficar(data);
                graficador.graficar1(data);
                var bestPais = data.GetBestPais();
                label3.Text = "PAIS SELECCIONADO";
                label1.Text = "Pais:"+" "+bestPais.Nombre.ToString().Replace('"', ' ').Trim();
                label2.Text = "Poblacion:" + " " + bestPais.Poblacion.ToString();
                path = bestPais.Bandera.ToString().Replace('"', ' ').Trim();
                pictureBox2.Image = Image.FromFile(path);
                pictureBox2.Visible = true;
                label2.Visible = true;
                label1.Visible = true;
                label3.Visible = true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

       
        private void CrearPestañaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPage = new TabPage("pestaña" + contador++);
            RichTextBox richTextBox1 = new RichTextBox();
            richTextBox1.Dock = DockStyle.Fill;
            tabPage.Controls.Add(richTextBox1);
            tabControl1.Controls.Add(tabPage);
            Console.WriteLine(tabControl1.SelectedIndex + 1);
            richTextBoxes[tabControl1.SelectedIndex + contadorPes++] = richTextBox1;
            MessageBox.Show("Se añadio una nueva pestaña :)", "Analizador Lexico", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileDialog.InitialDirectory = "C:\\Users\\User\\Desktop";
            fileDialog.Filter = "Documento de Texto (*.ORG) |*.ORG";
            fileDialog.FilterIndex = 0;
            fileDialog.RestoreDirectory = true; 
            fileDialog.Multiselect = false;
            try
            {
                if (fileDialog.ShowDialog() == DialogResult.OK && !String.IsNullOrEmpty(fileDialog.FileName))
                {
                    string direccion = fileDialog.FileName;
                    tabPage.Text = fileDialog.SafeFileName.ToString().Trim('.', 'O', 'R', 'G');
                    Process proceso = new Process();
                    proceso.StartInfo.FileName = direccion;
                    proceso.Start();
                    try
                    {
                        TextReader textReader = new StreamReader(direccion);

                        richTextBoxes[tabControl1.SelectedIndex].Text = textReader.ReadToEnd();
                        fileDirectoryFocus = fileDialog.FileName;
                        this.Text = "Form1 - " + Path.GetFileNameWithoutExtension(fileDirectoryFocus);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error de Carga", "Analizador Lexico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.fileDirectoryFocus))
            {
                StreamWriter outputFile = null;
                try
                {
                    using (outputFile = new StreamWriter(fileDirectoryFocus))
                    {
                        outputFile.Write(richTextBoxes[tabControl1.SelectedIndex].Text);
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    try
                    {
                        if (outputFile != null)
                        {
                            outputFile.Close();
                            this.Text = "Form1 - " + Path.GetFileNameWithoutExtension(fileDirectoryFocus);
                            this.textChange = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void GuardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFileDialog1 = new SaveFileDialog()
            {
                //Establece el directorio inicial
                InitialDirectory = "C:\\Users\\User\\Desktop",
                //Se crea y agrega un filtro para los archivos
                Filter = "Documento de Texto (*.ORG) |*.ORG",
                FilterIndex = 0,
                //Deja el directorio en el directorio en el que se cerro la 
                //última vez que se abrio el cuadro de dialogo durante 
                //el tiempo de ejecuacion
                RestoreDirectory = true,
                //Muestra el dialogo
            };
            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.fileDirectoryFocus = SaveFileDialog1.FileName;
                if (SaveFileDialog1.FileName != "")
                {
                    StreamWriter outputFile = null;
                    try
                    {
                        using (outputFile = new StreamWriter(SaveFileDialog1.FileName))
                        {
                            outputFile.Write(richTextBoxes[tabControl1.SelectedIndex].Text);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        try
                        {
                            if (outputFile != null)
                            {
                                outputFile.Close();
                                this.Text = "Form1 - " + Path.GetFileNameWithoutExtension(fileDirectoryFocus);
                                this.textChange = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Graficador graficador = new Graficador();
            graficador.abrirpdf();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            FileStream fs = File.OpenRead(graficador.rpng);
            pictureBox1.Image = Image.FromStream(fs);
            fs.Close();
            fs.Dispose();
        }

       
    }
}
