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
        const float lifetime = 60f;
        public float size;
        public float life;
     

        

        public Particle(Entity entity) : base(entity)
        {
            this.position = entity.body.GetPosition();
            this.color = Raylib_cs.Color.WHITE;
            this.life = lifetime;
        }

        public void Update(float deltaTime)
        {
            position = body.GetPosition();
            velocity = body.GetLinearVelocity();
            life -= deltaTime;
        }

        public void Draw(float deltaTime, Texture2D tex)
        {
            Raylib.DrawTextureEx(tex, new(position.X,position.Y), 0f, size / tex.width, color);
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
        private float emitTimer = 60;
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
                
                particles[i].Draw(deltaTime, particleTexture);
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
        particleTexture = Sys.textureContainer.GetTexture("Ship1.png");
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
