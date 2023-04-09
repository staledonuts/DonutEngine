namespace DonutEngine.Backbone;
using DonutEngine.Backbone.Systems;
using System.Numerics;
using Box2DX.Common;
using Box2DX.Dynamics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using DonutEngine;

class Particle
    {
        public Vec2 position;
        public Vec2 velocity;
        public Raylib_cs.Color color;
        public float size;
        public float life;

        

        public Particle(Vec2 position, Vec2 velocity, Raylib_cs.Color color, float size, float life)
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
        private Entity? entity = null;
        private Random random = new Random();
        private Particle[]? particles = null;
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

                Raylib.DrawTextureEx(particleTexture, new(particles[i].position.X, particles[i].position.Y), 0f, particles[i].size / particleTexture.width, particles[i].color);
            }
        }

        private void Emit()
        {
            for (int i = 0; i < maxParticles; i++)
            {
                if (particles[i] == null)
                {
                    particles[i] = new Particle(entity.currentBody.GetPosition(), new Vec2(random.Next(5, 10), random.Next(5, 10)), Raylib_cs.Color.WHITE, random.Next(15, 20), 20);
                    break;
                }
            }
        }

    public override void OnAddedToEntity(Entity entity)
    {
        //entityPhysics = entity.entityPhysics;
        particles = new Particle[maxParticles];
        particleTexture = LoadTexture(DonutSystems.settingsVars.spritesPath+textureName);
        ECS.ecsUpdate += Update;
        ECS.ecsDrawUpdate += Draw;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        ECS.ecsUpdate -= Update;
        ECS.ecsDrawUpdate -= Draw;
        UnloadTexture(particleTexture);
        Dispose();
    }
}
