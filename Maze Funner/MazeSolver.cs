using System;
using System.Collections.Generic;
using System.Text;

namespace YAMS
{
    class MazeSolver
    {

        public int[,] maze;
        public int width;
        public int height;


        public MazeSolver (int width, int height, string MazeInput)
        {
            //Initialise MazeSolver
            this.maze = new int[width, height];
            this.height = height;
            this.width = width;

            //Populate the maze
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    this.maze[col, row] = MazeInput[row * width + col] == '0' ? 0 : 1;
                }
            }
        }

        public List<Node> getNodeNeighbours(Node currentNode, Node start, Node end)
        {
            List<Node> result = new List<Node>();

            //We can only go up, down, left, right

            //up
            if (currentNode.position.y - 1 >= 0)
            {
                Point position = new Point(currentNode.position.x, currentNode.position.y - 1);
                Node up = new Node(position, maze[position.x, position.y]);
                result.Add(up);
            }

            //down
            if (currentNode.position.y + 1 < height)
            {
                Point position = new Point(currentNode.position.x, currentNode.position.y + 1);
                Node down = new Node(position, maze[position.x, position.y]);
                result.Add(down);
            }

            //left
            if (currentNode.position.x - 1 >= 0)
            {
                Point position = new Point(currentNode.position.x - 1, currentNode.position.y);
                Node left = new Node(position, maze[position.x, position.y]);
                result.Add(left);
            }

            //right
            if (currentNode.position.x + 1 < width)
            {
                Point position = new Point(currentNode.position.x + 1, currentNode.position.y);
                Node right = new Node(position, maze[position.x, position.y]);
                result.Add(right);
            }

            return result;
        }

        public void DrawMaze()
        {
            //Draw it
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Console.Write(maze[col, row].ToString() == "0" ? " " : "#");
                }
                Console.WriteLine();
            }
        }

        public void DrawMaze(Point start, Point end)
        {
            //Draw it
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Point currentPosition = new Point(col, row);
                    //Label key bits
                    if (currentPosition.Equals(start))
                    {
                        Console.Write("S");
                    }
                    else if(currentPosition.Equals(end))
                    {
                        Console.Write("E");
                    }
                    else
                    {
                        Console.Write(maze[col, row].ToString() == "0" ? " " : "#");
                    }
                }
                Console.WriteLine();
            }
        }

        public void DrawMaze(Point start, Point end, Point[] points)
        {
            //Draw it
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    Point currentPosition = new Point(col, row);

                    //Label key bits
                    if (currentPosition.Equals(start))
                    {
                        Console.Write("S");
                    }
                    else if (currentPosition.Equals(end))
                    {
                        Console.Write("E");
                    }
                    else
                    {

                        bool hasMarker = false;

                        //Take care of markers
                        foreach (Point p in points)
                        {
                            if (p.Equals(currentPosition))
                            {
                                hasMarker = true;
                            }
                        }

                        Console.ForegroundColor = (hasMarker ? ConsoleColor.Red : ConsoleColor.White);
                        Console.Write(hasMarker ? "x" : (maze[col, row].ToString() == "0" ? " " : "#"));
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
        }


        public Point[] SolveMaze(Point startPoint, Point endPoint)
        {

            //Make sure the start and end points are plausible
            if (maze[startPoint.x, startPoint.y] != 0 || maze[endPoint.x, endPoint.y] != 0)
            {
                throw new Exception("You are starting/finishing on a wall! The maze is impossible");
            }

            //Check if start/end point is within the maze
            if (startPoint.x < 0 || startPoint.x > width - 1 || endPoint.x < 0 || endPoint.x > width - 1 || startPoint.y < 0 || startPoint.y > width - 1 || endPoint.y < 0 || endPoint.y > width - 1)
            {
                throw new Exception("You are starting/finishing outside of the maze! The maze is impossible");
            }


            Node start = new Node(startPoint, 0);
            Node end = new Node(endPoint, 0);
            start.distanceToStart = 0;
            start.distanceToEnd = Node.Distance(start, end);

            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();

            open.Add(start);
            
            while (open.Count > 0)
            {

                Node current = open[0];

                foreach (Node node in open)
                {
                    node.distanceToStart = Node.Distance(node, start);
                    node.distanceToEnd = Node.Distance(node, end);
                    if (node == current)
                    {
                        continue;
                    }
                    if (node.cost < current.cost || node.cost == current.cost) 
                    {
                        if (node.distanceToEnd < current.distanceToEnd)
                        {
                            current = node;
                        }
                    }
                }
                
                open.Remove(current);
                closed.Add(current);

                if (current.position.Equals(end.position))
                {
                    Console.WriteLine("Solving...");
                    return BuildSolution(current);
                }

                List<Node> neighbours = getNodeNeighbours(current, start, end);

                foreach (Node neighbour in neighbours)
                {
                    if (neighbour.value == 1 || closed.Contains(neighbour))
                    {
                        continue;
                    }


                    float moveCost = current.distanceToStart + Node.Distance(current, neighbour);
                    if (moveCost < neighbour.distanceToStart || !open.Contains(neighbour))
                    {
                        neighbour.distanceToStart = moveCost;
                        neighbour.distanceToEnd = Node.Distance(neighbour, end);
                        neighbour.parent = current;

                        if (!open.Contains(neighbour))
                        {
                            open.Add(neighbour);
                        }
                    }

                }
                
            }

            return new Point[0];

        }

        public Point[] BuildSolution(Node end)
        {
            List<Point> solution = new List<Point>();
            solution.Add(end.position);
            Node currentNode = end;
            while (true)
            {
                if(currentNode.parent != null)
                {
                    solution.Add(currentNode.position);
                    currentNode = currentNode.parent;
                }
                else
                {
                    break;
                }
            }
            solution.Reverse();
            return solution.ToArray();
        }

    }

    public class Point : IEquatable<Point>
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Point other)
        {
            if (other == null)
                return false;

            if (this.x == other.x && this.y == other.y)
                return true;
            else
                return false;
        }

    }

    public class Node : IComparable, IEquatable<Node>
    {
        public Point position;
        public int value;
        public float distanceToStart;
        public float distanceToEnd;
        public float cost
        {
            get
            {
                return this.distanceToEnd + this.distanceToStart;
            }
        }
        public Node parent;

        public Node(Point position, int value)
        {
            this.position = position;
            this.value = value;
        }
        
        public static float Distance(Node start, Node target)
        {
            float x = Math.Abs(start.position.x - target.position.x);
            float y = Math.Abs(start.position.y - target.position.y);
            return x+y;
        }


        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            Node otherNode = obj as Node;
            if (otherNode != null)
            {
                return this.cost.CompareTo(otherNode.cost);
            }
            else
            {
                throw new ArgumentException("You're trying to compare a node to a not-node!");
            }
        }

        public bool Equals(Node other)
        {
            if (other == null)
                return false;

            if (this.position.Equals(other.position))
                return true;
            else
                return false;
        }

    }

}
