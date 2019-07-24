
namespace Problema_3
{
    class Student
    {
        //Campurile private ale clasei
        private string nume;
        private double nota_Medie;
        private byte curs;

        //Proprietatile clasei
        public string Nume { get { return nume; } set { nume = value; } }
        public double Nota_Medie { get { return nota_Medie; } set { nota_Medie = value; } }
        public byte Curs { get { return curs; } set { curs = value; } }

        //Constructorul fara parametri
        public Student()
        {
            Nume = "";
            Nota_Medie = 0;
            Curs = 0;
        }

        //Constructorul cu parametri
        public Student(string nume, double nota, byte curs)
        {
            Nume = nume;
            Nota_Medie = nota;
            Curs = curs;
        }

        //Functia "calitate"
        public double Q()
        {
            return 0.2 * Nota_Medie * Curs;
        }
    }
}
