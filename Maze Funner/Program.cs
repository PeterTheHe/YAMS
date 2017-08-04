using System;
using System.IO;
using YAMS;

namespace YAMS
{
    class Program
    {

        static void Main(string[] args)
        {
            //Load the maze from a text file and store all the parsed features in a MazeInput object
            MazeFileLoader loader = new MazeFileLoader();
            MazeInput input = new MazeInput();

            string path = "";
            if (args.Length != 0)
            {
                path = args[0];
            }
            else
            {
                Console.WriteLine("Where is your maze?");
                path = Console.ReadLine();
            }

            try
            {
                input = loader.Load(path);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found!");
                return;
            }

            //Solve the maze
            try
            {
                MazeSolver maze = new MazeSolver(input.width, input.height, input.data);
                Point[] solution = maze.SolveMaze(input.start, input.end);       
                if (solution.Length == 0)
                {
                    //Impossible
                    Console.WriteLine("Uh oh! The challenge seems impossible!");
                }
                else
                {
                    maze.DrawMaze(input.start, input.end, solution);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            }
            
            
            Console.ReadKey();
        }
        
    }

}