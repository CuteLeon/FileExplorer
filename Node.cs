using System.Collections.Generic;

namespace FileExplorer
{
    public class Node
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public int LayoutNumber { get; set; }
        public bool IsLast { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
        public override string ToString() => this.Name;
    }
}
