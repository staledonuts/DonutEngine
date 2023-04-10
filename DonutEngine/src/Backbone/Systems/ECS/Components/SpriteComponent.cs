namespace DonutEngine.Backbone;
using DonutEngine.Backbone.Systems.Shaders;
using Raylib_cs;
public class SpriteComponent : Component
{
    public Texture2D Sprite { get; set; }
    //public OutlineShader outlineShader;
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
        animator = new Animator(AnimatorName, FramesPerRow, Rows, FrameRate, PlayInReverse, Continuous, Looping);
        animator.AssignSprite(Sprite);
        //outlineShader = new(Sprite, new(0,0,0,1),2);
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
    public void Draw(float deltaTime)
    {
        //Raylib.BeginShaderMode(outlineShader.shader);
        Raylib.DrawTextureRec(animator.GetSprite(), animator.GetFrameRec(), new(entity.currentBody.GetPosition().X, entity.currentBody.GetPosition().Y), Color.WHITE);
        //Raylib.EndShaderMode();
    }

    public void  FlipSprite(bool flipBool)
    {
        animator.FlipSprite(flipBool, false);
    }
}