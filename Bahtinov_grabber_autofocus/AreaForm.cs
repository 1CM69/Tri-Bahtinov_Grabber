using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Bahtinov_grabber_autofocus
{
  public class AreaForm : Form
  {
    public Point ClickPoint = new Point();
    public Point CurrentTopLeft = new Point();
    public Point CurrentBottomRight = new Point();
    private Pen MyPen = new Pen(Color.Blue, 1f);
    private Pen EraserPen = new Pen(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192), 1f);
    private IContainer components;
    public bool LeftButtonDown;
    public bool activated;
    private Graphics g;

    public AreaForm()
    {
      this.InitializeComponent();
      this.MouseDown += new MouseEventHandler(this.mouse_Click);
      this.MouseUp += new MouseEventHandler(this.mouse_Up);
      this.MouseMove += new MouseEventHandler(this.mouse_Move);
      this.MyPen.DashStyle = DashStyle.Dot;
      this.g = this.CreateGraphics();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(292, 273);
      this.ControlBox = false;
      this.Cursor = Cursors.Arrow;
      this.FormBorderStyle = FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AreaForm";
      this.Opacity = 0.33;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "AreaForm";
      this.TopMost = true;
      this.WindowState = FormWindowState.Maximized;
      this.ResumeLayout(false);
    }

    private void mouse_Click(object sender, MouseEventArgs e)
    {
      this.g.Clear(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192));
      this.LeftButtonDown = true;
      this.ClickPoint = new Point(Control.MousePosition.X, Control.MousePosition.Y);
      this.activated = false;
    }

    private void mouse_Up(object sender, MouseEventArgs e)
    {
      this.LeftButtonDown = false;
      this.activated = true;
      this.Hide();
    }

    private void mouse_Move(object sender, MouseEventArgs e)
    {
      int num = 1;
      if (this.LeftButtonDown)
      {
        this.g.DrawRectangle(this.EraserPen, this.CurrentTopLeft.X, this.CurrentTopLeft.Y, this.CurrentBottomRight.X - this.CurrentTopLeft.X, this.CurrentBottomRight.Y - this.CurrentTopLeft.Y);
        num = Math.Max(Math.Abs(Cursor.Position.X - this.ClickPoint.X), Math.Abs(Cursor.Position.Y - this.ClickPoint.Y));
      }
      this.CurrentTopLeft.X = this.ClickPoint.X - num;
      this.CurrentTopLeft.Y = this.ClickPoint.Y - num;
      this.CurrentBottomRight.X = this.ClickPoint.X + num;
      this.CurrentBottomRight.Y = this.ClickPoint.Y + num;
      this.g.DrawRectangle(this.MyPen, this.CurrentTopLeft.X, this.CurrentTopLeft.Y, this.CurrentBottomRight.X - this.CurrentTopLeft.X, this.CurrentBottomRight.Y - this.CurrentTopLeft.Y);
    }
  }
}
