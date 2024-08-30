using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banknoten_Code
{
    public  class Djordje
    {
        public List<string> codeList = new List<string>();
        public List<int> intList = new List<int>();

        public int digitsum = 0;
        public int checksum = 0;
        //public string faelschung = ("Die Prüfsumme betraegt {0}\nAchtung: gefaelschte Banknote!", checksum);//chechsum needed to be static to be able to do this.. but then it's not working...

        public Djordje(string eingabe)
        {
            //all needed methods;
            SaveInList(eingabe);
            bool b = obEingabePasst();   // wenn unsinnige Eingabe, dann Fehlermeldung und return ist fail :D
            if (!b)
            {
                CWriteFail();   //Console.WriteLine("Fail");
            }
            else
            {
                BuildIntList();
                CalcDigitSum(); //look above p int digitsum; no digitsum = calcdigitsum needed
                checksum = BerechneJ();
            }
        }


        public void SaveInList(string eingabe)
        {
            //Buchstabe in Zahl und an Stelle 0 in codeList
            foreach (char x in eingabe)
            {
                codeList.Add(x.ToString());
            }
        }
        public void CWriteFail()
        {
            Console.WriteLine("Fail: ungueltiger Code. Siehe Doku.");
        }
        public void BuildIntList()
        {
            codeList[0].ToUpper();
            //codeList[0] = wandleBuchstabeInZahl(codeList[0]); //not possible!
            intList.Add(wandleBuchstabeInZahl());

            for (int i = 1; i < codeList.Count - 1; i++) //-1 b/c last int is check digit! works!
            {
                intList.Add(int.Parse(codeList[i]));
            }
        }
        public void CalcDigitSum()
        {
            digitsum = intList.Sum(); //intList ist mit dem umgewandelten LänderCode!
        }
        public string ChecksumComparison()
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
        public int wandleBuchstabeInZahl()
        {
            //char 'A' hat einen INT-WErt von 65 bei int.Parse('A');! um 1? --> -64! wie mache ich char.ToUpper()?
            //int.Parse geht nicht bei Chars also --> 
            int zahlMit65zuViel = (int)(Convert.ToChar(codeList[0].ToUpper()));
            return zahlMit65zuViel - 64;
        }
        public int BerechneJ()
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
        public bool obEingabePasst()
        {
            bool b = true;
            for (int i = 1; i < codeList.Count; i++)
            {
                if (codeList[i] != "0" && codeList[i] != "1" && codeList[i] != "2" && codeList[i] != "3" && codeList[i] != "4" && codeList[i] != "5" && codeList[i] != "6" && codeList[i] != "7" && codeList[i] != "8" && codeList[i] != "9")
                {
                    b = false;  //ist hier nötig!
                    break;
                }
            }
            if (codeList.Count != 12)
            {
                b = false;
            }
            return b;
        }
    }
}
