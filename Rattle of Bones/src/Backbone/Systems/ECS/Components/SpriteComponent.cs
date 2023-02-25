using Raylib_cs;
using DonutEngine.Backbone;
public class SpriteComponent : Component
{

    public Texture2D Sprite { get; set; }
    public Int32 Width { get; set; }
    public Int32 Height { get; set; }
    public string AnimatorName { get; set; }
    public Int32 FramesPerRow { get; set; }
    public Int32 Rows { get; set; }
    public Int32 FrameRate { get; set; }
    public bool PlayInReverse { get; set; }
    public bool Continuous { get; set; }
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