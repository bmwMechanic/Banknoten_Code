namespace Banknoten_Code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string eingabe = Console.ReadLine();//first string var
            //then object creation w/ contructor
            Djordje dj = new Djordje(eingabe);
            //Console.WriteLine(Djordje dj = new Djordje(eingabe));    ---not possible---

            Console.WriteLine(dj.ChecksumComparison());

            Console.ReadKey();
        }
    }
}
