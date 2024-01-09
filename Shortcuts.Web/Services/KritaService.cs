namespace Shortcuts.Web.Services;

public class KritaService
{
    private readonly ShortcutsClient _shortcuts;
    private const string _profileName = "Krita";

    public KritaService(ShortcutsClient shortcuts)
    {
        _shortcuts = shortcuts;
    }

    public void Undo()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Undo");
    }

    public void Redo()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Redo");
    }

    public void FullScreen()
    {
        _shortcuts.ExecuteShortcut(_profileName, "FullScreen");
    }

    public void ClearLayer()
    {
        _shortcuts.ExecuteShortcut(_profileName, "ClearLayer");
    }

    public void Erase()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Erase");
    }
}
