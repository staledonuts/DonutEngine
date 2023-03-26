namespace DonutEngine.Backbone;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using DonutEngine;

class Particle
    {
        public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public float size;
        public float life;

        public Particle(Vector2 position, Vector2 velocity, Color color, float size, float life)
        {
            this.position = position;
            this.velocity = velocity;
            this.color = color;
            this.size = size;
            this.life = life;
        }

        public void Update(float deltaTime)
        {
            position += velocity;
            life -= deltaTime;
        }

        public void Draw(float deltaTime)
        {
            Raylib.DrawCircle((int)position.X, (int)position.Y, size, color);
        }

        public bool IsDead()
        {
            return life <= 0f;
        }

    }

    class ParticleSystem : Component
    {
        private EntityPhysics entityPhysics;
        private Random random = new Random();
        private Particle[] particles;
        private Texture2D particleTexture;
        private float emitTimer = 0;
        public int maxParticles { get; set; }
        public float emitRate { get; set; }
        public string? textureName { get; set; }

        

        public void Update(float deltaTime)
        {
            for (int i = 0; i < maxParticles; i++)
            {
                if (particles[i] == null)
                {
                    continue;
                }

                particles[i].Update(deltaTime);

                if (particles[i].IsDead())
                {
                    particles[i] = null;
                }
            }

            emitTimer += deltaTime;

            while (emitTimer > 1f / emitRate)
            {
                Emit();
                emitTimer -= 1f / emitRate;
            }
        }

        public void Draw(float deltaTime)
        {
            for (int i = 0; i < maxParticles; i++)
            {
                if (particles[i] == null)
                {
                    continue;
                }

                Raylib.DrawTextureEx(particleTexture, particles[i].position, 0f, particles[i].size / particleTexture.width, particles[i].color);
            }
        }

        private void Emit()
        {
            for (int i = 0; i < maxParticles; i++)
            {
                if (particles[i] == null)
                {
                    particles[i] = new Particle(entityPhysics.GetVector2Pos(), new Vector2(random.Next(5, 10), random.Next(5, 10)), Color.WHITE, random.Next(15, 20), 20);
                    break;
                }
            }
        }

    public override void OnAddedToEntity(DynamicEntity entity)
    {
        entityPhysics = entity.entityPhysics;
        particles = new Particle[maxParticles];
        particleTexture = LoadTexture(DonutFilePaths.sprites+textureName);
        ECS.ecsUpdate += Update;
        ECS.ecsDrawUpdate += Draw;
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
    {
        ECS.ecsUpdate -= Update;
        ECS.ecsDrawUpdate -= Draw;
        UnloadTexture(particleTexture);
    }

    public override void OnAddedToEntity(StaticEntity entity)
    {
        //entityPhysics = entity.entityPhysics;
        particles = new Particle[maxParticles];
        particleTexture = LoadTexture(DonutFilePaths.sprites+textureName);
        ECS.ecsUpdate += Update;
        ECS.ecsDrawUpdate += Draw;
    }

    public override void OnRemovedFromEntity(StaticEntity entity)
    {
        ECS.ecsUpdate -= Update;
        ECS.ecsDrawUpdate -= Draw;
        UnloadTexture(particleTexture);
    }
}
