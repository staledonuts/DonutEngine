using System.Numerics;
using Raylib_cs;


namespace DonutEngine
{
    namespace Backbone
    {
        public partial class ECS
        {
            public delegate void StartEvent();
            public delegate void UpdateEvent(float deltaTime);
            public delegate void DrawUpdateEvent(float deltaTime);
            public delegate void LateUpdateEvent(float deltaTime);

            public static event StartEvent? ecsStart;
            public static event UpdateEvent? ecsUpdate;
            public static event DrawUpdateEvent? ecsDrawUpdate;
            public static event LateUpdateEvent? ecsLateUpdate;

            
            public static void ProcessStart()
            {
                ecsStart?.Invoke();
            }
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

            
            public class Entity
            {
                public class Transform2D
                {

                    public Transform2D(Vector2 inputPosition)
                    {
                        position = inputPosition;
                    }

                    public Transform2D(Vector2 inputPosition, float inputRotation)
                    {
                        position = inputPosition;
                        rotation = inputRotation;
                    }

                    public Transform2D(Vector2 inputPosition, float inputRotation, Vector2 inputScale)
                    {
                        position = inputPosition;
                        rotation = inputRotation;
                        scale = inputScale;
                    }
                    public Vector2 position = Vector2.Zero;
                    public float rotation = 0f;
                    public Vector2 scale = Vector2.Zero;

                    public static double Distance(Vector2 positionA, Vector2 positionB)
                    {
                        return Math.Sqrt(Math.Pow((positionA.X - positionA.X), 2f) + Math.Pow((positionA.Y - positionB.Y), 2f));
                    }
                }
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
                public virtual void Update(float deltaTime){}
                public virtual void DrawUpdate(float deltaTime){}

                public virtual void LateUpdate(float deltaTime){}
            }

            public class Component
            {
                public virtual void Update(float deltaTime){}
                public virtual void DrawUpdate(float deltaTime){}
                public virtual void LateUpdate(float deltaTime){}
            }

            public class EntityRegistry
            {
                public static void StartEntity(Entity entity)
                {
                    entity.Start();
                }
                public static void SubscribeEntity(Entity entity)
                {
                    ecsStart += entity.Start;
                    ecsUpdate += entity.Update;
                    ecsDrawUpdate += entity.DrawUpdate;
                    ecsLateUpdate += entity.LateUpdate;
                }
                public static void UnsubscribeEntity(Entity entity)
                {
                    ecsStart -= entity.Start;
                    ecsUpdate -= entity.Update;
                    ecsDrawUpdate -= entity.DrawUpdate;
                    ecsLateUpdate -= entity.LateUpdate;
                }
            }            
        }
    }
}