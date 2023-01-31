/*using Raylib_cs;
using System.Numerics;

namespace DonutEngine.Physics
{

    public class DVector2
    {
        public double x;
        public double y;
        public DVector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static double Distance(DVector2 a, DVector2 b)
        {
            return Math.Sqrt(Math.Pow((a.x - b.x), 2) + Math.Pow((a.y - b.y), 2));
        }
    }

    public class Transform
    {
        public DVector2 position;
        public double scale;
        public Transform(DVector2 position, double scale)
        {
            this.position = position;
            this.scale = scale;
        }
    }

    

    
    
    public class Physics
    {
        public double mass;
        public double newton;
        public double velocity;
        public DVector2 v;
        public DVector2 a;
        public DVector2 n;
        public DVector2 nextN;
        public DVector2 nextV;
        public DVector2 rot;
        public const double G = .0000000000667;
        public Physics(double velocity, double mass)
        {
            this.velocity = velocity;
            this.mass = mass;
            this.v = new DVector2(0, 0);
            this.a = new DVector2(0, 0);
            this.n = new DVector2(0, 0);
            this.nextN = new DVector2(0, 0);
            this.nextV = new DVector2(0, 0);
        }

        public static GameObject[] IsColliding(GameObject self, params GameObject[] GameObjects)
        {
            List<GameObject> coll = new List<GameObject>();
            foreach (GameObject obj in GameObjects)
            {
                if (DVector2.Distance(self.transform.position, obj.transform.position) <= (self.transform.scale + obj.transform.scale) / 2 && self != obj)
                {
                    coll.Add(obj);
                }
            }
            return coll.ToArray();
        }

        public double Velocity()
        {
            return Math.Sqrt(Math.Pow(this.v.x, 2) + Math.Pow(this.v.y, 2));
        }

        public static void Gravity(params GameObject[] GameObjects)
        {
            foreach (GameObject _obj in GameObjects)
            {
                foreach (GameObject obj in GameObjects)
                {
                    if (_obj != obj && Physics.IsColliding(_obj, GameObjects) == null)
                    {
                        obj.physics.newton = G * (obj.physics.mass * _obj.physics.mass / Math.Pow(DVector2.Distance(obj.transform.position, _obj.transform.position), 2));
                        double acceleration = obj.physics.newton / _obj.physics.mass;
                        GameObject.GoToObj(_obj, obj, acceleration);
                        _obj.physics.velocity = acceleration;
                    }
                }
            }
        }
    }

    public class GameObject
    {
        public Physics physics;
        public Transform transform;
        public static GameObject[] universe = null;

        public GameObject(Physics physics, Transform transform)
        {
            this.physics = physics;
            this.transform = transform;
        }

        public void Acceleration()
        {
            this.physics.n.x = this.physics.nextN.x;
            this.physics.n.y = this.physics.nextN.y;
            this.physics.a.x = this.physics.n.x / this.physics.mass;
            this.physics.a.y = this.physics.n.y / this.physics.mass;
            this.physics.v.x += this.physics.a.x;
            this.physics.v.y += this.physics.a.y;
        }

        public void GravityForce()
        {
            DVector2 gSum = new DVector2(0, 0);

            foreach (GameObject obj in GameObject.universe)
            {
                if (obj != this)
                {
                    DVector2 trgRot = new DVector2(obj.transform.position.x - this.transform.position.x, obj.transform.position.y - this.transform.position.y);
                    gSum.x += (trgRot.x / Math.Abs(trgRot.y)) * (Physics.G * this.physics.mass * obj.physics.mass / DVector2.Distance(this.transform.position, obj.transform.position));
                    gSum.y += (trgRot.y / Math.Abs(trgRot.y)) * (Physics.G * this.physics.mass * obj.physics.mass / DVector2.Distance(this.transform.position, obj.transform.position));
                }
            }
            this.physics.nextN = gSum;
        }
        public void Calc()
        {
            if (Physics.IsColliding(this, GameObject.universe).Length == 0)
            {
                this.GravityForce();
            }
            else
            {
                this.GravityForce();
                GameObject[] collide = Physics.IsColliding(this, GameObject.universe);
                foreach (GameObject obj in collide)
                {
                    this.physics.v.x = (((this.physics.mass - obj.physics.mass) / (this.physics.mass + obj.physics.mass) * this.physics.Velocity()) +
                        (2 * obj.physics.mass / (this.physics.mass + obj.physics.mass) * obj.physics.Velocity()));

                    this.physics.v.y = (((this.physics.mass - obj.physics.mass) / (this.physics.mass + obj.physics.mass) * this.physics.Velocity()) +
                        (2 * obj.physics.mass / (this.physics.mass + obj.physics.mass) * obj.physics.Velocity()));
                }

            }
        }
        public void Go()
        {
            this.Acceleration();

            this.transform.position.x += this.physics.v.x / Time.deltaTime;
            this.transform.position.y += this.physics.v.y / Time.deltaTime;
        }
        public static void GoTo(GameObject self, DVector2 target, double a, params GameObject[] obj)
        {
            DVector2 trgRot = new DVector2(target.x - self.transform.position.x, target.y - self.transform.position.y);

            for (int i = 0; i < Math.Abs(trgRot.x); i++)
            {
                if (Physics.IsColliding(self, obj) == null)
                {
                    self.transform.position.x += trgRot.x < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }

            for (int i = 0; i < Math.Abs(trgRot.y); i++)
            {
                if (Physics.IsColliding(self, obj) == null)
                {
                    self.transform.position.y += trgRot.y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }
        }
        public static void GoToObj(GameObject self, GameObject target, double a)
        {
            DVector2 trgRot = new DVector2(target.transform.position.x - self.transform.position.x, target.transform.position.y - self.transform.position.y);
            self.physics.rot = trgRot;
            for (int i = 0; i < Math.Abs(trgRot.x); i++)
            {
                if (Physics.IsColliding(self, target) == null)
                {
                    self.transform.position.x += trgRot.x < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }

            for (int i = 0; i < Math.Abs(trgRot.y); i++)
            {
                if (Physics.IsColliding(self, target) == null)
                {
                    self.transform.position.y += trgRot.y < 0 ? -1 * a / Time.deltaTime : 1 * a / Time.deltaTime;
                }
            }
        }
    }
}*/