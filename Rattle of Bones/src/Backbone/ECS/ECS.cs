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
                public List<Component> components;
                public void AddComponent(Component component)
                {
                    components.Add(component);
                }
                public virtual void Update()
                {
                    foreach(Component c in components) 
                    {
                        c.Update(Time.deltaTime);
                    }
                }
            }

            public class Component
            {
                public Component()
                {
                    owner = entity;
                }
                Entity owner; 
                public virtual void Update(float deltaTime)
                {

                }
            }

            public class Registry
            {
                public List<Entity> entities;
                public void AddEntity(Entity entity)
                {
                    entities.Add(entity);
                }
                void RemoveEntity(Entity entity)
                {
                    entities.Remove(entity);
                }
                void Update() 
                {

                }
            }            
        }
    }
}