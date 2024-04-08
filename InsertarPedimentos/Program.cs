using System;
using System.Collections.Generic;
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

            string currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);            
            Console.WriteLine("Inicia el proceso de inserción");

            InsertarPedimentosArchivo.LeerInsertaPedimentos(currentPath);

            Console.WriteLine("El proceso de inserción ha terminado");
            Console.ReadLine();

        }
    }
}
