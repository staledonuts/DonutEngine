using System.Numerics;
using DonutEngine.Backbone.Rendering;
using Raylib_cs;
namespace DonutEngine.Backbone;

public partial class ECS
{
    public class Entity
    {
        void AddComponent<T>(Component component);
        virtual void Update();
    }

    public class Component
    {
        virtual void Update();
    }

    public class Registry
    {
        Vector<Entity> entities;
        public void AddEntity(Entity entity)
        {

        }
        void RemoveEntity(Entity entity)
        {

        }
        void Update();  
    }

    public class DonutTransform : Component
    {
        Vector2 position;
        Vector2 rotation;
        Vector2 scale;  

        public override void Update()
        {

        }
    }
}

