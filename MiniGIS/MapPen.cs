using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public class MapPen : MapObject
    {
        private int _width, _pattern;
        private Color _color;

        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Pattern
        {
            get
            {
                return _pattern;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
        }

        public MapPen(int width, int pattern, int color)
        {
            _width = width;
            _pattern = pattern;
            _color = IntToColor(color);
        }
    }
}
