using System.Numerics;
using Raylib_cs;


namespace DonutEngine
{
    namespace Backbone
    {
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

            
            public abstract class Entity
            {
                public void SubscribeComponent(Component component)
                {
                    ecsUpdate += component.Update;
                    ecsDrawUpdate += component.DrawUpdate;
                    ecsLateUpdate += component.LateUpdate;
                }

                public void UnsubscribeComponent(Component component)
                {
                    ecsUpdate -= component.Update;
                    ecsDrawUpdate -= component.DrawUpdate;
                    ecsLateUpdate -= component.LateUpdate;
                }

                public virtual void Start(){}
                public abstract void Update(float deltaTime);
                public abstract void DrawUpdate(float deltaTime);
                public abstract void LateUpdate(float deltaTime);
            }

            public class Component
            {
                public virtual void Update(float deltaTime){}
                public virtual void DrawUpdate(float deltaTime){}
                public virtual void LateUpdate(float deltaTime){}
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
    }
}