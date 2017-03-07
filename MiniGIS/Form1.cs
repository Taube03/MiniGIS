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
    public partial class Form1 : Form
    {
        private Map _map;
        private Point _mousePosition;
        private bool _isMovement;
        private bool _canMove;
        private bool _canMark;
        private GroupBox _modeGroupBox;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Имя", 149);
            listView1.LabelEdit = true;
            listView1.CheckBoxes = true;
            _isMovement = false;
            deleteLayersButton.Enabled = false;
            layerUpButton.Enabled = false;
            layerDownButton.Enabled = false;
            DoubleBuffered = true;
            nearestPolygonButton.Enabled = false;
            _map = new Map(mapControl.Width, mapControl.Height);
            _modeGroupBox = new GroupBox();
            movementModeRadioButton.Location = new Point(10, 15);
            movementModeRadioButton.Checked = true;
            _canMove = true;
            _modeGroupBox.Controls.Add(movementModeRadioButton);
            markingModeRadioButton.Location = new Point(10, 35);
            _canMark = false;
            _modeGroupBox.Controls.Add(markingModeRadioButton);
            _modeGroupBox.Height = 60;
            _modeGroupBox.Width = listView1.Width;
            _modeGroupBox.FlatStyle = FlatStyle.Popup;
            Controls.Add(_modeGroupBox);
            LocateControls();
        }

        private void Form1_SizeChanged_1(object sender, EventArgs e)
        {
            LocateControls();
        }


        private string GetNameForLayer()
        {
            int max = 0;
            for (int j = 0; j < listView1.Items.Count; j++)
            {
                int num = 0;
                if (listView1.Items[j].SubItems[0].Text.Substring(0, 5) == "Layer" && Int32.TryParse(listView1.Items[j].SubItems[0].Text.Substring(5), out num))
                {
                    max = Math.Max(num, max);
                }
            }
            max++;
            string res = "Layer" + Convert.ToString(max);
            return res;
        }

        private void UpdateButtons()
        {
            if (_map.IsMinScale)
            {
                decreaseScaleButton.Enabled = false;
            }
            else
            {
                decreaseScaleButton.Enabled = true;
            }
            if (_map.IsMaxScale)
            {
                increaseScaleButton.Enabled = false;
            }
            else
            {
                increaseScaleButton.Enabled = true;
            }
        }

        private void LocateControls()
        {
            const int delta = 17;
            const int buttonRight = 70;
            listView1.Location = new Point(Size.Width - 45 - listView1.Width, delta + 8);
            _modeGroupBox.Location = new Point(Size.Width - 45 - _modeGroupBox.Width, listView1.Bounds.Bottom + delta);
            decreaseScaleButton.Location = new Point(Size.Width - 50 - decreaseScaleButton.Width, _modeGroupBox.Bounds.Bottom + delta);
            increaseScaleButton.Location = new Point(Size.Width - 90 - decreaseScaleButton.Width - increaseScaleButton.Width, _modeGroupBox.Bounds.Bottom + delta);
            openFileButton.Location = new Point(Size.Width - buttonRight - openFileButton.Width, increaseScaleButton.Bounds.Bottom + delta);
            layerUpButton.Location = new Point(Size.Width - buttonRight - openFileButton.Width, openFileButton.Bounds.Bottom + delta);
            layerDownButton.Location = new Point(Size.Width - buttonRight - openFileButton.Width, layerUpButton.Bounds.Bottom + delta);
            deleteLayersButton.Location = new Point(Size.Width - buttonRight - openFileButton.Width, layerDownButton.Bounds.Bottom + delta);
            nearestPolygonButton.Location = new Point(Size.Width - buttonRight - openFileButton.Width, deleteLayersButton.Bounds.Bottom + delta);
            mapControl.Location = new Point(delta + 8, delta + 8);
            mapControl.Size = new Size(Width - openFileButton.Width - delta - 2 * buttonRight, Height - 2 * (delta + 8) - 40);
            _map.UpdateControlSize(mapControl.Size);
            Invalidate();
            mapControl.Invalidate();
        }


        private void mapControl_Paint(object sender, PaintEventArgs e)
        {
            _map.Paint(sender, e);
        }

        private void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canMove)
            {
                if (_isMovement)
                {
                    _map.Move(e.Location.X - _mousePosition.X, e.Location.Y - _mousePosition.Y);
                    _mousePosition = e.Location;
                    Invalidate();
                    mapControl.Invalidate();
                }
            }
        }

        private void mapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (_canMove)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isMovement = true;
                    _mousePosition = e.Location;
                }
            }
        }

        private void mapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (_canMove)
            {
                _isMovement = false;
            }
        }

        private void mapControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (_canMark)
            {
                _map.PickOut(e.Location);
                if (_map.PickedObject != null)
                {
                    nearestPolygonButton.Enabled = (_map.PickedObject.GetMapObjectType == 4);
                }
                else
                {
                    nearestPolygonButton.Enabled = false;
                }
                mapControl.Invalidate();
            }
        }


        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "mif files (*.mif)|*.mif";
            DialogResult result = open.ShowDialog();
            if (result == DialogResult.OK)
            {
                _map.AddLayer(open.FileName);
                ListViewItem item = new ListViewItem(GetNameForLayer());
                item.Checked = true;
                listView1.Items.Insert(0, item);
                listView1.Items[0].Checked = true;
            }
            //listView1.Controls.Add(new Button());
            Invalidate();
            mapControl.Invalidate();
        }

        private void deleteLayersButton_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    _map.DeleteLayer(i);
                    listView1.Items.RemoveAt(i);
                }
            }
            Invalidate();
            mapControl.Invalidate();
            
        }

        private void increaseScaleButton_Click(object sender, EventArgs e)
        {
            _map.IncreaseScale();
            UpdateButtons();
            Invalidate();
            mapControl.Invalidate();
            
        }

        private void decreaseScaleButton_Click(object sender, EventArgs e)
        {
            _map.DecreaseScale();
            UpdateButtons();
            Invalidate();
            mapControl.Invalidate();
            
        }

        private void layerUpButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 1)
            {
                return;
            }
            int ind = listView1.SelectedIndices[0];
            _map.MoveLayerUp(ind);
            for (int i = 0; i < listView1.Items[ind].SubItems.Count; i++)
            {
                ListViewItem.ListViewSubItem temp = listView1.Items[ind].SubItems[i];
                listView1.Items[ind].SubItems[i] = listView1.Items[ind + 1].SubItems[i];
                listView1.Items[ind + 1].SubItems[i] = temp;
            }
            bool c = listView1.Items[ind].Checked;
            listView1.Items[ind].Checked = listView1.Items[ind + 1].Checked;
            listView1.Items[ind + 1].Checked = c;
            listView1.Items[ind].Selected = false;
            listView1_SelectedIndexChanged(listView1, new EventArgs());
            Invalidate();
            listView1.Invalidate();
            mapControl.Invalidate();
            
        }

        private void layerDownButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 1)
            {
                return;
            }
            int ind = listView1.SelectedIndices[0];
            _map.MoveLayerUp(ind - 1);
            for (int i = 0; i < listView1.Items[ind].SubItems.Count; i++)
            {
                ListViewItem.ListViewSubItem temp = listView1.Items[ind - 1].SubItems[i];
                listView1.Items[ind - 1].SubItems[i] = listView1.Items[ind].SubItems[i];
                listView1.Items[ind].SubItems[i] = temp;
            }
            bool c = listView1.Items[ind].Checked;
            listView1.Items[ind].Checked = listView1.Items[ind - 1].Checked;
            listView1.Items[ind - 1].Checked = c;
            listView1.Items[ind].Selected = false;
            listView1_SelectedIndexChanged(listView1, new EventArgs());
            Invalidate();
            mapControl.Invalidate();
            
        }

        private void NearestPolygonButton_Click(object sender, EventArgs e)
        {
            _map.GetNearestPolygon();
            nearestPolygonButton.Enabled = false;
            mapControl.Invalidate();

            Invalidate();
        }


        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Unchecked)
            {
                _map.ShowLayer(e.Index);
            }
            else
            {
                _map.HideLayer(e.Index);
            }
            Invalidate();
            mapControl.Invalidate();
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                deleteLayersButton.Enabled = true;
            }
            else
            {
                deleteLayersButton.Enabled = false;
            }
            if (listView1.SelectedIndices.Count == 1 && listView1.SelectedIndices[0] + 1 < listView1.Items.Count)
            {
                layerUpButton.Enabled = true;
            }
            else
            {
                layerUpButton.Enabled = false;
            }
            if (listView1.SelectedIndices.Count == 1 && listView1.SelectedIndices[0] > 0)
            {
                layerDownButton.Enabled = true;
            }
            else
            {
                layerDownButton.Enabled = false;
            }
        }


        private void markingModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (markingModeRadioButton.Checked)
            {
                _canMark = true;
                movementModeRadioButton.Checked = false;
            }
            else
            {
                _canMark = false;
            }
        }

        private void movementModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (movementModeRadioButton.Checked)
            {
                _canMove = true;
                markingModeRadioButton.Checked = false;
            }
            else
            {
                _canMove = false;
            }
        }
    }
}
