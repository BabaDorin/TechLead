using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema_3
{
    class Program
    {
        //Metoda pentru citirea campurilor: Nume, nota_medie, curs.
        public static void Citire(out string Nume, out double Nota_Medie, out byte Curs)
        {
            bool eroare = false;
            Console.Write("Nume: ");
            Nume = Console.ReadLine();
            Nota_Medie = 0;
            Curs = 0;
            do
            {
                try
                {
                    Console.Write("Nota medie: ");
                    Nota_Medie = double.Parse(Console.ReadLine());
                    if (Nota_Medie <= 0 || Nota_Medie > 10) throw new Exception();
                    eroare = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Invalid imput.");
                    eroare = true;
                }
            } while (eroare);

            do
            {
                try
                {
                    Console.Write("Curs: ");
                    Curs = byte.Parse(Console.ReadLine());
                    if (Curs < 0) throw new Exception();
                    eroare = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Invalid imput.");
                    eroare = true;
                }
            } while (eroare);
        }
        static void Main(string[] args)
        {
            //Testarea clasei de baza
            Student s1 = new Student();

            string nume;
            double nota;
            byte curs;

            Console.WriteLine("Stundentul nr:1");
            Citire(out nume, out nota, out curs);
            s1.Nume = nume;
            s1.Curs = curs;
            s1.Nota_Medie = nota;

            Console.WriteLine("\nRezultatul metodei 'Calitate': "+s1.Q());
            Console.WriteLine();
            Console.WriteLine("Apasa orice tasta pentru a continua.");
            Console.ReadKey();
            Console.Clear();

            Student2 s2 = new Student2();

            Console.WriteLine("Student nr2:");
            Citire(out nume, out nota, out curs);
            s2.Nume = nume;
            s2.Curs = curs;
            s2.Nota_Medie = nota;

            //Citirea campului P
            bool eroare = false;
            bool P = false;
            do
            {
                try
                {
                    Console.WriteLine("Tasteaza '1' daca {0} studiaza engleza. In caz contrar tastati '2'",s2.Nume);
                    byte optiune = byte.Parse(Console.ReadLine());

                    if (optiune > 2 || optiune < 1) throw new Exception();
                    P = (optiune == 1) ? true : false;
                    eroare = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: Invalid imput");
                    eroare = true;
                }
            } while (eroare);

            Console.WriteLine("\nRezultatul metodei 'Calitate': " + s2.Q(P));
            Console.ReadKey();
        }
    }
}
