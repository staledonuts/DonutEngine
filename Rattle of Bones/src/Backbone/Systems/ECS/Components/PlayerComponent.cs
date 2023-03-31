namespace DonutEngine.Backbone;
using Box2DX.Common;
using DonutEngine.Backbone.Systems;
using DonutEngine.DonutMath;
using Raylib_cs;

public class PlayerComponent : Component
{
    SpriteComponent? sprite = null;
    Entity? entity = null;
    public override void OnAddedToEntity(DynamicEntity entity)
    {
        this.entity = entity;
        sprite = entity.GetComponent<SpriteComponent>();
        ECS.ecsUpdate += Update;
        InputEventSystem.JumpEvent += OnJump;
        InputEventSystem.AttackEvent += OnAttack;
        InputEventSystem.DpadEvent += OnDpad;
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
    {

        this.entity = entity;
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
            entity.currentBody.ApplyImpulse(new(0,-20000), this.entity.currentBody.GetPosition());
        }
    }

    public void OnAttack(CBool boolean)
    {

    }

    public void OnDpad(Vec2 vector)
    {
        entity.currentBody.ApplyImpulse(new(Math.Clamp(vector.X * 900, -900, 900),vector.Y), entity.currentBody.GetPosition());
    }

    public void Update(float deltaTime)
    {
        bool PlayerHasHorizonalVelocity = Math.Abs(entity.currentBody.GetLinearVelocity().X) > Mathdf.Epsilon;
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