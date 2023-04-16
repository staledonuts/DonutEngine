using System.Numerics;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
using Newtonsoft.Json;
namespace DonutEngine.Backbone;

public class Entity : EntityPhysics, IDisposable
{
    public Entity(int id, dynamic data)
    {
        Id = id;
        Name = data.EntityType.Name;
        Components = new Dictionary<string, Component>();
        Tags = new HashSet<string>();
        X = float.Parse(data.EntityType.X.ToString());
        Y = float.Parse(data.EntityType.Y.ToString());
        Width = float.Parse(data.EntityType.Width.ToString());
        Height = float.Parse(data.EntityType.Height.ToString());
        Type = (Body.BodyType)int.Parse(data.EntityType.BodyType.ToString());
        Density = float.Parse(data.EntityType.Density.ToString());
        Friction = float.Parse(data.EntityType.Friction.ToString());
        Restitution = float.Parse(data.EntityType.Restitution.ToString());
        InitEntityPhysics(this);
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public Dictionary<string, Component> Components { get; set; }
    public HashSet<string> Tags { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Body.BodyType Type { get; set; }
    public float Density { get; set; }
    public float Friction { get; set; }
    public float Restitution { get; set; }
    public Body? body = null;
    public Fixture? fixture = null;
    public BodyDef bodyDef = new();
    public PolygonDef polyDef = new();

    private bool disposedValue;

    public void AddComponent<T>(T component, Entity entity) where T : Component 
    {
        Components.TryAdd(component.GetType().Name, component);
        component.OnAddedToEntity(entity);
    }

    public T GetComponent<T>() where T : Component 
    {
        return (T)Components[typeof(T).Name];
    }

    public void DestroyEntity()
    {
        foreach(KeyValuePair<string, Component> c in Components)
        {
            c.Value.OnRemovedFromEntity(this);
            Components.Remove(c.Key);
        }
        DestroyEntityPhysics(this);
        Dispose();
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
    // ~Entity()
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

