using System;

namespace ContarDías
{
    class Program
    {
        DateTime dtHoy = DateTime.Now;

        /// <summary>
        /// Evento que se ejecuta al ejercutar el programa
        /// </summary>
        /// <param name="args">los argumentos que se envian al iniciar las clases</param>
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.LeerArchivo();
        }

        /// <summary>
        /// Metodo que se encarga de leer el archivo e ir recorriendo las líneas
        /// </summary>
        public void LeerArchivo()
        {
            #region daclaracion de variables
            string cRutaArchivo = @"C:\Trabajo\Cursos\Buenas practicas\dias.txt";//TODO: leer la ruta de manera dinamica
            string[] arrcLineas = System.IO.File.ReadAllLines(cRutaArchivo);//TODO: validar que el archivo exista
            string[] arrcColumnas = new string[2];
            string cTiempo = "";
            #endregion

            #region recorro el archivo
            foreach (string l in arrcLineas)
            {
                arrcColumnas = l.Split(',');//TODO: validar que cada linea tenga el formato corecto
                cTiempo = CalcularTiempo(arrcColumnas[1]);
                Console.WriteLine(string.Format("El evento {0} {1}", arrcColumnas[0], cTiempo));
            }
            #endregion

            System.Console.ReadKey();
        }

        /// <summary>
        /// Metodo que se encarga de calcular el tiempo trascurrido
        /// </summary>
        /// <param name="_cFecha">el formato de la fecha la cual se comparara con el dia de hoy</param>
        /// <returns>una cadena con el timepo trascurrido y la unidad de tiempo, igual distigue si ya le fecha y apaso o esta por pasar</returns>
        public string CalcularTiempo(string _cFecha)
        {
            #region declaracion de variables
            DateTime dtFecha = Convert.ToDateTime(_cFecha);//TODO: validar que la fecha tenga el formato correcto
            TimeSpan tsTiempo = dtHoy - dtFecha;
            int iCantidad = 0;
            string cUnidad = "";
            string cRespuesta = "";
            #endregion

            #region calculo del tiempo
            //TODO: Detectar cuando es 1 sola unidad ponerlo en particular y no plural
            //TODO: Hacer mas optimo este codigo que no sean if anidados
            if (tsTiempo.Days >= 360 || tsTiempo.Days <= -360)
            {
                iCantidad = tsTiempo.Days / 360;
                cUnidad = (iCantidad == 1)?"año":"años";
            }
            else
            {
                if (tsTiempo.Days >= 30 || tsTiempo.Days <= -30)
                {
                    iCantidad = tsTiempo.Days / 30;
                    cUnidad = (iCantidad == 1) ? "mes" : "meses";
                    //return "ocurrio hace " + valorMostrar + " meses";
                }
                else
                {
                    if (tsTiempo.Days != 0)
                    {
                        iCantidad = tsTiempo.Days;
                        cUnidad = (iCantidad == 1) ? "día" : "días";
                    }
                    else
                    {
                        if (tsTiempo.Hours != 0)
                        {
                            iCantidad = tsTiempo.Hours;
                            cUnidad = (iCantidad == 1) ? "hora" : "horas";
                        }
                        else
                        {
                            iCantidad = tsTiempo.Minutes;
                            cUnidad = (iCantidad == 1) ? "minuto" : "minutos";
                        }
                    }
                }
            }
            #endregion

            #region definicion de mensaje
            if (dtHoy < dtFecha)
            {
                iCantidad = Math.Abs(iCantidad);
                cRespuesta = string.Format("ocurrirá dentro de {0} {1}", iCantidad,cUnidad);
            }
            else
                cRespuesta = string.Format("ocurrio hace {0} {1}", iCantidad, cUnidad);
            #endregion

            return cRespuesta;
        }
    }
}
