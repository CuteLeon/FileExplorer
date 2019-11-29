using System;
using System.Collections.Generic;
using System.Linq;

namespace FileExplorer
{
    public static class Printer
    {
        public static void PrintLineLite(List<Node> nodes)
        {
            Console.WriteLine("——<<<一行 Linq 输出：>>>——");
            Console.WriteLine(string.Join("\n", nodes.Select(node => $"{string.Empty.PadRight(node.LayoutNumber, ' ') }{(node.IsLast ? "└" : "├")}{(node.Children.Count == 0 ? "─" : "┬")} {node.Name}")));
        }

        public static void PrintList(List<Node> nodes)
        {
            Console.WriteLine("——<<<完整树输出：>>>——");
            Dictionary<string, string> prefixes = new Dictionary<string, string>();

            foreach (var node in nodes)
            {
                string prefix = string.Empty;
                string line = string.Empty;
                if (node.Parent != null)
                {
                    if (!prefixes.TryGetValue(node.Parent.Path, out prefix))
                    {
                        prefix = $"{string.Empty.PadRight(node.LayoutNumber, ' ')}";
                        List<int> indexes = new List<int>();
                        var parent = node.Parent;
                        while (parent != null)
                        {
                            if (!parent.IsLast)
                            {
                                indexes.Add(parent.LayoutNumber);
                            }
                            parent = parent.Parent;
                        }
                        var chars = prefix.ToCharArray();
                        indexes.ForEach(index => chars[index] = '│');
                        prefix = new string(chars);
                        prefixes.Add(node.Parent.Path, prefix);
                    }
                }

                line = $"{prefix}{(node.IsLast ? "└" : "├")}{(node.Children.Count == 0 ? "─" : "┬")} {node.Name}";
                Console.WriteLine(line);
            }
        }
    }
}
