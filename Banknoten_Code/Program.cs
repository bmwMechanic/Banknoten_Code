using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Banknoten_Code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //idea: divide into more classes intelligently to reply in various languages. sounds fun right? --> plus work w/getter and setter for those private properties from outside of class Djordje!
            Console.Write("Enter Code like this: ^[A-Za-z]\\d(11)$\nInsert Code Here: ");
            string eingabe = Console.ReadLine();//first string var
            //then object creation w/ contructor
            Djordje dj = new Djordje(eingabe);
            //Console.WriteLine(-Initioalization of object-) is not possible!
            Leo leo = new Leo(eingabe);

            Console.WriteLine(dj.getBanknoteResult());  //returns string only; now w/o CW in methods exception's calling....
            Console.ReadKey();
            dj = null;  //'non-nullable type'? 
            GC.Collect();   //Destructor seems not to be working..? or Garbage is not collected...?
            Console.ReadKey();

        }
    }
}
