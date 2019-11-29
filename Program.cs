using System;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            static string InputRootDirectory()
            {
                Console.WriteLine("请输入根目录 (输入 exit 退出)：");
                Console.Write("\t");
                return Console.ReadLine();
            }

            string rootDirectory;
            while ((rootDirectory = InputRootDirectory()) != "exit")
            {
                if (string.IsNullOrEmpty(rootDirectory))
                {
                    rootDirectory = @".\";
                }
                if (!Directory.Exists(rootDirectory))
                {
                    Console.WriteLine($"不存在的目录：{rootDirectory}");
                    continue;
                }

                // nodes = Explorer.ExploreRootByStack(rootDirectory);
                // Printer.PrintLineLite(nodes);
                List<Node> nodes = Explorer.ExploreRootByRecursion(rootDirectory);
                Printer.PrintList(nodes);
                Console.WriteLine("输出完毕！");
                
                nodes.Clear();
                GC.Collect();
            }
        }
    }
}
