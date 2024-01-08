using Grpc.Net.Client;
using Shortcuts.Server;

namespace Shortcuts.Web;

public class ShortcutsClient
{
    private readonly IConfiguration _config;
    private readonly ILogger<ShortcutsClient> _logger;
    private string _profile;

    public ShortcutsClient(IConfiguration config, ILogger<ShortcutsClient> logger)
    {
        _config = config;
        _logger = logger;
        _profile = _config.GetValue<string>("ActiveShortcutsProfile");
        _logger.LogInformation("Profile: {profile}", _profile);
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
        var client = new Shortcuts.Server.Shortcut.ShortcutClient(channel);

        ShortcutRequest request = new ShortcutRequest
        {
            ProcessName = _config.GetValue<string>($"ShortcutProfiles:{_profile}:ProcessName"),
            KeyStroke = _config.GetValue<string>($"ShortcutProfiles:{_profile}:Shortcuts:{shortcutName}")
        };

        _logger.LogInformation($"Shortcut request: {request.ProcessName}, {request.KeyStroke}");
        client.ExecuteShortcut(request);
    }
}
