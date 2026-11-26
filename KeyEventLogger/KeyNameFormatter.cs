using System.Text;

namespace KeyEventLogger
{
    /// <summary>
    /// Windows Forms "Keys" değerini okunabilir bir tuş adına çevirir.
    /// Örnek: (A, Control) → "Ctrl+A", Return → "Enter", Back → "Backspace".
    /// </summary>
    public static class KeyNameFormatter
    {
        // Modifier önekleri
        private const string CtrlPrefix = "Ctrl+";
        private const string ShiftPrefix = "Shift+";
        private const string AltPrefix = "Alt+";

        // Okunabilir isim eşlemesi. Listede olmayan tuşlar için Keys.ToString() kullanılır.
        // Not: Keys.Return == Keys.Enter, Keys.Prior == Keys.PageUp vb. aynı değerlerdir; bu yüzden tek kez yazılır.
        private static readonly Dictionary<Keys, string> FriendlyNames = new Dictionary<Keys, string>
        {
            { Keys.Enter, "Enter" },
            { Keys.Back, "Backspace" },
            { Keys.Escape, "Esc" },
            { Keys.Space, "Space" },
            { Keys.Delete, "Delete" },
            { Keys.Insert, "Insert" },
            { Keys.CapsLock, "CapsLock" },
            { Keys.PageUp, "PageUp" },
            { Keys.PageDown, "PageDown" },
            { Keys.NumLock, "NumLock" },
            { Keys.Scroll, "ScrollLock" },
            { Keys.ControlKey, "Ctrl" },
            { Keys.LControlKey, "Ctrl" },
            { Keys.RControlKey, "Ctrl" },
            { Keys.ShiftKey, "Shift" },
            { Keys.LShiftKey, "Shift" },
            { Keys.RShiftKey, "Shift" },
            { Keys.Menu, "Alt" },
            { Keys.LMenu, "Alt" },
            { Keys.RMenu, "Alt" },
            { Keys.LWin, "Win" },
            { Keys.RWin, "Win" },
            { Keys.Apps, "Menu" },
            { Keys.D0, "0" },
            { Keys.D1, "1" },
            { Keys.D2, "2" },
            { Keys.D3, "3" },
            { Keys.D4, "4" },
            { Keys.D5, "5" },
            { Keys.D6, "6" },
            { Keys.D7, "7" },
            { Keys.D8, "8" },
            { Keys.D9, "9" },
            { Keys.Add, "NumPad+" },
            { Keys.Subtract, "NumPad-" },
            { Keys.Multiply, "NumPad*" },
            { Keys.Divide, "NumPad/" },
            { Keys.Decimal, "NumPad." }
        };

        // Kendisi modifier olan tuşlar: bunlara önek eklenmez ("Ctrl+Ctrl" gibi tekrarları önler).
        private static readonly HashSet<Keys> ModifierKeyCodes = new HashSet<Keys>
        {
            Keys.ControlKey, Keys.LControlKey, Keys.RControlKey,
            Keys.ShiftKey, Keys.LShiftKey, Keys.RShiftKey,
            Keys.Menu, Keys.LMenu, Keys.RMenu
        };

        /// <summary>
        /// Tuş kodu ve basılı modifier tuşlardan okunabilir ad üretir.
        /// </summary>
        /// <param name="keyCode">Tuşun kendisi (ör. Keys.A)</param>
        /// <param name="modifiers">Basılı modifier'lar (ör. Keys.Control | Keys.Shift)</param>
        public static string Format(Keys keyCode, Keys modifiers)
        {
            string baseName = FriendlyNames.TryGetValue(keyCode, out string? friendlyName)
                ? friendlyName
                : keyCode.ToString();

            if (ModifierKeyCodes.Contains(keyCode))
            {
                return baseName;
            }

            var builder = new StringBuilder();
            if ((modifiers & Keys.Control) == Keys.Control)
            {
                builder.Append(CtrlPrefix);
            }
            if ((modifiers & Keys.Shift) == Keys.Shift)
            {
                builder.Append(ShiftPrefix);
            }
            if ((modifiers & Keys.Alt) == Keys.Alt)
            {
                builder.Append(AltPrefix);
            }
            builder.Append(baseName);

            return builder.ToString();
        }
    }
}
