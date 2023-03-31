namespace DonutEngine.Backbone.Factory;
using Newtonsoft.Json;

public class EntityFactory 
{
    private int nextEntityId = 1;

    public Entity CreateEntity(string jsonPath) 
    {
        System.Console.WriteLine(jsonPath);
        //FileIniDataParser parser = new FileIniDataParser();
        string json = File.ReadAllText(jsonPath);
        dynamic data = JsonConvert.DeserializeObject(json);

        Entity? currentEntity = null;
        switch (data.EntityType.Type.ToString())
            {
                case "DynamicEntity":
                    currentEntity = new DynamicEntity(nextEntityId++, data);
                    CreateComponents(data, currentEntity as DynamicEntity);
                    break;
                case "StaticEntity":
                    currentEntity = new StaticEntity(nextEntityId++, data);
                    CreateComponents(data, currentEntity as StaticEntity);
                    break;
                case "GameObjectEntity":
                    currentEntity = new GameObjectEntity(nextEntityId++, data);
                    CreateComponents(data, currentEntity as GameObjectEntity);
                    break;
            }

            return currentEntity;

    }

    public void CreateComponents(dynamic data, Entity entity)
    {
        Component? component = null;
        foreach (dynamic componentData in data.Components) 
        {
            if(component != null)
            {
                component = null;
            }
            switch (componentData.Type.ToString()) 
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
                        Sprite = Raylib_cs.Raylib.LoadTexture(DonutFilePaths.sprites + componentData.Sprite.ToString()),
                        Width = int.Parse(componentData.Width.ToString()),
                        Height = int.Parse(componentData.Height.ToString()),
                        AnimatorName = componentData.AnimatorName.ToString(),
                        FramesPerRow = int.Parse(componentData.FramesPerRow.ToString()),
                        Rows = int.Parse(componentData.Rows.ToString()),
                        FrameRate = int.Parse(componentData.FrameRate.ToString()),
                        PlayInReverse = bool.Parse(componentData.PlayInReverse.ToString()),
                        Continuous = bool.Parse(componentData.Continuous.ToString()),
                        Looping = bool.Parse(componentData.Looping.ToString())
                    };
                    break;
                case "GameCamera2D":
                    component = new GameCamera2D
                    {
                        IsActive = bool.Parse(componentData.IsActive.ToString())
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
                        maxParticles = int.Parse(componentData.maxParticles.ToString()),
                        emitRate = float.Parse(componentData.emitRate.ToString()),
                        textureName = componentData.textureName.ToString()
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