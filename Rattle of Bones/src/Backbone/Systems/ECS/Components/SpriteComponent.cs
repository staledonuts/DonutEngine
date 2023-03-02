using Raylib_cs;
using Newtonsoft.Json;
using DonutEngine.Backbone;
public class SpriteComponent : Component
{
    [JsonProperty("Sprite")]
    public Texture2D Sprite { get; set; }
    [JsonProperty("Width")]
    public Int32 Width { get; set; }
    [JsonProperty("Height")]
    public Int32 Height { get; set; }
    [JsonProperty("AnimatorName")]
    public string AnimatorName { get; set; }
    [JsonProperty("FramesPerRow")]
    public Int32 FramesPerRow { get; set; }
    [JsonProperty("Row")]
    public Int32 Rows { get; set; }
    [JsonProperty("FrameRate")]
    public Int32 FrameRate { get; set; }
    [JsonProperty("PlayInReverse")]
    public bool PlayInReverse { get; set; }
    [JsonProperty("Continuous")]
    public bool Continuous { get; set; }
    [JsonProperty("Looping")]
    public bool Looping { get; set; }
    

    private Animator animator;

    PositionComponent? position;

    public override void OnAddedToEntity(Entity entity)
    {
        animator = new Animator(AnimatorName, FramesPerRow, Rows, FrameRate, PlayInReverse, Continuous, Looping);
        position = entity.GetComponent<PositionComponent>();
        ECS.ecsDrawUpdate += Draw;
        ECS.ecsUpdate += Update;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        ECS.ecsDrawUpdate -= Draw;
        ECS.ecsUpdate -= Update;
    }

    public void Update(float deltaTime)
    {
        animator.Play();
    }
    public void Draw(float deltaTime)
    {
        
        Raylib.DrawTextureRec(animator.GetSprite(), animator.GetFrameRec(), position.GetPosition(), Color.WHITE);
    }
}