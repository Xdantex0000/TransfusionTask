using System;
using System.Collections.Generic;

namespace Lab1_AI
{
    class Node
    {
        /// Имя вершины.
        public string Name { get; }
        public int FValue { get; }
        public int SValue { get; }

        /// Список соседних вершин.
        public List<Node> Children { get; }

        public Node(string name, int fvalue, int svalue)
        {
            Name = name;
            Children = new List<Node>();
            this.FValue = fvalue;
            this.SValue = svalue;
        }

        /// Добавляет новую соседнюю вершину.
        public Node AddChildren(Node node, bool bidirect = false)
        {
            Children.Add(node);
            if (bidirect)
            {
                node.Children.Add(this);
            }
            return this;
        }
    }
}
