using Grpc.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ShortcutsServiceServer.Services;

public class ShortcutService : Shortcut.ShortcutBase
{
    private readonly ILogger<ShortcutService> _logger;

    public ShortcutService(ILogger<ShortcutService> logger)
    {
        _logger = logger;
    }

    public override async Task<ShortcutResponse> ExecuteShortcut(ShortcutRequest request, ServerCallContext context)
    {
        ShortcutResponse output = new();

        _logger.LogInformation("Processing request: {processName}, {keyStroke}", request.ProcessName, request.KeyStroke);

        Process[] processes = Process.GetProcessesByName(request.ProcessName);

        _logger.LogDebug("{ProcessCount} Processes found", processes.Length);

        foreach (Process process in processes)
        {
            _logger.LogDebug("Process.MainWindowHandle: {MainWindowHandle}", process.MainWindowHandle);
            try
            {
                await Task.Delay(100);
                SetForegroundWindow(process.MainWindowHandle);
                SendKeys.SendWait(request.KeyStroke);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: {Message}\n{StackTrace}", ex.Message, ex.StackTrace);
            }
            finally
            {
                _logger.LogDebug("Exception handled.");
            }
        }

        return await Task.FromResult(output);
    }

    [DllImport("user32.dll")]
    private static extern int SetForegroundWindow(IntPtr hWnd);

}
