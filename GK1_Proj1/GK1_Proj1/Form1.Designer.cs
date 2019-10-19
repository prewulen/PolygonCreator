namespace GK1_Proj1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.zakonczToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cofnijToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ponowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.DrawingField = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Select = new System.Windows.Forms.Button();
            this.Move = new System.Windows.Forms.Button();
            this.Delete = new System.Windows.Forms.Button();
            this.AddVertice = new System.Windows.Forms.Button();
            this.Polygon = new System.Windows.Forms.Button();
            this.Circle = new System.Windows.Forms.Button();
            this.Undo = new System.Windows.Forms.Button();
            this.Redo = new System.Windows.Forms.Button();
            this.CompletePoly = new System.Windows.Forms.Button();
            this.menuStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(526, 24);
            this.menuStrip2.TabIndex = 0;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zakonczToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(38, 20);
            this.toolStripMenuItem1.Text = "Plik";
            // 
            // zakonczToolStripMenuItem
            // 
            this.zakonczToolStripMenuItem.Name = "zakonczToolStripMenuItem";
            this.zakonczToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.zakonczToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.zakonczToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.zakonczToolStripMenuItem.Text = "Zakoncz";
            this.zakonczToolStripMenuItem.Click += new System.EventHandler(this.ZakonczToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cofnijToolStripMenuItem,
            this.ponowToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(53, 20);
            this.toolStripMenuItem2.Text = "Edycja";
            // 
            // cofnijToolStripMenuItem
            // 
            this.cofnijToolStripMenuItem.Name = "cofnijToolStripMenuItem";
            this.cofnijToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.cofnijToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cofnijToolStripMenuItem.Text = "Cofnij";
            this.cofnijToolStripMenuItem.Click += new System.EventHandler(this.CofnijToolStripMenuItem_Click);
            // 
            // ponowToolStripMenuItem
            // 
            this.ponowToolStripMenuItem.Name = "ponowToolStripMenuItem";
            this.ponowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.ponowToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ponowToolStripMenuItem.Text = "Ponow";
            this.ponowToolStripMenuItem.Click += new System.EventHandler(this.PonowToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 22);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(523, 338);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.Controls.Add(this.DrawingField, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listBox1, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 39);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(517, 296);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // DrawingField
            // 
            this.DrawingField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DrawingField.BackColor = System.Drawing.Color.Silver;
            this.DrawingField.Location = new System.Drawing.Point(3, 0);
            this.DrawingField.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.DrawingField.Name = "DrawingField";
            this.DrawingField.Size = new System.Drawing.Size(361, 293);
            this.DrawingField.TabIndex = 1;
            this.DrawingField.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingField_Paint);
            this.DrawingField.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingField_MouseDown);
            this.DrawingField.MouseLeave += new System.EventHandler(this.DrawingField_MouseLeave);
            this.DrawingField.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingField_MouseMove);
            this.DrawingField.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawingField_MouseUp);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.Location = new System.Drawing.Point(370, 0);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(144, 293);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.Select);
            this.flowLayoutPanel1.Controls.Add(this.Move);
            this.flowLayoutPanel1.Controls.Add(this.Delete);
            this.flowLayoutPanel1.Controls.Add(this.AddVertice);
            this.flowLayoutPanel1.Controls.Add(this.Polygon);
            this.flowLayoutPanel1.Controls.Add(this.Circle);
            this.flowLayoutPanel1.Controls.Add(this.Undo);
            this.flowLayoutPanel1.Controls.Add(this.Redo);
            this.flowLayoutPanel1.Controls.Add(this.CompletePoly);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 0, 3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(518, 33);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // Select
            // 
            this.Select.Location = new System.Drawing.Point(3, 3);
            this.Select.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Select.Name = "Select";
            this.Select.Size = new System.Drawing.Size(30, 30);
            this.Select.TabIndex = 0;
            this.Select.Text = "s";
            this.Select.UseVisualStyleBackColor = true;
            this.Select.Click += new System.EventHandler(this.Select_Click);
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(39, 3);
            this.Move.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(30, 30);
            this.Move.TabIndex = 6;
            this.Move.Text = "m";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(75, 3);
            this.Delete.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(30, 30);
            this.Delete.TabIndex = 7;
            this.Delete.Text = "d";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // AddVertice
            // 
            this.AddVertice.Location = new System.Drawing.Point(111, 3);
            this.AddVertice.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.AddVertice.Name = "AddVertice";
            this.AddVertice.Size = new System.Drawing.Size(30, 30);
            this.AddVertice.TabIndex = 8;
            this.AddVertice.Text = "av";
            this.AddVertice.UseVisualStyleBackColor = true;
            this.AddVertice.Click += new System.EventHandler(this.AddVertice_Click);
            // 
            // Polygon
            // 
            this.Polygon.Location = new System.Drawing.Point(147, 3);
            this.Polygon.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Polygon.Name = "Polygon";
            this.Polygon.Size = new System.Drawing.Size(30, 30);
            this.Polygon.TabIndex = 1;
            this.Polygon.Text = "p";
            this.Polygon.UseVisualStyleBackColor = true;
            this.Polygon.Click += new System.EventHandler(this.Polygon_Click);
            // 
            // Circle
            // 
            this.Circle.Location = new System.Drawing.Point(183, 3);
            this.Circle.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Circle.Name = "Circle";
            this.Circle.Size = new System.Drawing.Size(30, 30);
            this.Circle.TabIndex = 2;
            this.Circle.Text = "c";
            this.Circle.UseVisualStyleBackColor = true;
            this.Circle.Click += new System.EventHandler(this.Circle_Click);
            // 
            // Undo
            // 
            this.Undo.Location = new System.Drawing.Point(219, 3);
            this.Undo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(30, 30);
            this.Undo.TabIndex = 3;
            this.Undo.Text = "z";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // Redo
            // 
            this.Redo.Location = new System.Drawing.Point(255, 3);
            this.Redo.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Redo.Name = "Redo";
            this.Redo.Size = new System.Drawing.Size(30, 30);
            this.Redo.TabIndex = 4;
            this.Redo.Text = "y";
            this.Redo.UseVisualStyleBackColor = true;
            this.Redo.Click += new System.EventHandler(this.Redo_Click);
            // 
            // CompletePoly
            // 
            this.CompletePoly.Enabled = false;
            this.CompletePoly.Location = new System.Drawing.Point(291, 3);
            this.CompletePoly.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.CompletePoly.Name = "CompletePoly";
            this.CompletePoly.Size = new System.Drawing.Size(30, 30);
            this.CompletePoly.TabIndex = 5;
            this.CompletePoly.Text = "a";
            this.CompletePoly.UseVisualStyleBackColor = true;
            this.CompletePoly.Click += new System.EventHandler(this.CompletePoly_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(526, 363);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "Form1";
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zakończToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edycjaToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem zakonczToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cofnijToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ponowToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel DrawingField;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Select;
        private System.Windows.Forms.Button Polygon;
        private System.Windows.Forms.Button Circle;
        private System.Windows.Forms.Button Undo;
        private System.Windows.Forms.Button Redo;
        private System.Windows.Forms.Button CompletePoly;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.Button AddVertice;
    }
}

