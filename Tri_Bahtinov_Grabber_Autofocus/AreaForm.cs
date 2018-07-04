using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Tri_Bahtinov_Grabber_Autofocus
{
    public class AreaForm : Form
    {
        public Point ClickPoint = new Point();
        public Point CurrentTopLeft = new Point();
        public Point CurrentBottomRight = new Point();
        private Pen MyPen = new Pen(Color.Blue, 1f);
        private Pen EraserPen = new Pen(Color.FromArgb((int)byte.MaxValue, (int)byte.MaxValue, 192), 1f);
        private IContainer components;
        public bool LeftButtonDown;
        public bool activated;
        private Graphics g;
        private Cursor precisionCursor = CursorResourceLoader.LoadEmbeddedCursor(Properties.Resources.Precision);


        public AreaForm()
        {
            this.InitializeComponent();
            this.MouseDown += new MouseEventHandler(this.mouse_Click);
            this.MouseUp += new MouseEventHandler(this.mouse_Up);
            this.MouseMove += new MouseEventHandler(this.mouse_Move);
            this.MouseClick += new MouseEventHandler(this.mouse_Button);
            this.MyPen.DashStyle = DashStyle.Dash;
            this.g = this.CreateGraphics();
            Cursor = precisionCursor;
        }

        public static class CursorResourceLoader
        {
            #region Methods

            public static Cursor LoadEmbeddedCursor(byte[] cursorResource, int imageIndex = 0)
            {
                var resourceHandle = GCHandle.Alloc(cursorResource, GCHandleType.Pinned);
                var iconImage = IntPtr.Zero;
                var cursorHandle = IntPtr.Zero;

                try
                {
                    var header = (IconHeader)Marshal.PtrToStructure(resourceHandle.AddrOfPinnedObject(), typeof(IconHeader));

                    if (imageIndex >= header.count)
                        throw new ArgumentOutOfRangeException("imageIndex");

                    var iconInfoPtr = resourceHandle.AddrOfPinnedObject() + Marshal.SizeOf(typeof(IconHeader)) + imageIndex * Marshal.SizeOf(typeof(IconInfo));
                    var info = (IconInfo)Marshal.PtrToStructure(iconInfoPtr, typeof(IconInfo));

                    iconImage = Marshal.AllocHGlobal(info.size + 4);
                    Marshal.WriteInt16(iconImage + 0, info.hotspot_x);
                    Marshal.WriteInt16(iconImage + 2, info.hotspot_y);
                    Marshal.Copy(cursorResource, info.offset, iconImage + 4, info.size);

                    cursorHandle = NativeMethods.CreateIconFromResource(iconImage, info.size + 4, false, 0x30000);
                    return new Cursor(cursorHandle);
                }
                finally
                {
                    if (cursorHandle != IntPtr.Zero)
                        NativeMethods.DestroyIcon(cursorHandle);

                    if (iconImage != IntPtr.Zero)
                        Marshal.FreeHGlobal(iconImage);

                    if (resourceHandle.IsAllocated)
                        resourceHandle.Free();
                }
            }

            #endregion Methods

            #region Native Methods

            static class NativeMethods
            {
                [DllImportAttribute("user32.dll", CharSet = CharSet.Unicode)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DestroyIcon(IntPtr hIcon);

                [DllImport("user32.dll", SetLastError = true)]
                public static extern IntPtr CreateIconFromResource(IntPtr pbIconBits, int dwResSize, bool fIcon, int dwVer);
            }

            #endregion Native Methods

            #region Native Structures

            [StructLayout(LayoutKind.Explicit, Pack = 1)]
            struct IconHeader
            {
                [FieldOffset(0)]
                public short reserved;

                [FieldOffset(2)]
                public short type;

                [FieldOffset(4)]
                public short count;
            }

            /// <summary>Union structure for icons and cursors.</summary>
            /// <remarks>For icons, field offset 4 is used for planes and field offset 6 for
            /// bits-per-pixel, while for cursors field offset 4 is used for the x coordinate
            /// of the hotspot, and field offset 6 is used for the y coordinate.</remarks>
            [StructLayout(LayoutKind.Explicit, Pack = 1)]
            struct IconInfo
            {
                [FieldOffset(0)]
                public byte width;

                [FieldOffset(1)]
                public byte height;

                [FieldOffset(2)]
                public byte colors;

                [FieldOffset(3)]
                public byte reserved;

                [FieldOffset(4)]
                public short planes;

                [FieldOffset(6)]
                public short bpp;

                [FieldOffset(4)]
                public short hotspot_x;

                [FieldOffset(6)]
                public short hotspot_y;

                [FieldOffset(8)]
                public int size;

                [FieldOffset(12)]
                public int offset;
            }

            #endregion Native Structures
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
            // 
            // AreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AreaForm";
            this.Opacity = 0.33D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AreaForm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
        }

        private void mouse_Button(object sender, MouseEventArgs e)
        {
            this.Cursor = precisionCursor;
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
        this.Cursor = Cursors.Hand;
            }
      this.CurrentTopLeft.X = this.ClickPoint.X - num;
      this.CurrentTopLeft.Y = this.ClickPoint.Y - num;
      this.CurrentBottomRight.X = this.ClickPoint.X + num;
      this.CurrentBottomRight.Y = this.ClickPoint.Y + num;
      this.g.DrawRectangle(this.MyPen, this.CurrentTopLeft.X, this.CurrentTopLeft.Y, this.CurrentBottomRight.X - this.CurrentTopLeft.X, this.CurrentBottomRight.Y - this.CurrentTopLeft.Y);
    }
  }


}
