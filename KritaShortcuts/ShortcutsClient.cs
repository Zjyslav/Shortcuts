using Grpc.Net.Client;
using ShortcutsServiceServer;

namespace KritaShortcuts;

public class ShortcutsClient
{
    private readonly IConfiguration _config;
    private readonly ILogger<ShortcutsClient> _logger;
    private string profile;

    public ShortcutsClient(IConfiguration config, ILogger<ShortcutsClient> logger)
    {
        _config = config;
        _logger = logger;
        profile = _config.GetValue<string>("ActiveShortcutsProfile");
        _logger.LogInformation("Profile: {profile}", profile);
    }

    public void Undo()
    {
        ExecuteShortcut("Undo");
    }

    public void Redo()
    {
        ExecuteShortcut("Redo");
    }

    public void FullScreen()
    {
        ExecuteShortcut("FullScreen");
    }

    public void ClearLayer()
    {
        ExecuteShortcut("ClearLayer");
    }

    public void Erase()
    {
        ExecuteShortcut("Erase");
    }

    private void ExecuteShortcut(string shortcutName)
    {
        _logger.LogInformation("Executing shortcut: {shortcutName}", shortcutName);
        var channel = GrpcChannel.ForAddress(_config.GetValue<string>("Grpc:Shortcuts"));
        var client = new ShortcutsServiceServer.Shortcut.ShortcutClient(channel);

        ShortcutRequest request = new ShortcutRequest
        {
            ProcessName = _config.GetValue<string>($"ShortcutProfiles:{profile}:ProcessName"),
            KeyStroke = _config.GetValue<string>($"ShortcutProfiles:{profile}:Shortcuts:{shortcutName}")
        };

        _logger.LogInformation($"Shortcut request: {request.ProcessName}, {request.KeyStroke}");
        client.ExecuteShortcut(request);
    }
}
