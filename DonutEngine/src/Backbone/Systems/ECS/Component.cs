using System.Numerics;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
using Newtonsoft.Json;
namespace DonutEngine.Backbone;

public abstract class Component : IDisposable
{
    private bool disposedValue;
    public abstract void OnAddedToEntity(Entity entity);
    public abstract void OnRemovedFromEntity(Entity entity);

    public void PlaySFX(string name)
    {
        audioControl.PlaySFX(name);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Component()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}





