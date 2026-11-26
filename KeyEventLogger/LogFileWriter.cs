using System.Globalization;
using System.Text;

namespace KeyEventLogger
{
    /// <summary>
    /// Kayıtları .txt dosyasına yazan sınıf.
    /// Teknik dosya hatalarını kullanıcıya gösterilebilecek Türkçe bir IOException'a çevirir.
    /// </summary>
    public class LogFileWriter
    {
        // Başlık satırı sabitleri
        public const string ApplicationName = "Key Event Logger - Eğitsel Uygulama";
        private const string HeaderDateFormat = "yyyy-MM-dd HH:mm:ss";
        private const string HeaderSeparatorLine = "--------------------------------------------------";

        /// <summary>
        /// Kayıtları UTF-8 olarak verilen yola yazar. Dosya varsa üzerine yazılır.
        /// </summary>
        /// <exception cref="IOException">Dosya yazılamazsa anlaşılır mesajla fırlatılır.</exception>
        public void SaveToTxt(string path, IEnumerable<KeyLogEntry> entries)
        {
            ArgumentNullException.ThrowIfNull(entries);

            // Kayıt sayısını başlıkta yazabilmek için listeye alıyoruz.
            List<KeyLogEntry> entryList = entries.ToList();

            try
            {
                // using: hata olsa bile dosya kapatılır. true → UTF-8 BOM (Not Defteri Türkçe karakterleri doğru gösterir).
                using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
                {
                    writer.WriteLine("Uygulama     : " + ApplicationName);
                    writer.WriteLine("Kayıt tarihi : " + DateTime.Now.ToString(HeaderDateFormat, CultureInfo.InvariantCulture));
                    writer.WriteLine("Kayıt sayısı : " + entryList.Count);
                    writer.WriteLine(HeaderSeparatorLine);

                    foreach (KeyLogEntry entry in entryList)
                    {
                        writer.WriteLine(entry.ToLogLine());
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new IOException("Seçilen konuma yazma izniniz yok veya dosya salt okunur.\n" +
                                      "Lütfen farklı bir klasör seçin (ör. Belgeler).", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new IOException("Seçilen klasör bulunamadı. Lütfen geçerli bir klasör seçin.", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new IOException("Dosya yolu çok uzun. Daha kısa bir yol veya dosya adı seçin.", ex);
            }
            catch (IOException ex)
            {
                // Disk dolu, dosya başka programda açık vb.
                throw new IOException("Dosya yazılırken bir hata oluştu. Disk dolu olabilir veya dosya başka bir " +
                                      "program tarafından kullanılıyor olabilir.\nAyrıntı: " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new IOException("Geçersiz dosya yolu.", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new IOException("Dosya yolu biçimi desteklenmiyor.", ex);
            }
        }
    }
}
