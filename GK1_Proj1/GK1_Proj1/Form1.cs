using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK1_Proj1
{
    public partial class Form1 : Form
    {
        List<Polygon> polygons = new List<Polygon>();
        List<Circle> circles = new List<Circle>();
        List<Action> actions = new List<Action>();
        Figure Selected;

        bool IsMouseDown = false;
        enum modes {Select, Edit, AddPolygon, AddVertice, AddCircle, Move, Delete}
        enum moveModes { Point, Edge, Polygon, Radius, None }
        modes CurrentMode;
        moveModes CurrentMoveMode;
        int WhichPoint;

        public Form1()
        {
            InitializeComponent();
            CurrentMode = modes.Select;

            Action a = new Action(true, new Point(2, 2), "ASD");
            
        }

        private void ZakonczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("cofnij");
        }

        private void PonowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("ponow");
        }

        private void DrawingField_Paint(object sender, PaintEventArgs e)
        {
            
            foreach (Polygon p in polygons)
            {
                //draw poly
                for (int i = 0; i < p.points.Count; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Red), p.points[i], p.points[(i + 1) % p.points.Count]);
                }
            }
            if (Selected is Polygon)
                for (int i = 0; i < ((Polygon)Selected).points.Count; i++)
                    e.Graphics.DrawLine(new Pen(Color.Black), ((Polygon)Selected).points[i], ((Polygon)Selected).points[(i + 1) % ((Polygon)Selected).points.Count]);

            foreach (Circle c in circles)
            {
                //draw circle
                e.Graphics.DrawEllipse(new Pen(Color.Red), new Rectangle(c.center.X - c.radius, c.center.Y - c.radius, 2 * c.radius, 2 * c.radius));
                e.Graphics.FillRectangle(Brushes.Red, c.center.X, c.center.Y, 1, 1);
            }
            if (Selected is Circle)
                e.Graphics.DrawEllipse(new Pen(Color.Black), new Rectangle(((Circle)Selected).center.X - ((Circle)Selected).radius + 1, ((Circle)Selected).center.Y - ((Circle)Selected).radius + 1, 2 * ((Circle)Selected).radius, 2 * ((Circle)Selected).radius));

        }
        

        private void DrawingField_MouseDown(object sender, MouseEventArgs e)
        {
            switch(CurrentMode)
            {
                case modes.Delete:
                    Circle sc1 = Selected as Circle;
                    if (sc1 != null)
                    {
                        if (sc1.IsInside(e.Location))
                        {
                            circles.Remove(sc1);
                            Selected = null;
                        }
                    }
                    Polygon p2 = Selected as Polygon;
                    if (p2 != null)
                    {
                        bool Found2 = false;
                        for (int i = 0; i < p2.points.Count && !Found2; i++)
                        {
                            if ((Math.Abs(e.X - p2.points[i].X) < 10 && Math.Abs(e.Y - p2.points[i].Y) < 10))
                            {
                                Found2 = true;
                                if (p2.points.Count == 3) 
                                {
                                    polygons.Remove(p2);
                                    Selected = null;
                                }
                                else
                                {
                                    p2.points.RemoveAt(i);
                                }
                            }
                        }
                        if(!Found2 && p2.IsInside(e.Location))
                        {
                            polygons.Remove(p2);
                            Selected = null;
                        }
                    }
                    break;
                case modes.AddCircle:
                    // can undo = false
                    IsMouseDown = true;
                    circles.Add(new Circle(e.Location, 1));
                    break;
                case modes.AddPolygon:
                    IsMouseDown = true;
                    CompletePoly.Enabled = true;
                    if (polygons.Count == 0 || polygons[polygons.Count - 1].Completed)
                        polygons.Add(new Polygon(e.Location));
                    polygons[polygons.Count - 1].points.Add(e.Location);
                    break;
                case modes.Select:
                    Selected = null;
                    bool Found = false;
                    for (int i = 0; i < polygons.Count && !Found; i++)
                    {
                        if (polygons[i].IsInside(e.Location))
                        {
                            Found = true;
                            Selected = polygons[i];
                        }
                    }
                    for (int i = 0; i < circles.Count && !Found; i++)
                    {
                        if(circles[i].IsInside(e.Location))
                        {
                            Found = true;
                            Selected = circles[i];
                        }
                    }
                    //for dla polygon
                    break;
                case modes.Move:
                    IsMouseDown = true;
                    CurrentMoveMode = moveModes.None;
                    Circle sc = Selected as Circle;
                    if (sc != null)
                    {
                        if ((Math.Abs(e.X - sc.center.X) < 10 && Math.Abs(e.Y - sc.center.Y) < 10))
                        {
                            CurrentMoveMode = moveModes.Point;
                        }
                        else
                        {
                            CurrentMoveMode = moveModes.Radius;
                        }
                    }
                    Polygon p = Selected as Polygon;
                    if (p != null)
                    {
                        bool Found1 = false;
                        for (int i = 0; i < p.points.Count && !Found1; i++) //przesuwanie wierzcholka
                        {
                            if ((Math.Abs(e.X - p.points[i].X) < 10 && Math.Abs(e.Y - p.points[i].Y) < 10))
                            {
                                Found1 = true;
                                CurrentMoveMode = moveModes.Point;
                                WhichPoint = i;
                            }
                        }
                        if (!Found1)
                        {
                            bool FoundEdge = false;
                            for (int i = 0; i < p.points.Count && !FoundEdge; i++) //przesuwanie krawedzi
                            {
                                if (Math.Abs(
                                    Math.Sqrt(
                                    (e.X - p.points[i].X) * (e.X - p.points[i].X) + (e.Y - p.points[i].Y) * (e.Y - p.points[i].Y)
                                    ) + Math.Sqrt(
                                    (e.X - p.points[(i + 1) % p.points.Count].X) * (e.X - p.points[(i + 1) % p.points.Count].X) +
                                    (e.Y - p.points[(i + 1) % p.points.Count].Y) * (e.Y - p.points[(i + 1) % p.points.Count].Y)
                                    ) - Math.Sqrt(
                                    (p.points[i].X - p.points[(i + 1) % p.points.Count].X) * (p.points[i].X - p.points[(i + 1) % p.points.Count].X) +
                                    (p.points[i].Y - p.points[(i + 1) % p.points.Count].Y) * (p.points[i].Y - p.points[(i + 1) % p.points.Count].Y)
                                    )
                                    ) < 10)
                                {
                                    FoundEdge = true;
                                    CurrentMoveMode = moveModes.Edge;
                                    WhichPoint = i;
                                }
                            }
                            if (!FoundEdge) //przesuwanie wielokata
                            {
                                CurrentMoveMode = moveModes.Polygon;
                            }
                        }
                    }
                    break;
                case modes.AddVertice:
                    Polygon p1 = Selected as Polygon;
                    if (p1 != null)
                    {
                        bool FoundEdge = false;
                        for (int i = 0; i < p1.points.Count && !FoundEdge; i++)
                        {
                            int a = (int)Math.Abs(
                                Math.Sqrt(
                                (e.X - p1.points[i].X) * (e.X - p1.points[i].X) + (e.Y - p1.points[i].Y) * (e.Y - p1.points[i].Y)
                                ) + Math.Sqrt(
                                (e.X - p1.points[(i + 1) % p1.points.Count].X) * (e.X - p1.points[(i + 1) % p1.points.Count].X) +
                                (e.Y - p1.points[(i + 1) % p1.points.Count].Y) * (e.Y - p1.points[(i + 1) % p1.points.Count].Y)
                                ) - Math.Sqrt(
                                (p1.points[i].X - p1.points[(i + 1) % p1.points.Count].X) * (p1.points[i].X - p1.points[(i + 1) % p1.points.Count].X) +
                                (p1.points[i].Y - p1.points[(i + 1) % p1.points.Count].Y) * (p1.points[i].Y - p1.points[(i + 1) % p1.points.Count].Y)
                                )
                                );
                            if (a < 10)
                            {
                                FoundEdge = true;
                                p1.points.Insert(i + 1, e.Location);
                            }
                        }
                    }
                    break;
            }
            Refresh();
        }

        private void DrawingField_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                switch (CurrentMode)
                {
                    case modes.AddCircle:
                        if (circles.Count > 0)
                        {
                            Circle c = circles[circles.Count - 1];
                            c.radius = (int)Math.Sqrt((c.center.X - e.X) * (c.center.X - e.X) + (c.center.Y - e.Y) * (c.center.Y - e.Y));
                        }
                        break;
                    case modes.AddPolygon:
                        polygons[polygons.Count - 1].points[polygons[polygons.Count - 1].points.Count - 1] = e.Location;
                        break;
                    case modes.Move:
                        Circle sc = Selected as Circle;
                        if (sc != null)
                        {
                            switch(CurrentMoveMode)
                            {
                                case moveModes.Point:
                                    sc.center = e.Location;
                                    break;
                                case moveModes.Radius:
                                    sc.radius = (int)Math.Sqrt((sc.center.X - e.X) * (sc.center.X - e.X) + (sc.center.Y - e.Y) * (sc.center.Y - e.Y));
                                    break;
                            }
                        }
                        Polygon p = Selected as Polygon;
                        if (p != null)
                        {
                            switch(CurrentMoveMode)
                            {
                                case moveModes.Point:
                                    p.points[WhichPoint] = e.Location;
                                    break;
                                case moveModes.Edge:
                                    Point middle = new Point((p.points[WhichPoint].X + p.points[(WhichPoint + 1) % p.points.Count].X) / 2, (p.points[WhichPoint].Y + p.points[(WhichPoint + 1) % p.points.Count].Y) / 2);
                                    p.points[WhichPoint] = new Point(p.points[WhichPoint].X + (e.X - middle.X), p.points[WhichPoint].Y + (e.Y - middle.Y));
                                    p.points[(WhichPoint + 1) % p.points.Count] = new Point(p.points[(WhichPoint + 1) % p.points.Count].X + (e.X - middle.X), p.points[(WhichPoint + 1) % p.points.Count].Y + (e.Y - middle.Y));
                                    break;
                                case moveModes.Polygon:
                                    Point middle1 = p.Middle();
                                    for (int i = 0; i < p.points.Count; i++)
                                    {
                                        p.points[i] = new Point(p.points[i].X + (e.X - middle1.X), p.points[i].Y + (e.Y - middle1.Y));
                                    }
                                    break;
                            }
                        }
                        break;
                }
                Refresh();
            }
        }

        private void DrawingField_MouseUp(object sender, MouseEventArgs e)
        {
            switch (CurrentMode)
            {
                case modes.AddCircle:
                    IsMouseDown = false;
                    break;
                case modes.AddPolygon:
                    IsMouseDown = false;
                    break;
                case modes.Move:
                    IsMouseDown = false;
                    break;
            }
            Refresh();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            CurrentMode = modes.Select;
        }

        private void Circle_Click(object sender, EventArgs e)
        {
            CurrentMode = modes.AddCircle;
        }

        private void Undo_Click(object sender, EventArgs e)
        {

        }

        private void Redo_Click(object sender, EventArgs e)
        {

        }

        private void Polygon_Click(object sender, EventArgs e)
        {
            CurrentMode = modes.AddPolygon;
        }

        private void DrawingField_MouseLeave(object sender, EventArgs e)
        {
            IsMouseDown = false;
            Refresh();
        }

        private void CompletePoly_Click(object sender, EventArgs e)
        {
            if(polygons.Count!=0)
            {
                polygons[polygons.Count - 1].Completed = true;
            }
            CompletePoly.Enabled = false;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            CurrentMode = modes.Delete;
        }

        private void Move_Click(object sender, EventArgs e)
        {
            CurrentMode = modes.Move;
        }

        private void AddVertice_Click(object sender, EventArgs e)
        {
            CurrentMode = modes.AddVertice;
        }
    }
}
