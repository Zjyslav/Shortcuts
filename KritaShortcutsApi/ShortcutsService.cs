using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KritaShortcuts;

public class ShortcutsService
{
    [DllImport("user32.dll")]
    private static extern int SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
    public void SendShortcut(string processName, string shortcut)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        foreach (Process process in processes)
        {
            SetForegroundWindow(process.MainWindowHandle);
            SendKeys.SendWait(shortcut);
        }
    }

    public void SendPlus(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        foreach (Process process in processes)
        {
            SetForegroundWindow(process.MainWindowHandle);
            PostMessage(process.MainWindowHandle, 0x0100, 0x6B, 0);
        }
    }
}
