using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MiniGIS
{
    public class MapLine : MapObject
    {
        private MapPoint _point1, _point2;

        public MapPoint Point1
        {
            get
            {
                return _point1;
            }
        }

        public MapPoint Point2
        {
            get
            {
                return _point2;
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

        public MapLine(double x1, double y1, double x2, double y2)
        {
            _priority = 3;
            _point1 = new MapPoint(x1, y1);
            _point2 = new MapPoint(x2, y2);
        }

        public double Length()
        {
            return _point1.GetDistance(_point2);
        }

        override public double GetDistance(MapPoint point)
        {
            double a = _point2.Y - _point1.Y;
            double b = _point1.X - _point2.X;
            double c = -1.0 * (a * _point1.X + b * _point1.Y);
            if (a * a + b * b == 0.0)
            {
                return _point1.GetDistance(point);
            }
            double a2 = -b;
            double b2 = a;
            double c2 = -1.0 * (a2 * point.X + b2 * point.Y);

            double d = a * b2 - b * a2;
            double d1 = b2 * (-c) - b * (-c2);
            double d2 = (-c2) * a - (-c) * a2;
            double x = d1 / d;
            double y = d2 / d;

            bool onLine = true;
            if (_point1.X != _point2.X)
            {
                if (x < Math.Min(_point1.X, _point2.X) || x > Math.Max(_point1.X, _point2.X))
                {
                    onLine = false;
                }
            }
            else
            {
                if (y < Math.Min(_point1.Y, _point2.Y) || y > Math.Max(_point1.Y, _point2.Y))
                {
                    onLine = false;
                }
            }
            if (!onLine)
            {
                return Math.Min(_point1.GetDistance(point), _point2.GetDistance(point));
            }
            else
            {
                return point.GetDistance(new MapPoint(x, y));
            }
        }

        override public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            MapPoint screen1 = _point1.MapToScreen(controlWidth, controlHeight, scale, center);
            MapPoint screen2 = _point2.MapToScreen(controlWidth, controlHeight, scale, center);
            int c = 1;
            if (picked)
            {
                c = 2;
            }

            Pen pen;
            if (_pen != null)
            {
                switch (_pen.Pattern)
                {
                    case 3:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dot;
                        break;
                    case 4:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dot;
                        break;
                    case 5:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 6:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 7:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 8:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 9:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    default:
                        pen = new Pen(new SolidBrush(_pen.Color), c * _pen.Width);
                        break;
                }
            }
            else
            {
                pen = new Pen(new SolidBrush(Color.Black), 2f * c);
            }
            g.DrawLine(pen, (float)screen1.X,
                            (float)screen1.Y,
                            (float)screen2.X,
                            (float)screen2.Y);
        }

        override public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, MapPen mapPen, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            MapPoint screen1 = _point1.MapToScreen(controlWidth, controlHeight, scale, center);
            MapPoint screen2 = _point2.MapToScreen(controlWidth, controlHeight, scale, center);
            if (mapPen == null && _pen != null)
            {
                mapPen = _pen;
            }
            int c = 1;
            if (picked)
            {
                c = 2;
            }
            Pen pen;
            if (mapPen != null)
            {
                switch (mapPen.Pattern)
                {
                    case 3:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dot;
                        break;
                    case 4:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dot;
                        break;
                    case 5:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 6:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 7:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 8:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    case 9:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        pen.DashStyle = DashStyle.Dash;
                        break;
                    default:
                        pen = new Pen(new SolidBrush(mapPen.Color), c * mapPen.Width);
                        break;
                }
            }
            else
            {
                pen = new Pen(new SolidBrush(Color.Black), 2f * c);
            }
            g.DrawLine(pen, (float)screen1.X,
                            (float)screen1.Y,
                            (float)screen2.X,
                            (float)screen2.Y);
        }
    }
}
