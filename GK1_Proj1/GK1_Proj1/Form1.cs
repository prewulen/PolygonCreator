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

        bool IsMouseDown = false;
        enum modes {Select, Edit, AddPolygon, AddCircle}
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
            
            foreach(Polygon p in polygons)
            {
                //draw poly
                for (int i = 0; i < p.points.Count; i++)
                {
                    e.Graphics.DrawLine(new Pen(Color.Red), p.points[i], p.points[(i + 1) % p.points.Count]);
                }
            }
            foreach(Circle c in circles)
            {
                //draw circle
                e.Graphics.DrawEllipse(new Pen(Color.Red), new Rectangle(c.center.X - c.radius, c.center.Y - c.radius, 2 * c.radius, 2 * c.radius));
                e.Graphics.FillRectangle(Brushes.Red, c.center.X, c.center.Y, 1, 1);
            }
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
    }
}
