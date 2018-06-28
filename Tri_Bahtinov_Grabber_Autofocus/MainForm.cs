using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Tri_Bahtinov_Grabber_Autofocus
{
  public class MainForm : Form
  {
    private static int num_errorvalues = 150;
    private int yzero = 1;
    private float errortarget = 0.25f;
    private float[] bahtinov_angles = new float[3];
    private int updateinterval = 100;
    private float[] errorvalues = new float[MainForm.num_errorvalues];
    private IContainer components;
    private Button StartButton;
    private PictureBox pictureBox;
    private GroupBox ScopeCamSetupGroupBox;
    private Label PixelSizeLabel;
    private NumericUpDown PixelSizeNumericUpDown;
    private Label DiameterLabel;
    private NumericUpDown DiameterNumericUpDown;
    private Label FocalLengthLabel;
    private NumericUpDown FocalLengthNumericUpDown;
    private CheckBox SoundCheckBox;
    private Label FocusErrorLabel;
    private Label AverageFocusErrorLabel;
    private Label WithinCriticalFocusLabel;
    private Label AbsFocusErrorLabel;
    private Label MaskTypeLabel;
    private Label MaskAnglesLabel;
    private CheckBox BlueCheckBox;
    private CheckBox RedCheckBox;
    private CheckBox GreenCheckBox;
    private GroupBox RGBGroupBox;
    private CheckBox RotatingFocusserCheckBox;
    private bool logging_enabled;
    private bool text_on_bitmap;
    private Grabber bahtinov_grabber;
    private DateTime DateTimePrevious;
    private Bitmap buffered_picture;
    private float error_previous;
    private float error;
    private float error0;
    private float error1;
    private float error2;
    private float sensitivity;
    private bool centered;
    private int autofocus;
    private Timer update_timer;
    private Label lblYES;
    private Label lblNO;
    private Label str3aLabel;
    private Label str3bLabel;
    private Label str3cLabel;
    private Button AboutBTN;
    private Button UpdateBTN;
    private ToolStripStatusLabel CreditStripStatusLabel;
    private ToolStripStatusLabel URLStripStatusLabel;
    private StatusStrip StatusBar;
    private Button HelpBTN;
        private GroupBox groupboxMode;
        private RadioButton RadioBTNmodeNight;
        private RadioButton RadioBTNmodeReg;
        private int errorcounter;
        

    public MainForm()
    {
      this.InitializeComponent();
      this.GetRegistryValues();
        }

        protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StartButton = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.ScopeCamSetupGroupBox = new System.Windows.Forms.GroupBox();
            this.PixelSizeLabel = new System.Windows.Forms.Label();
            this.RotatingFocusserCheckBox = new System.Windows.Forms.CheckBox();
            this.PixelSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.DiameterLabel = new System.Windows.Forms.Label();
            this.DiameterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.FocalLengthLabel = new System.Windows.Forms.Label();
            this.FocalLengthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SoundCheckBox = new System.Windows.Forms.CheckBox();
            this.FocusErrorLabel = new System.Windows.Forms.Label();
            this.AverageFocusErrorLabel = new System.Windows.Forms.Label();
            this.WithinCriticalFocusLabel = new System.Windows.Forms.Label();
            this.AbsFocusErrorLabel = new System.Windows.Forms.Label();
            this.MaskTypeLabel = new System.Windows.Forms.Label();
            this.MaskAnglesLabel = new System.Windows.Forms.Label();
            this.BlueCheckBox = new System.Windows.Forms.CheckBox();
            this.RedCheckBox = new System.Windows.Forms.CheckBox();
            this.GreenCheckBox = new System.Windows.Forms.CheckBox();
            this.RGBGroupBox = new System.Windows.Forms.GroupBox();
            this.lblYES = new System.Windows.Forms.Label();
            this.lblNO = new System.Windows.Forms.Label();
            this.str3aLabel = new System.Windows.Forms.Label();
            this.str3bLabel = new System.Windows.Forms.Label();
            this.str3cLabel = new System.Windows.Forms.Label();
            this.AboutBTN = new System.Windows.Forms.Button();
            this.UpdateBTN = new System.Windows.Forms.Button();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.CreditStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.URLStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.HelpBTN = new System.Windows.Forms.Button();
            this.groupboxMode = new System.Windows.Forms.GroupBox();
            this.RadioBTNmodeNight = new System.Windows.Forms.RadioButton();
            this.RadioBTNmodeReg = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.ScopeCamSetupGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PixelSizeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiameterNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FocalLengthNumericUpDown)).BeginInit();
            this.RGBGroupBox.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.groupboxMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartButton.Location = new System.Drawing.Point(261, 35);
            this.StartButton.MinimumSize = new System.Drawing.Size(245, 62);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(245, 62);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Set Capture Area";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(261, 103);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(245, 230);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // ScopeCamSetupGroupBox
            // 
            this.ScopeCamSetupGroupBox.Controls.Add(this.PixelSizeLabel);
            this.ScopeCamSetupGroupBox.Controls.Add(this.RotatingFocusserCheckBox);
            this.ScopeCamSetupGroupBox.Controls.Add(this.PixelSizeNumericUpDown);
            this.ScopeCamSetupGroupBox.Controls.Add(this.DiameterLabel);
            this.ScopeCamSetupGroupBox.Controls.Add(this.DiameterNumericUpDown);
            this.ScopeCamSetupGroupBox.Controls.Add(this.FocalLengthLabel);
            this.ScopeCamSetupGroupBox.Controls.Add(this.FocalLengthNumericUpDown);
            this.ScopeCamSetupGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScopeCamSetupGroupBox.Location = new System.Drawing.Point(9, 12);
            this.ScopeCamSetupGroupBox.Name = "ScopeCamSetupGroupBox";
            this.ScopeCamSetupGroupBox.Size = new System.Drawing.Size(188, 126);
            this.ScopeCamSetupGroupBox.TabIndex = 7;
            this.ScopeCamSetupGroupBox.TabStop = false;
            this.ScopeCamSetupGroupBox.Text = "Telescope && Camera Setup";
            // 
            // PixelSizeLabel
            // 
            this.PixelSizeLabel.AutoSize = true;
            this.PixelSizeLabel.Location = new System.Drawing.Point(18, 74);
            this.PixelSizeLabel.Name = "PixelSizeLabel";
            this.PixelSizeLabel.Size = new System.Drawing.Size(90, 13);
            this.PixelSizeLabel.TabIndex = 5;
            this.PixelSizeLabel.Text = "Pixel Size (μm)";
            // 
            // RotatingFocusserCheckBox
            // 
            this.RotatingFocusserCheckBox.AutoSize = true;
            this.RotatingFocusserCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RotatingFocusserCheckBox.Location = new System.Drawing.Point(30, 98);
            this.RotatingFocusserCheckBox.Name = "RotatingFocusserCheckBox";
            this.RotatingFocusserCheckBox.Size = new System.Drawing.Size(121, 17);
            this.RotatingFocusserCheckBox.TabIndex = 7;
            this.RotatingFocusserCheckBox.Text = "Rotating Focuser";
            this.RotatingFocusserCheckBox.UseVisualStyleBackColor = true;
            this.RotatingFocusserCheckBox.CheckedChanged += new System.EventHandler(this.RotatingFocusserCheckBox_CheckedChanged);
            // 
            // PixelSizeNumericUpDown
            // 
            this.PixelSizeNumericUpDown.BackColor = System.Drawing.Color.Red;
            this.PixelSizeNumericUpDown.DecimalPlaces = 2;
            this.PixelSizeNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PixelSizeNumericUpDown.Location = new System.Drawing.Point(114, 72);
            this.PixelSizeNumericUpDown.Name = "PixelSizeNumericUpDown";
            this.PixelSizeNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.PixelSizeNumericUpDown.TabIndex = 4;
            this.PixelSizeNumericUpDown.Value = new decimal(new int[] {
            375,
            0,
            0,
            131072});
            this.PixelSizeNumericUpDown.ValueChanged += new System.EventHandler(this.PixelSizeNumericUpDown_ValueChanged);
            // 
            // DiameterLabel
            // 
            this.DiameterLabel.AutoSize = true;
            this.DiameterLabel.Location = new System.Drawing.Point(27, 48);
            this.DiameterLabel.Name = "DiameterLabel";
            this.DiameterLabel.Size = new System.Drawing.Size(78, 13);
            this.DiameterLabel.TabIndex = 3;
            this.DiameterLabel.Text = "Diameter (m)";
            // 
            // DiameterNumericUpDown
            // 
            this.DiameterNumericUpDown.BackColor = System.Drawing.Color.Red;
            this.DiameterNumericUpDown.DecimalPlaces = 3;
            this.DiameterNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.DiameterNumericUpDown.Location = new System.Drawing.Point(115, 46);
            this.DiameterNumericUpDown.Name = "DiameterNumericUpDown";
            this.DiameterNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.DiameterNumericUpDown.TabIndex = 2;
            this.DiameterNumericUpDown.Value = new decimal(new int[] {
            235,
            0,
            0,
            196608});
            this.DiameterNumericUpDown.ValueChanged += new System.EventHandler(this.DiameterNumericUpDown_ValueChanged);
            // 
            // FocalLengthLabel
            // 
            this.FocalLengthLabel.AutoSize = true;
            this.FocalLengthLabel.Location = new System.Drawing.Point(7, 23);
            this.FocalLengthLabel.Name = "FocalLengthLabel";
            this.FocalLengthLabel.Size = new System.Drawing.Size(102, 13);
            this.FocalLengthLabel.TabIndex = 1;
            this.FocalLengthLabel.Text = "Focal Length (m)";
            // 
            // FocalLengthNumericUpDown
            // 
            this.FocalLengthNumericUpDown.BackColor = System.Drawing.Color.Red;
            this.FocalLengthNumericUpDown.DecimalPlaces = 3;
            this.FocalLengthNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.FocalLengthNumericUpDown.Location = new System.Drawing.Point(115, 21);
            this.FocalLengthNumericUpDown.Name = "FocalLengthNumericUpDown";
            this.FocalLengthNumericUpDown.Size = new System.Drawing.Size(60, 20);
            this.FocalLengthNumericUpDown.TabIndex = 0;
            this.FocalLengthNumericUpDown.Value = new decimal(new int[] {
            2350,
            0,
            0,
            196608});
            this.FocalLengthNumericUpDown.ValueChanged += new System.EventHandler(this.FocalLengthNumericUpDown_ValueChanged);
            // 
            // SoundCheckBox
            // 
            this.SoundCheckBox.AutoSize = true;
            this.SoundCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SoundCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SoundCheckBox.Location = new System.Drawing.Point(261, 12);
            this.SoundCheckBox.Name = "SoundCheckBox";
            this.SoundCheckBox.Size = new System.Drawing.Size(245, 17);
            this.SoundCheckBox.TabIndex = 8;
            this.SoundCheckBox.Text = "Play Sound When Best Focus Is Found";
            this.SoundCheckBox.UseVisualStyleBackColor = true;
            this.SoundCheckBox.CheckedChanged += new System.EventHandler(this.SoundCheckBox_CheckedChanged);
            // 
            // FocusErrorLabel
            // 
            this.FocusErrorLabel.AutoSize = true;
            this.FocusErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FocusErrorLabel.Location = new System.Drawing.Point(11, 150);
            this.FocusErrorLabel.Name = "FocusErrorLabel";
            this.FocusErrorLabel.Size = new System.Drawing.Size(97, 13);
            this.FocusErrorLabel.TabIndex = 9;
            this.FocusErrorLabel.Text = "Focus Error (px)";
            // 
            // AverageFocusErrorLabel
            // 
            this.AverageFocusErrorLabel.AutoSize = true;
            this.AverageFocusErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageFocusErrorLabel.Location = new System.Drawing.Point(11, 175);
            this.AverageFocusErrorLabel.Name = "AverageFocusErrorLabel";
            this.AverageFocusErrorLabel.Size = new System.Drawing.Size(148, 13);
            this.AverageFocusErrorLabel.TabIndex = 10;
            this.AverageFocusErrorLabel.Text = "Average Focus Error (px)";
            // 
            // WithinCriticalFocusLabel
            // 
            this.WithinCriticalFocusLabel.AutoSize = true;
            this.WithinCriticalFocusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WithinCriticalFocusLabel.Location = new System.Drawing.Point(11, 229);
            this.WithinCriticalFocusLabel.Name = "WithinCriticalFocusLabel";
            this.WithinCriticalFocusLabel.Size = new System.Drawing.Size(124, 13);
            this.WithinCriticalFocusLabel.TabIndex = 11;
            this.WithinCriticalFocusLabel.Text = "Within Critical Focus";
            // 
            // AbsFocusErrorLabel
            // 
            this.AbsFocusErrorLabel.AutoSize = true;
            this.AbsFocusErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbsFocusErrorLabel.Location = new System.Drawing.Point(11, 202);
            this.AbsFocusErrorLabel.Name = "AbsFocusErrorLabel";
            this.AbsFocusErrorLabel.Size = new System.Drawing.Size(153, 13);
            this.AbsFocusErrorLabel.TabIndex = 12;
            this.AbsFocusErrorLabel.Text = "Absolute Focus Error (μm)";
            // 
            // MaskTypeLabel
            // 
            this.MaskTypeLabel.AutoSize = true;
            this.MaskTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaskTypeLabel.Location = new System.Drawing.Point(11, 257);
            this.MaskTypeLabel.Name = "MaskTypeLabel";
            this.MaskTypeLabel.Size = new System.Drawing.Size(69, 13);
            this.MaskTypeLabel.TabIndex = 13;
            this.MaskTypeLabel.Text = "Mask Type";
            // 
            // MaskAnglesLabel
            // 
            this.MaskAnglesLabel.AutoSize = true;
            this.MaskAnglesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaskAnglesLabel.Location = new System.Drawing.Point(11, 282);
            this.MaskAnglesLabel.Name = "MaskAnglesLabel";
            this.MaskAnglesLabel.Size = new System.Drawing.Size(79, 13);
            this.MaskAnglesLabel.TabIndex = 14;
            this.MaskAnglesLabel.Text = "Mask Angles";
            // 
            // BlueCheckBox
            // 
            this.BlueCheckBox.AutoSize = true;
            this.BlueCheckBox.Checked = true;
            this.BlueCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BlueCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BlueCheckBox.Location = new System.Drawing.Point(14, 65);
            this.BlueCheckBox.Name = "BlueCheckBox";
            this.BlueCheckBox.Size = new System.Drawing.Size(49, 17);
            this.BlueCheckBox.TabIndex = 16;
            this.BlueCheckBox.Text = "Blue";
            this.BlueCheckBox.UseVisualStyleBackColor = true;
            // 
            // RedCheckBox
            // 
            this.RedCheckBox.AutoSize = true;
            this.RedCheckBox.Checked = true;
            this.RedCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RedCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RedCheckBox.Location = new System.Drawing.Point(14, 19);
            this.RedCheckBox.Name = "RedCheckBox";
            this.RedCheckBox.Size = new System.Drawing.Size(47, 17);
            this.RedCheckBox.TabIndex = 15;
            this.RedCheckBox.Text = "Red";
            this.RedCheckBox.UseVisualStyleBackColor = true;
            // 
            // GreenCheckBox
            // 
            this.GreenCheckBox.AutoSize = true;
            this.GreenCheckBox.Checked = true;
            this.GreenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GreenCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GreenCheckBox.Location = new System.Drawing.Point(14, 42);
            this.GreenCheckBox.Name = "GreenCheckBox";
            this.GreenCheckBox.Size = new System.Drawing.Size(58, 17);
            this.GreenCheckBox.TabIndex = 17;
            this.GreenCheckBox.Text = "Green";
            this.GreenCheckBox.UseVisualStyleBackColor = true;
            // 
            // RGBGroupBox
            // 
            this.RGBGroupBox.Controls.Add(this.GreenCheckBox);
            this.RGBGroupBox.Controls.Add(this.BlueCheckBox);
            this.RGBGroupBox.Controls.Add(this.RedCheckBox);
            this.RGBGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGBGroupBox.Location = new System.Drawing.Point(9, 307);
            this.RGBGroupBox.Name = "RGBGroupBox";
            this.RGBGroupBox.Size = new System.Drawing.Size(188, 91);
            this.RGBGroupBox.TabIndex = 18;
            this.RGBGroupBox.TabStop = false;
            this.RGBGroupBox.Text = "RGB Channels Used";
            // 
            // lblYES
            // 
            this.lblYES.AutoSize = true;
            this.lblYES.BackColor = System.Drawing.Color.Black;
            this.lblYES.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYES.ForeColor = System.Drawing.Color.Green;
            this.lblYES.Location = new System.Drawing.Point(150, 456);
            this.lblYES.Name = "lblYES";
            this.lblYES.Size = new System.Drawing.Size(38, 20);
            this.lblYES.TabIndex = 19;
            this.lblYES.Text = "yes";
            this.lblYES.Visible = false;
            // 
            // lblNO
            // 
            this.lblNO.AutoSize = true;
            this.lblNO.BackColor = System.Drawing.Color.Black;
            this.lblNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNO.ForeColor = System.Drawing.Color.Red;
            this.lblNO.Location = new System.Drawing.Point(150, 482);
            this.lblNO.Name = "lblNO";
            this.lblNO.Size = new System.Drawing.Size(29, 20);
            this.lblNO.TabIndex = 20;
            this.lblNO.Text = "no";
            this.lblNO.Visible = false;
            // 
            // str3aLabel
            // 
            this.str3aLabel.AutoSize = true;
            this.str3aLabel.BackColor = System.Drawing.Color.Black;
            this.str3aLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.str3aLabel.ForeColor = System.Drawing.Color.Red;
            this.str3aLabel.Location = new System.Drawing.Point(144, 407);
            this.str3aLabel.Name = "str3aLabel";
            this.str3aLabel.Size = new System.Drawing.Size(0, 13);
            this.str3aLabel.TabIndex = 21;
            // 
            // str3bLabel
            // 
            this.str3bLabel.AutoSize = true;
            this.str3bLabel.BackColor = System.Drawing.Color.Black;
            this.str3bLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.str3bLabel.ForeColor = System.Drawing.Color.Green;
            this.str3bLabel.Location = new System.Drawing.Point(150, 407);
            this.str3bLabel.Name = "str3bLabel";
            this.str3bLabel.Size = new System.Drawing.Size(0, 13);
            this.str3bLabel.TabIndex = 22;
            // 
            // str3cLabel
            // 
            this.str3cLabel.AutoSize = true;
            this.str3cLabel.BackColor = System.Drawing.Color.Black;
            this.str3cLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.str3cLabel.ForeColor = System.Drawing.Color.Blue;
            this.str3cLabel.Location = new System.Drawing.Point(156, 408);
            this.str3cLabel.Name = "str3cLabel";
            this.str3cLabel.Size = new System.Drawing.Size(0, 13);
            this.str3cLabel.TabIndex = 23;
            // 
            // AboutBTN
            // 
            this.AboutBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AboutBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AboutBTN.Location = new System.Drawing.Point(10, 482);
            this.AboutBTN.Name = "AboutBTN";
            this.AboutBTN.Size = new System.Drawing.Size(134, 23);
            this.AboutBTN.TabIndex = 24;
            this.AboutBTN.Text = "About Tri-Bahtinov Grabber";
            this.AboutBTN.UseVisualStyleBackColor = true;
            this.AboutBTN.Click += new System.EventHandler(this.AboutBTN_Click);
            // 
            // UpdateBTN
            // 
            this.UpdateBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.UpdateBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateBTN.Location = new System.Drawing.Point(9, 511);
            this.UpdateBTN.Name = "UpdateBTN";
            this.UpdateBTN.Size = new System.Drawing.Size(134, 23);
            this.UpdateBTN.TabIndex = 25;
            this.UpdateBTN.Text = "Check For Update";
            this.UpdateBTN.UseVisualStyleBackColor = true;
            this.UpdateBTN.Click += new System.EventHandler(this.UpdateBTN_Click);
            // 
            // StatusBar
            // 
            this.StatusBar.BackColor = System.Drawing.Color.Red;
            this.StatusBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreditStripStatusLabel,
            this.URLStripStatusLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 540);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.StatusBar.Size = new System.Drawing.Size(518, 22);
            this.StatusBar.SizingGrip = false;
            this.StatusBar.TabIndex = 26;
            // 
            // CreditStripStatusLabel
            // 
            this.CreditStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.CreditStripStatusLabel.Name = "CreditStripStatusLabel";
            this.CreditStripStatusLabel.Size = new System.Drawing.Size(287, 17);
            this.CreditStripStatusLabel.Text = "Credit to the late Niels Noordhoek for the original ";
            // 
            // URLStripStatusLabel
            // 
            this.URLStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.URLStripStatusLabel.IsLink = true;
            this.URLStripStatusLabel.Name = "URLStripStatusLabel";
            this.URLStripStatusLabel.Size = new System.Drawing.Size(106, 17);
            this.URLStripStatusLabel.Text = "Bahtinov Grabber";
            this.URLStripStatusLabel.Click += new System.EventHandler(this.toolStripStatusLabel3_Click);
            // 
            // HelpBTN
            // 
            this.HelpBTN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.HelpBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpBTN.Location = new System.Drawing.Point(10, 453);
            this.HelpBTN.Name = "HelpBTN";
            this.HelpBTN.Size = new System.Drawing.Size(134, 23);
            this.HelpBTN.TabIndex = 27;
            this.HelpBTN.Text = "Help";
            this.HelpBTN.UseVisualStyleBackColor = true;
            this.HelpBTN.Click += new System.EventHandler(this.HelpBTN_Click);
            // 
            // groupboxMode
            // 
            this.groupboxMode.Controls.Add(this.RadioBTNmodeNight);
            this.groupboxMode.Controls.Add(this.RadioBTNmodeReg);
            this.groupboxMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupboxMode.Location = new System.Drawing.Point(9, 405);
            this.groupboxMode.Name = "groupboxMode";
            this.groupboxMode.Size = new System.Drawing.Size(188, 42);
            this.groupboxMode.TabIndex = 28;
            this.groupboxMode.TabStop = false;
            this.groupboxMode.Text = "MODE";
            // 
            // RadioBTNmodeNight
            // 
            this.RadioBTNmodeNight.AutoSize = true;
            this.RadioBTNmodeNight.Checked = true;
            this.RadioBTNmodeNight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RadioBTNmodeNight.Location = new System.Drawing.Point(106, 16);
            this.RadioBTNmodeNight.Name = "RadioBTNmodeNight";
            this.RadioBTNmodeNight.Size = new System.Drawing.Size(54, 17);
            this.RadioBTNmodeNight.TabIndex = 1;
            this.RadioBTNmodeNight.TabStop = true;
            this.RadioBTNmodeNight.Text = "Night";
            this.RadioBTNmodeNight.UseVisualStyleBackColor = true;
            this.RadioBTNmodeNight.CheckedChanged += new System.EventHandler(this.RadioBTNmodeNight_CheckedChanged);
            // 
            // RadioBTNmodeReg
            // 
            this.RadioBTNmodeReg.AutoSize = true;
            this.RadioBTNmodeReg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RadioBTNmodeReg.Location = new System.Drawing.Point(21, 16);
            this.RadioBTNmodeReg.Name = "RadioBTNmodeReg";
            this.RadioBTNmodeReg.Size = new System.Drawing.Size(68, 17);
            this.RadioBTNmodeReg.TabIndex = 0;
            this.RadioBTNmodeReg.Text = "Regular";
            this.RadioBTNmodeReg.UseVisualStyleBackColor = true;
            this.RadioBTNmodeReg.CheckedChanged += new System.EventHandler(this.RadioBTNmodeReg_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(518, 562);
            this.Controls.Add(this.groupboxMode);
            this.Controls.Add(this.HelpBTN);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.UpdateBTN);
            this.Controls.Add(this.AboutBTN);
            this.Controls.Add(this.str3cLabel);
            this.Controls.Add(this.str3bLabel);
            this.Controls.Add(this.str3aLabel);
            this.Controls.Add(this.lblNO);
            this.Controls.Add(this.lblYES);
            this.Controls.Add(this.RGBGroupBox);
            this.Controls.Add(this.MaskAnglesLabel);
            this.Controls.Add(this.MaskTypeLabel);
            this.Controls.Add(this.AbsFocusErrorLabel);
            this.Controls.Add(this.WithinCriticalFocusLabel);
            this.Controls.Add(this.AverageFocusErrorLabel);
            this.Controls.Add(this.FocusErrorLabel);
            this.Controls.Add(this.SoundCheckBox);
            this.Controls.Add(this.ScopeCamSetupGroupBox);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.StartButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(526, 601);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Tri-Bahtinov Grabber  v1.0 Build 20";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ScopeCamSetupGroupBox.ResumeLayout(false);
            this.ScopeCamSetupGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PixelSizeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiameterNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FocalLengthNumericUpDown)).EndInit();
            this.RGBGroupBox.ResumeLayout(false);
            this.RGBGroupBox.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.groupboxMode.ResumeLayout(false);
            this.groupboxMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private void GetRegistryValues()
    {
      try
      {
        this.FocalLengthNumericUpDown.Value = Convert.ToDecimal(Application.UserAppDataRegistry.GetValue("FocalLength"));
        this.DiameterNumericUpDown.Value = Convert.ToDecimal(Application.UserAppDataRegistry.GetValue("Diameter"));
        this.PixelSizeNumericUpDown.Value = Convert.ToDecimal(Application.UserAppDataRegistry.GetValue("PixelSize"));
        this.RotatingFocusserCheckBox.Checked = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("RotatingFocusser"));
        this.SoundCheckBox.Checked = Convert.ToBoolean(Application.UserAppDataRegistry.GetValue("Sound"));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
        this.FocalLengthNumericUpDown.Value = new Decimal(2032, 0, 0, false, (byte) 3);
        this.DiameterNumericUpDown.Value = new Decimal(2032, 0, 0, false, (byte) 4);
        this.PixelSizeNumericUpDown.Value = new Decimal(56, 0, 0, false, (byte) 1);
        this.RotatingFocusserCheckBox.Checked = false;
        this.SoundCheckBox.Checked = false;
        Application.UserAppDataRegistry.SetValue("FocalLength", (object) this.FocalLengthNumericUpDown.Value);
        Application.UserAppDataRegistry.SetValue("Diameter", (object) this.DiameterNumericUpDown.Value);
        Application.UserAppDataRegistry.SetValue("PixelSize", (object) this.PixelSizeNumericUpDown.Value);
        Application.UserAppDataRegistry.SetValue("RotatingFocusser", (object) this.RotatingFocusserCheckBox.Checked);
        Application.UserAppDataRegistry.SetValue("Sound", (object) this.SoundCheckBox.Checked);
      }
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
      this.autofocus = 0;
      this.centered = false;
      if (this.bahtinov_grabber == null)
      {
        this.bahtinov_grabber = new Grabber();
        this.bahtinov_grabber.picture = new Bitmap(128, 128);
        this.startTimer();
      }
      else
      {
        this.update_timer.Stop();
        this.bahtinov_angles = new float[3];
        this.bahtinov_grabber.ReStart();
        this.update_timer.Start();
      }
      this.errorcounter = 0;
    }

    private void logMessage(string logtext)
    {
      try
      {
        if (!this.logging_enabled)
          return;
        File.AppendAllText("c:/bahtinovgrabber.txt", DateTime.Now.ToString() + " " + logtext + "\r\n");
      }
      catch
      {
      }
    }

    private void startTimer()
    {
      this.update_timer = new Timer();
      this.update_timer.Interval = this.updateinterval;
      this.update_timer.Start();
      this.update_timer.Tick += new EventHandler(this.Timer_Tick);
    }

    private bool equal(Bitmap bmp1, Bitmap bmp2)
    {
      bool flag;
      if (bmp1.Size != bmp2.Size)
      {
        flag = true;
      }
      else
      {
        flag = false;
        for (int x = 0; x < bmp1.Width & !flag; ++x)
        {
          for (int y = 0; y < bmp1.Height & !flag; ++y)
          {
            if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
              flag = true;
          }
        }
      }
      return !flag;
    }

    private void Timer_Tick(object sender, EventArgs eArgs)
    {
      if (!(this.bahtinov_grabber.activated & this.bahtinov_grabber.picture != null))
        return;
      if (this.buffered_picture == null)
        this.buffered_picture = new Bitmap(this.bahtinov_grabber.picture.Width, this.bahtinov_grabber.picture.Height);
      if (this.equal(this.bahtinov_grabber.picture, this.buffered_picture))
        return;
      this.pictureBox.Image = (Image) this.bahtinov_grabber.picture;
      this.buffered_picture = new Bitmap((Image) this.bahtinov_grabber.picture);
      this.ShowLines(ref this.bahtinov_grabber.picture, ref this.bahtinov_angles);
      this.Size = new Size(Math.Max(158, this.pictureBox.Size.Width + 300), Math.Max(320, this.pictureBox.Size.Height + 165));
      this.StartButton.Size = new Size(this.pictureBox.Size.Width, 62);
    }

    private void ShowLines(ref Bitmap bmp, ref float[] bahtinov_angles)
    {
      Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
      BitmapData bitmapdata = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
      IntPtr scan0 = bitmapdata.Scan0;
      int length1 = bitmapdata.Stride * bmp.Height;
      byte[] numArray1 = new byte[length1];
      Marshal.Copy(scan0, numArray1, 0, length1);
      Marshal.Copy(numArray1, 0, scan0, length1);
      bmp.UnlockBits(bitmapdata);
      int width = bmp.Width;
      int height = bmp.Height;
      float num1 = (width < height ? 0.5f * (float) Math.Sqrt(2.0) * (float) width : 0.5f * (float) Math.Sqrt(2.0) * (float) height) - 8f;
      int num2 = (int) (0.5 * ((double) width - (double) num1));
      int num3 = (int) (0.5 * ((double) width + (double) num1));
      int num4 = (int) (0.5 * ((double) height - (double) num1));
      int num5 = (int) (0.5 * ((double) height + (double) num1));
      int num6 = 1;
      int num7 = num6;
      float num8 = this.RedCheckBox.Checked ? 1f : 0.0f;
      float num9 = this.GreenCheckBox.Checked ? 1f : 0.0f;
      float num10 = this.BlueCheckBox.Checked ? 1f : 0.0f;
      float[,] numArray2 = new float[width, height];
      float num11 = 0.0f;
      float num12 = 0.0f;
      for (int index1 = num2; index1 < num3; ++index1)
      {
        for (int index2 = num4; index2 < num5; ++index2)
        {
          int index3 = (index1 + index2 * width) * bitmapdata.Stride / width;
          numArray2[index1, index2] = (float) ((double) num8 * (double) numArray1[index3] + (double) num9 * (double) numArray1[index3 + 1] + (double) num10 * (double) numArray1[index3 + 2]);
          numArray2[index1, index2] /= 3f;
          numArray2[index1, index2] /= (float) byte.MaxValue;
          numArray2[index1, index2] = (float) Math.Sqrt((double) numArray2[index1, index2]);
          num11 += numArray2[index1, index2];
          ++num12;
        }
      }
      try
      {
        float num13 = num11 / num12;
      }
      catch
      {
      }
      float val1 = (float) (((double) width + 1.0) / 2.0);
      float val2 = (float) (((double) height + 1.0) / 2.0);
      float[] numArray3 = new float[3];
      float[] numArray4 = new float[3];
      float num14 = 0.0f;
      float num15 = 0.0f;
      float num16 = 0.0f;
      float num17 = 0.0f;
      if ((double) bahtinov_angles[0] == 0.0 & (double) bahtinov_angles[1] == 0.0 & (double) bahtinov_angles[2] == 0.0)
      {
        int length2 = 180;
        float num13 = 3.141593f / (float) length2;
        float[] numArray5 = new float[length2];
        float[] numArray6 = new float[length2];
        float[,] numArray7 = new float[width, height];
        for (int index1 = 0; index1 < length2; ++index1)
        {
          float num18 = num13 * (float) index1;
          float num19 = (float) Math.Sin((double) num18);
          float num20 = (float) Math.Cos((double) num18);
          int index2 = num2;
          while (index2 < num3)
          {
            int index3 = num4;
            while (index3 < num5)
            {
              float num21 = (float) index2 - val1;
              float num22 = (float) index3 - val2;
              float num23 = (float) ((double) val1 + (double) num21 * (double) num20 + (double) num22 * (double) num19);
              float num24 = (float) ((double) val2 - (double) num21 * (double) num19 + (double) num22 * (double) num20);
              int index4 = (int) Math.Floor((double) num23);
              int index5 = (int) Math.Ceiling((double) num23);
              int index6 = (int) Math.Floor((double) num24);
              int index7 = (int) Math.Ceiling((double) num24);
              float num25 = num23 - (float) index4;
              float num26 = num24 - (float) index6;
              numArray7[index2, index3] = (float) ((double) numArray2[index4, index6] * (1.0 - (double) num25) * (1.0 - (double) num26) + (double) numArray2[index5, index6] * (double) num25 * (1.0 - (double) num26) + (double) numArray2[index5, index7] * (double) num25 * (double) num26 + (double) numArray2[index4, index7] * (1.0 - (double) num25) * (double) num26);
              index3 += num7;
            }
            index2 += num6;
          }
          float[] numArray8 = new float[height];
          for (int index3 = 0; index3 < height; ++index3)
            numArray8[index3] = 0.0f;
          int index8 = num4;
          while (index8 < num5)
          {
            int num21 = 0;
            int index3 = num2;
            while (index3 < num3)
            {
              numArray8[index8] += numArray7[index3, index8];
              ++num21;
              index3 += num6;
            }
            numArray8[index8] /= (float) num21;
            index8 += num7;
          }
          int num27 = 1;
          float[] numArray9 = new float[height];
          for (int index3 = 0; index3 < height; ++index3)
            numArray9[index3] = numArray8[index3];
          for (int index3 = num4; index3 < num5; ++index3)
          {
            numArray9[index3] = 0.0f;
            for (int index4 = -(num27 - 1) / 2; index4 <= (num27 - 1) / 2; ++index4)
              numArray9[index3] += numArray8[index3 + index4] / (float) num27;
          }
          for (int index3 = 0; index3 < height; ++index3)
            numArray8[index3] = numArray9[index3];
          float num28 = -1f;
          float num29 = -1f;
          for (int index3 = num4; index3 < num5; ++index3)
          {
            if ((double) numArray8[index3] > (double) num28)
            {
              num29 = (float) index3;
              num28 = numArray8[index3];
            }
          }
          try
          {
            numArray5[index1] = num29;
            numArray6[index1] = num28;
          }
          catch
          {
            int num21 = (int) MessageBox.Show("ERROR");
          }
        }
        int num30 = 0;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          float num18 = -1f;
          float num19 = -1f;
          float num20 = -1f;
          for (int index2 = 0; index2 < length2; ++index2)
          {
            if ((double) numArray6[index2] > (double) num18)
            {
              num18 = numArray6[index2];
              num20 = numArray5[index2];
              num19 = (float) index2 * num13;
              num30 = index2;
            }
          }
          numArray4[index1] = num20;
          numArray3[index1] = num19;
          bahtinov_angles[index1] = num19;
          int num21 = (int) (0.0872664600610733 / (double) num13);
          for (int index2 = num30 - num21; index2 < num30 + num21; ++index2)
          {
            int index3 = (index2 + length2) % length2;
            try
            {
              numArray6[index3] = 0.0f;
            }
            catch
            {
              int num22 = (int) MessageBox.Show("ERROR");
            }
          }
        }
        this.DateTimePrevious = DateTime.Now;
        if (this.RotatingFocusserCheckBox.Checked)
          bahtinov_angles = new float[3];
      }
      else
      {
        int length2 = 3;
        float[] numArray5 = new float[length2];
        float[] numArray6 = new float[length2];
        float[,] numArray7 = new float[width, height];
        for (int index1 = 0; index1 < length2; ++index1)
        {
          float num13 = bahtinov_angles[index1];
          float num18 = (float) Math.Sin((double) num13);
          float num19 = (float) Math.Cos((double) num13);
          int index2 = num2;
          while (index2 < num3)
          {
            int index3 = num4;
            while (index3 < num5)
            {
              float num20 = (float) index2 - val1;
              float num21 = (float) index3 - val2;
              float num22 = (float) ((double) val1 + (double) num20 * (double) num19 + (double) num21 * (double) num18);
              float num23 = (float) ((double) val2 - (double) num20 * (double) num18 + (double) num21 * (double) num19);
              int index4 = (int) Math.Floor((double) num22);
              int index5 = (int) Math.Ceiling((double) num22);
              int index6 = (int) Math.Floor((double) num23);
              int index7 = (int) Math.Ceiling((double) num23);
              float num24 = num22 - (float) index4;
              float num25 = num23 - (float) index6;
              numArray7[index2, index3] = (float) ((double) numArray2[index4, index6] * (1.0 - (double) num24) * (1.0 - (double) num25) + (double) numArray2[index5, index6] * (double) num24 * (1.0 - (double) num25) + (double) numArray2[index5, index7] * (double) num24 * (double) num25 + (double) numArray2[index4, index7] * (1.0 - (double) num24) * (double) num25);
              index3 += num7;
            }
            index2 += num6;
          }
          float[] yvals = new float[height];
          for (int index3 = 0; index3 < height; ++index3)
            yvals[index3] = 0.0f;
          int index8 = num4;
          while (index8 < num5)
          {
            int num20 = 0;
            int index3 = num2;
            while (index3 < num3)
            {
              yvals[index8] += numArray7[index3, index8];
              ++num20;
              index3 += num6;
            }
            yvals[index8] /= (float) num20;
            index8 += num7;
          }
          float num26 = -1f;
          float num27 = -1f;
          for (int index3 = num4; index3 < num5; ++index3)
          {
            if ((double) yvals[index3] > (double) num26)
            {
              num27 = (float) index3;
              num26 = yvals[index3];
            }
          }
          float num28 = new LSQcalculator().peakposition(yvals, (int) num27, 2);
          try
          {
            numArray5[index1] = num28;
            numArray6[index1] = num26;
          }
          catch
          {
            int num20 = (int) MessageBox.Show("ERROR");
          }
        }
        for (int index = 0; index < 3; ++index)
        {
          numArray4[index] = numArray5[index];
          numArray3[index] = bahtinov_angles[index];
        }
      }
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = index1; index2 < 3; ++index2)
        {
          if ((double) numArray3[index2] < (double) numArray3[index1])
          {
            float num13 = numArray3[index1];
            numArray3[index1] = numArray3[index2];
            numArray3[index2] = num13;
            float num18 = numArray4[index1];
            numArray4[index1] = numArray4[index2];
            numArray4[index2] = num18;
          }
        }
      }
      if ((double) numArray3[1] - (double) numArray3[0] > Math.PI / 2.0)
      {
        numArray3[1] -= 3.141593f;
        numArray3[2] -= 3.141593f;
        numArray4[1] = (float) height - numArray4[1];
        numArray4[2] = (float) height - numArray4[2];
      }
      if ((double) numArray3[2] - (double) numArray3[1] > 1.57079637050629)
      {
        numArray3[2] -= 3.141593f;
        numArray4[2] = (float) height - numArray4[2];
      }
      for (int index1 = 0; index1 < 3; ++index1)
      {
        for (int index2 = index1; index2 < 3; ++index2)
        {
          if ((double) numArray3[index2] < (double) numArray3[index1])
          {
            float num13 = numArray3[index1];
            numArray3[index1] = numArray3[index2];
            numArray3[index2] = num13;
            float num18 = numArray4[index1];
            numArray4[index1] = numArray4[index2];
            numArray4[index2] = num18;
          }
        }
      }
      int num31 = 2;
      Pen pen = new Pen(Color.Green, (float) num31);
      pen.DashStyle = DashStyle.Dash;
      Graphics graphics = Graphics.FromImage(this.pictureBox.Image);
      float num32 = 0.0f;
      float num33 = 0.0f;
      float num34 = 0.0f;
      float num35 = 0.0f;
      for (int index = 0; index < 3; ++index)
      {
        float num13 = Math.Min(val1, val2);
        float x1 = val1 + -num13 * (float) Math.Cos((double) numArray3[index]) + (numArray4[index] - val2) * (float) Math.Sin((double) numArray3[index]);
        float x2 = val1 + num13 * (float) Math.Cos((double) numArray3[index]) + (numArray4[index] - val2) * (float) Math.Sin((double) numArray3[index]);
        float num18 = val2 + -num13 * (float) Math.Sin((double) numArray3[index]) + (float) -((double) numArray4[index] - (double) val2) * (float) Math.Cos((double) numArray3[index]);
        float num19 = val2 + num13 * (float) Math.Sin((double) numArray3[index]) + (float) -((double) numArray4[index] - (double) val2) * (float) Math.Cos((double) numArray3[index]);
        if (index == 0)
        {
          float num20 = x1;
          float num21 = x2;
          float num22 = num18;
          float num23 = num19;
          num14 = (float) (((double) num23 - (double) num22) / ((double) num21 - (double) num20));
          num16 = (float) (-(double) num20 * (((double) num23 - (double) num22) / ((double) num21 - (double) num20))) + num22;
        }
        else if (index == 1)
        {
          num32 = x1;
          num34 = x2;
          num33 = num18;
          num35 = num19;
          double num20 = ((double) num35 - (double) num33) / ((double) num34 - (double) num32);
          double num21 = ((double) num35 - (double) num33) / ((double) num34 - (double) num32);
        }
        else if (index == 2)
        {
          float num20 = x1;
          float num21 = x2;
          float num22 = num18;
          float num23 = num19;
          num15 = (float) (((double) num23 - (double) num22) / ((double) num21 - (double) num20));
          num17 = (float) (-(double) num20 * (((double) num23 - (double) num22) / ((double) num21 - (double) num20))) + num22;
        }
        switch (index)
        {
          case 0:
            //Blue Diffraction Line
            pen.Color = Color.Blue;
            break;
          case 1:
            //Green Diffraction Line
            pen.Color = Color.Green;
            break;
          default:
            //Red Diffraction Line
            pen.Color = Color.Red;
            break;
        }
        graphics.DrawLine(pen, x1, (float) height - num18 + (float) this.yzero, x2, (float) height - num19 + (float) this.yzero);
      }
      float x3 = (float) (-((double) num16 - (double) num17) / ((double) num14 - (double) num15));
      float num36 = num14 * x3 + num16;
      //Centre Focus Circle Colour in Green Diffraction Line
      pen.Color = Color.Cyan;
      int num37 = num31 * 4;
      graphics.DrawEllipse(pen, x3 - (float) num37, (float) height - num36 - (float) num37 + (float) this.yzero, (float) (num37 * 2), (float) (num37 * 2));
      float num38 = (float) (((double) x3 - (double) num32) * ((double) num34 - (double) num32) + ((double) num36 - (double) num33) * ((double) num35 - (double) num33)) / (float) (((double) num34 - (double) num32) * ((double) num34 - (double) num32) + ((double) num35 - (double) num33) * ((double) num35 - (double) num33));
      float num39 = num32 + num38 * (num34 - num32);
      float num40 = num33 + num38 * (num35 - num33);
      float num41 = (float) Math.Sqrt(((double) x3 - (double) num39) * ((double) x3 - (double) num39) + ((double) num36 - (double) num40) * ((double) num36 - (double) num40));
      float num42 = 0.0f;
      float num43 = x3 - num39;
      float num44 = num36 - num40;
      float num45 = num34 - num32;
      float num46 = num35 - num33;
      try
      {
        num42 = (float) -Math.Sign((float) ((double) num43 * (double) num46 - (double) num44 * (double) num45));
      }
      catch
      {
      }
      this.error = num42 * num41;
      this.errorvalues[this.errorcounter % MainForm.num_errorvalues] = this.error;
      ++this.errorcounter;
      float num47 = 0.0f;
      int num48 = 0;
      if (this.errorcounter >= MainForm.num_errorvalues)
      {
        for (int index = 0; index < MainForm.num_errorvalues; ++index)
        {
          num47 += this.errorvalues[index];
          ++num48;
        }
      }
      else
      {
        for (int index = 0; index < this.errorcounter; ++index)
        {
          num47 += this.errorvalues[index];
          ++num48;
        }
      }
      double num49 = (double) num47;
      int num50 = num48;
      int num51 = 1;
      int num52 = num50 + num51;
      double num53 = (double) num50;
      float num54 = (float) (num49 / num53);
      float x4 = x3 + (float) (((double) num39 - (double) x3) * 20.0);
      float num55 = num36 + (float) (((double) num40 - (double) num36) * 20.0);
      //Moveable Focus Indicator Circle
      pen.Color = Color.Cyan;
      pen.Width = (float) num31;
      int num56 = num31 * 4;
      graphics.DrawEllipse(pen, x4 - (float) num56, (float) height - num55 - (float) num56 + (float) this.yzero, (float) (num56 * 2), (float) (num56 * 2));
      pen.Width = (float) num31;
      graphics.DrawLine(pen, new PointF(x4, (float) height - num55 + (float) this.yzero), new PointF(x3, (float) height - num36 + (float) this.yzero));
      Font font = new Font("Arial", 8f);
      SolidBrush solidBrush = new SolidBrush(Color.White);
      string str1 = "Focus Error: " + (num42 * num41).ToString("F2") + " px";
      this.logMessage(str1);
      if (this.text_on_bitmap)
        graphics.DrawString(str1, font, (Brush) solidBrush, (PointF) new Point(10, 10));
      this.FocusErrorLabel.Text = str1;
      string str2 = (MainForm.num_errorvalues / (1000 / this.updateinterval)).ToString() + "s Average: " + num54.ToString("F2") + " px";
      this.logMessage(str2);
      if (this.text_on_bitmap)
        graphics.DrawString(str2, font, (Brush) solidBrush, (PointF) new Point(10, 20));
      this.AverageFocusErrorLabel.Text = str2;
      float num57 = 57.29578f;
      float num58 = (float) Math.PI / 180f;
      float num59 = Math.Abs((float) (((double) numArray3[2] - (double) numArray3[0]) / 2.0));
      string str3 = (num59 * num57).ToString("F0") + "\u00b0 Bahtinov";
      float num60 = (float) (9.0 / 32.0 * ((double) (float) this.DiameterNumericUpDown.Value / ((double) (float) this.FocalLengthNumericUpDown.Value * (double) (float) this.PixelSizeNumericUpDown.Value)) * (1.0 + Math.Cos(45.0 * (double) num58) * (1.0 + Math.Tan((double) num59))));
      string str3a = (180 - (num57 * numArray3[2])).ToString("F1")  + " ";
      this.str3aLabel.Text = str3a;
      string str3b = (180 - (num57 * numArray3[1])).ToString("F1") + " ";
      this.str3bLabel.Text = str3b;
      string str3c = (180 - (num57 * numArray3[0])).ToString("F1");
      this.str3cLabel.Text = str3c;
      this.MaskTypeLabel.Text = str3;
      this.MaskAnglesLabel.Text = "Angles: ";
      str3aLabel.Location = new Point(60, 282);
      str3bLabel.Location = new Point(105, 282);
      str3cLabel.Location = new Point(150, 282);
      float num61 = num42 * num41 / num60;
      string str4 = "Absolute Focus Error: " + num61.ToString("F2") + " μm";
                this.logMessage(str4);
                if (this.text_on_bitmap)
                    graphics.DrawString(str4, font, (Brush)solidBrush, (PointF)new Point(10, 30));
                this.AbsFocusErrorLabel.Text = str4;
            
      float num62 = (float) (8.99999974990351E-07 * ((double) (float) this.FocalLengthNumericUpDown.Value / (double) (float) this.DiameterNumericUpDown.Value) * ((double) (float) this.FocalLengthNumericUpDown.Value / (double) (float) this.DiameterNumericUpDown.Value));
      bool flag = Math.Abs((double) num61 * 1E-06) < (double) Math.Abs(num62);
            lblYES.Text = "\u2714";
            lblNO.Text = "X";
            if (flag == true)
            {
                lblNO.Visible = false;
                lblYES.Location = new Point(140, 227);
                lblYES.Visible = true;
                string str5y = "Within Critical Focus:";
                this.logMessage(str5y);
                if (this.text_on_bitmap)
                    graphics.DrawString(str5y, font, (Brush)solidBrush, (PointF)new Point(10, 40));
                this.WithinCriticalFocusLabel.Text = str5y;
            }
            if (flag == false)
            {
                lblYES.Visible = false;
                lblNO.Location = new Point(140, 227);
                lblNO.Visible = true;
                string str5n = "Within Critical Focus:";
                this.logMessage(str5n);
                if (this.text_on_bitmap)
                    graphics.DrawString(str5n, font, (Brush)solidBrush, (PointF)new Point(10, 40));
                this.WithinCriticalFocusLabel.Text = str5n;
            }
            if (flag)
      {
        pen.Color = Color.Yellow;
        pen.Width = (float) num31;
        int num13 = 32;
        while (num13 < 128)
        {
          int num18 = num13;
          graphics.DrawEllipse(pen, x3 - (float) num18, (float) height - num36 - (float) num18 + (float) this.yzero, (float) (num18 * 2), (float) (num18 * 2));
          num13 += 32;
        }
        if (this.SoundCheckBox.Checked)
          SystemSounds.Exclamation.Play();
      }
      if (!this.centered)
      {
        this.bahtinov_grabber.areaForm.CurrentTopLeft.X -= (int) ((double) (bmp.Width / 2) - ((double) x3 + (double) num39) / 2.0);
        this.bahtinov_grabber.areaForm.CurrentTopLeft.Y += (int) ((double) (bmp.Height / 2) - ((double) num36 + (double) num40) / 2.0);
        this.bahtinov_grabber.areaForm.CurrentBottomRight.X -= (int) ((double) (bmp.Width / 2) - ((double) x3 + (double) num39) / 2.0);
        this.bahtinov_grabber.areaForm.CurrentBottomRight.Y += (int) ((double) (bmp.Height / 2) - ((double) num36 + (double) num40) / 2.0);
        this.centered = true;
      }
      this.centered = false;
      if (!this.RotatingFocusserCheckBox.Checked)
        return;
      bahtinov_angles = new float[3];
    }

    private void center_button_Click(object sender, EventArgs e)
    {
      this.centered = false;
    }

    private void D_trackBar_Scroll(object sender, EventArgs e)
    {
    }

    private void FocalLengthNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      Application.UserAppDataRegistry.SetValue("FocalLength", (object) this.FocalLengthNumericUpDown.Value);
    }

    private void DiameterNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      Application.UserAppDataRegistry.SetValue("Diameter", (object) this.DiameterNumericUpDown.Value);
    }

    private void PixelSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      Application.UserAppDataRegistry.SetValue("PixelSize", (object) this.PixelSizeNumericUpDown.Value);
    }

    private void RotatingFocusserCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      Application.UserAppDataRegistry.SetValue("RotatingFocusser", (object) this.RotatingFocusserCheckBox.Checked);
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
    }

    private void SoundCheckBox_CheckedChanged(object sender, EventArgs e)
    {
    }

        private void AboutBTN_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://web.archive.org/web/20160220123031/http://www.njnoordhoek.com:80/?cat=10");
        }

        private void HelpBTN_Click(object sender, EventArgs e)
        {
            new Help().ShowDialog();
        }

        private void UpdateBTN_Click(object sender, EventArgs e)
        {
            string downloadURL = "";
            Version newVersion = null;
            string xmlURL = "http://1cm69.uk/downloads/Tri-Bahtinov_Grabber/tribupdate.xml";
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(xmlURL);
                reader.MoveToContent();
                string elementName = "";
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "trib"))
                {
                    while (reader.Read())
                    {
                        if(reader.NodeType == XmlNodeType.Element)
                        {
                            elementName = reader.Name;
                        }
                        else
                        {
                            if((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                            {
                                switch(elementName)
                                {
                                    case "version":
                                        newVersion = new Version(reader.Value);
                                        break;
                                    case "url":
                                        downloadURL = reader.Value;
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Cannot install the latest version of the application. " + "\n" + "Please check your network connection, or try again later.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            Version applicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if(applicationVersion.CompareTo(newVersion) <0)
            {
                DialogResult updateResult = MessageBox.Show("Version " + newVersion.Major + "." + newVersion.Minor + "." + newVersion.Build + " of Tri-Bahtinov Grabber is available" + "\n" + "Do you wish to download the update? Y/N", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (updateResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(downloadURL);
                }
                else if (updateResult == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("This application is currently up to date.", "No Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void RadioBTNmodeReg_CheckedChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("RegularMode", (object)this.RadioBTNmodeReg.Checked);
            this.BackColor = SystemColors.Control;
            FocalLengthNumericUpDown.BackColor = SystemColors.Control;
            DiameterNumericUpDown.BackColor = SystemColors.Control;
            PixelSizeNumericUpDown.BackColor = SystemColors.Control;
            StatusBar.BackColor = SystemColors.Control;
            lblNO.BackColor = SystemColors.Control;
            lblYES.BackColor = SystemColors.Control;
            str3aLabel.BackColor = SystemColors.Control;
            str3bLabel.BackColor = SystemColors.Control;
            str3cLabel.BackColor = SystemColors.Control;
        }

        private void RadioBTNmodeNight_CheckedChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("NightMode", (object)this.RadioBTNmodeNight.Checked);
            this.BackColor = System.Drawing.Color.Red;
            FocalLengthNumericUpDown.BackColor = System.Drawing.Color.Red;
            DiameterNumericUpDown.BackColor = System.Drawing.Color.Red;
            PixelSizeNumericUpDown.BackColor = System.Drawing.Color.Red;
            StatusBar.BackColor = System.Drawing.Color.Red;
            lblNO.BackColor = System.Drawing.Color.Black;
            lblYES.BackColor = System.Drawing.Color.Black;
            str3aLabel.BackColor = System.Drawing.Color.Black;
            str3bLabel.BackColor = System.Drawing.Color.Black;
            str3cLabel.BackColor = System.Drawing.Color.Black;
        }
    }
}
