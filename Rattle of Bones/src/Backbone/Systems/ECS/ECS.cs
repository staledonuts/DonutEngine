using System.Numerics;
using Raylib_cs;
using Newtonsoft.Json;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;

namespace DonutEngine.Backbone;

public static class ECS
{
    public delegate void UpdateEvent(float deltaTime);
    public delegate void DrawUpdateEvent(float deltaTime);
    public delegate void LateUpdateEvent(float deltaTime);

    public static event UpdateEvent? ecsUpdate;
    public static event DrawUpdateEvent? ecsDrawUpdate;
    public static event LateUpdateEvent? ecsLateUpdate;

    public static void ProcessUpdate()
    {
        ecsUpdate?.Invoke(Time.deltaTime);
    }
    public static void ProcessDrawUpdate()
    {
        ecsDrawUpdate?.Invoke(Time.deltaTime);
    }
    public static void ProcessLateUpdate()
    {
        ecsLateUpdate?.Invoke(Time.deltaTime);
    }
}
public class EntityManager 
{
    private Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
    private EntityFactory factory = new EntityFactory();


    public void CreateDirectory()
    {
        string[] jsonFilePath = Directory.GetFiles(DonutFilePaths.entities, "*.json");

        foreach(string jsonFile in jsonFilePath)
        {
            Entity entity = CreateEntity(jsonFile);
        }
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

        private class EntityFactory 
        {
            private int nextEntityId = 1;

            public Entity CreateEntity(string json) 
            {
                Entity entity = new Entity(nextEntityId++);

                Dictionary<string, object> data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                foreach (KeyValuePair<string, object> componentData in data)
                {
                    string componentName = componentData.Key;

                    Dictionary<string, object>? componentProperties = JsonConvert.DeserializeObject<Dictionary<string, object>>(componentData.Value.ToString());
                    Component? component = null;

                    switch (componentName) 
                    {
                        case nameof(PositionComponent):
                            component = new PositionComponent()
                            {
                                X = float.Parse(componentProperties["X"].ToString()),
                                Y = float.Parse(componentProperties["Y"].ToString())
                            };
                            break;
                        /*case nameof(SpriteComponent):
                            component = new SpriteComponent
                            {
                                Sprite = Raylib.LoadTexture(DonutFilePaths.sprites+componentProperties["Sprite"].ToString()),
                                Width = Int32.Parse(componentProperties["Width"].ToString()),
                                Height = Int32.Parse(componentProperties["Height"].ToString()),
                                AnimatorName = componentProperties["AnimatorName"].ToString(),
                                FramesPerRow = Int32.Parse(componentProperties["FramesPerRow"].ToString()),
                                Rows = Int32.Parse(componentProperties["Rows"].ToString()),
                                FrameRate = Int32.Parse(componentProperties["FrameRate"].ToString()),
                                PlayInReverse = bool.Parse(componentProperties["PlayInReverse"].ToString()),
                                Continuous = bool.Parse(componentProperties["Continuous"].ToString()),
                                Looping = bool.Parse(componentProperties["Looping"].ToString())
                            };
                            break;
                        case nameof(PhysicsComponent):
                            component = new PhysicsComponent
                            {
                                X = float.Parse(componentProperties["X"].ToString()),
                                Y = float.Parse(componentProperties["Y"].ToString()),
                                Width = float.Parse(componentProperties["Width"].ToString()),
                                Height = float.Parse(componentProperties["Height"].ToString()),
                                Type = (BodyType)int.Parse(componentProperties["BodyType"].ToString()),
                                Density = float.Parse(componentProperties["Density"].ToString()),
                                Friction = float.Parse(componentProperties["Friction"].ToString()),
                                Restitution = float.Parse(componentProperties["Restitution"].ToString())
                            };
                            break;
                        case nameof(GameCamera2D):
                            component = new GameCamera2D
                            {
                                IsActive = bool.Parse(componentProperties["IsActive"].ToString())
                            };
                            break;*/
                    }

                    if (component != null) 
                    {
                        entity.AddComponent(component);
                    };
                }
            return entity;
        }
    }
}

