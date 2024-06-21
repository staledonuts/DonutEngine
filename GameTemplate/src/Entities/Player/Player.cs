using System.Numerics;
using Engine;
using Engine.FlatPhysics;
using Engine.Systems;
using Engine.Systems.FSM;
using Engine.Systems.SceneSystem;
using Engine.Utils;

namespace Template;

public class Player : Entity
{

    public Player(FlatBody flatBody) : base(flatBody)
    {

    }
    public Player(Vector2 position, float density, float mass, float restitution, float area, bool isStatic, float radius, float width, float height, ShapeType shapeType) : base(position, density, mass, restitution, area, isStatic, radius, width, height, shapeType)
    {
        
    }

    public override void Destroy()
    {
        
    }

    public override void DrawUpdate()
    {
           
    }

    public override void Initialize()
    {
        
    }

    public override void LateUpdate()
    {
        
    }

    public override void Update()
    {
        
    }
}