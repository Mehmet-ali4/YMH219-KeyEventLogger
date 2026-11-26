namespace KeyEventLogger
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Yüksek DPI, görsel stiller ve varsayılan font ayarlarını uygular.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
