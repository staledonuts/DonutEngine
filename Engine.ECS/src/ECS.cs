namespace Engine.Systems.ECS;
using Newtonsoft.Json;
using System.Numerics;
using Raylib_cs;
using Engine.Logging;
using Engine.Systems.SceneSystem;

public static class EntityManager
{
    public static List<Entity> GetEntitiesByTag(string tag, ECSData eCSData)
    {
        try
        {
            return eCSData.entities.Where(e => e.Tags.Contains(tag)).ToList();
        }
        catch
        {
            DonutLogging.Log(TraceLogLevel.LOG_ERROR, "Could not find a Entity with those Tags");
            return null;
        }
    }

    public static Entity GetEntityByGuid(Guid guid , ECSData eCSData)
    {
        try
        {
            return eCSData.entities.FirstOrDefault(entity => entity.GetEntityGuid() == guid);
        }
        catch
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, "Could not find a Entity with that Guid");
            return null;
        }
    }
    public static ECSData LoadEntities(string JSON)
    {
        try
        {
            string JsonData = File.ReadAllText(JSON);
            ECSData eCSData = JsonConvert.DeserializeObject<ECSData>(JsonData);
            foreach(Entity entity in eCSData.entities)
            {
                entity.InitAllComponents();
            }
            return eCSData;
        }
        catch (Exception e)
        {
            DonutLogging.Log(TraceLogLevel.LOG_FATAL, e.Message);
            return new();
        }
    }

    public static void CreateEntity(Entity entity, ECSData eCSData)
    {
        eCSData.Add(entity);
    }
    

    public class ECSData
    {
        int nextEntityId;
        public List<Entity> entities;

        public void Add(Entity entity)
        {

            entity.intID = nextEntityId;
            entities.Add(entity);
            nextEntityId =+ 1;
        }
        public ECSData()
        {
            nextEntityId = 1;
            entities = new List<Entity>();
        }
    }
}
