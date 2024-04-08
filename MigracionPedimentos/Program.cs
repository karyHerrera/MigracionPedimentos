using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MigracionPedimentos
{
    class Program
    {
        static void Main(string[] args)
        {

            string currentPath =  Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //Console.WriteLine(currentPath);
            Console.WriteLine("Inicia el proceso");
            MigracionPedimentos.Pedimentos(currentPath);
            Console.WriteLine("El proceso ha terminado");
            Console.ReadLine();
        }
    }
}
