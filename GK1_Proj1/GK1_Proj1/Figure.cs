using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK1_Proj1
{
    abstract class Figure
    {
       
    }

    class Polygon: Figure
    {
        public List<Point> points;
        public bool Completed = false;
        public Polygon()
        {
            points = new List<Point>();
        }
        public Polygon(Point p):this()
        {
            points.Add(p);
        }
    }

    class Circle: Figure
    {
        public Point center;
        public int radius;
        public Circle(Point p, int r)
        {
            center = p;
            radius = r;
        }
    }
}
