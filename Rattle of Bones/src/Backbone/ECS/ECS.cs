using System.Numerics;
using Raylib_cs;


namespace DonutEngine
{
    namespace Backbone
    {
        public partial class ECS
        {
            public class Entity
            {
                public List<Component> components = new();
                public void AddComponent(Component component)
                {
                    components.Add(component);
                }
                public virtual void Update(float deltaTime)
                {
                    foreach(Component c in components) 
                    {
                        c.Update(deltaTime);
                    }
                }

                public virtual void DrawUpdate(float deltaTime)
                {
                    foreach(Component c in components) 
                    {
                        c.DrawUpdate(deltaTime);
                    }
                }
            }

            public class Component
            {
                public virtual void Update(float deltaTime){}
                public virtual void DrawUpdate(float deltaTime){}
            }

            public class Registry
            {
                static List<Entity> entities = new();
                public static void AddEntity(Entity entity)
                {
                    entities.Add(entity);
                }
                public static void RemoveEntity(Entity entity)
                {
                    entities.Remove(entity);
                }
                public static void Update() 
                {
                    foreach(Entity e in entities) 
                    {
                        e.Update(Time.deltaTime);
                    }
                }

                public static void DrawUpdate()
                {
                    foreach(Entity e in entities) 
                    {
                        e.DrawUpdate(Time.deltaTime);
                    }
                }
            }            
        }
    }
}