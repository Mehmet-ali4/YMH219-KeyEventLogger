namespace KeyEventLogger
{
    /// <summary>
    /// Tuş kayıtlarını tutan ve kayıt durumunu (açık/durduruldu) yöneten sınıf.
    /// Arayüzden bağımsızdır: Form, TextBox, MessageBox gibi hiçbir arayüz öğesi kullanmaz.
    /// Arayüz, değişikliklerden EntryAdded ve StatusChanged olayları (event) ile haberdar olur.
    /// </summary>
    public class KeyLogManager
    {
        private readonly List<KeyLogEntry> _entries = new List<KeyLogEntry>();

        /// <summary>Yeni bir kayıt eklendiğinde tetiklenir.</summary>
        public event EventHandler<KeyLogEntry>? EntryAdded;

        /// <summary>Kayıt durumu (IsRecording) değiştiğinde tetiklenir.</summary>
        public event EventHandler? StatusChanged;

        /// <summary>Kayıt açık mı?</summary>
        public bool IsRecording { get; private set; }

        /// <summary>Kayıtların dışarıdan değiştirilemeyen (salt okunur) görünümü.</summary>
        public IReadOnlyList<KeyLogEntry> Entries => _entries.AsReadOnly();

        /// <summary>Toplam tuş sayısı (yalnızca KeyDown olayları sayılır).</summary>
        public int TotalKeyCount => _entries.Count(e => e.EventType == KeyEventType.KeyDown);

        /// <summary>Son kaydetmeden sonra yeni kayıt eklendi mi?</summary>
        public bool HasUnsavedChanges { get; private set; }

        /// <summary>Kaydı başlatır.</summary>
        public void Start()
        {
            if (IsRecording)
            {
                return;
            }
            IsRecording = true;
            OnStatusChanged();
        }

        /// <summary>Kaydı durdurur. Durdurulmuşken AddEntry kayıt eklemez.</summary>
        public void Stop()
        {
            if (!IsRecording)
            {
                return;
            }
            IsRecording = false;
            OnStatusChanged();
        }

        /// <summary>Tüm kayıtları siler. Silinen kayıtlar artık "kaydedilmemiş" sayılmaz.</summary>
        public void Clear()
        {
            _entries.Clear();
            HasUnsavedChanges = false;
        }

        /// <summary>
        /// Kayıt açıksa yeni bir kayıt ekler ve EntryAdded olayını tetikler.
        /// </summary>
        /// <returns>Kayıt eklendiyse true, kayıt durdurulmuşsa false.</returns>
        public bool AddEntry(KeyEventType eventType, string keyName)
        {
            if (!IsRecording || string.IsNullOrWhiteSpace(keyName))
            {
                return false;
            }

            var entry = new KeyLogEntry(DateTime.Now, keyName, eventType);
            _entries.Add(entry);
            HasUnsavedChanges = true;

            EntryAdded?.Invoke(this, entry);
            return true;
        }

        /// <summary>Dosyaya başarıyla kaydedildikten sonra çağrılır.</summary>
        public void MarkAsSaved()
        {
            HasUnsavedChanges = false;
        }

        /// <summary>
        /// En sık basılan tuşları (KeyDown sayısına göre) çoktan aza sıralı döndürür.
        /// </summary>
        /// <param name="count">Kaç tuş döndürüleceği</param>
        public IReadOnlyList<KeyValuePair<string, int>> GetTopKeys(int count)
        {
            if (count <= 0)
            {
                return Array.Empty<KeyValuePair<string, int>>();
            }

            return _entries
                .Where(e => e.EventType == KeyEventType.KeyDown)
                .GroupBy(e => e.KeyName)
                .Select(g => new KeyValuePair<string, int>(g.Key, g.Count()))
                .OrderByDescending(pair => pair.Value)
                .ThenBy(pair => pair.Key, StringComparer.Ordinal)
                .Take(count)
                .ToList();
        }

        private void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
