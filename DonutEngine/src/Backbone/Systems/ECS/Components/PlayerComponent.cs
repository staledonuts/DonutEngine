namespace DonutEngine.Backbone;
using Box2DX.Common;
using DonutEngine.Backbone.Systems.Input;
using Raylib_cs;

public class PlayerComponent : Component
{
    SpriteComponent? sprite = null;
    Entity? entity = null;
    public int PlayerNumber { get; set; }
    
    public override void OnAddedToEntity(Entity entity)
    {
        this.entity = entity;
        sprite = entity.GetComponent<SpriteComponent>();
        ECS.ecsUpdate += Update;
        InputEventSystem.CrossButtonEvent += OnJump;
        InputEventSystem.RectangleButtonEvent += OnAttack;
        InputEventSystem.DpadEvent += OnDpad;
        InputEventSystem.LeftStickEvent += OnLeftStick;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        ECS.ecsUpdate -= Update;
        InputEventSystem.CrossButtonEvent -= OnJump;
        InputEventSystem.RectangleButtonEvent -= OnAttack;
        InputEventSystem.DpadEvent -= OnDpad;
        InputEventSystem.LeftStickEvent -= OnLeftStick;
        Dispose();
    }

    private void OnJump(CBool boolean)
    {
        if(boolean)
        {
            boolean = false;
            Sys.audioControl.PlaySFX("confirmation001", 0.9f, 1.1f);
            entity.body.ApplyForce(new(0,-20000), entity.body.GetPosition());
        }
    }

    private void OnLeftStick(Vec2 InputVector)
    {
        entity.body.ApplyImpulse(InputVector * 2000, entity.body.GetPosition());
    }
    private void OnAttack(CBool boolean)
    {

    }

    private void OnDpad(Vec2 vector)
    {
        entity.body.ApplyImpulse(new(Math.Clamp(vector.X * 900, -900, 900),vector.Y), entity.body.GetPosition());
    }

    public void Update(float deltaTime)
    {
        bool PlayerHasHorizonalVelocity = Math.Abs(entity.body.GetLinearVelocity().X) > MathD.Epsilon;
        if(PlayerHasHorizonalVelocity)
        {
            sprite.FlipSprite(PlayerHasHorizonalVelocity);
        }
    }
}