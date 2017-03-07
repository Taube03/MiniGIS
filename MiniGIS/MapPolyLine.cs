using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public class MapPolyLine : MapObject
    {
        private List <MapPoint> _points;
        private bool _smoothed;

        public MapPolyLine()
        {
            _priority = 2;
            _smoothed = false;
            _points = new List<MapPoint>();
            //_polyLines = new List<MapPolyLine>();
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

        public void DeletePoint(int id)
        {
            if (id >= 0 && id < _points.Count)
            {
                _points.RemoveAt(id);
            }
        }

        public void AddPoint(MapPoint point)
        {
            _points.Add(point);
        }

        public void DeleteAllPoints()
        {
            _points.Clear();
        }

        public void SetSmooth()
        {
            _smoothed = true;
        }

        public double Length()
        {
            double res = 0.0;
            for (int i = 0; i + 1 < _points.Count; i++)
            {
                MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
                res += line.Length();
            }
            return res;
        }

        public override double GetDistance(MapPoint point)
        {
            double res = 1e9;
            for (int i = 0; i + 1 < _points.Count; i++)
            {
                MapLine l = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
                res = Math.Min(res, l.GetDistance(point));
            }
            return res;
        }

        override public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            for (int i = 0; i + 1 < _points.Count; i++)
            {
                MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
                line.Draw(g, controlWidth, controlHeight, scale, center, _pen, picked);
            }
        }

        //override public void Draw(Graphics g, double controlWidth, double controlHeight, double scale, MapPoint center, MapPen mapPen)
        //{
        //    if (mapPen == null && _pen != null)
        //    {
        //        mapPen = _pen;
        //    }
        //    for (int i = 0; i < _polyLines.Count; i++)
        //    {
        //        _polyLines[i].Draw(g, controlWidth, controlHeight, scale, center, mapPen);
        //    }
        //    for (int i = 0; i + 1 < _points.Count; i++)
        //    {
        //        MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
        //        line.Draw(g, controlWidth, controlHeight, scale, center, mapPen);
        //    }
        //}
    }
}
