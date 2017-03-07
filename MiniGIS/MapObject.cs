using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public abstract class MapObject
    {
        protected MapPen _pen;
        protected MapBrush _brush;
        protected int _priority;
        protected const double pickLimit = 5.0;

		public int Priority
		{
			get
			{
				return _priority;
			}
		}

        public bool Picked
        {
            get;
            set;
        }

        public int GetMapObjectType
        {
            get
            {
                return _priority;
            }
        }

        virtual public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, bool picked = false)
        {
        }

        virtual public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, MapPen pen, bool picked = false)
        {
        }

        virtual public void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, MapPen pen, MapBrush brush, bool picked = false)
        {
        }

        virtual public bool IsPicked(Point screen, int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            MapPoint scr = new MapPoint(screen.X, screen.Y);
            MapPoint mouse = scr.ScreenToMap(controlWidth, controlHeight, scale, center);
            return GetDistance(mouse) / scale <= pickLimit;
        }

        public void PickOut()
        {
            Picked = true;
        }

        public void ResetPick()
        {
            Picked = false;
        }

        virtual public double GetDistance(MapPoint point)
        {
            return 0.0;
        }

        public MapObject()
        {
            _priority = 100;
            Picked = false;
        }

        public void SetPen(MapPen mapPen)
        {
            _pen = mapPen;
        }

        public void SetBrush(MapBrush mapBrush)
        {
            _brush = mapBrush;
        }

        protected Color IntToColor(int argb)
        {
            var r = ((argb >> 16) & 0xff); 
            var g = ((argb >> 8) & 0xff); 
            var b = (argb & 0xff); 
            return System.Drawing.Color.FromArgb(r, g, b);
            //return Color.FromArgb(255, Color.FromArgb(Convert.ToInt32(color)));
        }

        public int CompareTo(MapObject m)
        {
            if (_priority < m._priority)
            {
                return -1;
            }
            else
            {
                if (_priority == m._priority)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
