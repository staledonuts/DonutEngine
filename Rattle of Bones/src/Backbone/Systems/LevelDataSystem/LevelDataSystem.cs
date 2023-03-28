namespace DonutEngine.Backbone.Systems;

public class LevelDataSystem : SystemsClass
{
    Dictionary<string, LevelContainer> levelContainers = new();

    public void CreateLevelContainer()
    {
        
    }
    public override void DrawUpdate()
    {
        if(levelContainers.Any())
        {
            /*foreach(LevelContainer lc in levelContainers)
            {
                lc.DrawObjects();
            }*/
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
