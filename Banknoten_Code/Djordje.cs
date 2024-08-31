using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banknoten_Code
{
    public  class Djordje
    {
        private List<string> codeList = new List<string>(); //why does private work here?
        private List<int> intList = new List<int>();        //why does private work here?

        //protected int digitsum { get; private set; }
        //protected int checksum { get;  private set; }
        private int digitsum { get; set; }  //getter & seeter are private by default?
        private int checksum { get; set; }  //getter & seeter are private by default?
        private string eingabe { get; set; }
        private bool b { get; set; }
        //public string faelschung = ("Die Prüfsumme betraegt {0}\nAchtung: gefaelschte Banknote!", checksum);//chechsum needed to be static to be able to do this.. but then it's not working...

        public Djordje(string Eing) : base()    //ok thanks David      https://stackoverflow.com/questions/63957152/error-cs7036-there-is-no-argument-given-that-corresponds-to-the-required-formal
        {
            // "samo da instancira polja"; polja su linije 17-20? kako to onda da uspem?
            //jer ne mogu ovako ovde u konstrukteru:       private bool b { get; set; }
            // pitao sam chatgpt sta ja instanciranje polja... a ovde sam dodao polje eingabe koje mogu inteligantno
            //instancirati s konstruktorom. I vide mi ne treba Parameter za SaveInList();
            eingabe = Eing;
        }

        ~ Djordje()
        {
            Console.WriteLine("vidimo se :)");  //destr. is enabled in Windows Forms upon closing the window; not here?
        }

        public string getBanknoteResult()
        {
            //all needed methods;
            SaveInList();
            b = obEingabePasst();  
            //ContinueProgram();
            return ContinueProgram() + ChecksumComparison();    //nice, works
        }

        private string ContinueProgram()   //put if in a surrounding method that will conain more of them... :)
        {
            if (!b)
            {
                return FailReturn();   //Console.WriteLine("Fail");
            }
            else
            {
                BuildIntList();
                CalcDigitSum(); //look above p int digitsum; no digitsum = calcdigitsum needed
                checksum = BerechneJ();
                return null;    // "" is possible as well
            }

            //(!b) ? Method1() : Method2(); NOT POSSIBLE!
        }
        public void SaveInList()
        {
            //Buchstabe in Zahl und an Stelle 0 in codeList
            foreach (char x in eingabe)
            {
                codeList.Add(x.ToString());
            }
        }
        private string FailReturn()
        {
            return "Fail: ungueltiger Code. Siehe Doku.";   //CW weg! für get...result
        }
        private void BuildIntList()
        {
            codeList[0].ToUpper();
            //codeList[0] = wandleBuchstabeInZahl(codeList[0]); //not possible!
            intList.Add(wandleBuchstabeInZahl());

            for (int i = 1; i < codeList.Count - 1; i++) //-1 b/c last int is check digit! works!
            {
                intList.Add(int.Parse(codeList[i]));
            }
        }
        private void CalcDigitSum()
        {
            digitsum = intList.Sum(); //intList ist mit dem umgewandelten LänderCode!
        }
        public string ChecksumComparison()  //bc this is being called from within class Program, it needs to be public. But Encapsulation here?
        {
            if (b)  //dass die Ausgabe Prüfsumme... ausgegeben wird, wenn falscher code eingegeben wurde. Test (falsche UND (zu kurze ODER zu lange) string-eingabe erfolgreich
            {
                if (int.Parse(codeList[11]) != checksum)    //12.Stelle des Codes! (L)(10xN)(J)
                {
                    return $"Die Pruefsumme betraegt {checksum}\ngefaelschte Banknote!";
                }
                else
                {
                    return $"Die Pruefsumme betraegt {checksum}\nOriginale Banknote!";
                }
            }
            else return "";
        }
        private int wandleBuchstabeInZahl()
        {
            //char 'A' hat einen INT-WErt von 65 bei int.Parse('A');! um 1? --> -64! wie mache ich char.ToUpper()?
            //int.Parse geht nicht bei Chars also --> 
            int zahlMit65zuViel = (int)(Convert.ToChar(codeList[0].ToUpper()));
            return zahlMit65zuViel - 64;
        }
        private int BerechneJ() //J is checksum
        {
            int rest = digitsum % 9;
            if (8 - rest == 0)
            {
                return 9;
            }
            else
            {
                return 8 - rest;
            }
        }
        private bool obEingabePasst()   //logic is not too beautiful but works; cannot put for
        {
            string pattern = @"^[0-9]$";
            Regex reg = new Regex(pattern);
            if (codeList.Count != 12)
            {
                return false;
            }
            else
            {
                for (int i = 1; i < codeList.Count; i++)
                {
                    if (!reg.IsMatch(codeList[i]))  //reg.IsMatch returns BOOL, so if(!BOOL) means if bool = false  :) 
                    {
                        return false;  //ist hier nötig!
                    }
                }
            }
            return true;
        }
    }
}
