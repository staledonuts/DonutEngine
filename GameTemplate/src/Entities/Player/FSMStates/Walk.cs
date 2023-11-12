using Engine.Systems;
using Engine.Systems.FSM;

namespace Template.Entities.FSM;

class Walk<T> : State<Player>
{
    Entity thisEntity;
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Init(Engine.Systems.FiniteStateMachine<Player> fsm)
    {
        thisEntity = fsm.entity;
    }

    public void Update(float deltaTime)
    {
        
    }

    public void DrawUpdate()
    {

    }

    public void LateUpdate()
    {

    }
}
