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
    public void ExecuteShortcut(string profileName, string shortcutName)
    {
        _logger.LogInformation("Executing shortcut: {profileName}: {shortcutName}", profileName, shortcutName);
        var grpcAddress = _config.GetValue<string>("Grpc:Shortcuts");
        if (string.IsNullOrWhiteSpace(grpcAddress))
        {
            _logger.LogError("Grpc:Shortcuts address not provided. Shortcut execution terminated.");
            return;
        }
        var channel = GrpcChannel.ForAddress(grpcAddress);
        var client = new Server.Shortcut.ShortcutClient(channel);

        ShortcutRequest request = new ShortcutRequest
        {
            ProcessName = _config.GetValue<string>($"ShortcutProfiles:{profileName}:ProcessName"),
            KeyStroke = _config.GetValue<string>($"ShortcutProfiles:{profileName}:Shortcuts:{shortcutName}")
        };

        _logger.LogInformation("Shortcut request: {ProcessName}, {KeyStroke}", request.ProcessName, request.KeyStroke);
        try
        {
            client.ExecuteShortcut(request);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error while calling server (address: {grpcAddress})\n{ex.Message}", grpcAddress, ex.Message);
        }
    }
}
