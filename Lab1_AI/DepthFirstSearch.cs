using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab1_AI
{
    class DepthFirstSearch
    {
        // Список посещенных вершин
        private HashSet<Node> visited;
        // Путь из начальной вершины в целевую.
        private LinkedList<Node> path;
        private Node goal;

        // Был ли поиск завершен из-за того, что достиг предела глубины.
        private bool limitWasReached;

        public LinkedList<Node> DFS(Node start, Node goal)
        {
            visited = new HashSet<Node>();
            path = new LinkedList<Node>();
            this.goal = goal;
            DFS(start);
            if (path.Count > 0)
            {
                path.AddFirst(start);
            }
            return path;
        }

        private bool DFS(Node node)
        {
            if (node == goal)
            {
                return true;
            }
            visited.Add(node);
            foreach (var child in node.Children.Where(x => !visited.Contains(x)))
            {
                if (DFS(child))
                {
                    path.AddFirst(child);
                    return true;
                }
            }
            return false;
        }

        public LinkedList<Node> DLS(Node start, Node goal, int limit)
        {
            visited = new HashSet<Node>();
            path = new LinkedList<Node>();
            limitWasReached = true;
            this.goal = goal;
            DLS(start, limit);
            if (path.Count > 0)
            {
                path.AddFirst(start);
            }
            return path;
        }

        private bool DLS(Node node, int limit)
        {
            if (node == goal)
            {
                return true;
            }
            if (limit == 0)
            {
                limitWasReached = false;
                return false;
            }
            visited.Add(node);
            foreach (var child in node.Children.Where(x => !visited.Contains(x)))
            {
                if (DLS(child, limit - 1))
                {
                    path.AddFirst(child);
                    return true;
                }
            }
            return false;
        }
    }
}
