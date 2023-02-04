using Raylib_cs;
using System.Numerics;
using static DonutEngine.Backbone.ECS;

namespace DonutEngine.Physics
{

    /*public class DVector2
    {
        public float x;
        public float y;
        public DVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        
    }

    public class Transform
    {
        public DVector2 position;
        public float scale;
        public Transform(DVector2 position, float scale)
        {
            this.position = position;
            this.scale = scale;
        }
    }*/

    

    
    
    public class PhysicsVars
    {
        public float mass;
        public float newton;
        public float velocity;
        public Vector2 v;
        public Vector2 a;
        public Vector2 n;
        public Vector2 nextN;
        public Vector2 nextV;
        public Vector2 rot;
        public const float G = .0000000000667f;
        public PhysicsVars(float velocity, float mass)
        {
            this.velocity = velocity;
            this.mass = mass;
            this.v = new Vector2(0, 0);
            this.a = new Vector2(0, 0);
            this.n = new Vector2(0, 0);
            this.nextN = new Vector2(0, 0);
            this.nextV = new Vector2(0, 0);
        }

        public static PhysicsObject[] IsColliding(PhysicsObject self, params PhysicsObject[] PhysicsObjects)
        {
            List<PhysicsObject> coll = new List<PhysicsObject>();
            /*foreach (PhysicsObject obj in PhysicsObjects)
            {
                if ((float)Transform2D.Distance(self.transform.position, obj.transform.position) <= (self.transform.scale + obj.transform.scale) / 2f && self != obj)
                {
                    coll.Add(obj);
                }
            }*/
            return coll.ToArray();
        }

        public float Velocity()
        {
            return (float)Math.Sqrt(Math.Pow(this.v.X, 2) + Math.Pow(this.v.Y, 2));
        }

        public static void Gravity(params PhysicsObject[] PhysicsObjects)
        {
            foreach (PhysicsObject _obj in PhysicsObjects)
            {
                foreach (PhysicsObject obj in PhysicsObjects)
                {
                    if (_obj != obj && PhysicsVars.IsColliding(_obj, PhysicsObjects) == null)
                    {
                        obj.physics.newton = G * (obj.physics.mass * _obj.physics.mass / (float)Math.Pow(Vector2.Distance(obj.transform.position, _obj.transform.position), 2));
                        float acceleration = obj.physics.newton / _obj.physics.mass;
                        PhysicsObject.GoToObj(_obj, obj, acceleration);
                        _obj.physics.velocity = acceleration;
                    }
                }
            }
        }
    }

    public class PhysicsObject : Component
    {
        public PhysicsVars physics;
        public Transform2D transform;
        public static PhysicsObject[]? universe = null;

        public PhysicsObject(PhysicsVars physics, Transform2D transform)
        {
            this.physics = physics;
            this.transform = transform;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Go();   
        }
        public void Acceleration()
        {
            this.physics.n.X = this.physics.nextN.X;
            this.physics.n.Y = this.physics.nextN.Y;
            this.physics.a.X = this.physics.n.X / this.physics.mass;
            this.physics.a.Y = this.physics.n.Y / this.physics.mass;
            this.physics.v.X += this.physics.a.X;
            this.physics.v.Y += this.physics.a.Y;
        }

        public void GravityForce()
        {
            Vector2 gSum = new Vector2(0, 0);

            foreach (PhysicsObject obj in PhysicsObject.universe)
            {
                if (obj != this)
                {
                    Vector2 trgRot = new Vector2(obj.transform.position.X - this.transform.position.X, obj.transform.position.Y - this.transform.position.Y);
                    gSum.X += (trgRot.X / Math.Abs(trgRot.Y)) * (PhysicsVars.G * this.physics.mass * obj.physics.mass / Vector2.Distance(this.transform.position, obj.transform.position));
                    gSum.Y += (trgRot.Y / Math.Abs(trgRot.Y)) * (PhysicsVars.G * this.physics.mass * obj.physics.mass / Vector2.Distance(this.transform.position, obj.transform.position));
                }
            }
            this.physics.nextN = gSum;

            
        }
        public void Calc()
        {
            if (PhysicsVars.IsColliding(this, PhysicsObject.universe).Length == 0)
            {
                this.GravityForce();
            }
            else
            {
                this.GravityForce();
                PhysicsObject[] collide = PhysicsVars.IsColliding(this, PhysicsObject.universe);
                foreach (PhysicsObject obj in collide)
                {
                    this.physics.v.X = (((this.physics.mass - obj.physics.mass) / (this.physics.mass + obj.physics.mass) * this.physics.Velocity()) +
                        (2 * obj.physics.mass / (this.physics.mass + obj.physics.mass) * obj.physics.Velocity()));

                    this.physics.v.Y = (((this.physics.mass - obj.physics.mass) / (this.physics.mass + obj.physics.mass) * this.physics.Velocity()) +
                        (2 * obj.physics.mass / (this.physics.mass + obj.physics.mass) * obj.physics.Velocity()));
                }

            }
        }
        public void Go()
        {
            this.Acceleration();

            this.transform.position.X += this.physics.v.X / Time.deltaTime;
            this.transform.position.Y += this.physics.v.Y / Time.deltaTime;
        }
        public static void GoTo(PhysicsObject self, Vector2 target, float a, params PhysicsObject[] obj)
        {
            Vector2 trgRot = new Vector2(target.X - self.transform.position.X, target.Y - self.transform.position.Y);

            for (int i = 0; i < Math.Abs(trgRot.X); i++)
            {
                if (PhysicsVars.IsColliding(self, obj) == null)
                {
                    self.transform.position.X += trgRot.X < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }

            for (int i = 0; i < Math.Abs(trgRot.Y); i++)
            {
                if (PhysicsVars.IsColliding(self, obj) == null)
                {
                    self.transform.position.Y += trgRot.Y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }
        }
        public static void GoToObj(PhysicsObject self, PhysicsObject target, float a)
        {
            Vector2 trgRot = new Vector2(target.transform.position.X - self.transform.position.X, target.transform.position.Y - self.transform.position.Y);
            self.physics.rot = trgRot;
            for (int i = 0; i < Math.Abs(trgRot.X); i++)
            {
                if (PhysicsVars.IsColliding(self, target) == null)
                {
                    self.transform.position.X += trgRot.X < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }

            for (int i = 0; i < Math.Abs(trgRot.Y); i++)
            {
                if (PhysicsVars.IsColliding(self, target) == null)
                {
                    self.transform.position.Y += trgRot.Y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }
        }
    }
}