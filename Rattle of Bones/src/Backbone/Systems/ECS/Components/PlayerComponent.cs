namespace DonutEngine.Backbone;
using System.Numerics;
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
            physics.currentBody.ApplyImpulse(new(0,-2000),physics.currentBody.GetPosition());
        }
    }

    public void OnAttack(CBool boolean)
    {

    }

    public void OnDpad(Vector2 vector)
    {
        physics.currentBody.ApplyImpulse(new(Math.Clamp(vector.X * 900, -900, 900),vector.Y), physics.currentBody.GetPosition());
    }

    public void Update(float deltaTime)
    {
        
        //Console.WriteLine(physics.currentBody.GetLinearVelocity().X < Mathdf.Epsilon);
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