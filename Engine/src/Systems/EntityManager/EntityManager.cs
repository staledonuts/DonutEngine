using Raylib_CSharp.Logging;
using Engine.Entities;

namespace Engine.EntityManager;
public static class EntitySystem
{
    public static IEntity GetEntity(this IEntitiesData entitiesData, Guid entityID)
    {
        try
        {
            entitiesData.Entities.TryGetValue(entityID, out IEntity entity);
            return entity;
        }
        catch(Exception e)
        {
            Logger.TraceLog(TraceLogLevel.Error, "Could not find that Entity: "+e);
            return null;
        }
    }
 
    public static List<IEntity> GetEntities<T>(this IEntitiesData entitiesData)
    {
        try
        {
            List<IEntity> typeList = new();
            foreach(KeyValuePair<Guid, IEntity> keyValuePair in entitiesData.Entities)
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

    public static void Update(this IEntitiesData entitiesData)
    {
        foreach(KeyValuePair<Guid, IEntity> e in entitiesData.Entities)
        {
            e.Value.Update();
        }
    }

    public static void DrawUpdate(this IEntitiesData entitiesData)
    {
        foreach(KeyValuePair<Guid, IEntity> e in entitiesData.Entities)
        {
            e.Value.DrawUpdate();
        }
    }

    public static void LateUpdate(this IEntitiesData entitiesData)
    {
        foreach(KeyValuePair<Guid, IEntity> e in entitiesData.Entities)
        {
            e.Value.LateUpdate();
        }
        entitiesData.UpdatePhysics();
    }

    public static void LoadJSON(this IEntitiesData entitiesData, string filePath)
    {

    }

    public static void WipeEntities(this IEntitiesData entitiesData)
    {
        foreach(KeyValuePair<Guid, IEntity> e in entitiesData.Entities)
        {
            e.Value.Destroy();
            e.Value.Dispose();
        }
        entitiesData.Entities.Clear();
    }  
}