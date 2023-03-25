using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2DX.Dynamics;
using Box2DX.Collision;
using IniParser.Model;
using IniParser;
using Raylib_cs;
namespace DonutEngine.Backbone;

public class Entity 
{
    public EntityPhysics? entityPhysics;
    public Entity(int id, IniData data)
    {
        Id = id;
        Components = new Dictionary<string, Component>();
        Tags = new HashSet<string>();
        entityPhysics = new EntityPhysics()
        {
            X = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("X").Value),
            Y = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("Y").Value),
            Width = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("Width").Value),
            Height = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("Height").Value),
            Type = (Body.BodyType)int.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("BodyType").Value),
            Density = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("Density").Value),
            Friction = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("Friction").Value),
            Restitution = float.Parse(data.Sections.GetSectionData("EntityPhysics").Keys.GetKeyData("Restitution").Value)            
        };
        entityPhysics.InitEntityPhysics(this);
    }
    public int Id { get; set; }
    public Dictionary<string, Component> Components { get; set; }
    public HashSet<string> Tags { get; set; }



    public void AddComponent<T>(T component) where T : Component 
    {
        Components.TryAdd(component.GetType().Name, component);
        component.OnAddedToEntity(this);
        Console.WriteLine(component.GetType().Name);
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
    }
}

public abstract class Component 
{
    public abstract void OnAddedToEntity(Entity entity);
    public abstract void OnRemovedFromEntity(Entity entity);
}





