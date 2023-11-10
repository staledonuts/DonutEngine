namespace Engine.Systems.UI;
public abstract class DocumentWindow
{
    public bool Open = false;

    public abstract void Setup();
    public abstract void Shutdown();
    public abstract void Show();

    public bool Focused = false;
}