using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;
using Raylib_cs;
namespace DonutEngine.Backbone;

public class Entity 
{
    public int Id { get; set; }
    public Dictionary<string, Component> Components { get; set; }

    public Entity(int id) 
    {
        Id = id;
        Components = new Dictionary<string, Component>();
    }

    public void AddComponent<T>(T component) where T : Component 
    {
        Components[typeof(T).Name] = component;
    }

    public T GetComponent<T>() where T : Component 
    {
        return (T)Components[typeof(T).Name];
    }

    public void DestroyEntity()
    {

    }
}

public abstract class Component 
{ 
    public abstract void OnAddedToEntity(Entity entity);
    public abstract void OnRemovedFromEntity(Entity entity);
}





