namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using DonutEngine.DonutMath;
using Raylib_cs;

public class PlayerComponent : Component
{
    PositionComponent? position = null;
    SpriteComponent? sprite = null;
    PhysicsComponent? physics = null;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.entityPos;
        sprite = entity.GetComponent<SpriteComponent>();
        physics = entity.GetComponent<PhysicsComponent>();
        ECS.ecsUpdate += Update;
        InputEventSystem.JumpEvent += OnJump;
        InputEventSystem.AttackEvent += OnAttack;
        InputEventSystem.DpadEvent += OnDpad;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        //throw new NotImplementedException();
    }

    public void OnJump(CBool boolean)
    {
        if(boolean)
        {
            physics.Body.ApplyLinearImpulse(new Vector2(0, 0),physics.Body.GetPosition());
        }
    }

    public void OnAttack(CBool boolean)
    {

    }

    public void OnDpad(Vector2 vector)
    {
        physics.Body.ApplyLinearImpulse(new(Math.Clamp(vector.X * 900, -900, 900),vector.Y), physics.Body.GetPosition(), true);
    }

    public void Update(float deltaTime)
    {

        Console.WriteLine(physics.Body.GetLinearVelocity().X < Mathdf.Epsilon);
        bool PlayerHasHorizonalVelocity = Math.Abs(physics.Body.GetLinearVelocity().X) > Mathdf.Epsilon;
        if(PlayerHasHorizonalVelocity)
        {
            sprite.FlipSprite(PlayerHasHorizonalVelocity);
        }
    }


}