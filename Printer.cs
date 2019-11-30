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

            foreach (var node in nodes)
            {
                string prefix = string.Empty;
                string line = string.Empty;
                if (node.Parent != null)
                {
                    // 存在祖节点的节点需要计算祖节点的其他子节点的竖线，并使用字典缓存，实现 O(1) 时间复杂度
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

            prefixes.Clear();
        }
    }
}
