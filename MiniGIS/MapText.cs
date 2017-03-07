using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public class MapText : MapObject
    {
        private string _text;
        private MapRectangle _rect;

        public MapText(string text, MapRectangle rect)
        {
            _priority = 5;
            _text = text;
            _rect = rect;
        }

        public MapRectangle MapRectangle
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public override double GetDistance(MapPoint point)
        {
            return _rect.Point1.GetDistance(point);
        }

        public override void Draw(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center, bool picked = false)
        {
            if (Picked)
            {
                picked = true;
            }
            MapRectangle screen = new MapRectangle(_rect.Point1.MapToScreen(controlWidth, controlHeight, scale, center),
                                                    _rect.Point2.MapToScreen(controlWidth, controlHeight, scale, center));
            Font font;
            if (picked)
            {
                font = new Font("MapInfo Symbols", 12, FontStyle.Bold);
            }
            else
            {
                font = new Font("MapInfo Symbols", 12);
            }
            g.DrawString(_text, font, new SolidBrush(Color.Black), new RectangleF(new PointF((float)screen.Point1.X, (float)screen.Point1.Y),
                                                                                                            new SizeF((float)(screen.Point2.X - screen.Point1.X), (float)(screen.Point2.Y - screen.Point1.Y))));
        }
    }
}
