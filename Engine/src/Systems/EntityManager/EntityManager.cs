using Engine.Systems;
using Raylib_cs;

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
            Raylib.TraceLog(Raylib_cs.TraceLogLevel.LOG_ERROR, "Could not find that Entity: "+e);
            return null;
        }
    }

    public static void CreateEntity(this EntitiesData entitiesData, Entity entity)
    {
        entitiesData.entities.Add(entitiesData.EntityID, entity);
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
    }

    public static void LoadJSON(this EntitiesData entitiesData, string filePath)
    {

    }
    
}


public struct EntitiesData
{
    public EntitiesData()
    {
        entities = new();
        EntityID = 0;
    }

    

    public int EntityID;
    public Dictionary<int, Entity> entities;
    
}