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
        string[] jsonFilePath = Directory.GetFiles(DonutFilePaths.entities, "*.json");

        foreach(string jsonFile in jsonFilePath)
        {
            LoadingScreen.DrawLoadingScreen("Loading Entites");
            Entity entity = CreateEntity(jsonFile);
        }
    }

    public void ReloadEntities()
    {
        foreach(KeyValuePair<int, Entity> entity in entities)
        {
            entity.Value.DestroyEntity();
        }
        entities = new Dictionary<int, Entity>();
        CreateDirectory();
    }



    public Entity CreateEntity(string json) 
    {
        Entity entity = factory.CreateEntity(json);
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