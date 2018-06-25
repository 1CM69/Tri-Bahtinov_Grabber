using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bahtinov_grabber_autofocus
{
  internal class Grabber
  {
    public Bitmap picture;
    private Timer Clock;
    public AreaForm areaForm;
    public bool activated;

    public Grabber()
    {
      this.areaForm = new AreaForm();
      this.areaForm.Show();
      this.startTimer();
    }

    public void ReStart()
    {
      this.activated = false;
      this.Clock.Stop();
      this.areaForm.activated = false;
      this.areaForm.Show();
      this.Clock.Start();
    }

    private void startTimer()
    {
      this.Clock = new Timer();
      this.Clock.Interval = 100;
      this.Clock.Start();
      this.Clock.Tick += new EventHandler(this.Timer_Tick);
    }

    private void Timer_Tick(object sender, EventArgs eArgs)
    {
      if (!this.areaForm.activated)
        return;
      this.captureArea(this.areaForm.CurrentTopLeft, this.areaForm.CurrentBottomRight);
    }

    private void captureArea(Point TopLeft, Point BottomRight)
    {
      if (!(TopLeft != BottomRight))
        return;
      this.activated = true;
      Screen.GetBounds(Screen.GetBounds(Point.Empty));
      Size size = new Size(1 + BottomRight.X - TopLeft.X, 1 + BottomRight.Y - TopLeft.Y);
      if (this.picture.Size != size)
        this.picture = new Bitmap(size.Width, size.Height);
      using (Graphics graphics = Graphics.FromImage((Image) this.picture))
        graphics.CopyFromScreen(TopLeft, new Point(0, 0), this.picture.Size);
    }
  }
}
