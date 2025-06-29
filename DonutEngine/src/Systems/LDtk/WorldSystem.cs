

using Raylib_cs;
using System.Numerics;

namespace DonutEngine
{
    public class WorldSystem : SystemClass, IUpdateSys, IDrawUpdateSys
    {
        private List<GameEntity> entities = new();
        
        public override void Initialize()
        {
            Console.WriteLine("WorldSystem Initialized");
        }

        public override void Shutdown()
        {
            entities.Clear();
            Console.WriteLine("WorldSystem Shutdown");
        }

        public void CreateEntitiesForLevel(LdtkData.Level level)
        {
            entities.Clear(); // Remove entities from the previous level

            // Find the "Entities" layer
            var entityLayer = level.LayerInstances.FirstOrDefault(l => l.Type == "Entities");
            if (entityLayer == null) return;

            foreach (var entityInstance in entityLayer.EntityInstances)
            {
                // For GridVania, entity positions are relative to the level's world position.
                Vector2 entityPosition = new Vector2(
                    level.WorldX + entityInstance.Px[0], 
                    level.WorldY + entityInstance.Px[1]);

                GameEntity newEntity = null;

                // --- Entity Factory ---
                // Here, you would decide what C# class to instantiate based on the entity's identifier in LDtk.
                switch (entityInstance.Identifier)
                {
                    case "Player":
                        newEntity = new Player(entityPosition);
                        break;
                    // Add cases for other entities like "Enemy", "NPC", "Coin", etc.
                    // case "Enemy":
                    //     newEntity = new Enemy(entityPosition);
                    //     break;
                }

                if (newEntity != null)
                {
                    // Apply any custom fields from LDtk to the new entity
                    newEntity.ApplyLdtkFields(entityInstance.FieldInstances);
                    entities.Add(newEntity);
                    Console.WriteLine($"Created entity: {entityInstance.Identifier} at {entityPosition}");
                }
            }
        }
        
        public T GetEntity<T>() where T : GameEntity
        {
            return entities.OfType<T>().FirstOrDefault();
        }

        public void Update()
        {
            foreach (var entity in entities)
            {
                entity.Update();
            }
        }

        public void DrawUpdate()
        {
            foreach (var entity in entities)
            {
                entity.Draw();
            }
        }
    }
}