using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MonoHelper;

namespace Physics
{
    public class Force
    {
        public PointF pos; public float angle; public float force;

        public Force(PointF _pos, float _angle, float _force)
        {
            pos = _pos;
            angle = _angle;
            force = _force;
        }
    }

    public class PhysicsObject
    {
        //public delegate void MOIF();
        //public MOIF MonentOfInertia;
        public float MonentOfInertia;
        /// <summary>
        /// Angular speed in degrees
        /// </summary>
        public float angularvelocity = 0;
        /// <summary>
        /// Mass of object in kilos
        /// </summary>
        public float mass;
        /// <summary>
        /// Vector of speed
        /// </summary>
        public PointF speed = new PointF(0, 0);

        public PhysicsObject()
        {

        }

        public PhysicsObject(float _MomentOfInertia, float _mass)
        {
            MonentOfInertia = _MomentOfInertia;
            mass = _mass;
        }

        /// <summary>
        /// Apply force in N(Newtons)
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="angle">Angle in radians</param>
        /// <param name="force"></param>
        public virtual void ApplyForce(PointF pos, float angle, float force)
        {
            //angle - Math.Atan2(pos.X, pos.Y) angle between ANGLE and tangent
            float torque = (float)(force * Math.Sqrt(pos.X * pos.X + pos.Y * pos.Y) * Math.Sin(angle - Math.Atan2(pos.X, pos.Y)));
            angularvelocity += (torque / MonentOfInertia);

            speed.X += force * (float)Math.Sin(angle) / mass;
            speed.Y += force * (float)Math.Cos(angle) / mass;
        }
    }
}
