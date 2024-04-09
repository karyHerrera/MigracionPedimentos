using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InsertarPedimentos
{
    class Program
    {
        static void Main(string[] args)
        {

            string currentPath = ConfigurationManager.AppSettings["CarpetaArchivos"].ToString(); //Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);            

            Console.WriteLine($"El path donde debe de estar el archivo es el siguiente: {currentPath}");
            Console.WriteLine("El nombre del archivo debe de ser Pedimentos.txt");
            
            Console.WriteLine("Inicia el proceso de inserción");

            InsertarPedimentosArchivo.LeerInsertaPedimentos(currentPath);

            Console.WriteLine("El proceso de inserción ha terminado");
            Console.ReadLine();

        }
    }
}
