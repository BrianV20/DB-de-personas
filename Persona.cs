using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VELAZQUEZ
{
    class Persona
    {
        String apellido;
        String nombre;
        DateTime fnac;
        long documento;

        public Persona()
        {
        }

        public Persona(string ape, string nom, DateTime fn, long doc)
        {
            this.APELLIDO = ape;
            this.NOMBRE = nom;
            this.FNAC = fn;
            this.DOCUMENTO = doc;
        }

        public string APELLIDO
        {
            get
            {
                return this.apellido;
            }
            set
            {
                if(value.Length > 0)
                {
                    this.apellido = value;
                }
                else
                {
                    this.apellido = "García";
                }
            }
        }

        public string NOMBRE
        {
            get
            {
                return this.nombre;
            }
            set
            {
                if(value.Length > 0)
                {
                    this.nombre = value;
                }
                else
                {
                    this.nombre = "Charly";
                }
            }
        }

        public DateTime FNAC
        {
            get
            {
                return this.fnac;
            }
            set
            {
                this.fnac = value;
            }
        }

        public long DOCUMENTO
        {
            get
            {
                return this.documento;
            }
            set
            {
                this.documento = value;
            }
        }

        public string csv()
        {
            string codigo = string.Format("{0},{1},{2},{3}", this.APELLIDO, this.NOMBRE, this.FNAC, this.DOCUMENTO);
            return codigo;
        }
    }
}
