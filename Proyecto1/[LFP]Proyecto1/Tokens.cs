using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    class Tokens
    {
       
        public enum Tipo
        {
            Palabra_Reservada_Grafica,
            Simbolo_DosPuntos,
            Simbolo_LlaveIzq,
            Simbolo_LlaveDerech,
            Palabra_Reservada_Nombre, 
            Cadena,
            Simbolo_PuntoComa,
            Palabra_Reservada_Pais,
            Palabra_Reservada_Continente,
            Palabra_Reservada_Poblacion,
            Numeros,
            Palabra_Reservada_Saturacion,
            Porcentaje,
            Palabra_Reservada_Bandera
        }
        private Tipo tipo;
        private String valor;
        private int fila;
        private int columna;

        public Tokens(Tipo tipo, String valor, int fila, int columna)
        {
            this.tipo = tipo;
            this.valor = valor;
            this.fila = fila;
            this.columna = columna; 
        }

                public String GetVal()
        {
            return valor; 
        }
                    public int GetFila()
        {
            return fila; 
        }

                        public int GetColumna()
        {
            return columna; 
        }
                            public String GetTipo()
        {
            switch (tipo)
            {
                case Tipo.Palabra_Reservada_Grafica:
                    return "Palabra Reservada Grafica";
                case Tipo.Simbolo_DosPuntos:
                    return "Simbolo DosPuntos";
                case Tipo.Simbolo_LlaveIzq:
                    return "Simbolo Llave Izquierda";
                case Tipo.Palabra_Reservada_Nombre:
                    return "Palabra Reservada Nombre";
                case Tipo.Cadena:
                    return "Cadena";
                case Tipo.Simbolo_PuntoComa:
                    return "Simbolo Punto Coma";
                case Tipo.Palabra_Reservada_Pais:
                    return "Palabra Reservada Pais";
                case Tipo.Palabra_Reservada_Continente:
                    return "Palabra Reservada Continente";
                case Tipo.Simbolo_LlaveDerech:
                    return "Simbolo llave derecha";
                case Tipo.Palabra_Reservada_Poblacion:
                    return "Palabra Reservada Poblacion"; 
                case Tipo.Numeros:
                    return "Numeros";
                case Tipo.Palabra_Reservada_Saturacion:
                    return "Palabra Reservada Saturacion";
                case Tipo.Porcentaje:
                    return "Simbolo Porcentaje Poblacion";
                case Tipo.Palabra_Reservada_Bandera:
                    return "Palabra Reservada Bandera";
                default:
                    return "Token Desconocido";
            }
        }

    }
}
