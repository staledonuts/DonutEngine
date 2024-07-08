using System.Diagnostics.CodeAnalysis;
using Engine.FlatPhysics;
using Engine.Systems;
using Engine.Systems.Particles;
using Raylib_CSharp;
using Raylib_CSharp.Logging;

namespace Engine.EntityManager;
public static class EntitySystem
{
    public static Entity GetEntity(this EntitiesData entitiesData, int intID)
    {
        try
        {
            entitiesData.entities.TryGetValue(intID, out Entity entity);
            return entity;
        }
        catch(Exception e)
        {
            Logger.TraceLog(TraceLogLevel.Error, "Could not find that Entity: "+e);
            return null;
        }
    }
 
    public static List<Entity> GetEntities<T>(this EntitiesData entitiesData)
    {
        try
        {
            List<Entity> typeList = new();
            foreach(KeyValuePair<int, Entity> keyValuePair in entitiesData.entities)
            {
                if(keyValuePair.Value is T t)
                {
                    typeList.Add(keyValuePair.Value);
                }
            }
            return typeList;
        }
        catch(Exception e)
        {
            Logger.TraceLog(TraceLogLevel.Error, "Could not find that Entity: "+e);
            return null;
        }
    }

    public static void CreateEntity(this EntitiesData entitiesData, Entity entity)
    {
        entitiesData.entities.Add(entitiesData.EntityID, entity);
        entitiesData.world.AddBody(entity.body);
        entitiesData.EntityID +=1;
        entity.Initialize();
    }

    public static void Update(this EntitiesData entitiesData)
    {
        foreach(KeyValuePair<int, Entity> e in entitiesData.entities)
        {
            e.Value.Update();
        }
    }

    public static void DrawUpdate(this EntitiesData entitiesData)
    {
        foreach(KeyValuePair<int, Entity> e in entitiesData.entities)
        {
            e.Value.DrawUpdate();
        }
    }

    public static void LateUpdate(this EntitiesData entitiesData)
    {
        foreach(KeyValuePair<int, Entity> e in entitiesData.entities)
        {
            e.Value.LateUpdate();
        }
        entitiesData.UpdatePhysics();

    }

    public static void LoadJSON(this EntitiesData entitiesData, string filePath)
    {

    }

    public static void WipeEntities(this EntitiesData entitiesData)
    {
        foreach(KeyValuePair<int, Entity> e in entitiesData.entities)
        {
            e.Value.Destroy();
            e.Value.Dispose();
        }
        entitiesData.entities.Clear();
    }
    
}


public class EntitiesData
{
    public EntitiesData()
    {
        entities = new();
        world = new();
        EntityID = 0;
    }

    public int EntityID;

    const int iterations = 6;
    public readonly FlatWorld world;
    public Dictionary<int, Entity> entities;

    public void UpdatePhysics()
    {
        world.Step(Time.GetFrameTime(), iterations);
    }


    
}