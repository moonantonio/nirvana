#region Librerias
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace NirvanaEditor.Hub
{
    /// <summary>
    /// <para></para>
    /// </summary>
    [DataContract]
    public class Escena : VistaBase
    {
        #region Variables Privadas
        /// <summary>
        /// <para>Nombre de la escena.</para>
        /// </summary>
        private string _nombre;
        #endregion

        #region Propiedades
        /// <summary>
        /// <para>Nombre del nuevo proyecto.</para>
        /// </summary>
        [DataMember] public string Nombre
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
        /// <para>Proyecto de la escena.</para>
        /// </summary>
        [DataMember] public Proyecto Proyecto { get; private set; }
        #endregion

        #region Constructores
        /// <summary>
        /// <para>Constructor de <see cref="Escena"/>.</para>
        /// </summary>
        /// <param name="proyecto">Proyecto de la escena.</param>
        /// <param name="nombre">Nombre de la escena.</param>
        public Escena(Proyecto proyecto, string nombre)
        {
            Debug.Assert(proyecto != null);
            this.Proyecto = proyecto;
            this.Nombre = nombre;
        }
        #endregion
    }
}
