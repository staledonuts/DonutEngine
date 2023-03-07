namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
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
        physics.Body.ApplyLinearImpulse(vector * 900, physics.Body.GetPosition(), true);
    }


}