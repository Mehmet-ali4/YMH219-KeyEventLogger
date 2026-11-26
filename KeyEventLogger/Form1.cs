namespace KeyEventLogger
{
    /// <summary>
    /// Ana form. Yalnızca arayüzü günceller ve olayları ilgili sınıflara bağlar.
    /// Kayıt mantığı KeyLogManager'da, dosya yazma LogFileWriter'da, tuş adı üretme KeyNameFormatter'dadır.
    /// </summary>
    public partial class Form1 : Form
    {
        // ---- Sabitler ----
        private const int TopKeyCount = 5;
        private const string BaseTitle = "Key Event Logger - Eğitsel Uygulama";

        private const string RecordingIndicatorText = "● KAYIT AÇIK";
        private const string StoppedIndicatorText = "○ KAYIT DURDURULDU";
        private const string RecordingTitleText = "Kayıt Açık";
        private const string StoppedTitleText = "Kayıt Durduruldu";
        private const string RecordingStatusText = "Durum: Kayıt açık";
        private const string StoppedStatusText = "Durum: Kayıt durduruldu";

        private const string DefaultFileNamePrefix = "keylog_";
        private const string FileNameDateFormat = "yyyyMMdd_HHmmss";
        private const string TxtExtension = "txt";
        private const string TxtFileFilter = "Metin Dosyaları (*.txt)|*.txt";

        private static readonly Color RecordingColor = Color.Red;
        private static readonly Color StoppedColor = Color.Gray;

        // Odaktaki kontrol tarafından "yutulabilen" tuşlar (KeyDown olayı gelmeyebilir).
        // Bunların KeyDown kaydı ProcessCmdKey içinde yapılır.
        private static readonly HashSet<Keys> SwallowableKeys = new HashSet<Keys>
        {
            Keys.Tab, Keys.Enter, Keys.Escape, Keys.Up, Keys.Down, Keys.Left, Keys.Right
        };

        // ---- Alanlar ----
        private readonly KeyLogManager _logManager = new KeyLogManager();
        private readonly LogFileWriter _fileWriter = new LogFileWriter();

        // Şu an basılı olan tuşlar: basılı tutmada tekrar eden KeyDown'ları ve çift kaydı engeller.
        private readonly HashSet<Keys> _pressedKeys = new HashSet<Keys>();

        public Form1()
        {
            InitializeComponent();

            // KeyLogManager olaylarını arayüz güncelleme metotlarına bağla.
            _logManager.EntryAdded += LogManager_EntryAdded;
            _logManager.StatusChanged += LogManager_StatusChanged;

            lblTopKeysTitle.Text = $"En sık kullanılan {TopKeyCount} tuş";
            UpdateStatusIndicator();
            UpdateStatistics();
        }

        // ================= Klavye olayları =================

        /// <summary>
        /// Tab, Enter, ok tuşları gibi tuşlar odaktaki kontrol tarafından işlenip KeyDown'a ulaşmayabilir.
        /// ProcessCmdKey bu tuşlar için KeyDown'dan ÖNCE çalışır; kaydı burada yaparız.
        /// Aynı tuş için KeyDown olayı da gelirse _pressedKeys kontrolü çift kaydı engeller.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = keyData & Keys.KeyCode;
            if (SwallowableKeys.Contains(keyCode))
            {
                HandleKeyDown(keyCode, keyData & Keys.Modifiers);
            }

            // Normal davranış (ör. Tab ile odak değiştirme) bozulmasın diye temel metodu çağırıyoruz.
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // KeyPreview = true olduğu için form, tuşları odaktaki kontrolden önce alır.
        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            HandleKeyDown(e.KeyCode, e.Modifiers);
        }

        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            HandleKeyUp(e.KeyCode, e.Modifiers);

            // Alt veya F10 bırakılınca Windows pencere menüsüne geçer ve sonraki tuş kaybolabilir; bunu engelliyoruz.
            if (e.KeyCode == Keys.Menu || e.KeyCode == Keys.F10)
            {
                e.Handled = true;
            }
        }

        private void HandleKeyDown(Keys keyCode, Keys modifiers)
        {
            // Add false dönerse tuş zaten basılıdır (otomatik tekrar ya da ProcessCmdKey'de işlendi) → yok say.
            if (!_pressedKeys.Add(keyCode))
            {
                return;
            }

            // Kayıt durdurulmuşsa AddEntry hiçbir şey eklemez.
            _logManager.AddEntry(KeyEventType.KeyDown, KeyNameFormatter.Format(keyCode, modifiers));
        }

        private void HandleKeyUp(Keys keyCode, Keys modifiers)
        {
            // Yalnızca bu pencerede basılmış tuşun KeyUp'ını kaydet
            // (ör. bir dialogda basılıp formda bırakılan Enter kaydedilmez).
            if (!_pressedKeys.Remove(keyCode))
            {
                return;
            }

            _logManager.AddEntry(KeyEventType.KeyUp, KeyNameFormatter.Format(keyCode, modifiers));
        }

        /// <summary>
        /// Form pasif olunca (başka pencereye geçilince) KeyUp olayları bize gelmez;
        /// basılı tuş listesini sıfırlarız ki geri dönünce tuşlar yeniden kaydedilebilsin.
        /// </summary>
        private void Form1_Deactivate(object? sender, EventArgs e)
        {
            _pressedKeys.Clear();
        }

        // ================= Buton olayları =================

        private void btnStart_Click(object? sender, EventArgs e)
        {
            _logManager.Start();
            SetStatusMessage("Kayıt başlatıldı.");
            FocusLogList();
        }

        private void btnStop_Click(object? sender, EventArgs e)
        {
            _logManager.Stop();
            SetStatusMessage("Kayıt durduruldu.");
            FocusLogList();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            if (_logManager.Entries.Count == 0)
            {
                SetStatusMessage("Temizlenecek kayıt yok.");
                FocusLogList();
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Ekrandaki tüm kayıtlar silinecek. Emin misiniz?",
                "Temizle",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer == DialogResult.Yes)
            {
                _logManager.Clear();
                lstLog.Items.Clear();
                UpdateStatistics();
                SetStatusMessage("Kayıtlar temizlendi.");
            }

            FocusLogList();
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            if (_logManager.Entries.Count == 0)
            {
                MessageBox.Show("Kaydedilecek kayıt bulunmuyor.", "Kaydet",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FocusLogList();
                return;
            }

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "Kayıtları Kaydet";
                dialog.Filter = TxtFileFilter;
                dialog.DefaultExt = TxtExtension;
                dialog.AddExtension = true;
                dialog.OverwritePrompt = true;
                dialog.FileName = DefaultFileNamePrefix + DateTime.Now.ToString(FileNameDateFormat) + "." + TxtExtension;

                // Dosya yalnızca kullanıcı bir yol seçip onaylarsa yazılır.
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        _fileWriter.SaveToTxt(dialog.FileName, _logManager.Entries);
                        _logManager.MarkAsSaved();
                        SetStatusMessage($"{_logManager.Entries.Count} kayıt kaydedildi: {dialog.FileName}");
                    }
                    catch (IOException ex)
                    {
                        // Uygulama kapanmaz; kullanıcıya anlaşılır mesaj gösterilir.
                        MessageBox.Show(ex.Message, "Kaydetme Hatası",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SetStatusMessage("Kaydetme başarısız.");
                    }
                }
            }

            FocusLogList();
        }

        // ================= Form olayları =================

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (!_logManager.HasUnsavedChanges)
            {
                return;
            }

            DialogResult answer = MessageBox.Show(
                "Kaydedilmemiş kayıtlar var, çıkılsın mı?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (answer == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        // ================= KeyLogManager olayları =================

        private void LogManager_EntryAdded(object? sender, KeyLogEntry entry)
        {
            lstLog.Items.Add(entry);
            // Son satıra otomatik kaydır.
            lstLog.TopIndex = lstLog.Items.Count - 1;
            UpdateStatistics();
        }

        private void LogManager_StatusChanged(object? sender, EventArgs e)
        {
            UpdateStatusIndicator();
        }

        // ================= Arayüz yardımcı metotları =================

        private void UpdateStatusIndicator()
        {
            bool isRecording = _logManager.IsRecording;
            Color statusColor = isRecording ? RecordingColor : StoppedColor;

            lblStatus.Text = isRecording ? RecordingIndicatorText : StoppedIndicatorText;
            lblStatus.BackColor = statusColor;

            tsslRecordingStatus.Text = isRecording ? RecordingStatusText : StoppedStatusText;
            tsslRecordingStatus.ForeColor = statusColor;

            Text = $"{BaseTitle} [{(isRecording ? RecordingTitleText : StoppedTitleText)}]";

            btnStart.Enabled = !isRecording;
            btnStop.Enabled = isRecording;
        }

        private void UpdateStatistics()
        {
            lblTotalKeys.Text = $"Toplam tuş: {_logManager.TotalKeyCount}";

            lstTopKeys.BeginUpdate();
            lstTopKeys.Items.Clear();

            IReadOnlyList<KeyValuePair<string, int>> topKeys = _logManager.GetTopKeys(TopKeyCount);
            if (topKeys.Count == 0)
            {
                lstTopKeys.Items.Add("(henüz kayıt yok)");
            }
            else
            {
                for (int i = 0; i < topKeys.Count; i++)
                {
                    lstTopKeys.Items.Add($"{i + 1}. {topKeys[i].Key} - {topKeys[i].Value} kez");
                }
            }

            lstTopKeys.EndUpdate();
        }

        private void SetStatusMessage(string message)
        {
            tsslMessage.Text = message;
        }

        /// <summary>
        /// Odağı butondan alır; böylece Space/Enter tuşları butonu tekrar tetiklemez.
        /// </summary>
        private void FocusLogList()
        {
            lstLog.Focus();
        }
    }
}
