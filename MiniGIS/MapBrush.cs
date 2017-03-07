using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MiniGIS
{
    public class MapBrush : MapObject
    {
        private int _pattern;
        private Color _foreColor, _backColor;

        public int Pattern
        {
            get
            {
                return _pattern;
            }
        }

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
        }

        public Color BackColor
        {
            get
            {
                return _backColor;
            }
        }

        public MapBrush(int pattern, int foreColor, int backColor = -1)
        {
            _pattern = pattern;
            _foreColor = IntToColor(foreColor);
            if (backColor != -1)
            {
                _backColor = IntToColor(backColor);
            }
            else
            {
                _backColor = Color.White;
            }
        }
    }
}
