using _LFP_Proyecto1.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _LFP_Proyecto1
{
    class sintactico_analisis
    {
        private bool
            is_graph = false,
            is_graph_name = false,
            is_continente = false,
            is_continente_name = false,
            is_pais = false,
            is_pais_name = false,
            is_poblacion = false,
            is_saturacion = false,
            is_bandera = false;

        //is_description = false,
        //is_image = false,
        //is_event = false,
        //is_year = false,
        //is_month = false,
        //is_day = false;

        List<string> punctuator;

        private Grafica grafica_plan;
        private Continente continente_actual;
        private Pais pais_actual;

        public sintactico_analisis()
        {
            punctuator = new List<string>();
            punctuator.Add("[");
            punctuator.Add("]");
            punctuator.Add("{");
            punctuator.Add("}");
            punctuator.Add("<");
            punctuator.Add(">");
            punctuator.Add(":");
            punctuator.Add(";");
            punctuator.Add("(");
            punctuator.Add(")");
        }

        public Grafica Process(LinkedList<Tokens> analisises)
        {
            grafica_plan = new Modal.Grafica();
            is_graph = true;
            var head = analisises.First;
            while (head != null)
            {
                while (!IsLiteral(head.Value))
                {
                    head = head.Next;
                    if (head == null)
                        return grafica_plan;
                }
                AggregateToken(head.Value);

                head = head.Next;
            }
            return grafica_plan;
        }

        private void AggregateToken(Tokens value)
        {
            string literal = value.GetVal();
            if (is_graph_name)
            {
                is_graph = false;
                is_graph_name = false;
                grafica_plan.Nombre = literal;
            }
            else if (is_continente_name)
            {
                is_continente = false;
                is_continente_name = false;
                grafica_plan.Continente.Add(continente_actual);
                continente_actual.Nombre = literal;
            }
            else if (is_pais_name)
            {
                is_pais = false;
                is_pais_name = false;
                continente_actual.Paiss.Add(pais_actual);
                pais_actual.Nombre = literal;
            }
            else if (is_poblacion)
            {
                is_poblacion = false;
                pais_actual.Poblacion = Convert.ToInt64(literal);
            }
            else if (is_saturacion)
            {
                is_saturacion = false;
                pais_actual.Saturacion = literal;
            }
            else if (is_bandera)
            {
                is_bandera = false;
                pais_actual.Bandera = literal;
            }
        }

        private bool IsLiteral(Tokens value)
        {
            string token = value.GetVal();
            if (punctuator.Contains(token) || verificar(token))
                return false;
            return true;
        }


        public bool verificar(string token)
        {
            if (punctuator.Contains(token))
            {
                return false;
            }
            token = token.ToLower();
            if (token.Equals("nombre"))
            {
                if (is_graph)
                {
                    is_graph_name = true;
                    return true;
                }
                if (is_continente)
                {
                    is_continente_name = true;
                    return true;
                }
                if (is_pais)
                {
                    is_pais_name = true;
                    return true;
                }
            }
            if (token.Equals("continente"))
            {
                continente_actual = new Modal.Continente();
                is_continente = true;
                return true;
            }
            else if (token.Equals("pais"))
            {
                pais_actual = new Modal.Pais();
                is_pais = true;
                return true;
            }
            else if (token.Equals("poblacion"))
            {
                is_poblacion = true;
                return true;
            }
            else if (token.Equals("saturacion"))
            {
                is_saturacion = true;
                return true;
            }
            else if (token.Equals("bandera"))
            {
                is_bandera = true;
                return true;
            }
            return false;
        }
    }
}


/*
 * Grafica: Graph
 * Nombre: Name
 * Continente: Continent
 * Pais: Country
 * Poblacion: Population
 * Saturacion: Saturation
 * Bandera: Flag
 * 
 
 */
