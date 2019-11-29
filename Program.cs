using System;
using System.Collections.Generic;
using System.Linq;

namespace FileExplorer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Node> nodes = null;

            nodes = Explorer.ExploreRootByStack(@"..\..\..\");
            // Printer.PrintLineLite(nodes);
            Printer.PrintList(nodes);

            nodes = Explorer.ExploreRootByRecursion(@"..\..\..\");
            // Printer.PrintLineLite(nodes);
            Printer.PrintList(nodes);

            Console.ReadLine();
        }
    }
}
