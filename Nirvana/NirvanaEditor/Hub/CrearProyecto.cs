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
        /// <summary>
        /// <para>Caracter de separacion de directorio.</para>
        /// </summary>
        private const char CARACTER_SEPARACION_DIR = '\\';
        /// <summary>
        /// <para>Caracter de separacion de directorio alternativo.</para>
        /// </summary>
        private const char CARACTER_SEPARACION_DIR_ALT = '/';
        #endregion

        #region Variables Privadas
        /// <summary>
        /// <para>Nombre del nuevo proyecto.</para>
        /// </summary>
        private string _nombre = "NuevoProyecto";
        /// <summary>
        /// <para>Ruta del nuevo proyecto.</para>
        /// </summary>
        private string _ruta = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\ProyectosNirvana\";
        /// <summary>
        /// <para>Determina si es valida la ruta.</para>
        /// </summary>
        private bool _esValida;
        /// <summary>
        /// <para>Mensaje de error.</para>
        /// </summary>
        private string _msjError;
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
                    this.ValidarRutasProyecto();
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
        /// <para>Determina si es valida la ruta.</para>
        /// </summary>
        public bool EsValida 
        {
            get => this._esValida;
            set
            {
                if (this._esValida != value)
                {
                    this._esValida = value;
                    this.EventoPropiedadCambia(nameof(this.EsValida));
                }
            }
        }

        /// <summary>
        /// <para>Mensaje de error.</para>
        /// </summary>
        public string MsjError
        {
            get => this._msjError;
            set
            {
                if (this._msjError != value)
                {
                    this._msjError = value;
                    this.EventoPropiedadCambia(nameof(this.MsjError));
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
                }
                this.ValidarRutasProyecto();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region API
        /// <summary>
        /// <para>Crea un nuevo proyecto.</para>
        /// </summary>
        /// <param name="plantilla">Plantilla del proyecto.</param>
        /// <returns></returns>
        public string CrearNuevoProyecto(PlantillaProyecto plantilla)
        {
            this.ValidarRutasProyecto();
            if (!this.EsValida)
            {
                return string.Empty;
            }

            if (!this.TerminaConSeparador(this.Ruta)) this.Ruta += @"\";
            var r = $@"{this.Ruta}{this.Nombre}\";

            try
            {
                if (!Directory.Exists(r)) Directory.CreateDirectory(r);
                foreach (var carp in plantilla.Carpetas)
                {
                    Directory.CreateDirectory(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(r), carp)));
                }
                var infoDir = new DirectoryInfo(r + @".Nirvana\");
                infoDir.Attributes |= FileAttributes.Hidden;
                File.Copy(plantilla.RutaIcono, Path.GetFullPath(Path.Combine(infoDir.FullName, "Icono.png")));
                File.Copy(plantilla.RutaIcono, Path.GetFullPath(Path.Combine(infoDir.FullName, "Imagen.png")));

                var proyectoXml = File.ReadAllText(plantilla.RutaProyectoArchivo);
                proyectoXml = string.Format(proyectoXml, this.Nombre, this.Ruta);
                var proyectoRuta = Path.GetFullPath(Path.Combine(r, $"{this.Nombre}{Proyecto.Extension}"));
                File.WriteAllText(proyectoRuta, proyectoXml);

                return r;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        #endregion

        #region Metodos Privados
        /// <summary>
        /// <para>Valida las rutas del proyecto.</para>
        /// </summary>
        /// <returns></returns>
        private bool ValidarRutasProyecto() 
        {
            string r = this.Ruta;
            if (!this.TerminaConSeparador(r)) r += @"\";
            r += $@"{this.Nombre}\";

            this.EsValida = false;
            if (string.IsNullOrWhiteSpace(this.Nombre.Trim()))
            {
                this.MsjError = "Escriba un nombre de proyecto.";
            }
            else if (this.Nombre.IndexOfAny(Path.GetInvalidFileNameChars()) != -1) 
            {
                this.MsjError = "Caracter(es) invalidos en el nombre del proyecto.";
            }
            else if (string.IsNullOrWhiteSpace(this.Ruta.Trim()))
            {
                this.MsjError = "Selecciona un directorio valido para el proyecto.";
            }
            else if (this.Ruta.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                this.MsjError = "Caracter(es) invalidos en la ruta del proyecto.";
            }
            else if (Directory.Exists(r) && Directory.EnumerateFileSystemEntries(r).Any())
            {
                this.MsjError = "El directorio seleccionado ya existe y no esta vacio.";
            }
            else 
            {
                this.MsjError = string.Empty;
                this.EsValida = true;
            }

            return this.EsValida;
        }

        /// <summary>
        /// <para>Determina si la ruta termina con un caracter de separador
        /// de directorios.</para>
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private bool TerminaConSeparador(string r) => r.Length > 0 && this.DirectorioConSeparador(r[r.Length - 1]);

        /// <summary>
        /// <para>Comprueba los diferentes tipos de separadores.</para>
        /// </summary>
        /// <param name="c">Caracter.</param>
        /// <returns></returns>
        private bool DirectorioConSeparador(char c) => c == CARACTER_SEPARACION_DIR || c == CARACTER_SEPARACION_DIR_ALT;
        #endregion
    }
}
