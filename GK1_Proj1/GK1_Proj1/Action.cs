using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK1_Proj1
{
    class Action
    { 
        protected string Name { get; set; }
        protected Form1.modes mode;
        protected Form1.moveModes moveMode;
        public Action() { Name = "???"; }
        public Action(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
        public virtual void Undo(List<Polygon> polygons, List<Circle> circles) { }
        public virtual void Redo(List<Polygon> polygons, List<Circle> circles) { }
    }
    class CircleAction : Action
    {
        public Point centerB, centerA;
        public int radiusB, radiusA;
        public int i;
        public SolidBrush SolidBrush;
        public CircleAction(string name, Form1.modes m, Form1.moveModes mm, SolidBrush sb, int i, Point cB, Point cA, int rB, int rA)
        {
            Name = name;
            mode = m;
            moveMode = mm;
            this.i = i;
            centerB = cB;
            centerA = cA;
            radiusA = rA;
            radiusB = rB;
            SolidBrush = sb;
        }
        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            switch (mode)
            {
                case Form1.modes.AddCircle:
                    if (circles == null || circles.Count == 0) return;
                    circles.RemoveAt(i);
                    break;
                case Form1.modes.Move:
                    if (circles == null || circles.Count == 0) return;
                    switch (moveMode)
                    {
                        case Form1.moveModes.Point:
                            circles[i].center = centerB;
                            break;
                        case Form1.moveModes.Radius:
                            circles[i].radius = radiusB;
                            break;
                    }
                    break;
                case Form1.modes.Delete:
                    circles.Insert(i, new Circle(centerB, radiusB) { SolidBrush = this.SolidBrush });
                    break;
            }
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            switch (mode)
            {
                case Form1.modes.AddCircle:
                    circles.Add(new Circle(centerA, radiusA) { SolidBrush = this.SolidBrush });
                    break;
                case Form1.modes.Move:
                    switch (moveMode)
                    {
                        case Form1.moveModes.Point:
                            circles[i].center = centerA;
                            break;
                        case Form1.moveModes.Radius:
                            circles[i].radius = radiusA;
                            break;
                    }
                    break;
                case Form1.modes.Delete:
                    if (circles == null || circles.Count == 0) return;
                    circles.RemoveAt(i);
                    break;
            }
        }
    }

    class PolygonMovePointAction : Action
    {
        public Point pointB, pointA;
        int iPolygon, iPoint;

        public PolygonMovePointAction(string name, int iPolygon, int iPoint, Point pointB, Point pointA)
        {
            Name = name;
            this.iPolygon = iPolygon;
            this.iPoint = iPoint;
            this.pointB = pointB;
            this.pointA = pointA;
        }

        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points[iPoint] = pointB;
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points[iPoint] = pointA;
        }
    }

    class PolygonMoveEdgeAction : Action
    {
        public Point point1B, point1A, point2B, point2A;
        int iPolygon, iPoint;

        public PolygonMoveEdgeAction(string name, int iPolygon, int iPoint, Point point1B, Point point1A, Point point2B, Point point2A)
        {
            Name = name;
            this.iPolygon = iPolygon;
            this.iPoint = iPoint;
            this.point1B = point1B;
            this.point1A = point1A;
            this.point2B = point2B;
            this.point2A = point2A;
        }
        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points[iPoint] = point1B;
            polygons[iPolygon].points[(iPoint + 1) % polygons[iPolygon].points.Count] = point2B;
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points[iPoint] = point1A;
            polygons[iPolygon].points[(iPoint + 1) % polygons[iPolygon].points.Count] = point2A;
        }
    }

    class PolygonMoveWholeAction : Action
    {
        public List<Point> pointsB, pointsA;
        int i;

        public PolygonMoveWholeAction(string name, int i, List<Point> pointsB, List<Point> pointsA)
        {
            Name = name;
            this.i = i;
            this.pointsA = pointsA;
            this.pointsB = pointsB;
        }

        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[i].points = pointsB;
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[i].points = pointsA;
        }
    }

    class PolygonDeleteAction : Action
    {
        SolidBrush solidBrush;
        List<Point> points;
        int i;

        public PolygonDeleteAction(string name, int i, SolidBrush solidBrush, List<Point> points)
        {
            Name = name;
            this.solidBrush = solidBrush;
            this.points = points;
            this.i = i;
        }

        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons.Insert(i, new Polygon(points) { SolidBrush = solidBrush });
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons.RemoveAt(i);
        }
    }

    class PolygonCreatePolygonAction : Action
    {
        Point pointB, pointA;
        SolidBrush solidBrush;
        Button b;

        public PolygonCreatePolygonAction(string name, SolidBrush solidBrush, Point pointB, Point pointA, Button b)
        {
            this.b = b;
            this.solidBrush = solidBrush;
            Name = name;
            this.pointB = pointB;
            this.pointA = pointA;
        }
        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons.RemoveAt(polygons.Count - 1);
            b.Enabled = false;
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons.Add(new Polygon(pointA) { SolidBrush = this.solidBrush , Completed = false});
            b.Enabled = true;
        }
    }

    class PolygonAddVerticeAction : Action
    {
        int iPolygon, iPoint;
        Point pointB, pointA;

        public PolygonAddVerticeAction(string name, int iPolygon, int iPoint, Point pointB, Point pointA)
        {
            Name = name;
            this.iPolygon = iPolygon;
            this.iPoint = iPoint;
            this.pointB = pointB;
            this.pointA = pointA;
        }

        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points.RemoveAt(iPoint);
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points.Insert(iPoint, pointA);
        }
    }

    class PolygonDeleteVerticeAction : Action
    {
        int iPolygon, iPoint;
        Point pointB, pointA;

        public PolygonDeleteVerticeAction(string name, int iPolygon, int iPoint, Point pointB, Point pointA)
        {
            Name = name;
            this.iPolygon = iPolygon;
            this.iPoint = iPoint;
            this.pointB = pointB;
            this.pointA = pointA;
        }

        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points.Insert(iPoint, pointB);
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[iPolygon].points.RemoveAt(iPoint);
        }
    }

    class PolygonCompleteAction : Action
    {
        public int i;
        Button b;

        public PolygonCompleteAction(string name, int i, Button b)
        {
            Name = name;
            this.i = i;
            this.b = b;
        }

        public override void Undo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[i].Completed = false;
            b.Enabled = true;
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            polygons[i].Completed = true;
            b.Enabled = false;
        }
    }
}
