using Box2DX.Dynamics;
using IniParser.Model;
namespace DonutEngine.Backbone;

public class DynamicEntity : Entity
{
    public DynamicEntity(int id, IniData data) : base(id, data)
    {
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
}

public class StaticEntity : Entity
{
    public StaticEntity(int id, IniData data) : base(id, data)
    {
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

    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}

public class GameObjectEntity : Entity
{
    public GameObjectEntity(int id, IniData data) : base(id, data)
    {
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

    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}

public class Entity 
{
    
    public Entity(int id, IniData data)
    {
        Id = id;
        Components = new Dictionary<string, Component>();
        Tags = new HashSet<string>();
    }
    public int Id { get; set; }
    public Dictionary<string, Component> Components { get; set; }
    public HashSet<string> Tags { get; set; }
    public EntityPhysics? entityPhysics;



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
        entityPhysics.DestroyEntityPhysics();
    }
}

public abstract class Component
{
    public abstract void OnAddedToEntity(StaticEntity entity);
    public abstract void OnRemovedFromEntity(StaticEntity entity);
    public abstract void OnAddedToEntity(DynamicEntity entity);
    public abstract void OnRemovedFromEntity(DynamicEntity entity);
}





