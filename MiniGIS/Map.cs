using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MiniGIS
{
    public class Map
    {
        private List<MapLayer> _layers;
        private int _controlWidth, _controlHeight;
        private int _increase = 0;
        private const double ScaleCoefficient = 1.2;
        private const int MinScale = -50, MaxScale = 50;
        //private double _shiftX = 0.0, _shiftY = 0.0;
        private double _scale;
        private MapPoint _center;
        private double _minX, _maxX, _minY, _maxY;
        private MapObject _picked;

        private void Center()
        {
            _increase = 0;
            _minX = _minY = 1e9;
            _maxX = _maxY = -1e9;
            for (int i = 0; i < _layers.Count; i++)
            {
                _minX = Math.Min(_minX, _layers[i].MinX);
                _maxX = Math.Max(_maxX, _layers[i].MaxX);
                _minY = Math.Min(_minY, _layers[i].MinY);
                _maxY = Math.Max(_maxY, _layers[i].MaxY);
            }
            _scale = Math.Max((_maxX - _minX) / _controlWidth, (_maxY - _minY) / _controlHeight);
            _center = new MapPoint((_minX + _maxX) / 2.0, (_minY + _maxY) / 2.0);
        }

        public void Move(double dx, double dy)
        {
            //for (int i = 0; i < _layers.Count; i++)
            //{
            //    _layers[i].Move(dx, dy);
            //}
            //_shiftX += dx;
            //_shiftY += dy;
            _center.X -= dx * _scale;
            _center.Y += dy * _scale;
        }

        public bool IsMaxScale
        {
            get
            {
                return _increase == MaxScale;
            }
        }

        public bool IsMinScale
        {
            get
            {
                return _increase == MinScale;
            }
        }

        public MapObject PickedObject
        {
            get
            {
                return _picked;
            }
        }

        public Map(int controlWidth, int controlHeight)
        {
            _controlWidth = controlWidth;
            _controlHeight = controlHeight;
            _layers = new List<MapLayer>();
            _minX = _minY = 1e9;
            _maxX = _maxY = -1e9;
        }

        public void DeleteLayer(int num)
        {
            if (num >= 0 && num < _layers.Count)
            {
                _layers.RemoveAt(num);
                Center();
            }
        }

        public void IncreaseScale()
        {
            if (_increase < MaxScale)
            {
                _increase++;
                _scale /= ScaleCoefficient;
                //_shiftX *= ScaleCoefficient;
                //_shiftY *= ScaleCoefficient;
                //for (int i = 0; i < _layers.Count; i++)
                //{
                //    _layers[i].IncreaseScale(1.0 / ScaleCoefficient);
                //}
            }
        }

        public void DecreaseScale()
        {
            if (_increase > MinScale)
            {
                _increase--;
                _scale *= ScaleCoefficient;
                //_shiftX /= ScaleCoefficient;
                //_shiftY /= ScaleCoefficient;
                //for (int i = 0; i < _layers.Count; i++)
                //{
                //    _layers[i].IncreaseScale(ScaleCoefficient);
                //}
            }
        }

        public void AddLayer(string filename)
        {
            _layers.Insert(0, new MapLayer(filename));//, _controlWidth, _controlHeight));
            Center();
            //if (_increase < 0)
            //{
            //    for (int i = 1; i <= Math.Abs(_increase); i++)
            //    {
            //        _layers[_layers.Count - 1].IncreaseScale(ScaleCoefficient);
            //    }
            //}
            //if (_increase > 0)
            //{
            //    for (int i = 1; i <= Math.Abs(_increase); i++)
            //    {
            //        _layers[_layers.Count - 1].IncreaseScale(1.0 / ScaleCoefficient);
            //    }
            //}
            //_layers[_layers.Count - 1].Move(_shiftX, _shiftY);
        }

        public void HideLayer(int num)
        {
            _layers[num].Visible = false;
        }

        public void ShowLayer(int num)
        {
            _layers[num].Visible = true;
        }

        public void MoveLayerUp(int num)
        {
            if (num + 1 < _layers.Count)
            {
                MapLayer temp = _layers[num];
                _layers[num] = _layers[num + 1];
                _layers[num + 1] = temp;
            }
        }

        public void PickOut(Point screen)
        {
            if (_picked != null)
            {
                _picked.Picked = false;
            }
            for (int i = 0; i < _layers.Count; i++)
            {
                _picked = _layers[i].PickOut(screen, _controlWidth, _controlHeight, _scale, _center);
                if (_picked != null)
                {
                    return;
                }
            }
        }

        public void GetNearestPolygon()
        {
            double dist = 1e9;
            MapPolygon res = null;
            //MapPoint screen = (MapPoint)_picked;
            //for (int i = 0; i < _layers.Count; i++)
            //{
            //    if (_layers[i].Picked == _picked)
            //    {
            //        //screen = ((MapPoint)_picked).MapToScreen(_controlWidth, _controlHeight, _layers[i].Scale, _layers[i].Center);
            //        screen = ((MapPoint)_picked).MapToScreen(_controlWidth, _controlHeight, _scale, _center);
            //        break;
            //    }
            //}
            for (int i = 0; i < _layers.Count; i++)
            {
                MapPolygon mp = _layers[i].GetNearestPolygon((MapPoint)_picked, _controlWidth, _controlWidth, _scale, _center);
                if (mp == null)
                {
                    continue;
                }
                double curDist = mp.GetDistance((MapPoint)_picked) / _scale;//_layers[i].Scale;
                if (curDist < dist)
                {
                    dist = curDist;
                    res = mp;
                }
            }
            _picked.ResetPick();
            res.PickOut();
            _picked = res;
        }

        public void UpdateControlSize(Size s)
        {
            _controlWidth = s.Width;
            _controlHeight = s.Height;
            //for (int i = 0; i < _layers.Count; i++)
            //{
            //    _layers[i].UpdateControlSize(s);
            //}
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                _layers[i].Paint(e.Graphics, _controlWidth, _controlHeight, _scale, _center);
            }
        }

        public MapLayer MapLayer
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
