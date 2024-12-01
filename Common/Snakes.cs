using System;
using System.Collections.Generic;

namespace Common
{
    public class Snakes
    {
        public class Point 
        {
            public Int32 X {  get; set; }
            public Int32 Y { get; set; }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public Point() { }
        }
        public enum DirectionType { Left, Right, Up, Down, Start }
        public List<Point> Points = new List<Point>();
        public DirectionType Direction { get; set; } = DirectionType.Start;
        public Boolean GameOver = false;
    }
}
