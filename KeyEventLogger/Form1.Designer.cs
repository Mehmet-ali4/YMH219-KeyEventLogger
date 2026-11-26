namespace KeyEventLogger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblInfo = new Label();
            pnlTop = new Panel();
            lblStatus = new Label();
            btnSave = new Button();
            btnClear = new Button();
            btnStop = new Button();
            btnStart = new Button();
            grpLog = new GroupBox();
            lstLog = new ListBox();
            grpStatistics = new GroupBox();
            lstTopKeys = new ListBox();
            lblTopKeysTitle = new Label();
            lblTotalKeys = new Label();
            statusStrip = new StatusStrip();
            tsslRecordingStatus = new ToolStripStatusLabel();
            tsslMessage = new ToolStripStatusLabel();
            pnlTop.SuspendLayout();
            grpLog.SuspendLayout();
            grpStatistics.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.BackColor = Color.LightYellow;
            lblInfo.BorderStyle = BorderStyle.FixedSingle;
            lblInfo.Dock = DockStyle.Top;
            lblInfo.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblInfo.Location = new Point(0, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(784, 34);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Bu uygulama yalnızca bu pencere aktifken basılan tuşları gösterir ve kaydeder.";
            lblInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblStatus);
            pnlTop.Controls.Add(btnSave);
            pnlTop.Controls.Add(btnClear);
            pnlTop.Controls.Add(btnStop);
            pnlTop.Controls.Add(btnStart);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 34);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(784, 52);
            pnlTop.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblStatus.BackColor = Color.Gray;
            lblStatus.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblStatus.ForeColor = Color.White;
            lblStatus.Location = new Point(542, 10);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(230, 32);
            lblStatus.TabIndex = 4;
            lblStatus.Text = "○ KAYIT DURDURULDU";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(315, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(95, 32);
            btnSave.TabIndex = 3;
            btnSave.TabStop = false;
            btnSave.Text = "Kaydet";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(214, 10);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(95, 32);
            btnClear.TabIndex = 2;
            btnClear.TabStop = false;
            btnClear.Text = "Temizle";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(113, 10);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(95, 32);
            btnStop.TabIndex = 1;
            btnStop.TabStop = false;
            btnStop.Text = "Durdur";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(12, 10);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(95, 32);
            btnStart.TabIndex = 0;
            btnStart.TabStop = false;
            btnStart.Text = "Başlat";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // grpLog
            // 
            grpLog.Controls.Add(lstLog);
            grpLog.Dock = DockStyle.Fill;
            grpLog.Location = new Point(0, 86);
            grpLog.Name = "grpLog";
            grpLog.Padding = new Padding(8);
            grpLog.Size = new Size(544, 413);
            grpLog.TabIndex = 2;
            grpLog.TabStop = false;
            grpLog.Text = "Tuş Kayıtları";
            // 
            // lstLog
            // 
            lstLog.Dock = DockStyle.Fill;
            lstLog.Font = new Font("Consolas", 10F);
            lstLog.FormattingEnabled = true;
            lstLog.HorizontalScrollbar = true;
            lstLog.IntegralHeight = false;
            lstLog.ItemHeight = 15;
            lstLog.Location = new Point(8, 24);
            lstLog.Name = "lstLog";
            lstLog.Size = new Size(528, 381);
            lstLog.TabIndex = 0;
            // 
            // grpStatistics
            // 
            grpStatistics.Controls.Add(lstTopKeys);
            grpStatistics.Controls.Add(lblTopKeysTitle);
            grpStatistics.Controls.Add(lblTotalKeys);
            grpStatistics.Dock = DockStyle.Right;
            grpStatistics.Location = new Point(544, 86);
            grpStatistics.Name = "grpStatistics";
            grpStatistics.Padding = new Padding(8);
            grpStatistics.Size = new Size(240, 413);
            grpStatistics.TabIndex = 3;
            grpStatistics.TabStop = false;
            grpStatistics.Text = "İstatistik";
            // 
            // lstTopKeys
            // 
            lstTopKeys.Dock = DockStyle.Fill;
            lstTopKeys.Font = new Font("Segoe UI", 10F);
            lstTopKeys.FormattingEnabled = true;
            lstTopKeys.IntegralHeight = false;
            lstTopKeys.ItemHeight = 17;
            lstTopKeys.Location = new Point(8, 80);
            lstTopKeys.Name = "lstTopKeys";
            lstTopKeys.SelectionMode = SelectionMode.None;
            lstTopKeys.Size = new Size(224, 325);
            lstTopKeys.TabIndex = 2;
            lstTopKeys.TabStop = false;
            // 
            // lblTopKeysTitle
            // 
            lblTopKeysTitle.Dock = DockStyle.Top;
            lblTopKeysTitle.Location = new Point(8, 54);
            lblTopKeysTitle.Name = "lblTopKeysTitle";
            lblTopKeysTitle.Size = new Size(224, 26);
            lblTopKeysTitle.TabIndex = 1;
            lblTopKeysTitle.Text = "En sık kullanılan 5 tuş";
            lblTopKeysTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalKeys
            // 
            lblTotalKeys.Dock = DockStyle.Top;
            lblTotalKeys.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalKeys.Location = new Point(8, 24);
            lblTotalKeys.Name = "lblTotalKeys";
            lblTotalKeys.Size = new Size(224, 30);
            lblTotalKeys.TabIndex = 0;
            lblTotalKeys.Text = "Toplam tuş: 0";
            lblTotalKeys.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { tsslRecordingStatus, tsslMessage });
            statusStrip.Location = new Point(0, 499);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(784, 22);
            statusStrip.TabIndex = 4;
            // 
            // tsslRecordingStatus
            // 
            tsslRecordingStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tsslRecordingStatus.ForeColor = Color.Gray;
            tsslRecordingStatus.Name = "tsslRecordingStatus";
            tsslRecordingStatus.Size = new Size(141, 17);
            tsslRecordingStatus.Text = "Durum: Kayıt durduruldu";
            // 
            // tsslMessage
            // 
            tsslMessage.Name = "tsslMessage";
            tsslMessage.Size = new Size(628, 17);
            tsslMessage.Spring = true;
            tsslMessage.Text = "Hazır";
            tsslMessage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 521);
            Controls.Add(grpLog);
            Controls.Add(grpStatistics);
            Controls.Add(pnlTop);
            Controls.Add(lblInfo);
            Controls.Add(statusStrip);
            KeyPreview = true;
            MinimumSize = new Size(700, 450);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Key Event Logger - Eğitsel Uygulama";
            Deactivate += Form1_Deactivate;
            FormClosing += Form1_FormClosing;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            pnlTop.ResumeLayout(false);
            grpLog.ResumeLayout(false);
            grpStatistics.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInfo;
        private Panel pnlTop;
        private Label lblStatus;
        private Button btnSave;
        private Button btnClear;
        private Button btnStop;
        private Button btnStart;
        private GroupBox grpLog;
        private ListBox lstLog;
        private GroupBox grpStatistics;
        private ListBox lstTopKeys;
        private Label lblTopKeysTitle;
        private Label lblTotalKeys;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsslRecordingStatus;
        private ToolStripStatusLabel tsslMessage;
    }
}
