using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace brutalSoccer
{
   public class cGeneraDataSet
    {
      public  DataTable ObtenTablaEquipo(int idEquipo)
        {
            cModelo m = new cModelo();
           
            var res = m.Jornadas.Where(p => p.temporada.equipo.Id == idEquipo).ToList();
           
            DataTable data = new DataTable("tabla");
            Type tipo = typeof(cResultadosJornada);
            var properties = tipo.GetProperties().ToList();
            properties.Remove(properties.Where(p => p.Name == "Id").FirstOrDefault());
            properties.Remove(properties.Where(p => p.Name == "temporada").FirstOrDefault());
            foreach (PropertyInfo info in properties)
            {
                data.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (cResultadosJornada entity in res)
            {
                object[] values = new object[properties.Count];
                for (int i = 0; i < properties.Count; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

               data.Rows.Add(values);
            }

            return data;
        }
    }
}
