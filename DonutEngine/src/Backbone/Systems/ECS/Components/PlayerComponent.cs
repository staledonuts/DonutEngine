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
        }
    }

    private void OnLeftStick(Vec2 InputVector)
    {
        entity.body.ApplyImpulse(InputVector * -2000, entity.body.GetPosition());
    }
    
    private void OnRightStick(Vec2 InputVector)
    {
        entity.body.ApplyTorque(InputVector.X * 2000);
    }
    private void OnAttack(CBool boolean)
    {

    }

    private void OnDpad(Vec2 vector)
    {
       // entity.body.ApplyImpulse(vector * 900, entity.body.GetPosition());
    }

    public void Update(float deltaTime)
    {
        //bool PlayerHasHorizonalVelocity = Math.Abs(entity.body.GetLinearVelocity().X) > MathD.Epsilon;
        /*if(PlayerHasHorizonalVelocity)
        {
            sprite.FlipSprite(PlayerHasHorizonalVelocity);
        }*/
    }
}