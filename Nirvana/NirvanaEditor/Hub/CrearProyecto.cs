#region Librerias
using NirvanaEditor.Utilidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
#endregion

namespace NirvanaEditor.Hub
{
    /// <summary>
    /// <para>Contenido de la plantilla para crear un nuevo
    /// proyecto.</para>
    /// </summary>
    [DataContract]
    public class PlantillaProyecto
    {
        #region Propiedades
        /// <summary>
        /// <para>Tipo de proyecto.</para>
        /// </summary>
        [DataMember] public string ProyectoTipo {  get; set; }

        /// <summary>
        /// <para>Archivo principal del proyecto.</para>
        /// </summary>
        [DataMember] public string ProyectoArchivo { get; set; }

        /// <summary>
        /// <para>Carpetas del proyecto.</para>
        /// </summary>
        [DataMember] public List<string> Carpetas { get; set; }

        /// <summary>
        /// <para>Icono de la plantilla.</para>
        /// </summary>
        public byte[] Icono { get; set; }

        /// <summary>
        /// <para>Imagen de la plantilla.</para>
        /// </summary>
        public byte[] Imagen { get; set; }

        /// <summary>
        /// <para>Ruta del icono de la plantilla.</para>
        /// </summary>
        public string RutaIcono { get; set; }

        /// <summary>
        /// <para>Ruta de la imagen de la plantilla.</para>
        /// </summary>
        public string RutaImagen { get; set; }

        /// <summary>
        /// <para>Ruta del archivo del proyecto.</para>
        /// </summary>
        public string RutaProyectoArchivo { get; set; }
        #endregion
    }

    /// <summary>
    /// <para>Clase general de un nuevo proyecto.</para>
    /// </summary>
    class CrearProyecto : VistaBase
    {
        #region Constantes
        /// <summary>
        /// <para>Ruta de las plantillas.</para>
        /// </summary>
        private const string RUTA_PLANTILLAS = @"..\..\NirvanaEditor\Plantillas";
        #endregion

        #region Variables Publicas
        /// <summary>
        /// <para>Nombre del nuevo proyecto.</para>
        /// </summary>
        public string _nombre = "NuevoProyecto";
        /// <summary>
        /// <para>Ruta del nuevo proyecto.</para>
        /// </summary>
        public string _ruta = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Nirvana\";
        #endregion

        #region Variables Privadas
        /// <summary>
        /// <para>Lista de plantillas.</para>
        /// </summary>
        private ObservableCollection<PlantillaProyecto> _plantillas = new ObservableCollection<PlantillaProyecto>();
        #endregion

        #region Propiedades
        /// <summary>
        /// <para>Nombre del nuevo proyecto.</para>
        /// </summary>
        public string Nombre
        {
            get => this._nombre;
            set 
            {
                if (this._nombre != value)
                {
                    this._nombre = value;
                    this.EventoPropiedadCambia(nameof(this.Nombre));
                }
            }
        }

        /// <summary>
        /// <para>Ruta del nuevo proyecto.</para>
        /// </summary>
        public string Ruta
        {
            get => this._ruta;
            set
            {
                if (this._ruta != value)
                {
                    this._ruta = value;
                    this.EventoPropiedadCambia(nameof(this.Ruta));
                }
            }
        }

        /// <summary>
        /// <para>Lista de plantillas.</para>
        /// </summary>
        public ReadOnlyCollection<PlantillaProyecto> Plantillas 
        {
            get;
        }
        #endregion

        #region Constructores
        /// <summary>
        /// <para>Constructor de <see cref="CrearProyecto"/>.</para>
        /// </summary>
        public CrearProyecto() 
        {
            this.Plantillas = new ReadOnlyCollection<PlantillaProyecto>(this._plantillas);
            try
            {
                string[] archivosPlantillas = Directory.GetFiles(RUTA_PLANTILLAS, "plantilla.xml", SearchOption.AllDirectories);
                Debug.Assert(archivosPlantillas.Any());
                foreach (var arc in archivosPlantillas)
                {
                    var plantilla = Serializador.Deserializar<PlantillaProyecto>(arc);
                    plantilla.RutaIcono = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(arc), "Icono.png"));
                    plantilla.Icono = File.ReadAllBytes(plantilla.RutaIcono);
                    plantilla.RutaImagen = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(arc), "Imagen.png"));
                    plantilla.Imagen = File.ReadAllBytes(plantilla.RutaImagen);
                    plantilla.RutaProyectoArchivo = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(arc), plantilla.ProyectoArchivo));

                    this._plantillas.Add(plantilla);

                    /*var plantilla = new PlantillaProyecto()
                    {
                        ProyectoTipo = "Proyecto Vacio",
                        ProyectoArchivo = "proyecto.nirvana",
                        Carpetas = new List<string> { ".Nirvana", "Contenido", "Codigo" }
                    };
                    Serializador.Serializar(plantilla, arc);*/
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
