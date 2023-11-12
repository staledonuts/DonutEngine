using Engine;
using Engine.FlatPhysics;
using Engine.Systems;
using Engine.Systems.FSM;
using Engine.Utils;
using Template.Entities.FSM;

namespace Template;

public class Player : Entity
{
    public FlatBody body;
    protected FiniteStateMachine<Player> FSM;
    Idle<Player> idleState = new();
    Walk<Player> walkState = new();
    public override void Destroy()
    {
        
    }

    public override void DrawUpdate()
    {
        FSM.DrawUpdate();
    }

    public override void Initialize()
    {
        FSM = new(this, 
        (this, idleState),
        (this, walkState)       
        );
        FSM.SwitchTo(idleState);
    }

    public override void LateUpdate()
    {
        FSM.LateUpdate();
    }

    public override void Update()
    {
        FSM.Update(Time.Delta);
    }
}