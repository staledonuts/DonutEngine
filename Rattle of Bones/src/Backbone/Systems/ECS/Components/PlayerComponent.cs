namespace DonutEngine.Backbone;
using Box2DX.Common;
using DonutEngine.Backbone.Systems;
using DonutEngine.DonutMath;
using Raylib_cs;

public class PlayerComponent : Component
{
    SpriteComponent? sprite = null;
    EntityPhysics? physics = null;
    public override void OnAddedToEntity(DynamicEntity entity)
    {
        physics = entity.entityPhysics;
        sprite = entity.GetComponent<SpriteComponent>();
        ECS.ecsUpdate += Update;
        InputEventSystem.JumpEvent += OnJump;
        InputEventSystem.AttackEvent += OnAttack;
        InputEventSystem.DpadEvent += OnDpad;
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
    {

        physics = entity.entityPhysics;
        sprite = entity.GetComponent<SpriteComponent>();
        ECS.ecsUpdate += Update;
        InputEventSystem.JumpEvent += OnJump;
        InputEventSystem.AttackEvent += OnAttack;
        InputEventSystem.DpadEvent += OnDpad;
    }

    public void OnJump(CBool boolean)
    {
        if(boolean)
        {
            boolean = false;
            physics.currentBody.ApplyImpulse(new(0,-20000), physics.currentBody.GetPosition());
        }
    }

    public void OnAttack(CBool boolean)
    {

    }

    public void OnDpad(Vec2 vector)
    {
        physics.currentBody.ApplyImpulse(new(Math.Clamp(vector.X * 900, -900, 900),vector.Y), physics.currentBody.GetPosition());
    }

    public void Update(float deltaTime)
    {
        bool PlayerHasHorizonalVelocity = Math.Abs(physics.currentBody.GetLinearVelocity().X) > Mathdf.Epsilon;
        if(PlayerHasHorizonalVelocity)
        {
            sprite.FlipSprite(PlayerHasHorizonalVelocity);
        }
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