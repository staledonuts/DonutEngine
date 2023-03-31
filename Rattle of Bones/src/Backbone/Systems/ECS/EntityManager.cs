namespace DonutEngine.Backbone;
using DonutEngine.Backbone.Factory;
public class EntityManager
{
    private Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
    private EntityFactory factory = new EntityFactory();

    public List<Entity> GetEntitiesWithTag(string tag) 
    {
        List<Entity> result = new List<Entity>();
        foreach (KeyValuePair<int, Entity> entity in entities) 
        {
            var ent = GetEntity(entity.Key);
            if (ent.Tags.Contains(tag)) 
            {
                result.Add(ent);
            }
        }
        return result;
    }

    public void CreateDirectory()
    {
        string[] iniFilePath = Directory.GetFiles(DonutFilePaths.entities, "*.ini");

        foreach(string iniFile in iniFilePath)
        {
            Entity entity = CreateEntity(iniFile);
        }
    }

    public Entity CreateEntity(string ini) 
    {
        Entity entity = factory.CreateEntity(ini);
        entities[entity.Id] = entity;
        return entity;
    }

    public Entity GetEntity(int id) 
    {
        return entities[id];
    }

    public void RemoveEntity(int id) 
    {
        entities.Remove(id);
    }
}