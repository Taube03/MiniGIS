using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MiniGIS
{
    public class MapPolygon : MapObject
    {
        private List<MapPoint> _points;
        private List<PointF> _fpoints;

        public void deletePoint(int id)
        {
            if (0 <= id && id < _points.Count)
            {
                _points.RemoveAt(id);
                _fpoints.RemoveAt(id);
            }
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

        public MapPolygon()
        {
            _points = new List<MapPoint>();
            _fpoints = new List<PointF>();
        }

        public void AddPoint(MapPoint point)
        {
            _points.Add(point);
        }

        public void DeleteAllPoints()
        {
            _points.Clear();
            _fpoints.Clear();
        }

        private bool InterSect(MapLine l, MapPoint p)
        {
            const double eps = 1e-9;
            if (l.Point1.GetDistance(l.Point2) <= eps)
            {
                return false;
            }
            if (l.GetDistance(p) <= eps)
            {
                return false;
            }
            if (Math.Abs(l.Point1.Y - l.Point2.Y) < eps)
            {
                return false;
            }
            if (Math.Min(l.Point1.Y, l.Point2.Y) + eps < p.Y && p.Y - eps < Math.Max(l.Point1.Y, l.Point2.Y))
            {
                MapPoint p1, p2;
                if (l.Point1.Y < l.Point2.Y)
                {
                    p1 = l.Point2;
                    p2 = l.Point1;
                }
                else
                {
                    p1 = l.Point1;
                    p2 = l.Point2;
                }
                return (p2.X + ((p.Y - p2.Y) / (p1.Y - p2.Y) * (p1.X - p2.X)) > p.X);
            }
            else
            {
                return false;
            }
        }

        private bool IsInner(MapPoint point)
        {
            bool inner = false;
            for (int i = 0; i + 1 < _points.Count; i++)
            {
                MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
                if (InterSect(line, point))
                {
                    inner = !inner;
                }
            }
            if (_points.Count >= 3)
            {
                MapLine line = new MapLine(_points[_points.Count - 1].X, _points[_points.Count - 1].Y, _points[0].X, _points[0].Y);
                if (InterSect(line, point))
                {
                    inner = !inner;
                }
            }
            return inner;
        }

        public override double GetDistance(MapPoint point)
        {
            if (IsInner(point))
            {
                return 0.0;
            }
            if (_points.Count < 3)
            {
                return 1e9;
            }
            MapLine line = new MapLine(_points[0].X, _points[0].Y, _points[_points.Count - 1].X, _points[_points.Count - 1].Y);
            double res = line.GetDistance(point);
            for (int i = 0; i + 1 < _points.Count; i++)
            {
                MapLine l = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
                res = Math.Min(res, l.GetDistance(point));
            }
            return res;
        }

        private void UpdateCoordinates(int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            _fpoints.Clear();
            for (int i = 0; i < _points.Count; i++)
            {
                MapPoint screen = _points[i].MapToScreen(controlWidth, controlHeight, scale, center);
                _fpoints.Add(new PointF((float)screen.X, (float)screen.Y));
            }
        }

        override public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, MapPen mapPen, MapBrush brush, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            if (brush != null && brush.Pattern != 1)
            {
                //g.FillPolygon(new SolidBrush(brush.BackColor), _fpoints.ToArray());
                Brush hbrush;
                switch (brush.Pattern)
                {
                    case 2:
                        hbrush = new SolidBrush(brush.ForeColor);
                        break;
                    case 3:
                        hbrush = new HatchBrush(HatchStyle.DarkHorizontal, brush.BackColor, brush.ForeColor);
                        break;
                    case 4:
                        hbrush = new HatchBrush(HatchStyle.DarkVertical, brush.BackColor, brush.ForeColor);
                        break;
                    case 5:
                        hbrush = new HatchBrush(HatchStyle.DarkUpwardDiagonal, brush.BackColor, brush.ForeColor);
                        break;
                    case 6:
                        hbrush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, brush.BackColor, brush.ForeColor);
                        break;
                    default:
                        hbrush = new SolidBrush(brush.BackColor);
                        break;
                }
                UpdateCoordinates(controlWidth, controlHeight, scale, center);
                g.FillPolygon(hbrush, _fpoints.ToArray());
            }
            for (int i = 0; i + 1 < _points.Count; i++)
            {
                MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
                line.Draw(g, controlWidth, controlHeight, scale, center, mapPen, picked);
            }
            if (_points.Count >= 3)
            {
                MapLine line = new MapLine(_points[_points.Count - 1].X, _points[_points.Count - 1].Y, _points[0].X, _points[0].Y);
                line.Draw(g, controlWidth, controlHeight, scale, center, mapPen, picked);
            }
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

        //override public void Draw(Graphics g, double controlWidth, double controlHeight, double scale, MapPoint center)
        //{
        //    if (_pen != null)
        //    {
        //        for (int i = 0; i + 1 < _points.Count; i++)
        //        {
        //            MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
        //            line.Draw(g, controlWidth, controlHeight, scale, center, _pen);
        //        }
        //        if (_points.Count >= 3)
        //        {
        //            MapLine line = new MapLine(_points[_points.Count - 1].X, _points[_points.Count - 1].Y, _points[0].X, _points[0].Y);
        //            line.Draw(g, controlWidth, controlHeight, scale, center, _pen);
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i + 1 < _points.Count; i++)
        //        {
        //            MapLine line = new MapLine(_points[i].X, _points[i].Y, _points[i + 1].X, _points[i + 1].Y);
        //            line.Draw(g, controlWidth, controlHeight, scale, center);
        //        }
        //        if (_points.Count >= 3)
        //        {
        //            MapLine line = new MapLine(_points[_points.Count - 1].X, _points[_points.Count - 1].Y, _points[0].X, _points[0].Y);
        //            line.Draw(g, controlWidth, controlHeight, scale, center);
        //        }
        //    }
        //}
    }
}
