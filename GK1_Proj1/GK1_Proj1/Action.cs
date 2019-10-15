using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK1_Proj1
{
    class Action
    {
        bool IsPolygon;
        bool IsFirst;
        Point p;
        int radius;
        string name;
        public Action(bool IsPolygon, Point p, string name, bool IsFirst = false)
        {
            this.p = p;
            this.IsPolygon = IsPolygon;
            this.name = name;
            this.IsFirst = IsFirst;
        }

        public void Undo(List<Polygon> lp, List<Circle> lc)
        {
            
        }

        public void Redo(List<Polygon> lp, List<Circle> lc)
        {
            if(IsPolygon)
            {
                if(IsFirst)
                {
                    lp?.Add(new Polygon(p));
                }
                else
                {
                    
                }
            }
            else
            {
                lc?.Add(new Circle(p, radius));
            }
        }
        public override string ToString()
        {
            return name;
        }
    }
}
