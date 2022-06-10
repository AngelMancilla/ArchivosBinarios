using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Practica_ArchivosBinarios
{
    class Program
    {
        class ArchivoBinarioEmpleados
        {
            //Declaracion de flujos
            BinaryWriter bw = null;// flujo salida-escritura de datos
            BinaryReader br = null;// flujo entrada-lectura de datos
                                   // campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearArchivo(string Archivo)
            {
                //Variable locar Metodo
                char resp;

                try
                {
                    // creación del flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //Capturar datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del empleado: "); NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del empleado: "); Nombre = Console.ReadLine();
                        Console.Write("Direccion del empleado: "); Direccion = Console.ReadLine();
                        Console.Write("Telefono del empleado: "); Telefono = Int64.Parse(Console.ReadLine());
                        Console.Write("Dias trabajados: "); DiasTrabajados = Int32.Parse(Console.ReadLine());
                        Console.Write("Salario diario del empleado: "); SalarioDiario = Single.Parse(Console.ReadLine());

                        //Escribe los datos en el archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\nDeseas almacenar otro regustro (s/n)? ");
                        resp = char.Parse(Console.ReadLine());
                    } while ((resp == 's') | (resp == 'S'));
                }
                catch (IOException e)
                {
                    Console.WriteLine("\nError: " + e.Message);
                    Console.WriteLine("\nRuta: " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close();// cierra el flujo escritura
                    Console.Write("\nPresione <enter> para terminar la Escritura de datos y regresar al menu.");
                    Console.ReadKey();
                }
            }

            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //Comprobar si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        //Creacion flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //despliegue de datos en pantalla
                        Console.Clear();
                        do
                        {
                            //Lectura de registro mientras no llegue a EndOfFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //Muestra los datos
                            Console.WriteLine("Numero del empleado: " + NumEmp);
                            Console.WriteLine("Nombre del empleado: " + Nombre);
                            Console.WriteLine("Direccion del empleado: " + Direccion);
                            Console.WriteLine("Telefono del empleado: " + Telefono);
                            Console.WriteLine("Dias trabajados del empleado: " + DiasTrabajados);
                            Console.WriteLine("Salario diario del empleado:  (0:C)", SalarioDiario);
                            Console.WriteLine("SUELDO TOTAL del empleado: (0:C)", (DiasTrabajados * SalarioDiario));
                            Console.WriteLine("\n");
                        } while (true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEL archivo" + Archivo + " No existe en el disco!!!");
                        Console.WriteLine("\nPresione <enter> para continuar...");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.Write("\nPresione <enter >para Continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close();// cierra flujo
                    Console.Write("\nPresione<enter>para terminar la Lectura de datos y regresar al menu.");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            // declaración variables auxiliares
            string Arch = null;
            int opcion;

            //creación del objeto
            ArchivoBinarioEmpleados Al = new ArchivoBinarioEmpleados();

            //Menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el nombre del Archivo a Crear: "); Arch = Console.ReadLine();

                            //Verifica si existe el archivo
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo existe!!, Deseas sobreescribirlo (s/n)? ");
                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                Al.CrearArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;

                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: "); Arch = Console.ReadLine();
                            Al.MostrarArchivo(Arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione < enter > para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 3);
            
        }
    }
}
