using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGIS
{
    class MapArc : MapEllispe
    {
        private double _angle1, _angle2;

        public MapArc(MapPoint point1, MapPoint point2, double angle1, double angle2)
            : base(point1, point2)
        {
            _angle1 = angle1;
            _angle2 = angle2;
        }
    }
}
