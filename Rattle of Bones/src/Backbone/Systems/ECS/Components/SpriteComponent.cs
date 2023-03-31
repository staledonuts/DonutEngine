namespace DonutEngine.Backbone;
using Raylib_cs;
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

    Entity? entity = null;

    public override void OnAddedToEntity(DynamicEntity entity)
    {
        this.entity = entity;
        animator = new Animator(AnimatorName, FramesPerRow, Rows, FrameRate, PlayInReverse, Continuous, Looping);
        animator.AssignSprite(Sprite);
        ECS.ecsDrawUpdate += Draw;
        ECS.ecsUpdate += Update;
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
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
        Raylib.DrawTextureRec(animator.GetSprite(), animator.GetFrameRec(), new(entity.currentBody.GetPosition().X, entity.currentBody.GetPosition().Y), Color.WHITE);
    }

    public void  FlipSprite(bool flipBool)
    {
        animator.FlipSprite(flipBool, false);
    }

    public override void OnAddedToEntity(StaticEntity entity)
    {
        //throw new NotImplementedException();
    }

    public override void OnRemovedFromEntity(StaticEntity entity)
    {
        //throw new NotImplementedException();
    }
}