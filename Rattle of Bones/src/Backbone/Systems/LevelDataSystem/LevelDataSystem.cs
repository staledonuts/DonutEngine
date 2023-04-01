namespace DonutEngine.Backbone.Systems;

public class LevelDataSystem : SystemsClass
{
    Dictionary<string, LevelContainer> levelContainers = new Dictionary<string, LevelContainer>();

    public void CreateLevelContainer()
    {
        
    }
    public override void DrawUpdate()
    {
        if(levelContainers.Any())
        {
            foreach(KeyValuePair<string, LevelContainer> lc in levelContainers)
            {
                //lc.Value.DrawObjects();
            }
        }
    }

    public override void LateUpdate()
    {

    }

    public override void Shutdown()
    {

    }

    public override void Start()
    {
        
    }

    public override void Update()
    {

    }
}
