using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGIS
{
    public class MapRoundRectangle : MapRectangle
    {
        private double _rounding;

        MapRoundRectangle(MapPoint point1, MapPoint point2, double rounding) : base(point1, point2)
        {
            _rounding = rounding;
        }


    }
}
