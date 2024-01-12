namespace Shortcuts.Web.Services;

public class AsepriteService
{
    private readonly ShortcutsClient _shortcuts;
    private const string _profileName = "Aseprite";

    public AsepriteService(ShortcutsClient shortcuts)
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
    public void PencilTool()
    {
        _shortcuts.ExecuteShortcut(_profileName, "PencilTool");
    }
    public void Eraser()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Eraser");
    }
    public void Eyedropper()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Eyedropper");
    }
    public void BucketTool()
    {
        _shortcuts.ExecuteShortcut(_profileName, "BucketTool");
    }
    public void AdvancedMode()
    {
        _shortcuts.ExecuteShortcut(_profileName, "AdvancedMode");
    }
    public void Grid()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Grid");
    }
    public void FullscreenPreview()
    {
        _shortcuts.ExecuteShortcut(_profileName, "FullscreenPreview");
    }
    public void FitOnScreen()
    {
        _shortcuts.ExecuteShortcut(_profileName, "FitOnScreen");
    }
    public void Clear()
    {
        _shortcuts.ExecuteShortcut(_profileName, "Clear");
    }
}
