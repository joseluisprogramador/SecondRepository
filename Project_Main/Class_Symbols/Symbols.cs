using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Main.Class_Event
{
    public sealed class Symbols
    {
        private readonly static List<(string,string)>  ArithmeticSymbols = new List<(string,string)>() {("Sum","+"),("Rest","-"),("Mult","*"),("Div","/") };
        private readonly static List<(string, string)> NumericSymbols = new List<(string, string)>() { ("0", "0"), ("1", "1"), ("2", "2"), ("3", "3"), ("4", "4"), ("5", "5"), ("6", "6"), ("7", "7"), ("8", "8"), ("9", "9") };
        private  readonly static List<(string, string)> GroupingSymbols = new List<(string, string)>() { ("(", "("), (")", ")") };
        private readonly static List<(string, string)> ExponentSymbols = new List<(string, string)>() { ("Rz", "√") };
        private readonly static List<(string, string)> RootsSymbols = new List<(string, string)>() { ("Exp", "^") };
        private readonly static List<(string, string)> GroupAllSymbols = new List<(string, string)>();

        public static List<string> ClearSymbols = new List<string>() { "Ac", "Del" };
        
        public static List<(string, string)> AllSymbols()
        {
            GroupAllSymbols.AddRange(ArithmeticSymbols);
            GroupAllSymbols.AddRange(NumericSymbols);
            GroupAllSymbols.AddRange(GroupingSymbols);
            GroupAllSymbols.AddRange(ExponentSymbols);
            GroupAllSymbols.AddRange(RootsSymbols);
            return GroupAllSymbols ;
        }

    }
}
