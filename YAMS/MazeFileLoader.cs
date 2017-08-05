using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using YAMS;

namespace YAMS
{
    class MazeFileLoader
    {

        public MazeInput Load(string path)
        {
            int width = 0;
            int height = 0;
            Point start = new Point(0, 0);
            Point end = new Point(0, 0);

            string text = File.ReadAllText(path).Trim();
            string[] lines = text.Split('\n');

            //we count the no. chars in the first few lines so we can remove them when we make the maze
            int initalCharCount = lines[0].Length + lines[1].Length + lines[2].Length; 

            //Get maze dimensions
            string[] dimensions = lines[0].Split(' ');
            int.TryParse(dimensions[0], out width);
            int.TryParse(dimensions[1], out height);

            //Get start point
            string[] startPoint = lines[1].Split(' ');
            int.TryParse(startPoint[0], out start.x);
            int.TryParse(startPoint[1], out start.y);

            //Get end point
            string[] endPoint = lines[2].Split(' ');
            int.TryParse(endPoint[0], out end.x);
            int.TryParse(endPoint[1], out end.y);

            //Get the maze data
            string maze = text.Remove(0, initalCharCount+1).Replace(Environment.NewLine, "").Replace(" ","");

            return new MazeInput(width, height, start, end, maze);

        }

    }

    public class MazeInput
    {
        public int width;
        public int height;
        public Point start;
        public Point end;
        public string data;

        public MazeInput()
        {

        }
        
        public MazeInput(int width, int height, Point start, Point end, string data)
        {
            this.width = width;
            this.height = height;
            this.start = start;
            this.end = end;
            this.data = data;
        }

    }

}
