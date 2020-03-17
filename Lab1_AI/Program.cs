using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1_AI
{
    class Program
    {
        private static void PrintPath(LinkedList<Node> path)
        {
            Console.WriteLine();
            if (path.Count == 0)
            {
                Console.WriteLine("You shall not pass!");
            }
            else
            {
                Console.WriteLine(string.Join(" -> ", path.Select(x => $"({x.FValue} - {x.SValue})")));
            }
        }
        static void SetExercise(int barrel, int f_bottle, int s_bottle, int goal)
        {
            Data.Barrel = barrel;
            Data.FirstBottle = f_bottle;
            Data.SecondBottle = s_bottle;
            Data.Goal = goal;
        }
        static bool GraphBuild(ref int a, ref int b, int maxA, int maxB)
        {
            if (a > maxB)
            {
                if (b == 0)
                {
                    b = maxB;
                    a = a - b;
                    return true;
                }
                if (b == maxB)
                {
                    b = 0;
                    return true;
                }
                if (b > 0)
                {
                    a = a - (maxB - b);
                    b = maxB;
                    return true;
                }
            }
            if (a < maxB)
            {
                if (b == 0)
                {
                    b = a;
                    a = 0;
                    return true;
                }
                if (b == maxB)
                {
                    b = 0;
                    return true;
                }
                if (b > 0)
                {
                    if (b + a > maxB)
                    {
                        a = a - (maxB - b);
                        b = maxB;
                        return true;
                    }
                    if (b + a <= maxB)
                    {
                        b = b + a;
                        a = 0;
                        return true;
                    }
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            #region SetExercise
            int BarrelValue = 12;
            int FBottleValue = 5;
            int SBottleValue = 7;
            int Goal = 6;
            int limit = 0;

            Console.Write("Do you want to change values?(Press Enter if 'no') ");
            if (Console.ReadLine().ToLower() == "y")
            {
                Console.Write("Enter the Barrel value: ");
                BarrelValue = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the First bottle value: ");
                FBottleValue = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the Second bottle value: ");
                SBottleValue = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter the Goal value: ");
                Goal = Convert.ToInt32(Console.ReadLine());
            }

            SetExercise(BarrelValue, FBottleValue, SBottleValue, Goal);
            #endregion
            #region Graph
            int i = 3;
            int FBottle = 0;
            int SBottle = 0;
            Node Goal_node1 = null, Goal_node2 = null;
            Node node = new Node("1", FBottle, SBottle);
            FBottle = FBottleValue;
            Node nod = new Node("2", FBottle, SBottle);
            node.AddChildren(nod);
            Node n;
            int temp = i;
            for (; i<100; i++)
            {
                n = new Node($"{i}",FBottle,SBottle);

                if (FBottle == Data.Goal || SBottle == Data.Goal)
                {
                    nod.AddChildren(n);
                    Goal_node1 = n;
                    break;
                }
                if (FBottle == 0)
                {
                    FBottle = FBottleValue;
                    nod.AddChildren(n);
                    nod = n;
                    continue;
                }
                else
                {
                    GraphBuild(ref FBottle, ref SBottle, FBottleValue, SBottleValue);
                }
                if (temp != i)
                {
                    nod.AddChildren(n);
                    nod = n;
                }
            }
            FBottle = 0;
            SBottle = SBottleValue;
            Node nod2 = new Node($"{i}", FBottle, SBottle);
            i++;
            temp = i;
            node.AddChildren(nod2);
            for (; i < 200; i++)
            {
                n = new Node($"{i}", FBottle, SBottle);

                if (FBottle == Data.Goal || SBottle == Data.Goal)
                {
                    nod2.AddChildren(n);
                    Goal_node2 = n;
                    break;
                }
                if (SBottle == 0)
                {
                    SBottle = SBottleValue;
                    nod2.AddChildren(n);
                    nod2 = n;
                    continue;
                }
                else
                {
                    GraphBuild(ref SBottle, ref FBottle, SBottleValue, FBottleValue);
                }
                if (i != temp)
                {
                    nod2.AddChildren(n);
                    nod2 = n;
                }
            }
            #endregion
            #region AlgoritmDLS
            Console.Write("Enter the limit value: ");
            Int32.TryParse(Console.ReadLine(),out limit);

            var search = new DepthFirstSearch();
            var path = search.DLS(node, Goal_node1, limit);
            var path2 = search.DLS(node, Goal_node2, limit);

            if (path.Count != 0)
                PrintPath(path);
            if (path2.Count != 0)
                PrintPath(path2);
            else
                Console.WriteLine("There are no pathes found");
            #endregion

            Console.ReadKey();
        }
    }
}
