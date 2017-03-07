using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGIS
{
    public class MapEllispe : MapRectangle
    {
        public MapEllispe(MapPoint point1, MapPoint point2)
            : base(point1, point2)
        {
        }
    }
}
