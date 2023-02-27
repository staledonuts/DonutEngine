namespace DonutEngine.Backbone;

using System.Numerics;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

public class PlayerComponent : Component
{
    PositionComponent? position;
    SpriteComponent? sprite;
    PhysicsComponent? physics;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.GetComponent<PositionComponent>();
        sprite = entity.GetComponent<SpriteComponent>();
        physics = entity.GetComponent<PhysicsComponent>();
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

    }

    public void OnAttack(CBool boolean)
    {

    }

    public void OnDpad(Vector2 vector)
    {

    }


}