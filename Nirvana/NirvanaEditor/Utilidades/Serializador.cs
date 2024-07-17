#region Librerias
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
#endregion

namespace NirvanaEditor.Utilidades
{
    /// <summary>
    /// <para>Utilidades para la serializacion de elementos.</para>
    /// </summary>
    public static class Serializador
    {
        #region API
        /// <summary>
        /// <para>Serializa un valor en una ruta.</para>
        /// </summary>
        /// <typeparam name="T">Tipo de valor.</typeparam>
        /// <param name="instancia">Tipo de instancia.</param>
        /// <param name="ruta">Ruta del archivo.</param>
        public static void Serializar<T>(T instancia, string ruta)
        {
            try
            {
                var fs = new FileStream(ruta, FileMode.Create);
                var serializador = new DataContractSerializer(typeof(T));
                serializador.WriteObject(fs, instancia);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// <para>Deserializa un valor de una ruta.</para>
        /// </summary>
        /// <typeparam name="T">Tipo de valor.</typeparam>
        /// <param name="ruta">Ruta.</param>
        internal static T Deserializar<T>(string ruta)
        {
            try
            {
                var fs = new FileStream(ruta, FileMode.Open);
                var serializador = new DataContractSerializer(typeof(T));
                T instancia = (T)serializador.ReadObject(fs);
                return instancia;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(T);
            }
        }
        #endregion
    }
}
