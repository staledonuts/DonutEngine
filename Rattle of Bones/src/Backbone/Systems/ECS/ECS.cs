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

                        switch (componentType) 
                        {
                            /*case "Tags":
                            {
                                foreach(string s in componentType.)
                                {
                                    entity.Tags.Add(s.Length.ToString());
                                }
                            }
                            break;*/
                            case "PositionComponent":
                                component = new PositionComponent
                                {
                                    Position = new(float.Parse(sectionName.Keys.GetKeyData("X").Value), float.Parse(sectionName.Keys.GetKeyData("Y").Value)),
                                    Rotation = float.Parse(sectionName.Keys.GetKeyData("Rotation").Value),
                                    Scale = new(float.Parse(sectionName.Keys.GetKeyData("ScaleX").Value), float.Parse(sectionName.Keys.GetKeyData("ScaleY").Value))
                                };
                                break;
                            case "SpriteComponent":
                                component = new SpriteComponent
                                {
                                    Sprite = Raylib.LoadTexture(DonutFilePaths.sprites+sectionName.Keys.GetKeyData("Sprite").Value),
                                    Width = int.Parse(sectionName.Keys.GetKeyData("Width").Value),
                                    Height = int.Parse(sectionName.Keys.GetKeyData("Height").Value),
                                    AnimatorName = sectionName.Keys.GetKeyData("AnimatorName").Value,
                                    FramesPerRow = int.Parse(sectionName.Keys.GetKeyData("FramesPerRow").Value),
                                    Rows = int.Parse(sectionName.Keys.GetKeyData("Rows").Value),
                                    FrameRate = int.Parse(sectionName.Keys.GetKeyData("FrameRate").Value),
                                    PlayInReverse = bool.Parse(sectionName.Keys.GetKeyData("PlayInReverse").Value),
                                    Continuous = bool.Parse(sectionName.Keys.GetKeyData("Continuous").Value),
                                    Looping = bool.Parse(sectionName.Keys.GetKeyData("Looping").Value)
                                };
                                break;
                            case "PhysicsComponent":
                                component = new PhysicsComponent
                                {
                                    Width = float.Parse(sectionName.Keys.GetKeyData("Width").Value),
                                    Height = float.Parse(sectionName.Keys.GetKeyData("Height").Value),
                                    Type = (BodyType)int.Parse(sectionName.Keys.GetKeyData("BodyType").Value),
                                    Density = float.Parse(sectionName.Keys.GetKeyData("Density").Value),
                                    Friction = float.Parse(sectionName.Keys.GetKeyData("Friction").Value),
                                    Restitution = float.Parse(sectionName.Keys.GetKeyData("Restitution").Value)
                                };
                                break;
                            case "GameCamera2D":
                                component = new GameCamera2D
                                {
                                    IsActive = bool.Parse(sectionName.Keys.GetKeyData("IsActive").Value)
                                };
                                break;
                            case "PlayerComponent":
                                component = new PlayerComponent
                                {

                                };
                                break;
                            case "ParticleSystem":
                                component = new ParticleSystem
                                {
                                    maxParticles = int.Parse(sectionName.Keys.GetKeyData("maxParticles").Value),
                                    emitRate = float.Parse(sectionName.Keys.GetKeyData("emitRate").Value),
                                    textureName = sectionName.Keys.GetKeyData("textureName").Value
                                };
                                break;
                            case "BlockingComponent":
                                component = new BlockingComponent
                                {
                                    Width = float.Parse(sectionName.Keys.GetKeyData("Width").Value),
                                    Height = float.Parse(sectionName.Keys.GetKeyData("Height").Value)
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