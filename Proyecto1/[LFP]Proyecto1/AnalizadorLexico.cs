using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _LFP_Proyecto1
{
    class AnalizadorLexico
    {
        private LinkedList<Tokens> listaTokens = new LinkedList<Tokens>();
        public RichTextBox richTextBox; 
        private int estado;
        String auxlex;
        private int columna = 0;
        private int fila = 1;
        private char letra;
        private int contID = 0;
        private int contador = 0;
        

        private void ColorTokens(Object objecto, Color color)
        {
            String colortexto = objecto.ToString();
            
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(colortexto);
            richTextBox.SelectionColor = richTextBox.ForeColor;
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = richTextBox.SelectionLength + contador++;

        }


        public LinkedList<Tokens> analisisLex(String entra, RichTextBox richText)
        {
            entra = entra + "#";
            estado = 0;
            richTextBox = richText;
            richTextBox.Text = "";
            auxlex = "";
           
            Char c;
            int codigoascii = 0;
            for (int i = 0; i < entra.Length; i++)
            {
                c = entra.ElementAt(i);
                letra = entra[i];
                codigoascii = letra;

                switch (estado)
                {
                    case 0:
                        if (letra == '\t' || letra == '\r' || letra == '\b' || letra == '\f' || letra == ' ')
                        {
                            ColorTokens(letra, Color.Black);

                            estado = 0;
                        }
                        else if (letra == '\n')
                        {

                            ColorTokens(letra, Color.Black);
                            fila += 1;
                            estado = 0;
                        }
                        else if (letra.Equals('G'))
                        {

                            auxlex += letra;
                            estado = 1;
                            columna++;

                        } else if (letra.Equals('"'))
                        {
                            auxlex += letra;
                            estado = 8;
                            columna++;
                        }
                        else if (letra.Equals('%'))
                        {

                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Black);
                            agregartoken(Tokens.Tipo.Porcentaje);
                        }
                        else if (letra.Equals('{'))
                        {
                            auxlex += letra;
                            ColorTokens(auxlex, Color.Red);
                            columna++;
                            agregartoken(Tokens.Tipo.Simbolo_LlaveIzq);

                        }
                        else if (Char.IsDigit(c))
                        {
                            estado = 10;
                            auxlex += c;
                            columna++;

                        }
                        else if (letra.Equals('}'))
                        {
                            auxlex += letra;
                            ColorTokens(auxlex, Color.Red);
                            columna++;
                            agregartoken(Tokens.Tipo.Simbolo_LlaveDerech);
                        }
                        else if (letra.Equals(';'))
                        {
                            auxlex += letra;
                            ColorTokens(auxlex, Color.DarkOrange);
                            columna++;
                            agregartoken(Tokens.Tipo.Simbolo_PuntoComa);
                        }
                        else if (letra.Equals(':'))
                        {
                            auxlex += letra;
                            ColorTokens(auxlex, Color.Black);
                            columna++;
                            agregartoken(Tokens.Tipo.Simbolo_DosPuntos);
                        }
                        else if (letra.Equals('N'))
                        {
                            auxlex += letra;
                            estado = 11;
                            columna++;
                        }
                        else if (letra.Equals('P'))
                        {
                            auxlex += letra;
                            estado = 16;
                            columna++;
                        }
                        else if (letra.Equals('B'))
                        {
                            auxlex += letra;
                            estado = 26;
                            columna++;
                        }
                        else if (letra.Equals('C'))
                        {
                            auxlex += letra;
                            estado = 32;
                            columna++;
                        } else if (letra.Equals('S'))
                        {
                            auxlex += letra;
                            estado = 41;
                            columna++;
                        }
                        else
                        {
                            if (letra.CompareTo('#') == 0 && i == entra.Length - 1 && c.CompareTo('#') == 0)
                            {


                            }
                            else
                            {
                                ColorTokens(auxlex + letra, Color.Magenta);
                                Console.WriteLine("Error desconocido" + " " + letra);
                                auxlex = "";
                                estado = 0;
                            }
                        }
                        break;
                    case 1:
                        if (letra.Equals('r'))
                        {

                            auxlex += letra;
                            estado = 2;
                            columna++;

                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;


                        }
                        break;
                    case 2:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            estado = 3;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;

                        }
                        break;
                    case 3:
                        if (letra.Equals('f'))
                        {

                            auxlex += letra;
                            estado = 4;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 4:
                        if (letra.Equals('i'))
                        {

                            auxlex += letra;
                            estado = 5;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 5:
                        if (letra.Equals('c'))
                        {

                            auxlex += letra;
                            estado = 6;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 6:
                        if (letra.Equals('a'))
                        {

                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Grafica);

                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;

                        }
                        break;
                    case 8:
                        if (codigoascii == 32 || (codigoascii >= 65 && codigoascii <= 90) || (codigoascii >= 48 && codigoascii <= 57)
                            || (codigoascii >= 97 && codigoascii <= 122) || (codigoascii >= 123 && codigoascii <= 125) || (codigoascii >= 58 &&
                            codigoascii <= 64) || codigoascii == 33 || (codigoascii >= 35 && codigoascii <= 47) || (codigoascii >= 91 && codigoascii <= 96) ||
                            letra == '\t' || letra == '\r' || letra == '\b' || letra == '\f' || letra == ' ' || letra == '\n')
                        {
                            auxlex += letra;
                            columna++;
                            estado = 9;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 9:
                        if (codigoascii == 32 || (codigoascii >= 65 && codigoascii <= 90) || (codigoascii >= 48 && codigoascii <= 57)
                           || (codigoascii >= 97 && codigoascii <= 122) || (codigoascii >= 123 && codigoascii <= 125) || (codigoascii >= 58 &&
                           codigoascii <= 64) || codigoascii == 33 || (codigoascii >= 35 && codigoascii <= 47) ||(codigoascii >=91 && codigoascii <=96)||
                           letra == '\t' || letra == '\r' || letra == '\b' || letra == '\f' || letra == ' ' || letra == '\n')
                        {
                            auxlex += letra;
                            columna++;
                            estado = 9;
                        } else if (letra.Equals('"'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Goldenrod);
                            agregartoken(Tokens.Tipo.Cadena);
                        }
                        break;
                    case 10:
                        if (Char.IsDigit(c))
                        {
                            estado = 10;
                            auxlex += c;
                            columna++;


                        }

                        else
                        {
                            ColorTokens(auxlex, Color.GreenYellow);
                            agregartoken(Tokens.Tipo.Numeros);
                            i -= 1;

                        }
                        break;
                    case 11:
                        if (letra.Equals('o'))
                        {
                            auxlex += letra;
                            estado = 12;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 12:
                        if (letra.Equals('m'))
                        {
                            auxlex += letra;
                            estado = 13;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 13:
                        if (letra.Equals('b'))
                        {
                            auxlex += letra;
                            estado = 14;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 14:
                        if (letra.Equals('r'))
                        {
                            auxlex += letra;
                            estado = 15;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 15:
                        if (letra.Equals('e'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Nombre);
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 16:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            estado = 17;
                            columna++;
                        }
                        else if (letra.Equals('o'))
                        {
                            auxlex += letra;
                            estado = 19;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 17:
                        if (letra.Equals('i'))
                        {
                            auxlex += letra;
                            estado = 18;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 18:
                        if (letra.Equals('s'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Pais);
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 19:
                        if (letra.Equals('b'))
                        {
                            auxlex += letra;
                            estado = 20;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 20:
                        if (letra.Equals('l'))
                        {
                            auxlex += letra;
                            estado = 21;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 21:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            estado = 22;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 22:
                        if (letra.Equals('c'))
                        {
                            auxlex += letra;
                            estado = 23;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 23:
                        if (letra.Equals('i'))
                        {
                            auxlex += letra;
                            estado = 24;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 24:
                        if (letra.Equals('o'))
                        {
                            auxlex += letra;
                            estado = 25;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 25:
                        if (letra.Equals('n'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Poblacion);
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 26:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            estado = 27;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 27:
                        if (letra.Equals('n'))
                        {
                            auxlex += letra;
                            estado = 28;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 28:
                        if (letra.Equals('d'))
                        {
                            auxlex += letra;
                            estado = 29;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 29:
                        if (letra.Equals('e'))
                        {
                            auxlex += letra;
                            estado = 30;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 30:
                        if (letra.Equals('r'))
                        {
                            auxlex += letra;
                            estado = 31;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 31:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex,Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Bandera);
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 32:
                        if (letra.Equals('o'))
                        {
                            auxlex += letra;
                            estado = 33;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 33:
                        if (letra.Equals('n'))
                        {
                            auxlex += letra;
                            estado = 34;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 34:
                        if (letra.Equals('t'))
                        {
                            auxlex += letra;
                            estado = 35;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 35:
                        if (letra.Equals('i'))
                        {
                            auxlex += letra;
                            estado = 36;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 36:
                        if (letra.Equals('n'))
                        {
                            auxlex += letra;
                            estado = 37;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 37:
                        if (letra.Equals('e'))
                        {
                            auxlex += letra;
                            estado = 38;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 38:
                        if (letra.Equals('n'))
                        {
                            auxlex += letra;
                            estado = 39;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 39:
                        if (letra.Equals('t'))
                        {
                            auxlex += letra;
                            estado = 40;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 40:
                        if (letra.Equals('e'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Continente);
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 41:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            estado = 42;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 42:
                        if (letra.Equals('t'))
                        {
                            auxlex += letra;
                            estado = 43;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 43:
                        if (letra.Equals('u'))
                        {
                            auxlex += letra;
                            estado = 44;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 44:
                        if (letra.Equals('r')){
                            auxlex += letra;
                            estado = 45;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 45:
                        if (letra.Equals('a'))
                        {
                            auxlex += letra;
                            estado = 46;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 46:
                        if (letra.Equals('c'))
                        {
                            auxlex += letra;
                            estado = 47;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 47:
                        if (letra.Equals('i'))
                        {
                            auxlex += letra;
                            estado = 48;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 48:
                        if (letra.Equals('o'))
                        {
                            auxlex += letra;
                            estado = 49;
                            columna++;
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                    case 49:
                        if (letra.Equals('n'))
                        {
                            auxlex += letra;
                            columna++;
                            ColorTokens(auxlex, Color.Blue);
                            agregartoken(Tokens.Tipo.Palabra_Reservada_Saturacion);
                        }
                        else
                        {
                            ColorTokens(auxlex + letra, Color.Magenta);
                            Console.WriteLine("Error desconocido" + " " + letra);
                            auxlex = "";
                            estado = 0;
                        }
                        break;
                }
            }
            return listaTokens; 
        }
       
        public void agregartoken(Tokens.Tipo tipo)
        {
            
            listaTokens.AddLast(new Tokens(tipo, auxlex, fila,columna));
            auxlex = "";
            estado = 0;


        }
        public int generarID()
        {
            return contID += 1; 
        }
        public void imprimirToken(LinkedList<Tokens> tokens)
        {
            foreach (Tokens item in tokens)
            {
                Console.WriteLine(generarID() + "----" + item.GetTipo() + "-----------" + item.GetVal() + "--------------" + item.GetFila() + "----------------"+
                    item.GetColumna());
            }
        }


    }
}
