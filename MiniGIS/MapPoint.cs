using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public class MapPoint : MapObject
    {
        private double _x, _y;
        private char _symbol;
        private Color _color;
        private int _size;


        public MapPoint ScreenToMap(int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            MapPoint d = new MapPoint(_x - controlWidth / 2.0, -1.0 * (_y - controlHeight / 2.0));
            d.X *= scale;
            d.Y *= scale;
            d.X += center.X;
            d.Y += center.Y;
            return d;
        }

        public MapPoint MapToScreen(int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            MapPoint d = new MapPoint(_x - center.X, -1.0 * (_y - center.Y));
            d.X /= scale;
            d.Y /= scale;
            d.X += controlWidth / 2.0;
            d.Y += controlHeight / 2.0;
            return d;
        }

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public MapPoint(double x, double y, int symbol, int color, int size) : base()
        {
            _priority = 4;
            _x = x;
            _y = y;
            _symbol = Convert.ToChar(symbol);
            _color = IntToColor(color);
            _size = size;
        }

        public MapPoint(double x, double y) : base()
        {
            _priority = 4;
            _x = x;
            _y = y;
            _symbol = '\0';
            _color = Color.Black;
            _size = 0;
        }

        public override double GetDistance(MapPoint point)
        {
            return Math.Sqrt(Math.Pow(_x - point.X, 2.0) + Math.Pow(_y - point.Y, 2.0));
        }

        public override void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            MapPoint screen = MapToScreen(controlWidth, controlHeight, scale, center);
            string symbol;
            if (_symbol > 0)
            {
                symbol = Convert.ToString(_symbol);
            }
            else
            {
                symbol = ".";
            }
            Brush brush = new SolidBrush(_color);
            Font font;
            if (picked)
            {
                font = new Font("MapInfo Symbols", 12f, FontStyle.Bold);
            }
            else
            {
                font = new Font("MapInfo Symbols", 12f);
            }
            g.DrawString(symbol, font, brush, new PointF((float)screen.X, (float)screen.Y));
        }
    }
}
