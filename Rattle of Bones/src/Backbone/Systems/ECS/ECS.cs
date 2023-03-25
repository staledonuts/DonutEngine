using System.Numerics;
using Raylib_cs;
using IniParser.Model;
using IniParser;
using IniParser.Parser;
using Box2DX.Dynamics;
using Box2DX.Collision;

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
            System.Console.WriteLine(iniPath);
            FileIniDataParser parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);
            //Entity? entity = null;
            DynamicEntity? dynEnt = null;
            StaticEntity? staEnt = null;
            if(data.Sections.GetSectionData("EntityType").Keys.GetKeyData("Type").Value == "DynamicEntity")
            {
                dynEnt = new DynamicEntity(nextEntityId++, data);
                CreateComponents(data, dynEnt);
                return dynEnt;
            }
            else if(data.Sections.GetSectionData("EntityType").Keys.GetKeyData("Type").Value == "StaticEntity")
            {
                staEnt = new StaticEntity(nextEntityId++, data);
                CreateComponents(data, staEnt);
                return staEnt; 
            }
            else return staEnt;

        }

        public void CreateComponents(IniData data, DynamicEntity entity)
        {
            DynamicComponent? component = null;
            foreach (SectionData sectionName in data.Sections) 
            {
                if(component != null)
                {
                    component = null;
                }
                string componentType = sectionName.SectionName;
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
                    case "SpriteComponent":
                        component = new SpriteComponent
                        {
                            Sprite = Raylib_cs.Raylib.LoadTexture(DonutFilePaths.sprites+sectionName.Keys.GetKeyData("Sprite").Value),
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
                    // add support for additional components as needed
                }
                if (component != null) 
                {
                    System.Console.WriteLine(component);
                    entity.AddComponent(component, entity);
                }; 
            }
        }
        public void CreateComponents(IniData data, StaticEntity entity)
        {
            foreach (SectionData sectionName in data.Sections) 
            {
                string componentType = sectionName.SectionName;
                StaticComponent? component = null;
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
                        /*case "ParticleSystem":
                            component = new ParticleSystem
                            {
                                maxParticles = int.Parse(sectionName.Keys.GetKeyData("maxParticles").Value),
                                emitRate = float.Parse(sectionName.Keys.GetKeyData("emitRate").Value),
                                textureName = sectionName.Keys.GetKeyData("textureName").Value
                            };
                            break;*/
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
                        System.Console.WriteLine(component);
                        entity.AddComponent(component, entity);
                    }; 
                }
            }
        }
}