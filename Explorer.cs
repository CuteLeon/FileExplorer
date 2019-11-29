using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer
{
    public class Explorer
    {
        /// <summary>
        /// 使用堆栈扫描目录结构
        /// </summary>
        /// <param name="rootDirectory"></param>
        /// <returns></returns>
        public static List<Node> ExploreRootByStack(string rootDirectory)
        {
            // 根节点
            DirectoryInfo rootInfo = new DirectoryInfo(rootDirectory);
            Node rootNode = new Node()
            {
                Path = rootInfo.FullName,
                Name = rootInfo.Name,
                LayoutNumber = 0,
                IsLast = true
            };

            // 列表和堆栈
            List<Node> nodes = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(rootNode);

            // 扫描堆栈
            while (stack.Count > 0)
            {
                // 目录出栈
                Node parent = stack.Pop();
                nodes.Add(parent);

                DirectoryInfo parentInfo = new DirectoryInfo(parent.Path);
                int count = 0;

                // 扫描文件
                count = parentInfo.GetFiles().Select(childInfo => new Node()
                {
                    Name = childInfo.Name,
                    Path = childInfo.FullName,
                    LayoutNumber = parent.LayoutNumber + 1,
                    Parent = parent,
                }).Select(child =>
                {
                    parent.Children.Add(child);
                    nodes.Add(child);
                    return child;
                }).Count();

                // 扫描子目录
                count = parentInfo.GetDirectories().Select(childInfo => new Node()
                {
                    Name = childInfo.Name,
                    Path = childInfo.FullName,
                    LayoutNumber = parent.LayoutNumber + 1,
                    Parent = parent,
                }).Select(child =>
                {
                    parent.Children.Add(child);
                    return child;
                }).Reverse()
                .Select(child =>
                {
                    // 子目录倒序入栈
                    stack.Push(child);
                    return child;
                }).Count();

                // 标记最后一个子节点
                if (parent.Children.Count > 0)
                {
                    parent.Children.Last().IsLast = true;
                }
            }

            return nodes;
        }

        /// <summary>
        /// 使用递归扫描目录结构
        /// </summary>
        /// <param name="rootDirectory"></param>
        /// <returns></returns>
        public static List<Node> ExploreRootByRecursion(string rootDirectory)
        {
            var rootInfo = new DirectoryInfo(rootDirectory);
            _ = ExploreRootByRecursion(rootInfo, 0);

            // 标记根节点
            if (Nodes.Count > 0)
            {
                Nodes.First().IsLast = true;
            }
            return Nodes;
        }

        // 使用全局变量，降低子列表空间复杂度
        private static List<Node> Nodes = new List<Node>();

        /// <summary>
        /// 递归方法
        /// </summary>
        /// <param name="rootInfo"></param>
        /// <param name="layoutNumber"></param>
        /// <returns></returns>
        private static Node ExploreRootByRecursion(DirectoryInfo rootInfo, int layoutNumber)
        {
            // 当前目录节点
            Node parentNode = new Node()
            {
                Path = rootInfo.FullName,
                Name = rootInfo.Name,
                LayoutNumber = layoutNumber,
            };
            Nodes.Add(parentNode);
            int count = 0;

            // 扫描子目录
            count = rootInfo.GetDirectories().Select(childInfo =>
            {
                // 递归子目录
                var childNode = ExploreRootByRecursion(childInfo, layoutNumber + 1);
                return childNode;
            }).Select(childNode =>
            {
                childNode.Parent = parentNode;
                parentNode.Children.Add(childNode);
                return childNode;
            }).Count();

            // 扫描文件
            count = rootInfo.GetFiles().Select(childInfo => new Node()
            {
                Name = childInfo.Name,
                Path = childInfo.FullName,
                LayoutNumber = parentNode.LayoutNumber + 1,
                Parent = parentNode,
            }).Select(childNode =>
            {
                parentNode.Children.Add(childNode);
                Nodes.Add(childNode);
                return childNode;
            }).Count();

            // 标记最后一个子节点
            if (parentNode.Children.Count > 0)
            {
                parentNode.Children.Last().IsLast = true;
            }

            return parentNode;
        }
    }
}
