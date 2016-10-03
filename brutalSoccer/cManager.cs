﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cManager
    {


        public List<cEquipo> equipos { get; set; }

        public cManager()
        {
            this.equipos = new List<cEquipo>();
        }

        public void procesaNombreEquipos(cLinea linea)
        {
            this.addEquipo(linea.local);
            this.addEquipo(linea.visitante);

        }

        public void procesaTemporada(List<cLinea> lineas)
        {

            foreach (cLinea l in lineas)
            {
                this.procesaNombreEquipos(l);
            }

            DateTime fechaMin = lineas.Min(p => p.fecha);
            DateTime fechaMax = lineas.Max(p => p.fecha);


            foreach (cLinea l in lineas)
            {

                cEquipo local = this.equipos.Where(p => p.Nombre == l.local).FirstOrDefault();
                cEquipo visitante = this.equipos.Where(p => p.Nombre == l.visitante).FirstOrDefault();
                local.addPartido(l, fechaMin, fechaMax);
                visitante.addPartido(l, fechaMin, fechaMax);
            }

     
        }

        private void addEquipo(string nombre)
        {
            if (!equipos.Exists(p => p.Nombre == nombre))
            {
                cEquipo nuevoEquipo = new cEquipo(this);
                nuevoEquipo.Nombre = nombre;
                equipos.Add(nuevoEquipo);
            }
        }





    }

}
