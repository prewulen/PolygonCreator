﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Point centerB, centerA;
        int radiusB, radiusA;
        int i;
        public CircleAction(Form1.modes m, Form1.moveModes mm, int i, Point cB, Point cA, int rB, int rA)
        {
            Name = "cirlce action";
            mode = m;
            moveMode = mm;
            this.i = i;
            centerB = cB;
            centerA = cA;
            radiusA = rA;
            radiusB = rB;
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
                    circles.Add(new Circle(centerB, radiusB));
                    break;
            }
        }
        public override void Redo(List<Polygon> polygons, List<Circle> circles)
        {
            switch (mode)
            {
                case Form1.modes.AddCircle:
                    circles.Add(new Circle(centerB, radiusB));
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
}
