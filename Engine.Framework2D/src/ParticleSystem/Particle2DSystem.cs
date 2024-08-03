namespace Engine.Framework2D.Systems;

using System;
using System.Numerics;
using Engine.Systems;
using Engine.Framework2D.Systems.Particles;
using Engine;
using Engine.Enums;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Textures;

public class Particle2DManager<T> : SystemClass, IUpdateSys, IDrawUpdateSys
{
    // This delegate will be called for each particle.
    private Action<Particle> updateParticle;
    private CircularParticleArray particleList;

    /// <summary>
    /// Allows creation of particles.
    /// </summary>
    /// <param name="capacity">The maximum number of particles. An array of this size will be pre-allocated.</param>
    /// <param name="updateParticle">A delegate that lets you specify custom behaviour for your particles. Called once per particle, per frame.</param>
    public Particle2DManager(int capacity, Action<Particle> updateParticle)
    {
        this.updateParticle = updateParticle;
        particleList = new CircularParticleArray(capacity);

        // Populate the list with empty particle objects, for reuse.
        for (int i = 0; i < capacity; i++)
            particleList[i] = new Particle();
    }

    /// <summary>
    /// Update particle state, to be called every frame.
    /// </summary>
    public void Update()
    {
        int removalCount = 0;
        for (int i = 0; i < particleList.Count; i++)
        {
            var particle = particleList[i];

            updateParticle(particle);

            particle.PercentLife -= 1f / particle.Duration;

            // sift deleted particles to the end of the list
            Swap(particleList, i - removalCount, i);

            // if the alpha < 0, delete this particle
            if (particle.PercentLife < 0)
                removalCount++;
        }
        particleList.Count -= removalCount;
    }

    private static void Swap(CircularParticleArray list, int index1, int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }

    /// <summary>
    /// Draw the particles.
    /// </summary>
    public void DrawUpdate()
    {
        for (int i = 0; i < particleList.Count; i++)
        {
            Particle particle = particleList[i];
            if(particle.State is Particle2DState particleState && particleState.Type == Engine.Enums.ParticleType.Exhaust)
            {
                Draw2D.DrawCircle2D(Layers.Particles, particle.Position, 4, particle.Tint);
            }
            else
            {
                Draw2D.DrawLine2D(Layers.Particles, particle.Position, particle.TailPosition, particle.Scale.X, particle.Tint);
            }
        }
        
    }

    public void CreateParticle(Vector2 position, Vector2 tail, Color tint, float duration, float scale, T state, float theta = 0)
    {
        CreateParticle(position, tail, tint, duration, new Vector2(scale), state, theta);
    }



    public void CreateParticle(Vector2 position, Vector2 tail, Color tint, float duration, Vector2 scale, T state, float theta = 0)
    {
        Particle particle;
        if (particleList.Count == particleList.Capacity)
        {
            // if the list is full, overwrite the oldest particle, and rotate the circular list
            particle = particleList[0];
            particleList.Start++;
        }
        else
        {
            particle = particleList[particleList.Count];
            particleList.Count++;
        }

        // Create the particle
        particle.Position = position;
        particle.TailPosition = tail;
        particle.Tint = tint;

        particle.Duration = duration;
        particle.PercentLife = 1f;
        particle.Scale = scale;
        particle.Orientation = theta;
        particle.State = state;
    }

    /// <summary>
    /// Destroys all particles
    /// </summary>
    public void Clear()
    {
        particleList.Count = 0;
    }
    
    
    public override void Initialize()
    {
        
    }
    public override void Shutdown()
    {
        
    }

    public int ParticleCount
    {
        get { return particleList.Count; }
    }

    public class Particle
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 TailPosition;
        public float Orientation;
        public Vector2 Scale;
        public Color Tint;
        public float Duration;
        public float PercentLife = 1f;
        public T State;
    }

    // Represents a circular array with an arbitrary starting point. It's useful for efficiently overwriting
    // the oldest particles when the array gets full. Simply overwrite particleList[0] and advance Start.
    private class CircularParticleArray
    {
        private int start;
        public int Start
        {
            get { return start; }
            set { start = value % list.Length; }
        }

        public int Count { get; set; }
        public int Capacity { get { return list.Length; } }
        private Particle[] list;

        public CircularParticleArray() { }  // for serialization

        public CircularParticleArray(int capacity)
        {
            list = new Particle[capacity];
        }

        public Particle this[int i]
        {
            get { return list[(start + i) % list.Length]; }
            set { list[(start + i) % list.Length] = value; }
        }
    }
}