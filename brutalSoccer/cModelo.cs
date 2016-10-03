﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
    public class cModelo : DbContext
    {

        public cModelo()
            : base("name=cModelo")
        {
        }

        public virtual DbSet<cEquipo> Equipos { get; set; }
        public virtual DbSet<cJornada> Jornadas { get; set; }
        public virtual DbSet<cManager> Managers { get; set; }
        public virtual DbSet<cTemporada> Temporadas { get; set; }
        public virtual DbSet<cPartido> Partidos { get; set; }
        public static cModelo Create()
        {
            return new cModelo();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//constructor of identity framework

        //    modelBuilder.Entity<cPartido>().HasMany<>(s => s.partidos);
                      

            /* modelBuilder.Entity<cMessage>().HasRequired(c=>c.Send)
                .WithMany(j=>j.Messages)
                .HasForeignKey(c=>c.Send);*/

        }
    }

}
