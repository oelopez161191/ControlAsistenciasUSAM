using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Func<int, int> b = (a) => a * 2;
            //Func<int, int,int> suma = (a,b) => a +b;
            //int resultado=suma(4,2);
            /*
                        Func<int, int, int> mayores = (a, b) =>
                        {
                            if (a > b) { return a; }
                            else { return b; }
                        };
                        int resultado=mayores(2,3);
                        Console.Write(resultado);*/
            var numeros = new List<int> { 3, 5, 7, 4, 8, 9, 2, 6 };
            Func<int, bool> pares = (A) => A % 2 == 0;
            var par = numeros.Where(pares);

            Console.ReadKey();
        }
    }
}
