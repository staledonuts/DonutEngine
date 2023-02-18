using System.Numerics;
using Raylib_cs;


namespace DonutEngine.Backbone;

public partial class ECS
{
    public delegate void UpdateEvent(float deltaTime);
    public delegate void DrawUpdateEvent(float deltaTime);
    public delegate void LateUpdateEvent(float deltaTime);

    public static event UpdateEvent? ecsUpdate;
    public static event DrawUpdateEvent? ecsDrawUpdate;
    public static event LateUpdateEvent? ecsLateUpdate;

    public static void ProcessUpdate()
    {
        ecsUpdate?.Invoke(Time.deltaTime);
    }
    public static void ProcessDrawUpdate()
    {
        ecsDrawUpdate?.Invoke(Time.deltaTime);
    }
    public static void ProcessLateUpdate()
    {
        ecsLateUpdate?.Invoke(Time.deltaTime);
    }

    
    

    public abstract class Component
    {
        public abstract void Update(float deltaTime);
        public abstract void DrawUpdate(float deltaTime);
    }

    public class EntityRegistry
    {
        public static void SubscribeEntity(Entity entity)
        {
            entity.Start();
            ecsUpdate += entity.Update;
            ecsDrawUpdate += entity.DrawUpdate;
            ecsLateUpdate += entity.LateUpdate;
        }
        public static void UnsubscribeEntity(Entity entity)
        {
            ecsUpdate -= entity.Update;
            ecsDrawUpdate -= entity.DrawUpdate;
            ecsLateUpdate -= entity.LateUpdate;
        }
    }         
}