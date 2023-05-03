namespace DonutEngine.Backbone;
using DonutEngine.Shaders;
using Raylib_cs;
using Box2DX.Common;
public class SpriteComponent : Component
{
    public string Sprite { get; set; }
    Texture2D tex;
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

    public override void OnAddedToEntity(Entity entity)
    {
        this.entity = entity;
        tex = Sys.textureContainer.GetTexture(Sprite);
        animator = new Animator(AnimatorName, FramesPerRow, Rows, FrameRate, PlayInReverse, Continuous, Looping);
        animator.AssignSprite(tex);
        ECS.ecsDrawUpdate += Draw;
        ECS.ecsUpdate += Update;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        this.entity = null;
        ECS.ecsDrawUpdate -= Draw;
        ECS.ecsUpdate -= Update;
        Dispose();
    }

    public void Update(float deltaTime)
    {
        animator.Play();
    }

    Rectangle tempRec;
    Vec2 tempPos;
    public void Draw(float deltaTime)
    {
        tempPos = entity.body.GetPosition();
        tempRec = animator.GetFrameRec();
        Raylib.DrawTexturePro(animator.GetSprite(), tempRec, tempRec, new(tempPos.X * -1, tempPos.Y * -1), entity.body.GetAngle(), Color.WHITE);
    }

    public void  FlipSprite(bool flipBool)
    {
        animator.FlipSprite(flipBool, false);
    }
}