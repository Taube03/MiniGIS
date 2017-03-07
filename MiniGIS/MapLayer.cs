using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace MiniGIS
{
    public class MapLayer
    {
        public string name;
        
        private List<MapObject> _objects;
        private char _delimiter = ',';

        public bool Visible
        {
            get;
            set;
        }

        private double _minX, _maxX, _minY, _maxY;
        //private int _controlWidth, _controlHeight;
        //private MapPoint _center;
        //private double _scale;
        private MapObject _picked;

        public MapObject Picked
        {
            get
            {
                return _picked;
            }
        }

        //public double Scale
        //{
        //    get
        //    {
        //        return _scale;
        //    }
        //}

        //public MapPoint Center
        //{
        //    get
        //    {
        //        return _center;
        //    }
        //}

        public double MinX { get { return _minX; } }
        public double MaxX { get { return _maxX; } }
        public double MinY { get { return _minY; } }
        public double MaxY { get { return _maxY; } }

        //public void IncreaseScale(double c)
        //{
        //    _scale *= c;
        //}

        //public void Move(double dx, double dy)
        //{
        //    _center.X -= dx * _scale;
        //    _center.Y += dy * _scale;
        //}

        private void UpdateEdges(double x, double y)
        {
            _minX = Math.Min(_minX, x);
            _minY = Math.Min(_minY, y);
            _maxX = Math.Max(_maxX, x);
            _maxY = Math.Max(_maxY, y);
        }

        private MapPen ParsePen(string input)
        {
            input = input.Trim();
            input = input.Remove(0, 5);
            input = input.Remove(input.Length - 1);
            string[] pen;
            if (input.Contains(_delimiter))
            {
                pen = input.Split(_delimiter);
            }
            else
            {
                pen = input.Split(',');
            }
            return new MapPen(Convert.ToInt32(pen[0]),
                                    Convert.ToInt32(pen[1]),
                                    Convert.ToInt32(pen[2]));
        }

        private MapBrush ParseBrush(string input)
        {
            input = input.Trim();
            input = input.Remove(0, 7);
            input = input.Remove(input.Length - 1);
            string[] brush;
            if (input.Contains(_delimiter))
            {
                brush = input.Split(_delimiter);
            }
            else
            {
                brush = input.Split(',');
            }
            if (brush.Length > 2)
            {
                return new MapBrush(Convert.ToInt32(brush[0]),
                                    Convert.ToInt32(brush[1]),
                                    Convert.ToInt32(brush[2]));
            }
            else
            {
                return new MapBrush(Convert.ToInt32(brush[0]),
                                    Convert.ToInt32(brush[1]));
            }
        }

        private MapPoint ReadMapPoint(ref string cur, ref StreamReader sr)
        {
            string[] coord = cur.Split(' ');
            string next = sr.ReadLine();
            UpdateEdges(Convert.ToDouble(coord[1]), Convert.ToDouble(coord[2]));
            if (next.Contains("Symbol"))
            {
                next = next.Trim();
                next = next.Remove(0, 8);
                next = next.Remove(next.Length - 1);
                string[] symbol = next.Split(_delimiter);
                cur = sr.ReadLine();
                return new MapPoint(Convert.ToDouble(coord[1]),
                    Convert.ToDouble(coord[2]),
                    Convert.ToInt32(symbol[0]),
                    Convert.ToInt32(symbol[1]),
                    Convert.ToInt32(symbol[2]));
            }
            else
            {
                cur = next;
                return new MapPoint(Convert.ToDouble(coord[1]), Convert.ToDouble(coord[2]));
            }
        }

        private MapLine ReadMapLine(ref string cur, ref StreamReader sr)
        {
            if (cur.Length <= 4)
            {
                cur = sr.ReadLine();
            }
            else
            {
                cur = cur.Remove(0, 4);
                cur = cur.Trim();
            }
            string[] line = cur.Split(' ');
            cur = sr.ReadLine();
            UpdateEdges(Convert.ToDouble(line[0]), Convert.ToDouble(line[1]));
            UpdateEdges(Convert.ToDouble(line[2]), Convert.ToDouble(line[3]));
            MapLine mapLine = new MapLine(Convert.ToDouble(line[0]),
                                        Convert.ToDouble(line[1]),
                                        Convert.ToDouble(line[2]),
                                        Convert.ToDouble(line[3]));
            if (cur.Contains("Pen"))
            {
                mapLine.SetPen(ParsePen(cur));
                cur = sr.ReadLine();
            }
            return mapLine;
        }

        private MapPolyLine ReadMapPolyLine(ref string cur, ref StreamReader sr)
        {
            MapPolyLine polyLine = new MapPolyLine();
            if (cur.Length <= 5)
            {
                cur = sr.ReadLine();
            }
            else
            {
                cur = cur.Remove(0, 5);
                cur = cur.Trim();
            }
            int segments = Convert.ToInt32(cur);
            for (int i = 1; i <= segments; i++)
            {
                string point = sr.ReadLine();
                point = point.Trim();
                string[] coordinates = point.Split(' ');
                UpdateEdges(Convert.ToDouble(coordinates[0]), Convert.ToDouble(coordinates[1]));
                polyLine.AddPoint(new MapPoint(Convert.ToDouble(coordinates[0]),
                                            Convert.ToDouble(coordinates[1])));
            }
            while (true)
            {
                cur = sr.ReadLine();
                if (cur == null)
                {
                    break;
                }
                if (cur.Contains("Pen"))
                {
                    polyLine.SetPen(ParsePen(cur));
                }
                else if (cur.Contains("Smooth"))
                {
                    polyLine.SetSmooth();
                }
                else
                {
                    break;
                }
            }
            return polyLine;
        }

        private MapRegion ReadMapRegion(ref string cur, ref StreamReader sr)
        {
            MapRegion mainRegion = new MapRegion();
            if (cur.Length <= 6)
            {
                cur = sr.ReadLine();
                cur = cur.Trim();
            }
            string[] parameters = cur.Split(' ');
            int segments = Convert.ToInt32(parameters[parameters.Length - 1]);
            
            for (int i = 1; i <= segments; i++)
            {
                MapPolygon curPolygon = new MapPolygon();
                string snumber = sr.ReadLine();
                snumber = snumber.Trim();
                int number = Convert.ToInt32(snumber);
                for (int j = 1; j <= number; j++)
                {
                    string point = sr.ReadLine();
                    point = point.Trim();
                    string[] coordinates = point.Split(' ');
                    UpdateEdges(Convert.ToDouble(coordinates[0]), Convert.ToDouble(coordinates[1]));
                    curPolygon.AddPoint(new MapPoint(Convert.ToDouble(coordinates[0]),
                                                    Convert.ToDouble(coordinates[1])));
                }
                mainRegion.AddPolygon(curPolygon);
            }
            while (true)
            {
                cur = sr.ReadLine();
                if (cur == null)
                {
                    break;
                }
                if (cur.ToLower().Contains("pen"))
                {
                    mainRegion.SetPen(ParsePen(cur));
                }
                else if (cur.ToLower().Contains("brush"))
                {
                    mainRegion.SetBrush(ParseBrush(cur));
                }
                else if (cur.ToLower().Contains("center"))
                {
                    cur = cur.Trim();
                    string[] center = cur.Split(' ');
                    mainRegion.SetCenter(new MapPoint(Convert.ToDouble(center[1]),
                                                        Convert.ToDouble(center[2])));
                }
                else
                {
                    break;
                }
            }
            return mainRegion;
        }

        private MapText ReadMapText(ref string cur, ref StreamReader sr)
        {
            cur = sr.ReadLine();
            string text = cur;
            text = text.Trim();
            text = text.Remove(0, 1);
            text = text.Remove(text.Length - 1);
            string location = sr.ReadLine();
            location = location.Trim();
            string[] coordinates = location.Split(' ');

            cur = sr.ReadLine();
            return new MapText(text, new MapRectangle(new MapPoint(Convert.ToDouble(coordinates[0]),
                                                                    Convert.ToDouble(coordinates[1])),
                                                    new MapPoint(Convert.ToDouble(coordinates[2]),
                                                                Convert.ToDouble(coordinates[3]))));
        }

		private static int CompareObjects(MapObject a, MapObject b)
		{
			if (a.Priority < b.Priority)
			{
				return -1;
			}
			else
			{
				if (a.Priority == b.Priority)
				{
					return 0;
				}
				else
				{
					return 1;
				}
			}
		}

        public MapLayer(string filename)
        {
            Visible = true;
            _objects = new List<MapObject>();
            _minX = _minY = 1e9;
            _maxX = _maxY = -1e9;
            //_controlWidth = controlWidth;
            //_controlHeight = controlHeight;
            StreamReader sr = new StreamReader(filename);
            string cur = sr.ReadLine();
            cur = cur.Trim();
            while (!sr.EndOfStream)
            {
                if (cur.ToLower().Contains("delimiter"))
                {
                    string[] items = cur.Split(' ');
                    _delimiter = items[1][items[1].Length - 2];
                    cur = sr.ReadLine();
                }
                else if (cur.ToLower().Contains("point"))
                {
                    _objects.Add(ReadMapPoint(ref cur, ref sr));
                }
                else if (cur.ToLower().Contains("pline"))
                {
                    _objects.Add(ReadMapPolyLine(ref cur, ref sr));
                }
                else if (cur.ToLower().Contains("line"))
                {
                    _objects.Add(ReadMapLine(ref cur, ref sr));
                }
                else if (cur.ToLower().Contains("region"))
                {
                    _objects.Add(ReadMapRegion(ref cur, ref sr));
                }
                else if (cur.ToLower().Contains("text"))
                {
                    _objects.Add(ReadMapText(ref cur, ref sr));
                }
                else
                {
                    cur = sr.ReadLine();
                    cur = cur.Trim();
                }
            }
            sr.Close();
            if (_minX == _maxX)
            {
                _maxX = _maxX + 10.0;
            }
            if (_minY == _maxY)
            {
                _maxY = _maxY + 10.0;
            }
            //_scale = Math.Max((_maxX - _minX) / _controlWidth, (_maxY - _minY) / _controlHeight);
            //_center = new MapPoint((_maxX + _minX) / 2.0, (_maxY + _minY) / 2.0);
			_objects.Sort(CompareObjects);
        }

        public MapPolygon GetNearestPolygon(MapPoint p, int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            if (!Visible)
            {
                return null;
            }
            double dist = 1e9;
            MapPolygon res = null;
            for (int i = 0; i < _objects.Count; i++)
            {
                if (_objects[i].GetMapObjectType != 1)
                {
                    continue;
                }
                double curDist = ((MapRegion)(_objects[i])).GetNearestPolygon(p).GetDistance(p);
                if (curDist < dist)
                {
                    dist = curDist;
                    res = ((MapRegion)(_objects[i])).GetNearestPolygon(p);
                }
            }
            return res;
        }

        public MapObject PickOut(Point screen, int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            if (!Visible)
            {
                return null;
            }
            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                if (_objects[i].IsPicked(screen, controlWidth, controlHeight, scale, center))
                {
                    _objects[i].Picked = true;
                    _picked = _objects[i];
                    return _objects[i];
                }
            }
            return null;
        }

        public void Paint(Graphics g, int controlWidth, int controlHeight, double scale, MapPoint center)
        {
            if (!Visible)
            {
                return;
            }
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Draw(g, controlWidth, controlHeight, scale, center);
            }
        }

        public MapObject MapObject
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
