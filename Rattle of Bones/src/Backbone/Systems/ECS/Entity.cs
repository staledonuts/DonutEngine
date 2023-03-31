using System.Numerics;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Box2DX.Dynamics;
using Newtonsoft.Json;
namespace DonutEngine.Backbone;

public class DynamicEntity : Entity
{
    public DynamicEntity(int id, object data) : base(id, data)
    {
        
    }
}

public class StaticEntity : Entity
{
    public StaticEntity(int id, object data) : base(id, data)
    {
        
    }
}

public class GameObjectEntity : Entity
{
    public GameObjectEntity(int id, object data) : base(id, data)
    {

    }
}

public class Entity
{
    public Entity(int id, dynamic data)
    {
        Id = id;
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
    public Body? currentBody = null;


    public void AddComponent<T>(T component, Entity entity) where T : Component 
    {
        Components.TryAdd(component.GetType().Name, component);
        if(entity is DynamicEntity dynEnt)
        {
            component.OnAddedToEntity(dynEnt);
        }
        else if (entity is StaticEntity staEnt)
        {
            component.OnAddedToEntity(staEnt);
        }
        Console.WriteLine(component.GetType().Name);
    }

    public T GetComponent<T>() where T : Component 
    {
        return (T)Components[typeof(T).Name];
    }

    public void DestroyEntity()
    {
        if(this is DynamicEntity dynEnt)
        {
            foreach(KeyValuePair<string, Component> c in Components)
            {
                c.Value.OnRemovedFromEntity(dynEnt);
                Components.Remove(c.Key);
            }
        }
        else if(this is StaticEntity staEnt)
        {
            foreach(KeyValuePair<string, Component> c in Components)
            {
                c.Value.OnRemovedFromEntity(staEnt);
                Components.Remove(c.Key);
            }
        }
        DestroyEntityPhysics();
    }

    public void InitEntityPhysics(Entity entity)
    {
        if (entity is DynamicEntity dynEnt)
        {
            currentBody = physicsSystem.CreateBody(this);
        }
        else if (entity is StaticEntity staEnt)
        {
            currentBody = physicsSystem.CreateStaticBody(this);
        }
        
    }

    public void DestroyEntityPhysics() 
    {
        if(currentBody != null)
        {
            physicsSystem.DestroyBody(currentBody);

        }
    }
}

public abstract class Component
{
    public abstract void OnAddedToEntity(StaticEntity entity);
    public abstract void OnRemovedFromEntity(StaticEntity entity);
    public abstract void OnAddedToEntity(DynamicEntity entity);
    public abstract void OnRemovedFromEntity(DynamicEntity entity);
}





