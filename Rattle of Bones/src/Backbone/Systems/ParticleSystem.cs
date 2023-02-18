namespace DonutEngine.Backbone.Systems;
using System.Numerics;
using Raylib_cs;

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
            position += velocity * deltaTime;
            life -= deltaTime;
        }

        public void Draw()
        {
            Raylib.DrawCircle((int)position.X, (int)position.Y, size, color);
        }

        public bool IsDead()
        {
            return life <= 0f;
        }
    }

    class ParticleSystem
    {
        private Random random = new Random();
        private Particle[] particles;
        private int maxParticles;
        private float emitRate;
        private float emitTimer;
        private Texture2D particleTexture;

        public ParticleSystem(int maxParticles, float emitRate, Texture2D particleTexture)
        {
            this.maxParticles = maxParticles;
            this.emitRate = emitRate;
            this.particleTexture = particleTexture;
            particles = new Particle[maxParticles];
        }

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

        public void Draw()
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
                    particles[i] = new Particle(new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2), new Vector2(random.Next(-100, 100), random.Next(-100, 100)), new Color(random.Next(256), random.Next(256), random.Next(256), 255), random.Next(5, 20), 1f);
                    break;
                }
            }
        }
    }
