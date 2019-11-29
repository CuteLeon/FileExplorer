using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileExplorer
{
    public class Explorer
    {
        public static List<Node> ExploreRootByStack(string rootDirectory)
        {
            DirectoryInfo rootInfo = new DirectoryInfo(rootDirectory);
            Node rootNode = new Node()
            {
                Path = rootInfo.FullName,
                Name = rootInfo.Name,
                LayoutNumber = 0,
                IsLast = true
            };
            List<Node> nodes = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(rootNode);

            while (stack.Count > 0)
            {
                Node parent = stack.Pop();
                nodes.Add(parent);

                DirectoryInfo parentInfo = new DirectoryInfo(parent.Path);
                int count = 0;

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
                    stack.Push(child);
                    return child;
                }).Count();


                if (parent.Children.Count > 0)
                {
                    parent.Children.Last().IsLast = true;
                }
            }

            return nodes;
        }

        public static List<Node> ExploreRootByRecursion(string rootDirectory)
        {
            var rootInfo = new DirectoryInfo(rootDirectory);
            _ = ExploreRootByRecursion(rootInfo, 0);
            if (Nodes.Count > 0)
            {
                Nodes.First().IsLast = true;
            }
            return Nodes;
        }

        private static List<Node> Nodes = new List<Node>();
        private static Node ExploreRootByRecursion(DirectoryInfo rootInfo, int layoutNumber)
        {
            Node rootNode = new Node()
            {
                Path = rootInfo.FullName,
                Name = rootInfo.Name,
                LayoutNumber = layoutNumber,
            };
            Nodes.Add(rootNode);
            int count = 0;

            count = rootInfo.GetDirectories().Select(childInfo =>
            {
                var childNode = ExploreRootByRecursion(childInfo, layoutNumber + 1);
                return childNode;
            }).Select(childNode =>
            {
                childNode.Parent = rootNode;
                rootNode.Children.Add(childNode);
                return childNode;
            }).Count();

            count = rootInfo.GetFiles().Select(childInfo => new Node()
            {
                Name = childInfo.Name,
                Path = childInfo.FullName,
                LayoutNumber = rootNode.LayoutNumber + 1,
                Parent = rootNode,
            }).Select(childNode =>
            {
                rootNode.Children.Add(childNode);
                Nodes.Add(childNode);
                return childNode;
            }).Count();

            if (rootNode.Children.Count > 0)
            {
                rootNode.Children.Last().IsLast = true;
            }

            return rootNode;
        }
    }
}
