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
        enum modes {Select, Edit, AddPolygon, AddCircle, Move, Delete}
        modes CurrentMode;

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
                            if((Math.Abs(e.X - sc.center.X) < 10 && Math.Abs(e.Y - sc.center.Y) < 10))
                            {
                                sc.center = e.Location;
                            }
                            else
                            {
                                sc.radius = (int)Math.Sqrt((sc.center.X - e.X) * (sc.center.X - e.X) + (sc.center.Y - e.Y) * (sc.center.Y - e.Y));
                            }
                        }
                        Polygon p = Selected as Polygon;
                        if (p != null)
                        {
                            bool Found = false;
                            for (int i = 0; i < p.points.Count && !Found; i++)
                            {
                                if ((Math.Abs(e.X - p.points[i].X) < 10 && Math.Abs(e.Y - p.points[i].Y) < 10))
                                {
                                    Found = true;
                                    p.points[i] = e.Location;
                                }
                            }
                            if(!Found)
                            {
                                bool FoundEdge = false;
                                for (int i = 0; i < p.points.Count && !Found; i++)
                                {
                                    if (Math.Abs(
                                        Math.Sqrt(
                                        (e.X + p.points[i].X) * (e.X + p.points[i].X) + (e.Y + p.points[i].Y) * (e.Y + p.points[i].Y)
                                        ) + Math.Sqrt(
                                        (e.X + p.points[(i+1)%p.points.Count].X) * (e.X + p.points[(i + 1) % p.points.Count].X) + 
                                        (e.Y + p.points[(i + 1) % p.points.Count].Y) * (e.Y + p.points[(i + 1) % p.points.Count].Y)
                                        ) - Math.Sqrt(
                                        (p.points[i].X + p.points[(i + 1) % p.points.Count].X) * (p.points[i].X + p.points[(i + 1) % p.points.Count].X) +
                                        (p.points[i].Y + p.points[(i + 1) % p.points.Count].Y) * (p.points[i].Y + p.points[(i + 1) % p.points.Count].Y)
                                        )
                                        )<10)
                                    {
                                        
                                    }
                                }
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
                    //can undo = true
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
    }
}
