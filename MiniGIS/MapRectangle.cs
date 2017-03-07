using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniGIS
{
    public class MapRectangle : MapObject
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

        public MapRectangle(MapPoint point1, MapPoint point2)
        {
            _point1 = point1;
            _point2 = point2;
        }
    }
}
