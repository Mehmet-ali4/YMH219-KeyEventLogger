using System.Globalization;

namespace KeyEventLogger
{
    /// <summary>
    /// Tek bir klavye olayı kaydını temsil eder (zaman, olay türü, tuş adı).
    /// Nesne oluşturulduktan sonra değiştirilemez (immutable).
    /// </summary>
    public class KeyLogEntry
    {
        // Kayıt formatı sabitleri: ekranda ve dosyada aynı format kullanılır.
        public const string TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff";
        public const string FieldSeparator = " | ";

        public DateTime Timestamp { get; }
        public string KeyName { get; }
        public KeyEventType EventType { get; }

        public KeyLogEntry(DateTime timestamp, string keyName, KeyEventType eventType)
        {
            if (string.IsNullOrWhiteSpace(keyName))
            {
                throw new ArgumentException("Tuş adı boş olamaz.", nameof(keyName));
            }

            Timestamp = timestamp;
            KeyName = keyName;
            EventType = eventType;
        }

        /// <summary>
        /// Kayıt satırını üretir. Örnek: 2026-11-27 00:40:12.345 | KeyDown | Ctrl+A
        /// </summary>
        public string ToLogLine()
        {
            string time = Timestamp.ToString(TimestampFormat, CultureInfo.InvariantCulture);
            return time + FieldSeparator + EventType + FieldSeparator + KeyName;
        }

        /// <summary>
        /// ListBox nesneyi gösterirken ToString() çağırır; böylece ekranda kayıt satırı görünür.
        /// </summary>
        public override string ToString()
        {
            return ToLogLine();
        }
    }
}
