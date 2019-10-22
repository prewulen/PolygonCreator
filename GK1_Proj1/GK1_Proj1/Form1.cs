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
        BindingList<Action> actions = new BindingList<Action>();
        public Figure Selected;
        int SelectedItem = 0;
        bool CanUndo = true;

        SolidBrush SelectedColor = new SolidBrush(Color.Black);

        bool IsMouseDown = false;
        public enum modes {Select, Edit, AddPolygon, AddVertice, AddCircle, Move, Delete}
        public enum moveModes { Point, Edge, Polygon, Radius, None }
        modes CurrentMode;
        moveModes CurrentMoveMode;
        int WhichPoint;

        public Form1()
        {
            InitializeComponent();

            CurrentMode = modes.Select;
            ColorB.BackColor = colorDialog1.Color;
            actions.Add(new Action("<Poczatek>"));
            listBox1.DataSource = actions;
        }

        private void ZakonczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UndoOneAction()
        {
            if (listBox1.SelectedIndex == 0) return;
            listBox1.SelectedIndex -= 1;
        }

        private void RedoOneAction()
        {
            if (listBox1.SelectedIndex == actions.Count - 1) return;
            listBox1.SelectedIndex += 1;
        }

        private void CofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoOneAction();
        }

        private void PonowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RedoOneAction();
        }

        void circleBresenham(int xc, int yc, int R, SolidBrush sb, Graphics g) //http://www.codemiles.com/c-examples/circle-drawing-using-bresenham-t10198.html
        {
            int x = 0, y = R;
            int d = 1 - R;
            drawVertice(new Point(xc,yc), sb, g);
            Draw8Points(xc, yc, x, y, sb, g);
            while (x < y)
            {
                if (d < 0)
                    d += 2 * x + 2;
                else
                {
                    d += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                Draw8Points(xc, yc, x, y, sb, g);
            }
        }

        void Draw8Points(int xc, int yc, int a, int b, SolidBrush sb, Graphics g)
        {
            g.FillRectangle(sb, xc + a, yc + b, 1, 1);
            g.FillRectangle(sb, xc - a, yc + b, 1, 1);
            g.FillRectangle(sb, xc - a, yc - b, 1, 1);
            g.FillRectangle(sb, xc + a, yc - b, 1, 1);
            g.FillRectangle(sb, xc + b, yc + a, 1, 1);
            g.FillRectangle(sb, xc - b, yc + a, 1, 1);
            g.FillRectangle(sb, xc - b, yc - a, 1, 1);
            g.FillRectangle(sb, xc + b, yc - a, 1, 1);
        }

        public void line(int x, int y, int x2, int y2, SolidBrush sb, Graphics g) //https://stackoverflow.com/questions/11678693/all-cases-covered-bresenhams-line-algorithm
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                g.FillRectangle(sb, x, y, 1, 1);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        void drawVertice(Point p, SolidBrush sb, Graphics g)
        {
            for (int i = -3; i <= 3; i++)
                g.FillRectangle(sb, p.X - i, p.Y - 3, 1, 1);
            for (int i = -3; i <= 3; i++)
                g.FillRectangle(sb, p.X - i, p.Y + 3, 1, 1);
            for (int i = -2; i <= 2; i++)
                g.FillRectangle(sb, p.X - 3, p.Y - i, 1, 1);
            for (int i = -3; i <= 3; i++)
                g.FillRectangle(sb, p.X + 3, p.Y - i, 1, 1);
        }

        private void DrawingField_Paint(object sender, PaintEventArgs e)
        {
            
            foreach (Polygon p in polygons)
            {
                //draw poly
                for (int i = 0; i < p.points.Count; i++)
                    drawVertice(p.points[i], p.SolidBrush, e.Graphics);

                if (p.Completed)
                {
                    for (int i = 0; i < p.points.Count; i++)
                    {
                        line(p.points[i].X, p.points[i].Y, p.points[(i + 1) % p.points.Count].X, p.points[(i + 1) % p.points.Count].Y, p.SolidBrush, e.Graphics);
                    }
                }
                else
                {
                    for (int i = 0; i < p.points.Count - 1; i++)
                    {
                        line(p.points[i].X, p.points[i].Y, p.points[(i + 1) % p.points.Count].X, p.points[(i + 1) % p.points.Count].Y, p.SolidBrush, e.Graphics);
                    }
                }
            }
            if (Selected is Polygon)
            {
                SolidBrush inv = new SolidBrush(Color.FromArgb(255 - Selected.SolidBrush.Color.R, 255 - Selected.SolidBrush.Color.G, 255 - Selected.SolidBrush.Color.B));
                for (int i = 0; i < ((Polygon)Selected).points.Count; i++)
                {
                    line(((Polygon)Selected).points[i].X, ((Polygon)Selected).points[i].Y, ((Polygon)Selected).points[(i + 1) % ((Polygon)Selected).points.Count].X, ((Polygon)Selected).points[(i + 1) % ((Polygon)Selected).points.Count].Y, inv, e.Graphics);
                }
            }

            foreach (Circle c in circles)
            {
                circleBresenham(c.center.X, c.center.Y, c.radius, c.SolidBrush, e.Graphics);
            }
            if (Selected is Circle)
            {
                SolidBrush inv = new SolidBrush(Color.FromArgb(255 - Selected.SolidBrush.Color.R, 255 - Selected.SolidBrush.Color.G, 255 - Selected.SolidBrush.Color.B));
                circleBresenham(((Circle)Selected).center.X, ((Circle)Selected).center.Y, ((Circle)Selected).radius, inv, e.Graphics);
            }

        }
        
        private void AddAction(Action a)
        {
            for (int i = actions.Count - 1; i > SelectedItem; i--)
            {
                actions.RemoveAt(i);
            }
            CanUndo = false;
            actions.Add(a);
            listBox1.SelectedIndex = actions.Count - 1;
            CanUndo = true;
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
                            CircleAction ca1 = new CircleAction("Usuniecie kola", modes.Delete, moveModes.None, sc1.SolidBrush, circles.FindIndex(cc => sc1 == cc), sc1.center, Point.Empty, sc1.radius, 0);
                            AddAction(ca1);
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
                                    AddAction(new PolygonDeleteAction("Usuniecie wielokata", polygons.FindIndex(cc1 => p2 == cc1), p2.SolidBrush, p2.points));
                                    polygons.Remove(p2);
                                    Selected = null;
                                }
                                else
                                {
                                    AddAction(new PolygonDeleteVerticeAction("Usuniecie wierzcholka", polygons.FindIndex(cc2 => p2 == cc2), i, p2.points[i], Point.Empty));
                                    p2.points.RemoveAt(i);
                                }
                            }
                        }
                        if(!Found2 && p2.IsInside(e.Location))
                        {
                            AddAction(new PolygonDeleteAction("Usuniecie wielokata", polygons.FindIndex(cc3 => p2 == cc3), p2.SolidBrush, p2.points));
                            polygons.Remove(p2);
                            Selected = null;
                        }
                    }
                    break;
                case modes.AddCircle:
                    IsMouseDown = true;
                    circles.Add(new Circle(e.Location, 1) { SolidBrush = SelectedColor });
                    CircleAction ca = new CircleAction("Dodanie kola", modes.AddCircle, moveModes.None, SelectedColor, circles.Count - 1, Point.Empty, e.Location, 0, 1);
                    AddAction(ca);
                    break;
                case modes.AddPolygon:
                    IsMouseDown = true;
                    CompletePoly.Enabled = true;
                    if (polygons.Count == 0 || polygons[polygons.Count - 1].Completed)
                    {
                        polygons.Add(new Polygon(e.Location) { SolidBrush = SelectedColor });
                        AddAction(new PolygonCreatePolygonAction("Utworzenie wielokata", SelectedColor, Point.Empty, e.Location, CompletePoly, polygons.Count - 1));
                    }
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
                    break;
                case modes.Move:
                    IsMouseDown = true;
                    CurrentMoveMode = moveModes.None;
                    Circle sc = Selected as Circle;
                    if (sc != null)
                    {
                        if ((Math.Abs(e.X - sc.center.X) < 10 && Math.Abs(e.Y - sc.center.Y) < 10))
                        {
                            AddAction(new CircleAction("Przesuniecie kola", modes.Move, moveModes.Point, sc.SolidBrush, circles.FindIndex(cc4 => cc4 == sc), sc.center, Point.Empty, 0, 0));
                            CurrentMoveMode = moveModes.Point;
                        }
                        else
                        {
                            AddAction(new CircleAction("Zmiana promienia kola", modes.Move, moveModes.Radius, sc.SolidBrush, circles.FindIndex(cc5 => cc5 == sc), Point.Empty, Point.Empty, sc.radius, 0));
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
                                AddAction(new PolygonMovePointAction("Przesuniecie wierzcholka", polygons.FindIndex(cc6 => cc6 == p), i, p.points[i], Point.Empty));
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
                                    AddAction(new PolygonMoveEdgeAction("Przesuniecie krawedzi", polygons.FindIndex(cc7 => cc7 == p), i, p.points[i], Point.Empty, p.points[(i + 1) % p.points.Count], Point.Empty));
                                    CurrentMoveMode = moveModes.Edge;
                                    WhichPoint = i;
                                }
                            }
                            if (!FoundEdge) //przesuwanie wielokata
                            {
                                AddAction(new PolygonMoveWholeAction("Przesuniecie wielokata", polygons.FindIndex(cc8 => cc8 == p), p.Middle(), Point.Empty));
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
                                AddAction(new PolygonAddVerticeAction("Dodanie wierzcholka", polygons.FindIndex(cc9 => cc9 == p1), i + 1, Point.Empty, e.Location));
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
                                    ((CircleAction)actions[actions.Count - 1]).centerA = e.Location;
                                    sc.center = e.Location;
                                    break;
                                case moveModes.Radius:
                                    sc.radius = (int)Math.Sqrt((sc.center.X - e.X) * (sc.center.X - e.X) + (sc.center.Y - e.Y) * (sc.center.Y - e.Y));
                                    ((CircleAction)actions[actions.Count - 1]).radiusA = sc.radius;
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
                                    ((PolygonMovePointAction)actions[actions.Count - 1]).pointA = e.Location;
                                    break;
                                case moveModes.Edge:
                                    Point middle = new Point((p.points[WhichPoint].X + p.points[(WhichPoint + 1) % p.points.Count].X) / 2, (p.points[WhichPoint].Y + p.points[(WhichPoint + 1) % p.points.Count].Y) / 2);
                                    p.points[WhichPoint] = new Point(p.points[WhichPoint].X + (e.X - middle.X), p.points[WhichPoint].Y + (e.Y - middle.Y));
                                    p.points[(WhichPoint + 1) % p.points.Count] = new Point(p.points[(WhichPoint + 1) % p.points.Count].X + (e.X - middle.X), p.points[(WhichPoint + 1) % p.points.Count].Y + (e.Y - middle.Y));
                                    ((PolygonMoveEdgeAction)actions[actions.Count - 1]).point1A = p.points[WhichPoint];
                                    ((PolygonMoveEdgeAction)actions[actions.Count - 1]).point2A = p.points[(WhichPoint + 1) % p.points.Count];
                                    break;
                                case moveModes.Polygon:
                                    Point middle1 = p.Middle();
                                    for (int i = 0; i < p.points.Count; i++)
                                    {
                                        p.points[i] = new Point(p.points[i].X + (e.X - middle1.X), p.points[i].Y + (e.Y - middle1.Y));
                                    }
                                    ((PolygonMoveWholeAction)actions[actions.Count - 1]).middleA = middle1;
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
                    CircleAction ca = actions[actions.Count - 1] as CircleAction;
                    ca.radiusA = circles[circles.Count - 1].radius;
                    break;
                case modes.AddPolygon:
                    AddAction(new PolygonAddVerticeAction("Dodanie wierzcholka", polygons.Count - 1, polygons[polygons.Count - 1].points.Count - 1, Point.Empty, e.Location));
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
            ModeLabel.Text = "Tryb: Zaznaczanie";
            CurrentMode = modes.Select;
        }

        private void Circle_Click(object sender, EventArgs e)
        {
            ModeLabel.Text = "Tryb: Tworzenie kola";
            CurrentMode = modes.AddCircle;
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            UndoOneAction();
        }

        private void Redo_Click(object sender, EventArgs e)
        {
            RedoOneAction();
        }

        private void Polygon_Click(object sender, EventArgs e)
        {
            ModeLabel.Text = "Tryb: Tworzenie wielokata";
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
                if (polygons[polygons.Count - 1].points.Count >= 3)
                {
                    polygons[polygons.Count - 1].Completed = true;
                    AddAction(new PolygonCompleteAction("Sfinalizowany wielokat", polygons.Count - 1, CompletePoly));
                    CompletePoly.Enabled = false;
                }
            }
            DrawingField.Refresh();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ModeLabel.Text = "Tryb: Usuwanie";
            CurrentMode = modes.Delete;
        }

        private void Move_Click(object sender, EventArgs e)
        {
            ModeLabel.Text = "Tryb: Przesuwanie";
            CurrentMode = modes.Move;
        }

        private void AddVertice_Click(object sender, EventArgs e)
        {
            ModeLabel.Text = "Tryb: Dodawanie wierzcholka";
            CurrentMode = modes.AddVertice;
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (actions.Count == 0) return;
            if ((listBox1.SelectedIndex != SelectedItem || actions.Count == 0) && CanUndo)
            {
                Selected = null;
                if (listBox1.SelectedIndex > SelectedItem)
                {
                    for (int i = SelectedItem + 1; i <= listBox1.SelectedIndex; i++)
                    {
                        actions[i].Redo(polygons, circles);
                    }
                }
                else
                {
                    for (int i = SelectedItem; i > listBox1.SelectedIndex; i--)
                    {
                        actions[i].Undo(polygons, circles);
                    }
                }
            }
            SelectedItem = listBox1.SelectedIndex;
            DrawingField.Refresh();
        }

        private void ColorB_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = new SolidBrush(colorDialog1.Color);
                ColorB.BackColor = colorDialog1.Color;
            }
        }
    }
}
