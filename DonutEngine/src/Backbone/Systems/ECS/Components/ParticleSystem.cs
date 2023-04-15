namespace DonutEngine.Backbone;
using Box2DX.Common;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Raylib_cs;
using static Raylib_cs.Raylib;

class Particle : ParticlePhysics
    {
        
        public Vec2 position;
        public Vec2 velocity;
        public Raylib_cs.Color color;
        public float size;
        public float life;
        

        

        public Particle(Entity entity) : base(entity)
        {
            this.position = entity.currentBody.GetPosition();
            this.color = Raylib_cs.Color.WHITE;
            this.life = life;
            CreateBody();
        }

        public void Update(float deltaTime)
        {
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
                    particles[i].DestroyBody();
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
                    particles[i] = new Particle(entity);
                    break;
                }
            }
        }

    public override void OnAddedToEntity(Entity entity)
    {
        particles = new Particle[maxParticles];
        particleTexture = textureContainer.GetTexture("Ship1.png");
        ECS.ecsUpdate += Update;
        ECS.ecsDrawUpdate += Draw;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        ECS.ecsUpdate -= Update;
        ECS.ecsDrawUpdate -= Draw;
        Dispose();
    }
}
