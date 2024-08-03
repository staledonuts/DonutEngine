using System.Numerics;

namespace Engine.Entities;

public interface IEntity : IDisposable
{
    public Guid GetGuid();
    public void Initialize();
    public void Update();
    public void DrawUpdate();
    public void LateUpdate();
    public void Destroy();
    public new void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}