using System;
using System.Collections.Generic;
using System.Linq;

namespace FileExplorer
{
    public static class Printer
    {
        /// <summary>
        /// 使用 Linq 简单输出
        /// </summary>
        /// <param name="nodes"></param>
        /// <remarks>无法展示祖节点的其他子节点的竖线</remarks>
        public static void PrintLineLite(List<Node> nodes)
        {
            Console.WriteLine("——<<<一行 Linq 输出：>>>——");
            Console.WriteLine(string.Join("\n", nodes.Select(node => $"{string.Empty.PadRight(node.LayoutNumber, ' ') }{(node.IsLast ? "└" : "├")}{(node.Children.Count == 0 ? "─" : "┬")} {node.Name}")));
        }

        /// <summary>
        /// 输出完整树结构
        /// </summary>
        /// <param name="nodes"></param>
        /// <remarks>可以展示祖节点的其他子节点的竖线</remarks>
        public static void PrintList(List<Node> nodes)
        {
            Console.WriteLine("——<<<完整树输出：>>>——");
            Dictionary<string, string> prefixes = new Dictionary<string, string>();

            string GetParentPrefix(Node parentNode)
            {
                if (parentNode == null) return string.Empty;
                if (prefixes.TryGetValue(parentNode.Path, out string parentPrefix)) return parentPrefix;
                parentPrefix = $"{GetParentPrefix(parentNode.Parent)}{(parentNode.IsLast ? " " : "│")}";
                prefixes.Add(parentNode.Path, parentPrefix);
                return parentPrefix;
            }

            foreach (var node in nodes)
            {
                string prefix = string.Empty;
                string line = string.Empty;
                prefix = GetParentPrefix(node.Parent);

                line = $"{prefix}{(node.IsLast ? "└" : "├")}{(node.Children.Count == 0 ? "─" : "┬")} {node.Name}";
                Console.WriteLine(line);
            }

            prefixes.Clear();
        }
    }
}
