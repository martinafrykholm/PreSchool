using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLLibrary_new;

namespace ConsoleForTesting
{
    class Program
    {
        static void Main(string[] args)
        {

            //int rowsaffected1 = SqlClass.AddChild("Lo", "Lopez", 4);
            //Console.WriteLine(rowsaffected1);


            //int rowsaffected = SqlClass.AddPreSchool("Byängen");
            //Console.WriteLine(rowsaffected);

            int rowsaffected2 = SqlClass.AddUnit("Grönan", 4);
            Console.WriteLine(rowsaffected2);





        }
    }
}
