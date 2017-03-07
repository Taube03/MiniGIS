namespace MiniGIS
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.mapControl = new GISControl.MapControl();
            this.openFileButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.deleteLayersButton = new System.Windows.Forms.Button();
            this.decreaseScaleButton = new System.Windows.Forms.Button();
            this.increaseScaleButton = new System.Windows.Forms.Button();
            this.layerUpButton = new System.Windows.Forms.Button();
            this.layerDownButton = new System.Windows.Forms.Button();
            this.nearestPolygonButton = new System.Windows.Forms.Button();
            this.markingModeRadioButton = new System.Windows.Forms.RadioButton();
            this.movementModeRadioButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mapControl
            // 
            this.mapControl.Location = new System.Drawing.Point(21, 22);
            this.mapControl.Name = "mapControl";
            this.mapControl.Size = new System.Drawing.Size(554, 509);
            this.mapControl.TabIndex = 0;
            this.mapControl.Paint += new System.Windows.Forms.PaintEventHandler(this.mapControl_Paint);
            this.mapControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapControl_MouseClick);
            this.mapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mapControl_MouseDown);
            this.mapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mapControl_MouseMove);
            this.mapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mapControl_MouseUp);
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(640, 280);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(101, 33);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Открыть слой";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(610, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(153, 109);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // deleteLayersButton
            // 
            this.deleteLayersButton.Location = new System.Drawing.Point(641, 439);
            this.deleteLayersButton.Name = "deleteLayersButton";
            this.deleteLayersButton.Size = new System.Drawing.Size(100, 33);
            this.deleteLayersButton.TabIndex = 3;
            this.deleteLayersButton.Text = "Удалить слой";
            this.deleteLayersButton.UseVisualStyleBackColor = true;
            this.deleteLayersButton.Click += new System.EventHandler(this.deleteLayersButton_Click);
            // 
            // decreaseScaleButton
            // 
            this.decreaseScaleButton.Image = ((System.Drawing.Image)(resources.GetObject("decreaseScaleButton.Image")));
            this.decreaseScaleButton.Location = new System.Drawing.Point(702, 204);
            this.decreaseScaleButton.Name = "decreaseScaleButton";
            this.decreaseScaleButton.Size = new System.Drawing.Size(50, 50);
            this.decreaseScaleButton.TabIndex = 4;
            this.decreaseScaleButton.UseVisualStyleBackColor = true;
            this.decreaseScaleButton.Click += new System.EventHandler(this.decreaseScaleButton_Click);
            // 
            // increaseScaleButton
            // 
            this.increaseScaleButton.Image = ((System.Drawing.Image)(resources.GetObject("increaseScaleButton.Image")));
            this.increaseScaleButton.Location = new System.Drawing.Point(626, 204);
            this.increaseScaleButton.Name = "increaseScaleButton";
            this.increaseScaleButton.Size = new System.Drawing.Size(50, 50);
            this.increaseScaleButton.TabIndex = 5;
            this.increaseScaleButton.UseVisualStyleBackColor = true;
            this.increaseScaleButton.Click += new System.EventHandler(this.increaseScaleButton_Click);
            // 
            // layerUpButton
            // 
            this.layerUpButton.Location = new System.Drawing.Point(641, 332);
            this.layerUpButton.Name = "layerUpButton";
            this.layerUpButton.Size = new System.Drawing.Size(101, 33);
            this.layerUpButton.TabIndex = 6;
            this.layerUpButton.Text = "Вниз";
            this.layerUpButton.UseVisualStyleBackColor = true;
            this.layerUpButton.Click += new System.EventHandler(this.layerUpButton_Click);
            // 
            // layerDownButton
            // 
            this.layerDownButton.Location = new System.Drawing.Point(641, 384);
            this.layerDownButton.Name = "layerDownButton";
            this.layerDownButton.Size = new System.Drawing.Size(101, 33);
            this.layerDownButton.TabIndex = 7;
            this.layerDownButton.Text = "Вверх";
            this.layerDownButton.UseVisualStyleBackColor = true;
            this.layerDownButton.Click += new System.EventHandler(this.layerDownButton_Click);
            // 
            // nearestPolygonButton
            // 
            this.nearestPolygonButton.Location = new System.Drawing.Point(641, 491);
            this.nearestPolygonButton.Name = "nearestPolygonButton";
            this.nearestPolygonButton.Size = new System.Drawing.Size(101, 40);
            this.nearestPolygonButton.TabIndex = 8;
            this.nearestPolygonButton.Text = "Ближайший полигон";
            this.nearestPolygonButton.UseVisualStyleBackColor = true;
            this.nearestPolygonButton.Click += new System.EventHandler(this.NearestPolygonButton_Click);
            // 
            // markingModeRadioButton
            // 
            this.markingModeRadioButton.AutoSize = true;
            this.markingModeRadioButton.Location = new System.Drawing.Point(626, 147);
            this.markingModeRadioButton.Name = "markingModeRadioButton";
            this.markingModeRadioButton.Size = new System.Drawing.Size(119, 17);
            this.markingModeRadioButton.TabIndex = 9;
            this.markingModeRadioButton.TabStop = true;
            this.markingModeRadioButton.Text = "Режим выделения";
            this.markingModeRadioButton.UseVisualStyleBackColor = true;
            this.markingModeRadioButton.CheckedChanged += new System.EventHandler(this.markingModeRadioButton_CheckedChanged);
            // 
            // movementModeRadioButton
            // 
            this.movementModeRadioButton.AutoSize = true;
            this.movementModeRadioButton.Location = new System.Drawing.Point(626, 170);
            this.movementModeRadioButton.Name = "movementModeRadioButton";
            this.movementModeRadioButton.Size = new System.Drawing.Size(134, 17);
            this.movementModeRadioButton.TabIndex = 10;
            this.movementModeRadioButton.TabStop = true;
            this.movementModeRadioButton.Text = "Режим перемещения";
            this.movementModeRadioButton.UseVisualStyleBackColor = true;
            this.movementModeRadioButton.CheckedChanged += new System.EventHandler(this.movementModeRadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.movementModeRadioButton);
            this.Controls.Add(this.markingModeRadioButton);
            this.Controls.Add(this.nearestPolygonButton);
            this.Controls.Add(this.layerDownButton);
            this.Controls.Add(this.layerUpButton);
            this.Controls.Add(this.increaseScaleButton);
            this.Controls.Add(this.decreaseScaleButton);
            this.Controls.Add(this.deleteLayersButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.openFileButton);
            this.Controls.Add(this.mapControl);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "МиниГИС";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GISControl.MapControl mapControl;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button deleteLayersButton;
        private System.Windows.Forms.Button decreaseScaleButton;
        private System.Windows.Forms.Button increaseScaleButton;
        private System.Windows.Forms.Button layerUpButton;
        private System.Windows.Forms.Button layerDownButton;
        private System.Windows.Forms.Button nearestPolygonButton;
        private System.Windows.Forms.RadioButton markingModeRadioButton;
        private System.Windows.Forms.RadioButton movementModeRadioButton;
    }
}

