using System.Numerics;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
using Newtonsoft.Json;
namespace DonutEngine.Backbone;

public class Entity : IDisposable
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
    public Body? body;
    public Fixture? fixture;
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
    
    public void InitEntityPhysics(Entity entity)
    {
        entity.bodyDef.Position.Set(entity.X, entity.Y);
        entity.polyDef.SetAsBox(entity.Width, entity.Height);
        entity.polyDef.Density = entity.Density;
        entity.polyDef.Friction = entity.Friction;
        entity.polyDef.Restitution = entity.Restitution;
        entity.bodyDef.IsSleeping = false;
        entity.bodyDef.AllowSleep = true;
        entity.bodyDef.LinearDamping = 0.2f;
        entity.bodyDef.AngularDamping= 0.1f;
        entity.bodyDef.FixedRotation = false;
        entity.body = Sys.physicsSystem.CreateBody(entity.bodyDef);
        entity.fixture = entity.body.CreateFixture(entity.polyDef);
        entity.fixture.ComputeMass(out entity.bodyDef.MassData);
        entity.body.SetMass(entity.bodyDef.MassData);
    }

    public void DestroyEntityPhysics(Entity entity) 
    {
        if(entity.body != null)
        {
            Sys.physicsSystem.DestroyBody(entity.body);
        }
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
