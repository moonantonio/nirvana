#region Librerias
using System.ComponentModel;
using System.Runtime.Serialization;
#endregion

namespace NirvanaEditor
{
    /// <summary>
    /// <para>Clase base de la vista. Controla los cambios de valores en las 
    /// propiedades locales y las notifica.</para>
    /// </summary>
    [DataContract(IsReference = true)]
    public class VistaBase : INotifyPropertyChanged
    {
        #region Eventos
        /// <summary>
        /// <para>Evento llamado cuando cambia el valor de una propiedad.</para>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Metodos Protegidos
        /// <summary>
        /// <para>Evento llamado cuando la propiedad cambia de valor.</para>
        /// </summary>
        /// <param name="nombrePropiedad">Nombre de la propiedad.</param>
        protected void EventoPropiedadCambia(string nombrePropiedad) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombrePropiedad));
        #endregion

    }
}
