namespace Engine.Systems.ECS;
using System;
using System.Collections;
using System.Collections.Generic;
using Engine.Logging;
using Raylib_cs;

public abstract class Entity
{
    public int intID { get; set; }
    private Guid Id { get; set; }
    public HashSet<string> Tags { get; set; }
    private List<Component> _components;
    public Transform2D transform = new();

    public Entity()
    {
        Id = Guid.NewGuid();
        Tags = new HashSet<string>();
        _components = new List<Component>();
    }

    public T GetComponent<T>() where T : Component
    {
        return _components.OfType<T>().FirstOrDefault();
    }

    public Component AddComponent(Component component)
    {
        _components.Add(component);
        component.OnComponentAdded();
        return component;
    }

    public Guid GetEntityGuid()
    {
        return Id;
    }

    public void RemoveComponent<T>() where T : Component
    {
        var component = GetComponent<T>();
        if (component != null)
        {
            component.OnComponentRemoved();
            _components.Remove(component);
        }
    }

    public void InitAllComponents()
    {
        try
        {
            foreach(Component comp in _components)
            {
                comp.OnComponentAdded();
            }
        }
        catch(Exception e)
        {
            DonutLogging.Log(TraceLogLevel.LOG_ERROR, "Something went wrong with the "+Id.ToString()+" => "+e.Message);
        }
    }
}


public abstract class Component
{
    public Entity Entity { get; internal set; }
    public abstract void OnComponentAdded();
    public abstract void OnComponentRemoved();
    
}