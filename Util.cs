using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Threading;

namespace VELAZQUEZ

{
    class Util
    {
        public static void menu()
        {
            string linea = "- - - - - - - - - -";
            string espacio = "           ";
            int opc = 0;
            string aux;
            do
            {
                opc = 0;
                Console.Clear();
                Console.WriteLine("\n{0}  M E N U  {1}", linea, linea);
                Console.WriteLine("\n{0}1) Ingresar una persona.\n{1}2) Mostrar una persona.\n{2}3) Salir.", espacio, espacio, espacio);
                Console.WriteLine("\nIngrese una opcion: "); aux = Console.ReadLine();
                while (!int.TryParse(aux, out opc))
                {
                    Console.WriteLine("\nError, ingrese un numero entero.");
                    Console.WriteLine("\nIngrese una opcion: "); aux = Console.ReadLine();
                }


                switch (opc)
                {
                    case 1:
                        {
                            crearPersona();
                            break;
                        }
                    case 2:
                        {
                            if(File.Exists(@"C:\Users\brvelazquez\Desktop\nro de documento.txt"))
                            {
                                listar();
                            }
                            else
                            {
                                Console.WriteLine("Error, el archivo a leer no existe.");
                                Console.ReadKey();
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("\nError, ingrese una opción valida.");
                            break;
                        }
                }
            } while (opc != 3);
            Console.Clear();
            Console.WriteLine("Adios!");
            Thread.Sleep(800);
        }

        public static void crearPersona()
        {
            string ape = Util.leerString("Apellido", "");
            string nom = Util.leerString("Nombre", "");
            DateTime fnacto = Util.leerFecha("Fecha de nacimiento", true);
            long doc = Util.leerLong("Documento", "");
            Persona Spinetta = new Persona(ape, nom, fnacto, doc);

            string texto = Spinetta.csv();
            Console.Clear();
            Util.ejec(texto);
        }

        public static String leerString(String campo, String margen = "")
        {
            String retval;
            while (true)
            {
                try
                {
                    Console.Write("{0}Ingrese un valor para {1}: ", margen, campo);
                    retval = Console.ReadLine();
                    if (string.IsNullOrEmpty(retval))
                        throw new InvalidOperationException("Debe ingresar una cadena válida");
                    else
                        break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}Error leyendo {1}: {2}", margen, campo, e.Message);
                }
            }
            return retval;
        }

        public static long leerLong(String campo, String margen = "")
        {
            long retval;
            while (true)
            {
                try
                {
                    Console.Write(
                        "{0}{1}Ingrese un valor para {2}, 0 para terminar: ",
                        Environment.NewLine, margen, campo);
                    String str = Console.ReadLine();
                    if (!long.TryParse(str, out retval))
                        throw new InvalidOperationException(
                            "Debe ingresar un entero válido");
                    else
                        break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}Error leyendo {1}: {2}",
                        margen, campo, e.Message);
                }
            }
            return retval;
        }

        public static DateTime leerFecha(String campo, Boolean pasado = true)
        {
            // pasado=true, solo toma fechas anteriores al dia de hoy
            // Por ejemplo para tomar fechas de nacimiento no siendo a 
            // recien nacidos. Medio q quiza no sirva para mucho...
            DateTime retval;

            // Para q las funciones de fecha usen el formato argentino
            // https://docs.microsoft.com/en-us/dotnet/api/system.globalization.cultureinfo?view=net-6.0
            CultureInfo cultura = CultureInfo.CreateSpecificCulture("es-AR");
            // Lo pide DateTime.tryParse como 3er parametro
            // Mas Info: https://docs.microsoft.com/en-us/dotnet/api/system.globalization.datetimestyles?view=net-6.0
            DateTimeStyles estilo = DateTimeStyles.None;

            while (true)
            {
                try
                {
                    Console.Write("Ingrese un valor para {0} (Formato: DD/MM/YYYY): ", campo);
                    String str = Console.ReadLine();
                    if (!DateTime.TryParse(str, cultura, estilo, out retval))
                        throw new InvalidOperationException("Debe ingresar una fecha válida");
                    else if (retval >= DateTime.Now.Date)
                        throw new InvalidOperationException("La fecha debe ser anterior a hoy");
                    else
                        break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error leyendo {0}: {1}", campo, e.Message);
                }
            }
            return retval;
        }

        public static void copiadoAUnArchivo(string tex)
        {
            FileStream archivo;
            StreamWriter grabador;
            // UTN:
            archivo = new FileStream(@"C:\Users\brvelazquez\Desktop\nro de documento.txt", FileMode.Append);
            // CASA:
            //archivo = new FileStream(@"C:\Users\Admin\Desktop\nro de documento.txt", FileMode.Append);
            grabador = new StreamWriter(archivo);
            grabador.WriteLine(tex);
            grabador.Close();
            archivo.Close();
        }

        public static void listar()
        {
            // UTN:
            string[] arr = File.ReadAllLines(@"C:\Users\brvelazquez\Desktop\nro de documento.txt", Encoding.UTF8);
            // CASA:
            Console.Clear();
            //string[] arr = File.ReadAllLines(@"C:\Users\Admin\Desktop\nro de documento.txt", Encoding.UTF8);
            string p;
            string[] data = new string[5];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   NRO   APELLIDO  NOMBRE   FNAC     DOCUMENTO");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < arr.Length; i++)
            {
                p = arr[i];
                data = p.Split(',');
                DateTime d2;
                d2 = DateTime.Parse(data[2]);
                Console.WriteLine("   {0}    {1}   {2}    {3}     {4}", i+1, data[0], data[1], d2.ToShortDateString(), data[3]);
            }
            Console.ReadKey();
            mostrar(arr);
        }

        public static void mostrar(string[] arc)
        {
            Console.Clear();
            string p;
            string[] data;
            int opc = 0;
            do
            {
                Console.WriteLine("Elija uno para mostrar, o escriba 0 para salir: "); string aux = Console.ReadLine();
                while(!int.TryParse(aux, out opc))
                { 
                    Console.WriteLine("\nError, debe ingresar un nro valido.");
                    Thread.Sleep(850);
                    Console.Clear();
                    Console.WriteLine("Elija uno para mostrar, o escriba 0 para salir: "); aux = Console.ReadLine();
                }
                if (opc != 0)
                {
                    if(opc <= arc.Length)
                    {
                        Console.Clear();
                        p = arc[opc-1];
                        data = p.Split(',');
                        DateTime da2;
                        da2 = DateTime.Parse(data[2]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(arc.Length);
                        Console.WriteLine("\n       NRO   APELLIDO  NOMBRE   FNAC     DOCUMENTO");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("       {0}    {1}    {2}    {3}    {4}", opc, data[0], data[1], da2.ToShortDateString(), data[3]);
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Error, debe igresar un valor valido.");
                        Thread.Sleep(850);
                    }
                    Console.Clear();
                }
            } while (opc != 0);
            Console.WriteLine("Adios!");
            Thread.Sleep(1000);
        }

        public static void ejec(string tex)
        {
            string opc = "";
            copiadoAUnArchivo(tex);
            Console.WriteLine("¿Desea ingresar otra persona? (S/N): "); opc = Console.ReadLine();
            if (opc == "S")
            {
                /*/string ape = Util.leerString("Apellido", "");
                string nom = Util.leerString("Nombre", "");
                DateTime fnacto = Util.leerFecha("Fecha de nacimiento", true);
                long doc = Util.leerLong("Documento", "");

                Persona Spinetta = new Persona(ape, nom, fnacto, doc);

                string texto = Spinetta.csv();
                Console.Clear();
                ejec(texto);/*/
                Console.Clear();
                crearPersona();
            }
            else
            {
                listar();
            }
        }
    }
}
//string ruta = @"C:\Users\Admin\Desktop\nro de documento.bat"; 
/*/string ruta = @"C:\";
if (!File.Exists(ruta))
{
    using(StreamWriter sw = new StreamWriter(ruta))
    {
        sw.WriteLine(tex);
    }

    using(StreamReader sr = new StreamReader(ruta))
    {
        string linea;
        while((linea = sr.ReadLine()) != null)
        {
            Console.WriteLine(linea);
        }
    }
}/*/
//archivo = File.Create(@"D:\" + nom_arc);
/*/string[] cadena = tex.Split(',');
string ape = cadena[0]; string nom = cadena[1]; string fnacto = cadena[2]; string doc = cadena[3];
grabador.WriteLine("{0} {1} {2} {3}", ape, nom, fnacto, doc);/*/