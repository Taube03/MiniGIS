using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public class MapRegion : MapObject
    {
        private List<MapPolygon> _polygons;
        private MapPoint _center;

        public MapRegion()
        {
            _priority = 1;
            _polygons = new List<MapPolygon>();
        }

        public MapPoint MapPoint
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public MapPolygon MapPolygon
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void AddPolygon(MapPolygon polygon)
        {
            _polygons.Add(polygon);
        }

        public void DeletePolygon(int id)
        {
            if (0 <= id && id < _polygons.Count)
            {
                _polygons.RemoveAt(id);
            }
        }

        public void DeleteAllPolygons()
        {
            _polygons.Clear();
        }

        public void SetCenter(MapPoint center)
        {
            _center = center;
        }

        public double Length()
        {
            double res = 0.0;
            for (int i = 0; i < _polygons.Count; i++)
            {
                res += _polygons[i].Length();
            }
            return res;
        }

        public MapPolygon GetNearestPolygon(MapPoint p)
        {
            double dist = 1e9;
            MapPolygon res = null;
            for (int i = 0; i < _polygons.Count; i++)
            {
                double curDist = _polygons[i].GetDistance(p);
                if (curDist < dist)
                {
                    dist = curDist;
                    res = _polygons[i];
                }
            }
            return res;
        }

        //override public void Draw(Graphics g, double controlWidth, double controlHeight, double scale, MapPoint center, MapPen mapPen)
        //{
        //    for (int i = 0; i < _polygons.Count; i++)
        //    {
        //        _polygons[i].Draw(g, controlWidth, controlHeight, scale, center, mapPen);
        //    }
        //}

        public override double GetDistance(MapPoint point)
        {
            double res = 1e9;
            for (int i = 0; i < _polygons.Count; i++)
            {
                res = Math.Min(_polygons[i].GetDistance(point), res);
            }
            return res;
        }

        override public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            for (int i = 0; i < _polygons.Count; i++)
            {
                _polygons[i].Draw(g, controlWidth, controlHeight, scale, center, _pen, _brush, picked);
            }
        }
    }
}
