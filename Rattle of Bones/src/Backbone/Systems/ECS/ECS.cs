using System.Numerics;
using Raylib_cs;
using IniParser.Model;
using IniParser;
using IniParser.Parser;
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

        private class EntityFactory 
        {
            private int nextEntityId = 1;

            public Entity CreateEntity(string iniPath) 
            {
                Entity entity = new Entity(nextEntityId++);
                
                FileIniDataParser parser = new FileIniDataParser();
                IniData data = parser.ReadFile(iniPath);
                

                foreach (SectionData sectionName in data.Sections) 
                {
                    string componentType = sectionName.SectionName;
                    Component? component = null;

                        switch (componentType) {
                            case "PositionComponent":
                                component = new PositionComponent() 
                                {
                                    X = float.Parse(sectionName.Keys.GetKeyData("X").Value),
                                    Y = float.Parse(sectionName.Keys.GetKeyData("Y").Value)
                                };
                                break;
                            case nameof(SpriteComponent):
                                component = new SpriteComponent
                                {
                                    Sprite = Raylib.LoadTexture(DonutFilePaths.sprites+sectionName.Keys.GetKeyData("Sprite").Value),
                                    Width = Int32.Parse(sectionName.Keys.GetKeyData("Width").Value),
                                    Height = Int32.Parse(sectionName.Keys.GetKeyData("Height").Value),
                                    AnimatorName = sectionName.Keys.GetKeyData("AnimatorName").Value,
                                    FramesPerRow = Int32.Parse(sectionName.Keys.GetKeyData("FramesPerRow").Value),
                                    Rows = Int32.Parse(sectionName.Keys.GetKeyData("Rows").Value),
                                    FrameRate = Int32.Parse(sectionName.Keys.GetKeyData("FrameRate").Value),
                                    PlayInReverse = bool.Parse(sectionName.Keys.GetKeyData("PlayInReverse").Value),
                                    Continuous = bool.Parse(sectionName.Keys.GetKeyData("Continuous").Value),
                                    Looping = bool.Parse(sectionName.Keys.GetKeyData("Looping").Value)
                                };
                                break;
                            // add support for additional components as needed
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


/*switch (componentName) 
                    {
                        case nameof(PositionComponent):
                            component = new PositionComponent
                            {
                                X = float.Parse(componentProperties["X"].ToString()),
                                Y = float.Parse(componentProperties["Y"].ToString())
                            };
                            break;
                        case nameof(SpriteComponent):
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
                            break;
                    }*/
