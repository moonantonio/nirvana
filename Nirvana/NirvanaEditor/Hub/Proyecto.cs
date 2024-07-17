#region Librerias
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
#endregion

namespace NirvanaEditor.Hub
{
    /// <summary>
    /// <para></para>
    /// </summary>
    [DataContract(Name = "Juego")]
    public class Proyecto : VistaBase
    {
        #region Variables Privadas
        /// <summary>
        /// <para>Lista de escenas.</para>
        /// </summary>
        [DataMember(Name = "Escenas")] private ObservableCollection<Escena> _escenas = new ObservableCollection<Escena>();
        #endregion

        #region Propiedades
        /// <summary>
        /// <para>Extension del proyecto.</para>
        /// </summary>
        public static string Extension { get; } = ".nirvana";

        /// <summary>
        /// <para>Nombre del nuevo proyecto.</para>
        /// </summary>
        [DataMember] public string Nombre { get; private set; }

        /// <summary>
        /// <para>Ruta del nuevo proyecto.</para>
        /// </summary>
        [DataMember] public string Ruta { get; private set; }

        /// <summary>
        /// <para>Ruta completa del nuevo proyecto.</para>
        /// </summary>
        public string RutaCompleta => $"{this.Ruta}{this.Nombre}{Extension}";

        /// <summary>
        /// <para>Lista de plantillas.</para>
        /// </summary>
        public ReadOnlyCollection<Escena> Escenas
        {
            get;
        }
        #endregion

        #region Constructores
        /// <summary>
        /// <para>Constructor de <see cref="Proyecto"/>.</para>
        /// </summary>
        /// <param name="nombre">Nombre del proyecto.</param>
        /// <param name="ruta">Ruta del proyecto.</param>
        public Proyecto(string nombre, string ruta) 
        {
            this.Nombre = nombre;
            this.Ruta = ruta;

            this._escenas.Add(new Escena(this, "Escena Principal"));
        }
        #endregion
    }
}
