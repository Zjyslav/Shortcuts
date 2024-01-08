using Grpc.Net.Client;
using Shortcuts.Server;

namespace Shortcuts.Web;

public class ShortcutsClient
{
    private readonly IConfiguration _config;
    private readonly ILogger<ShortcutsClient> _logger;

    public ShortcutsClient(IConfiguration config, ILogger<ShortcutsClient> logger)
    {
        _config = config;
        _logger = logger;
    }

    {
        _logger.LogInformation("Executing shortcut: {profileName}: {shortcutName}", profileName, shortcutName);
    }

    {
            _logger.LogError("Grpc:Shortcuts address not provided. Shortcut execution terminated.");
    }

        ShortcutRequest request = new ShortcutRequest
    {
            ProcessName = _config.GetValue<string>($"ShortcutProfiles:{profileName}:ProcessName"),
    }

        _logger.LogInformation("Shortcut request: {ProcessName}, {KeyStroke}", request.ProcessName, request.KeyStroke);
    {
            client.ExecuteShortcut(request);
    }

    {
            _logger.LogError("Error while calling server (address: {grpcAddress})\n{ex.Message}", grpcAddress, ex.Message);
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
